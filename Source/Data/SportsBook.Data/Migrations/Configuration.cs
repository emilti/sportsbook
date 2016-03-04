namespace SportsBook.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using SportsBook.Common;

    public sealed class Configuration : DbMigrationsConfiguration<SportsBookDbContext>
    {
        private UserManager<AppUser> userManager;
        private IRandomGenerator random;

        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
            this.random = new RandomGenerator();
        }

        protected override void Seed(SportsBookDbContext context)
        {
            // this.userManager = new UserManager<AppUser>(new UserStore<AppUser>(context));
            var userManager = new UserManager<AppUser>(new UserStore<AppUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!roleManager.Roles.Any())
            {
                roleManager.Create(new IdentityRole { Name = "User" });
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }

            var adminUser = userManager.Users.FirstOrDefault(x => x.Email == "admin@admin.com");
            if (adminUser == null)
            {
                var admin = new AppUser
                {
                    Email = "admin@admin.com",
                    UserName = "admin",
                    EmailConfirmed = true,
                    Avatar = "http://www.premiumdxb.com/assets/img/avatar/default-avatar.jpg",
                    FirstName = "admin",
                    LastName = "admin"
                };

                userManager.Create(admin, "123456");
                userManager.AddToRole(admin.Id, "Admin");
            }

            if (userManager.Users.FirstOrDefault(x => x.Email == "user1@mail.com") == null)
            {
                var user = new AppUser
                {
                    Email = "user1@mail.com",
                    UserName = "user1",
                    Avatar = "http://www.premiumdxb.com/assets/img/avatar/default-avatar.jpg",
                    FirstName = "user1",
                    LastName = "user1"
                };

                userManager.Create(user, "123456");
                userManager.AddToRole(user.Id, "User");
            }

            if (userManager.Users.FirstOrDefault(x => x.Email == "user2@mail.com") == null)
            {
                var user = new AppUser
                {
                    Email = "user2@mail.com",
                    UserName = "user2",
                    Avatar = "http://www.premiumdxb.com/assets/img/avatar/default-avatar.jpg",
                    FirstName = "user2",
                    LastName = "user2"
                };

                userManager.Create(user, "123456");
                userManager.AddToRole(user.Id, "User");
            }

            if (userManager.Users.FirstOrDefault(x => x.Email == "user3@mail.com") == null)
            {
                var user = new AppUser
                {
                    Email = "user3@mail.com",
                    UserName = "user3",
                    Avatar = "http://www.premiumdxb.com/assets/img/avatar/default-avatar.jpg",
                    FirstName = "user3",
                    LastName = "user3"
                };

                userManager.Create(user, "123456");
                userManager.AddToRoles(user.Id, "User", "Admin");
            }

            if (userManager.Users.FirstOrDefault(x => x.Email == "user4@mail.com") == null)
            {
                var user = new AppUser
                {
                    Email = "user4@mail.com",
                    UserName = "user4",
                    Avatar = "http://www.premiumdxb.com/assets/img/avatar/default-avatar.jpg",
                    FirstName = "user4",
                    LastName = "user4"
                };

                userManager.Create(user, "123456");
                userManager.AddToRole(user.Id, "User");
            }

            adminUser = userManager.Users.FirstOrDefault(x => x.Email == "admin@gmail.com");

            if (adminUser != null)
            {
                userManager.AddToRoles(adminUser.Id, "User", "Admin");
            }

            var allUsers = context.Users.AsQueryable();
            context.SaveChanges();

            if (context.Cities.Count() == 0)
            {
                City sofia = new City
                {
                    Name = "Sofia"
                };

                City plovdiv = new City
                {
                    Name = "Plovdiv"
                };

                context.Cities.Add(sofia);
                context.Cities.Add(plovdiv);

                context.SaveChanges();
            }

            if (context.Facilities.Count() == 0)
            {
                var userOwner = userManager.Users.FirstOrDefault(x => x.Email == "admin@admin.com");
                City sofia = context.Cities.FirstOrDefault(x => x.Name == "Sofia");
                Facility sportnaSofia = new Facility
                {
                    Name = "Спортна София - Борисова",
                    City = sofia,
                    Image = "http://www.sportnasofia2000.com/sites/default/files/brilliant_gallery_temp/bg_cached_resized_0a9e63899daf685189624faea3f31d88.jpg",
                    Description = "Един модерен, многофункционален спортен комплекс, изграден в паркова среда," +
                "съобразно съвременните тенденции и изисквания за спорт и възстановяване на всички слоеве от населението." +
                "С възможности за индивидуални или организирани занимания със спорт – Комплексът се превърна в любимо място за деца," +
                "ученици, студенти и възрастни. Разположен в пространството между националния стадион “Васил Левски” и стадион" +
                "“Българска армия” в близост до БНР на площ от 8 000 кв.м.комплексът разполага с четири игрища за футбол / малки," +
                "средни и големи врати / -покрити с изкуствена тревна настилка, игрище за тенис на корт,игрище за популярната" +
                "френска игра “Петанк”, площадка за тенис на маса / 5 тенис маси /. Две външни съблекални, две закрити съблекални," +
                "250 седящи места, осветление на всяко игрище, паркинг с 30 места и стоянка за велосипеди, кафе – клуб за отдих" +
                "и възстановяване - допълват удоволствието за спорт и почивка на посетителите.",
                    AuthorId = userOwner.Id,
                    CreatedOn = DateTime.UtcNow
                };

                context.Facilities.Add(sportnaSofia);

                Facility silverCity = new Facility
                {
                    Name = "Silver City",
                    City = sofia,
                    Image = "http://silvercitysport.bg/wp-content/uploads/2015/03/basein223.jpg",
                    Description = "Няма нищо по - ефективно и полезно от плуването.То развива всички мускулни групи," +
                    "без да натоварва ставите.Тонизира кожата и увеличава дихателния капацитет на белите дробове.Масажиращият" +
                    "ефект на водата помага при борбата с целулита.Подходящ спорт за всяка възраст. Комплексът разполага с басейн" +
                    "с четири  коридора с дълбочина 1.30 – 1.70м.и размери 17×10м. Треньорите ни се занимават както с начинаещи," +
                    "така и с плувци ориентирани професионално към спорта. Всеки вторник басейнът е в профилактика от 7:00 до 9:30 часа.",
                    AuthorId = userOwner.Id,
                    CreatedOn = DateTime.UtcNow
                };

                context.Facilities.Add(silverCity);

                Facility dema = new Facility
                {
                    Name = "Дема",
                    City = sofia,
                    Image = "http://demasport.com/wp-content/uploads/2014/04/s-t-2_0_0.png",
                    Description = "ТЕНИС КЛУБ „ДЕМА“ Е НА 2 - РО МЯСТО МЕЖДУ 136 КЛУБА В СТРАНАТА" +
                    "Базата предлага: 7 червени корта, от които 3 с осветление, балон върху 3 корта през зимата," +
                    "система за автоматично поливане, телевизионна площадка, трибуни с 400 седящи места",
                    AuthorId = userOwner.Id,
                    CreatedOn = DateTime.UtcNow
                };

                context.Facilities.Add(dema);

                Facility kristal = new Facility
                {
                    Name = "Кристал",
                    City = sofia,
                    Image = "https://imgrabo.com/pics/guide/900x600/20150512141847_28449.jpg",
                    Description = "Спортен комплекс КРИСТАЛ е многофункционална сграда," +
                    "в която Ви предлагаме спортни занимания, разнообразние от СПА, козметични и масажни процедури," +
                    "богата гама от спортни курсове като: аеробика, тай - бо, народни танци и др." +
                    "Комплексът разполага с фитнес зала TECHNOGYM 700 кв.м., басейн, боксова зала с квалифизирани инструктори," +
                    "четири съблекални със стаи за релакс, SPA център(сауна, парна баня, инфрачервена кабина, свето и цвето терапия)" +
                    "масажни и козметични процедури, солариум (хоризонтален, вертикален).Също така, може да ползвате и паркинг," +
                    "бензиностанция, автомивка, ресторант и коктейл - бар. Прредлагаме курсове по:" +
                    "цялостна кондиция на тялото, степ аеробика, йога, народни танци, модерни танци, модерни танци за деца" +
                    "кик - бокс и др. Масажи: класически, частични, спортни, антицелулитни, лечебни, аромотерапии и др.",
                    AuthorId = userOwner.Id,
                    CreatedOn = DateTime.UtcNow
                };

                context.Facilities.Add(kristal);

                Facility sportnaPalata = new Facility
                {
                    Name = "Спортна Палата",
                    City = sofia,
                    Image = "http://sportuvai.bg/pictures/470654_401_301.jpg",
                    Description = "Спортна палата към Министерство на физическото възпитание и спорта се намира на бул. Васил Левски." +
                    "Комплексът разполага с 25 м закрит басейн, фитнес зала.Провеждат се курсове по аеробика от професионални инструктори." +
                    "Спортният център предлага антицелулитни масажи и сауна.",
                    AuthorId = userOwner.Id,
                    CreatedOn = DateTime.UtcNow
                };

                context.Facilities.Add(sportnaPalata);

                Facility baroco = new Facility
                {
                    Name = "Бароко спорт",
                    City = sofia,
                    Image = "http://baroccosport.com/images/phocagallery/thumbs/phoca_thumb_l_imgp1530.jpg",
                    Description = "Настилката е изкуствена трева с дължина 5 см и пълнеж от гумени гранули.Fibrillated fibre - е тревно влакно - като преждата," +
                    "но изрязани по специална технология, което означава, че се правят малки разрези в самото влакно на тревата," +
                    "като се получава типичната структура на медена пита.Fibrillated тревите поради специфичната структура на влакната притежават по - добро запълване на каучуковите гранули," +
                    "по - трудно гранулите излизат от тревата по време на игра.Тревата е поставена върху каменна основа," +
                    "което осигурява добър дренаж на терена и по - голямата му мекота при игра.Игрището е оградено със 6 м.ограда," +
                    "като е покрито с мрежа и отгоре.Обурудвано е с 8 бр.осветителни тела," +
                    "които осугуряват необходимата светлина за игра," +
                    "в късните часове на денонощието.Вратите са стандартни за мини футбол 3 на 2 м.",
                    AuthorId = userOwner.Id,
                    CreatedOn = DateTime.UtcNow
                };

                context.Facilities.Add(baroco);
 
                Facility academika = new Facility
                {
                    Name = "Академика",
                    City = sofia,
                    Image = "http://academica2011.com/images/stories/skakademika4km/novstadion.jpg",
                    Description = "Спортен комплекс Академика се намира на бул.Цариградско шосе № 125. Обекта е с изключително" +
                    "добро инфраструктурно разположение, отлична транспортна достъпност до линиите на градския транспорт и има" +
                    "осигурен паркинг. Комплексът разполага с три спортни зали, плувен басейн и футболно игрище." +
                    "Основната зала има мултифункционален характер и може да се използва за тренировки по множество спортове като баскетбол," +
                    "волейбол, бадмигтон, футзал и др.Зала А е с по - малки размери, но също предлага отлични условия за практикуване" +
                    "на спортове като волейбол, баскетбол, бойни изкуства, а така също е снабдена с огледала, което я прави удобна" +
                    "за тренировки по танци, аеробика, каланетика, и други.Зала Б е подходяща за бокс, кикбокс и други бойни изкуства." +
                    "Спортният комплекс разполага с 25 метров плувен басейн, с пет коридора.В него се поддържа високо ниво" +
                    "на санитарно - хигиенните условия.Обекта предоставя отлични условия за практикуване на различни плувни спортове." +
                    "Футболното игрище в комплекса е подходящо както за провеждане на редовни тренировки по футбтол, така и за състезания" +
                    "и първенства.Игрището е осветено, което позволява да се провеждат тренировки и в по - късните часове.",
                    AuthorId = userOwner.Id,
                    CreatedOn = DateTime.UtcNow
                };

                context.Facilities.Add(academika);

                context.SaveChanges();
            }

            var facilities = context.Facilities.Take(10).ToList();

            if (context.SportCategories.Count() == 0)
            {
                SportCategory football = new SportCategory
                {
                    Name = "Football",
                    Description = "Description"
                };

                SportCategory fitness = new SportCategory
                {
                    Name = "Fitness",
                    Description = "Description"
                };

                SportCategory swimming = new SportCategory
                {
                    Name = "Swimming",
                    Description = "Description"
                };

                SportCategory tenis = new SportCategory
                {
                    Name = "Tenis",
                    Description = "Description"
                };

                SportCategory volleyball = new SportCategory
                {
                    Name = "Volleyball",
                    Description = "Description"
                };

                SportCategory basketball = new SportCategory
                {
                    Name = "basketball",
                    Description = "Description"
                };

                football.Facilities.Add(facilities[0]);
                swimming.Facilities.Add(facilities[1]);
                fitness.Facilities.Add(facilities[1]);
                tenis.Facilities.Add(facilities[2]);
                swimming.Facilities.Add(facilities[3]);
                swimming.Facilities.Add(facilities[4]);
                football.Facilities.Add(facilities[5]);
                volleyball.Facilities.Add(facilities[6]);
                football.Facilities.Add(facilities[6]);
                basketball.Facilities.Add(facilities[6]);

                context.SportCategories.Add(football);
                context.SportCategories.Add(volleyball);
                context.SportCategories.Add(basketball);
                context.SportCategories.Add(fitness);
                context.SportCategories.Add(swimming);
                context.SportCategories.Add(tenis);

                context.SaveChanges();
            }
        }
    }
}