# SPORTSBOOK
=============================================

By Emil Tishinov
 
- - - -
####1. __Application summary__
The application is for people interested in sport and want to inform themselves where are sport facilities and sport events. The user can browse through the collection of events and facilties and mark them as favorites. Once marked as favorites they appear in the account view. The user can create events and facilties and edit them. And the user can search for particular events and facilties, view their detailed pages and comment. Each comment can be edited by the user.
####2. __Data layer:__
The application have several data models:
* AppUser model: FirstName, LastName, Avatar. 
* Event: Name, Description, Start, Image
* Facility: Name, Description, Start, Image
* SportCategory: Name, Description 
* FacilityComment: Content

![alt tag](https://github.com/emilti/testdoc/blob/master/data.png)

####3.__Parts of the project:__

* Public part of the project:
	* Home 
	* Search pages for Facilities and Events
	* Detailed pages for Facilities and Events
* Private part of the project:
	* Page for adding facilities
	* Page for adding events
	* Page for editing facilities
	* Page for editing events
	* Adding comments
	* Editing comments
* Administration part - grid for:
	* AppUsers
	* Facilities
	* Comments
	* SportCategory

####4.__Used technologies:__

* ASP.NET MVC and Visual Studio 2015 with Update 1
* Razor template engine for generating the UI 
* Kendo UI widgets (with the ASP.NET MVC Wrappers)
* MS SQL Server as database back-end
	* Use Entity Framework 6 to access your database
* Bootstrap
* Standard ASP.NET Identity System for managing users and roles 
* AJAX forms for updating the favorite __Facilities__ and __Events__
* Caching at Home page
* Autofac- dependency injection provider
* Automapper for mapping the ViewModels to the data models

####5.__Structure of the project:__

#####Facility area
######FacilitiesPublicController
__FacilityDetails(int id)__ - get details for the facility
~~~c#
 public ActionResult FacilityDetails(int id)
        {
            Facility foundFacility = this.facilities.GetFacilityDetails(id);
            var facilityForView = AutoMapperConfig.Configuration.CreateMapper().Map<FacilityDetailedViewModel>(foundFacility);
            return this.View(facilityForView);
        }
~~~
__SearchFacilities(FacilitiesListViewModel model, int id = 0)__ - search for facility in the data
~~~c#
 [HttpGet]
        public ActionResult SearchFacilities(FacilitiesListViewModel model, int id = 0)
        {
           if (model.CurrentPage == 0)
            {
                model.CurrentPage = 1;
            }

            var sportCategory = this.sportCategories.All().FirstOrDefault(x => x.Id == id);

            var sportCategories = this.sportCategories.All();
            model.SportCategoriesDropDown = this.GetSelectListSportCategories(sportCategories);
            var foundFacilities = new List<Facility>();

            if (id > 0)
                {
                foundFacilities = this.facilities.All().Where(y => y.SportCategories.Where(x => x.Id == id).Count() > 0).ToList();
            }
            else
            {
                foundFacilities = this.facilities.All().ToList();
            }

            var allItemsCount = foundFacilities.Count();
            var foundFacilitiesAfterSearch = foundFacilities.ToList();
            if (!string.IsNullOrEmpty(model.Search))
            {
                foundFacilitiesAfterSearch = foundFacilities.Where(a => a.Name.ToUpper().Contains(model.Search.ToUpper())).ToList();
                allItemsCount = foundFacilitiesAfterSearch.Count();
            }

            var page = model.CurrentPage;

            var totalPages = (int)Math.Ceiling(allItemsCount / (decimal)ItemsPerPage);
            var itemsToSkip = (page - 1) * ItemsPerPage;
            var foundFacilitiestoView =
                foundFacilitiesAfterSearch
                .OrderBy(x => x.Name)
                .ThenBy(x => x.Id)
                .Skip(itemsToSkip).Take(ItemsPerPage)
               .ToList();
            var facilitiesToView = AutoMapperConfig.Configuration.CreateMapper().Map<List<FacilityViewModel>>(foundFacilitiestoView);

            var viewModel = new FacilitiesListViewModel()
            {
                CurrentPage = page,
                TotalPages = totalPages,
                Facilities = facilitiesToView
            };

            return this.View(viewModel);
        }
~~~
######FacilitiesPrivateController
__AddFacility()__ - get the view for adding facility providing the neccsesary data
~~~c#
 [HttpGet]
        [Authorize]
        public ActionResult AddFacility()
        {
            FacilityChangeViewModel model = new FacilityChangeViewModel();
            var cities = this.cities.All();
            model.CitiesDropDown = this.GetSelectListCities(cities);
            var sportCategories = this.sportCategories.All();
            model.SportCategoriesDropDown = this.GetSelectListSportCategories(sportCategories);
            return this.View(model);
        }
~~~	
__AddFacility(FacilityChangeViewModel model)__ - Post method for adding the new facility in the data
~~~c#
 [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult AddFacility(FacilityChangeViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                Facility mappedFacility = AutoMapperConfig.Configuration.CreateMapper().Map<Facility>(model);
                foreach (var categoryId in model.SportCategoriesIds)
                {
                    SportCategory currentCategory = this.sportCategories.GetById(categoryId);
                    mappedFacility.SportCategories.Add(currentCategory);
                }

                mappedFacility.AuthorId = this.User.Identity.GetUserId();

                this.facilities.Add(mappedFacility);
                return this.RedirectToAction("FacilityDetails", "FacilitiesPublic", new { id = mappedFacility.Id, area = "Facilities" });
            }

            return this.View(model);
        }
~~~	
__EditFacility(int Id)__ - getting the view frim which the facility will be edited.
~~~c#
 [HttpGet]
        [Authorize]
        public ActionResult EditFacility(int Id)
        {
            Facility editedFacilty = this.facilities.GetFacilityDetails(Id);
            FacilityChangeViewModel mappedFacility = AutoMapperConfig.Configuration.CreateMapper().Map<FacilityChangeViewModel>(editedFacilty);
            var cities = this.cities.All();
            mappedFacility.CitiesDropDown = this.GetSelectListCities(cities);
            var sportCategories = this.sportCategories.All();
            mappedFacility.SportCategoriesDropDown = this.GetSelectListSportCategories(sportCategories);
            return this.View(mappedFacility);
        }
~~~
__EditFacility(int id, FacilityChangeViewModel model)__ - Post query for adding the edited data for the facility in the database
~~~c#
[HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult EditFacility(int id, FacilityChangeViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                Facility mappedFacility = AutoMapperConfig.Configuration.CreateMapper().Map<Facility>(model);

                Facility currentFacility = this.facilities.GetFacilityDetails(id);
                foreach (var category in currentFacility.SportCategories)
                {
                    category.Facilities.Remove(currentFacility);
                }

                currentFacility.SportCategories.Clear();

                this.facilities.Save();
                foreach (var categoryId in model.SportCategoriesIds)
                {
                    SportCategory currentCategory = this.sportCategories.GetById(categoryId);
                    mappedFacility.SportCategories.Add(currentCategory);
                }

                this.facilities.UpdateFacility(id, mappedFacility);
                return this.RedirectToAction("FacilityDetails", "FacilitiesPublic", new { id = id, area = "Facilities" });
            }

            return this.View(model);
        }
~~~
######FavoriteFacilitiesController
__CheckFacilityInFavourites(int id)__ - checks whether viewed facility is in the favorites list for the current user or the user which account is viewed
~~~c#
 public ActionResult CheckFacilityInFavourites(int id)
        {
            List<Facility> foundFacilities = new List<Facility>();
            List<FacilityViewModel> foundFacilitiesToView = new List<FacilityViewModel>();
            AppUser currentUser = this.users.GetUserDetails(this.User.Identity.GetUserId());
            if (currentUser == null)
            {
                Facility curentFacility = this.facilities.GetFacilityDetails(id);
                this.ViewBag.className = "favorites-button";
                return this.PartialView("_FacilityInFavourites", curentFacility);
            }

            Facility checkedFacilityForCurrentUser = currentUser.FavoriteFacilities.FirstOrDefault(a => a.Id == id);
            if (checkedFacilityForCurrentUser == null)
            {
                Facility curentFacility = this.facilities.GetFacilityDetails(id);
                this.ViewBag.className = "favorites-button";
                return this.PartialView("_FacilityInFavourites", curentFacility);
            }
            else
            {
                this.ViewBag.className = "remove-from-favorites-button";
                return this.PartialView("_FacilityInFavourites", checkedFacilityForCurrentUser);
            }

            // return this.PartialView(foundFacilitiesToView);
        }
~~~
__AddToFavorites(int id)__ - Adding the facility to the list of favorite facilites for the user
~~~c#
     [Authorize]
        public void AddToFavorites(int id)
        {
            Facility foundFacility = this.facilities.GetFacilityDetails(id);
            var userId = this.User.Identity.GetUserId();
            AppUser currentUser = this.users.GetUserDetails(userId);
            currentUser.FavoriteFacilities.Add(foundFacility);
            foundFacility.UsersLiked.Add(currentUser);
            this.facilities.UpdateFacility(id, foundFacility);
            this.users.UpdateUser(currentUser);
        }
~~~
__RemoveFromFavorites(int id)__ - Removing the facility form the list of favorite facilites for the user
~~~c#
        [Authorize]
        public void RemoveFromFavorites(int id)
        {
            Facility foundFacility = this.facilities.GetFacilityDetails(id);
            var userId = this.User.Identity.GetUserId();
            AppUser currentUser = this.users.GetUserDetails(userId);
            currentUser.FavoriteFacilities.Remove(foundFacility);
            foundFacility.UsersLiked.Remove(currentUser);
            this.facilities.UpdateFacility(id, foundFacility);
            this.users.UpdateUser(currentUser);
           // return this.RedirectToAction("Index", "Home");
        }
~~~
#####CommentsController
__AddComment(int id)__ - getting the view for adding comment to a particular facility
~~~c#
 [HttpGet]
        [Authorize]
        public ActionResult AddComment(int id)
        {
            this.ViewBag.facilityId = id;
            return this.View();
        }
~~~
__AddComment(int id, CommentViewModel model)__ - post method for adding the comment in the database
~~~c#
 [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult AddComment(int id, CommentViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                // FacilityComment mappedComment = AutoMapperConfig.Configuration.CreateMapper().Map<FacilityComment>(model);
                Facility commentedFacility = this.facilities.GetFacilityDetails(id);
                AppUser user = this.users.GetUserDetails(this.User.Identity.GetUserId());
                string username = user.UserName;
                var comment = this.comments.Add(id, model.Content, this.User.Identity.GetUserId(), username, commentedFacility, user.Avatar);
                return this.RedirectToAction("FacilityDetails", "FacilitiesPublic", new { id = id, area = "Facilities" });
            }

            return this.RedirectToAction("FacilityDetails", "FacilitiesPublic", new { id = id, area = "Facilities" });
        }
~~~
__EditComment(int id)__ - getting the view for editing a comment
~~~c#
  [HttpGet]
        [Authorize]
        public ActionResult EditComment(int id)
        {
            FacilityComment foundComment = this.comments.GetById(id);
            var foundCommentForView = AutoMapperConfig.Configuration.CreateMapper().Map<CommentViewModel>(foundComment);
            return this.View(foundCommentForView);
        }
~~~
__EditComment(int id, CommentViewModel model)__ - Post method for recording the new data for the comment in the database
~~~c#
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult EditComment(int id, CommentViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                FacilityComment foundComment = this.comments.GetById(id);
                this.comments.UpdateComment(id, model.Content);
                return this.RedirectToAction("FacilityDetails", "FacilitiesPublic", new { id = foundComment.FacilityId, area = "Facilities" });
            }

            return this.View(model);
        }
