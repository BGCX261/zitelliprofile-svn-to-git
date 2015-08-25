using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace ZitelliProfile.Presentation.Web.Models
{

	public class ProfileContext : DbContext
	{
		public ProfileContext()
			: base("DefaultConnection")
		{
		}

		public DbSet<UserProfile> UserProfiles { get; set; }
		public DbSet<UserSuggestion> UserSuggestions { get; set; }
		public DbSet<Mail> Mails { get; set; }
	}

	[Table("UserProfile")]
	public class UserProfile
	{
		[Key]
		[DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
		public int UserId { get; set; }
		public string UserName { get; set; }
	}

	[Table("Mail")]
	public class Mail
	{
		[Key]
		[DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string UserName { get; set; }
		public string Email { get; set; }
		public string Subject { get; set; }
		public string Body { get; set; }
		public DateTime Datetime { get; set; }
	}

	[Table("UserSuggestion")]
	public class UserSuggestion
	{
		[Key]
		[DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string UserName { get; set; }
		public string Email { get; set; }
		public string Body { get; set; }
		public DateTime Datetime { get; set; }
	}

	public class RegisterExternalLoginModel
	{
		[Required]
		[Display(Name = "User name")]
		public string UserName { get; set; }

		public string ExternalLoginData { get; set; }
	}

	public class LocalPasswordModel
	{
		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "Current password")]
		public string OldPassword { get; set; }

		[Required]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(Name = "New password")]
		public string NewPassword { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "Confirm new password")]
		[Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
		public string ConfirmPassword { get; set; }
	}

	public class LoginModel
	{
		[Required]
		[Display(Name = "User name")]
		public string UserName { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string Password { get; set; }

		[Display(Name = "Remember me?")]
		public bool RememberMe { get; set; }
	}

	public class HomePageModel
	{

        public HomePageModel()
        {
            Mails = new List<Mail>();
            UserSuggestions = new List<UserSuggestion>();
        }


		[Display(Name = "User name")]
		public string UserName { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string Password { get; set; }

		[Display(Name = "Remember me?")]
		public bool RememberMe { get; set; }

		[Display(Name = "EMail")]
		public string Email { get; set; }

		[Display(Name = "Subject")]
		public string Subject { get; set; }

		[Display(Name = "Body")]
		public string Body { get; set; }

        [Display(Name = "Error")]
        public string ErrorMessage { get; set; }

        [Display(Name = "Success")]
        public string SuccessMessage { get; set; }

        [Display(Name = "Mails")]
        public IEnumerable<Mail> Mails { get; set; }

        [Display(Name = "Sugerencias")]
        public IEnumerable<UserSuggestion> UserSuggestions { get; set; }



	}

	public class RegisterModel
	{
		[Required]
		[Display(Name = "User name")]
		public string UserName { get; set; }

		[Required]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string Password { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "Confirm password")]
		[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
		public string ConfirmPassword { get; set; }
	}

	public class ExternalLogin
	{
		public string Provider { get; set; }
		public string ProviderDisplayName { get; set; }
		public string ProviderUserId { get; set; }
	}
}
