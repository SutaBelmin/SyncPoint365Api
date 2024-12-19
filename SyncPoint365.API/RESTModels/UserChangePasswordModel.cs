using System.ComponentModel.DataAnnotations;

namespace SyncPoint365.API.RESTModels
{
    public class UserChangePasswordModel
    {
        public int Id { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