~~~

__DeleteComment(int id)__ - deleting comment from the database
~~~c#
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteComment(int id)
        {
            FacilityComment foundComment = this.comments.GetById(id);
            this.comments.DeleteComment(foundComment);
            return this.RedirectToAction("FacilityDetails", "FacilitiesPublic", new { id = foundComment.FacilityId, area = "Facilities" });
        }
~~~
#####Events area
######EventsPublicController
__EventDetails(int id)__ - Getting the details for a particular event
~~~c#
   public ActionResult EventDetails(int id)
        {
            Event foundEvent = this.events.GetEventDetails(id);
            var eventForView = AutoMapperConfig.Configuration.CreateMapper().Map<EventDetailedViewModel>(foundEvent);
            return this.View(eventForView);
        }
~~~
__SearchEvents(EventsListViewModel model, int id = 0)__ - Searching for events in the database that follow some conditions
~~~c#
        [HttpGet]
        public ActionResult SearchEvents(EventsListViewModel model, int id = 0)
        {
           if (model.CurrentPage == 0)
            {
                model.CurrentPage = 1;
            }

            var sportCategory = this.sportCategories.All().FirstOrDefault(x => x.Id == id);

            var sportCategories = this.sportCategories.All();
            model.SportCategoriesDropDown = this.GetSelectListSportCategories(sportCategories);
            var foundEvents = new List<Event>();

            if (id > 0)
                {
                foundEvents = this.events.All().Where(y => y.SportCategories.Where(x => x.Id == id).Count() > 0).ToList();
            }
            else
            {
                foundEvents = this.events.All().ToList();
            }

            var allItemsCount = foundEvents.Count();
            var foundEventsAfterSearch = foundEvents.ToList();
            if (!string.IsNullOrEmpty(model.Search))
            {
                foundEventsAfterSearch = foundEvents.Where(a => a.Name.ToUpper().Contains(model.Search.ToUpper())).ToList();
                allItemsCount = foundEventsAfterSearch.Count();
            }

            var page = model.CurrentPage;

            var totalPages = (int)Math.Ceiling(allItemsCount / (decimal)ItemsPerPage);
            var itemsToSkip = (page - 1) * ItemsPerPage;
            var foundEventsToView =
                foundEventsAfterSearch
                .OrderBy(x => x.Name)
                .ThenBy(x => x.Id)
                .Skip(itemsToSkip).Take(ItemsPerPage)
               .ToList();
            var eventsToView = AutoMapperConfig.Configuration.CreateMapper().Map<List<EventViewModel>>(foundEventsToView);

            var viewModel = new EventsListViewModel()
            {
                CurrentPage = page,
                TotalPages = totalPages,
                Events = eventsToView
            };

            return this.View(viewModel);
        }
