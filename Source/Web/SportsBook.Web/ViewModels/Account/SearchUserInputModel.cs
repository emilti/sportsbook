using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsBook.Web.ViewModels.Account
{
    public class SearchUserInputModel
    {
        [Required(ErrorMessage = "Името е задължително!")]
        [AllowHtml]
        [RegularExpression(@"^[^<>]*$", ErrorMessage = "Невалиден символ")]
        [StringLength(50, ErrorMessage = "{0}то трябва да е поне {2} символа.", MinimumLength = 2)]
        public string search { get; set; }
    }
}