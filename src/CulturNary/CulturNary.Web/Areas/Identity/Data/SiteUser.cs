using Microsoft.AspNetCore.Identity;

namespace CulturNary.Web.Areas.Identity.Data
{
    public class SiteUser : IdentityUser
    {
        [PersonalData]
        public string DisplayName { get; set; }
        [PersonalData]
        public string Biography { get; set; }
        [PersonalData]
        public string ProfileImageName { get; set; }

    }

    // public class Image
    // {
    //     public int ImageId { get; set; }
    //     public string ImageName { get; set; }
    //     public string ImagePath { get; set; }
    // }
}