~~~

######EventsPrivateController
__AddEvent()__ - getting the view for adding an event
~~~c#
        [HttpGet]
        [Authorize]
        public ActionResult AddEvent()
        {
            EventChangeViewModel model = new EventChangeViewModel();
            var cities = this.cities.All();
            model.CitiesDropDown = this.GetSelectListCities(cities);
            var sportCategories = this.sportCategories.All();
            model.SportCategoriesDropDown = this.GetSelectListSportCategories(sportCategories);
            return this.View(model);
        }
~~~
__ AddEvent(EventChangeViewModel model) - recording the new event in the database through Post method
~~~c#
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult AddEvent(EventChangeViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                Event mappedEvent = AutoMapperConfig.Configuration.CreateMapper().Map<Event>(model);
                foreach (var categoryId in model.SportCategoriesIds)
                {
                    SportCategory currentCategory = this.sportCategories.GetById(categoryId);
                    mappedEvent.SportCategories.Add(currentCategory);
                }

                mappedEvent.AuthorId = this.User.Identity.GetUserId();

                this.events.Add(mappedEvent);
                return this.RedirectToAction("EventDetails", "EventsPublic", new { id = mappedEvent.Id, area = "Events" });
            }

            return this.View(model);
        }
~~~
__EditEvent(int Id)__ - getting the view for editing an event
~~~c#
        [HttpGet]
        [Authorize]
        public ActionResult EditEvent(int Id)
        {
            Event editedEvent = this.events.GetEventDetails(Id);
            EventChangeViewModel mappedEvent = AutoMapperConfig.Configuration.CreateMapper().Map<EventChangeViewModel>(editedEvent);
            var cities = this.cities.All();
            mappedEvent.CitiesDropDown = this.GetSelectListCities(cities);
            var sportCategories = this.sportCategories.All();
            mappedEvent.SportCategoriesDropDown = this.GetSelectListSportCategories(sportCategories);
            return this.View(mappedEvent);
        }
