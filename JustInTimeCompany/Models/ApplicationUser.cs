using Microsoft.AspNetCore.Identity;

namespace JustInTimeCompany.Models
{
    public enum Gender { Male, Female, Dragon }
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual Gender Gender { get; set; }
    }

}
