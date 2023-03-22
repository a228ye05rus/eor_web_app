using Microsoft.AspNetCore.Identity;


namespace eor_web_app.Models
{
    public class User : IdentityUser
    {
        public int Year { get; set; }
    }
}