~~~
__EditEvent(int id, EventChangeViewModel model)__ - post method for ecording the edited event in the database
~~~c#
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult EditEvent(int id, EventChangeViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                Event mappedEvent = AutoMapperConfig.Configuration.CreateMapper().Map<Event>(model);

                Event currentEvent = this.events.GetEventDetails(id);
                foreach (var category in currentEvent.SportCategories)
                {
                    category.Events.Remove(currentEvent);
                }

                currentEvent.SportCategories.Clear();

                this.events.Save();
                foreach (var categoryId in model.SportCategoriesIds)
                {
                    SportCategory currentCategory = this.sportCategories.GetById(categoryId);
                    mappedEvent.SportCategories.Add(currentCategory);
                }

                this.events.UpdateEvent(id, mappedEvent);
                return this.RedirectToAction("EventDetails", "EventsPublic", new { id = id, area = "Events" });
            }

            return this.View(model);
        }
~~~
#####FavoriteEventsController
__CheckEventInFavourites(int id) - checks whether a particular event is in favorites list for the user
~~~c#
  public ActionResult CheckEventInFavourites(int id)
        {
            List<Event> foundEvent = new List<Event>();
            List<EventViewModel> foundEventsToView = new List<EventViewModel>();
            AppUser currentUser = this.users.GetUserDetails(this.User.Identity.GetUserId());
            if (currentUser == null)
            {
                Event curentEvent = this.events.GetEventDetails(id);
                this.ViewBag.className = "favorites-button";
                return this.PartialView("_EventInFavourites", curentEvent);
            }

            Event checkedEventForCurrentUser = currentUser.FavoriteEvents.FirstOrDefault(a => a.Id == id);
            if (checkedEventForCurrentUser == null)
            {
                Event curentEvent = this.events.GetEventDetails(id);
                this.ViewBag.className = "favorites-button";
                return this.PartialView("_EventInFavourites", curentEvent);
            }
            else
            {
                this.ViewBag.className = "remove-from-favorites-button";
                return this.PartialView("_EventInFavourites", checkedEventForCurrentUser);
            }

            // return this.PartialView(foundFacilitiesToView);
        }
