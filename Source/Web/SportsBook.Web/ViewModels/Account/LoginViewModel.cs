namespace SportsBook.Web.ViewModels.Account
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class LoginViewModel
    {
        [Required]
        [AllowHtml]
        [Display(Name = "Username")]
        [RegularExpression(@"[a-zA-Z_0-9]+", ErrorMessage = "Only letters, numbers and _ allowed")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string UserName { get; set; }

        [Required]
        [AllowHtml]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [RegularExpression(@"[a-zA-Z_0-9]+", ErrorMessage = "Only letters, numbers and _ allowed")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
