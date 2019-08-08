using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Models
{
    /// <summary>
    /// Represents an individual website user
    /// </summary>
    public class Member
    {  
        /// <summary>
        /// The first and last name of the member
        /// ex. John Doe
        /// </summary>
        [StringLength(60)]
        [Required]
        [Display(Name ="Full Name")]
        public string FullName { get; set; }

        /// <summary>
        /// Personal email of the member
        /// </summary>
        [Required]
        [StringLength(100)]
        [EmailAddress(ErrorMessage = "That doesn't look like an email to me")]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        /// <summary>
        /// The username of the member
        /// </summary>
        [Required]
        [StringLength(20)]
        [RegularExpression(@"^[\d\w]+$", 
            ErrorMessage = "Usernames can only contain A-Z, 0-9, and underscores")]
        public string Username { get; set; }

        /// <summary>
        /// The password for this individual user
        /// </summary>
        [Required]
        [StringLength(100)]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// The date of birth
        /// </summary>
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        //Already required because DateTime is a structure(value type)
        //Make custom attribute to make a dynamid date range
        public DateTime DateOfBirth { get; set; }
    }
}