~~~
__AddToFavorites(int id)__ - adding an event to favorites for teh user
~~~c#
        [Authorize]
        public void AddToFavorites(int id)
        {
            Event foundEvent = this.events.GetEventDetails(id);
            var userId = this.User.Identity.GetUserId();
            AppUser currentUser = this.users.GetUserDetails(userId);
            currentUser.FavoriteEvents.Add(foundEvent);
            foundEvent.UsersLiked.Add(currentUser);
            this.events.UpdateEvent(id, foundEvent);
            this.users.UpdateUser(currentUser);
        }
~~~
__RemoveFromFavorites(int id)__ - removing from the favorites for the user
~~~c#
        [Authorize]
        public void RemoveFromFavorites(int id)
        {
            Event foundEvent = this.events.GetEventDetails(id);
            var userId = this.User.Identity.GetUserId();
            AppUser currentUser = this.users.GetUserDetails(userId);
            currentUser.FavoriteEvents.Remove(foundEvent);
            foundEvent.UsersLiked.Remove(currentUser);
            this.events.UpdateEvent(id, foundEvent);
            this.users.UpdateUser(currentUser);
           // return this.RedirectToAction("Index", "Home");
        }
~~~
#####Administration area
######AdminAppUserController
__AppUsers_Read([DataSourceRequest]DataSourceRequest request)__ - reading the data for the kendo grid
~~~c#
public ActionResult AppUsers_Read([DataSourceRequest]DataSourceRequest request)
        {
            DataSourceResult result = this.users.All()
                 .To<AppUserGridViewModel>()
                 .ToDataSourceResult(request);

            return this.Json(result);
        }
~~~
__AppUsers_Create([DataSourceRequest]DataSourceRequest request, AppUserGridViewModel appUser)__ - creating AppUser in the kendo grid
~~~c#
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AppUsers_Create([DataSourceRequest]DataSourceRequest request, AppUserGridViewModel appUser)
        {
            var userManager = new UserManager<AppUser>(new UserStore<AppUser>(this.context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(this.context));

            if (!roleManager.Roles.Any())
            {
                roleManager.Create(new IdentityRole { Name = "User" });
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }

            var newId = string.Empty;
            if (this.ModelState.IsValid)
            {
                AppUser userToCreate = new AppUser
                {
                    Avatar = appUser.Avatar,
                    Email = appUser.Email,
                    UserName = appUser.UserName,
                    FirstName = appUser.FirstName,
                    LastName = appUser.LastName
                };

                userManager.Create(userToCreate, appUser.Password);
                userManager.AddToRole(userToCreate.Id, "User");
                newId = userToCreate.Id;
            }


            var userToDisplay =
             this.users.All()
             .To<AppUserGridViewModel>()
             .FirstOrDefault(x => x.Id == newId);

            return this.Json(new[] { userToDisplay
    }.ToDataSourceResult(request, this.ModelState));
        }
~~~
__AppUsers_Update([DataSourceRequest]DataSourceRequest request, AppUserGridViewModel appUser)__ - updating AppUser in the Kendo grid
~~~c#
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AppUsers_Update([DataSourceRequest]DataSourceRequest request, AppUserGridViewModel appUser)
        {
            if (this.ModelState.IsValid)
            {
                AppUser userToEdit = this.users.GetUserDetails(appUser.Id);
                userToEdit.Avatar = appUser.Avatar;
                userToEdit.Email = appUser.Email;
                userToEdit.UserName = appUser.UserName;
                userToEdit.FirstName = appUser.FirstName;
                userToEdit.LastName = appUser.LastName;
                this.users.UpdateUser(userToEdit);
            }

            var userToDisplay =
          this.users.All()
          .To<AppUserGridViewModel>()
          .FirstOrDefault(x => x.Id == appUser.Id);


            return this.Json(new[] { userToDisplay }.ToDataSourceResult(request, this.ModelState));
        }
~~~
__AppUsers_Destroy([DataSourceRequest]DataSourceRequest request, AppUser appUser)__ - deleting AppUser from the database through the Kendo grid
~~~c#
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AppUsers_Destroy([DataSourceRequest]DataSourceRequest request, AppUser appUser)
        {

            this.users.Remove(appUser);
            this.users.SaveChanges();

            return this.Json(new[] { appUser }.ToDataSourceResult(request, this.ModelState));
        }
~~~
######AdminFacilitiesController
__Facilities_Read([DataSourceRequest]DataSourceRequest request)__ - reading the Facilities for the Kendo grid
~~~c#
 public ActionResult Facilities_Read([DataSourceRequest]DataSourceRequest request)
        {
            DataSourceResult result = this.facilities.All()
               .To<FacilityGridViewModel>()
               .ToDataSourceResult(request);

            return this.Json(result);
        }
~~~
__Facilities_Create([DataSourceRequest]DataSourceRequest request, FacilityGridViewModel facility)__ - Post method for creating a Facility through the Kendo grid
~~~c#
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Facilities_Create([DataSourceRequest]DataSourceRequest request, FacilityGridViewModel facility)
        {
            var newId = 0;
            if (this.ModelState.IsValid)
            {
                var entity = new Facility
                {
                    Name = facility.Name,
                    Description = facility.Description,
                    AuthorId = this.User.Identity.GetUserId(),
                    Image = facility.Image,
                    CityId = 1
                };


                this.facilities.Add(entity);
                this.facilities.Save();
                newId = entity.Id;
            }

            var facilityToDisplay =
                this.facilities.All()
                .To<FacilityGridViewModel>()
                .FirstOrDefault(x => x.Id == newId);
            return this.Json(new[] { facilityToDisplay }.ToDataSourceResult(request, ModelState));
        }
~~~
__Facilities_Update([DataSourceRequest]DataSourceRequest request, FacilityGridViewModel facility)__ Post method for updating facility through the kendo grid
~~~c#
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Facilities_Update([DataSourceRequest]DataSourceRequest request, FacilityGridViewModel facility)
        {
            if (this.ModelState.IsValid)
            {
                var entity = this.facilities.GetFacilityDetails(facility.Id);
                entity.Name = facility.Name;
                entity.Description = facility.Description;
                entity.Image = facility.Image;
                this.facilities.Save();
            }

            var postToDisplay =
             this.facilities.All()
             .To<FacilityGridViewModel>()
             .FirstOrDefault(x => x.Id == facility.Id);

            return this.Json(new[] { facility }.ToDataSourceResult(request, ModelState));
        }

~~~
__Facilities_Destroy([DataSourceRequest]DataSourceRequest request, Facility facility)__ - Deleting a Facility from the database through the Kendo grid
~~~c#
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Facilities_Destroy([DataSourceRequest]DataSourceRequest request, Facility facility)
        {
            this.facilities.Remove(facility);
            this.facilities.Save();

            return this.Json(new[] { facility }.ToDataSourceResult(request, ModelState));
        }
~~~
######AdminFacilityCommentsController
__Index()__ - Getting the view for the Kendo grid
~~~c#
 public ActionResult Index()
        {
            return this.View();
        }
~~~
__AppUsers_Read([DataSourceRequest]DataSourceRequest request)__ - reading the data for the kendo grid
~~~c#
        public ActionResult FacilityComments_Read([DataSourceRequest]DataSourceRequest request)
        {
            DataSourceResult result = this.comments.All()
               .To<FacilityCommentGridViewModel>()
               .ToDataSourceResult(request);
            return this.Json(result);
        }
~~~
__FacilityComments_Update([DataSourceRequest]DataSourceRequest request, FacilityCommentGridViewModel facilityComment)__ - updating comment through the Kendo grid
~~~c#
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult FacilityComments_Update([DataSourceRequest]DataSourceRequest request, FacilityCommentGridViewModel facilityComment)
        {
            if (this.ModelState.IsValid)
            {
                this.comments.UpdateComment(facilityComment.Id, facilityComment.Content);
            }

            var commentToDisplay =
             this.comments.All()
             .To<FacilityCommentGridViewModel>()
             .FirstOrDefault(x => x.Id == facilityComment.Id);

            return this.Json(new[] { commentToDisplay }.ToDataSourceResult(request, this.ModelState));
        }
~~~
__FacilityComments_Destroy([DataSourceRequest]DataSourceRequest request, FacilityComment facilityComment)__ - Deleting FacilityComment through the Kendo grid
~~~c#
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult FacilityComments_Destroy([DataSourceRequest]DataSourceRequest request, FacilityComment facilityComment)
        {
            FacilityComment foundComment = this.comments.GetById(facilityComment.Id);
            this.comments.DeleteComment(foundComment);
            return this.Json(new[] { facilityComment }.ToDataSourceResult(request, this.ModelState));
        }
~~~
######AdminSportCategoriesController
__Index()__ - Getting the view for the Kendo grid
~~~c#
  public ActionResult Index()
        {
            return this.View();
        }
~~~
__SportCategories_Read([DataSourceRequest]DataSourceRequest request)__ - Reading the sport categories for the Kendo grid
~~~c#
        public ActionResult SportCategories_Read([DataSourceRequest]DataSourceRequest request)
        {
            DataSourceResult result = this.sportCategories.All()
               .To<SportCategoryGridViewModel>()
               .ToDataSourceResult(request);

            return this.Json(result);
        }
~~~
__SportCategories_Create([DataSourceRequest]DataSourceRequest request, SportCategoryGridViewModel sportCategory)__ - Creating a new sport category
~~~c#
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SportCategories_Create([DataSourceRequest]DataSourceRequest request, SportCategoryGridViewModel sportCategory)
        {
            var newId = 0;
            if (this.ModelState.IsValid)
            {
                var entity = new SportCategory
                {
                    Name = sportCategory.Name,
                    Description = sportCategory.Description
                };

                this.sportCategories.Add(entity);
                this.sportCategories.Save();
                newId = entity.Id;
            }

            var postToDisplay =
                this.sportCategories.All()
                .To<SportCategoryGridViewModel>()
                .FirstOrDefault(x => x.Id == newId);

            return this.Json(new[] { sportCategory }.ToDataSourceResult(request, ModelState));
        }
~~~
__SportCategories_Update([DataSourceRequest]DataSourceRequest request, SportCategoryGridViewModel sportCategory)__ - Updating a sport category
~~~c#
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SportCategories_Update([DataSourceRequest]DataSourceRequest request, SportCategoryGridViewModel sportCategory)
        {
            if (this.ModelState.IsValid)
            {
                var entity = this.sportCategories.GetById(sportCategory.Id);
                entity.Name = sportCategory.Name;
                entity.Description = sportCategory.Description;
                this.sportCategories.Save();
            }

            var sportCategoryToDisplay =
             this.sportCategories.All()
             .To<SportCategoryGridViewModel>()
             .FirstOrDefault(x => x.Id == sportCategory.Id);

            return this.Json(new[] { sportCategoryToDisplay }.ToDataSourceResult(request, ModelState));
        }
~~~
__SportCategories_Destroy([DataSourceRequest]DataSourceRequest request, SportCategory sportCategory)__ - deleting sport category
~~~c#
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SportCategories_Destroy([DataSourceRequest]DataSourceRequest request, SportCategory sportCategory)
        {
            this.sportCategories.Remove(sportCategory);
            this.sportCategories.Save();

            return this.Json(new[] { sportCategory }.ToDataSourceResult(request, ModelState));
        }
~~~

