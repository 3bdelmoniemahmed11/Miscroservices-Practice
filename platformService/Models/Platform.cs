
using System.ComponentModel.DataAnnotations;

namespace PlatformService
{
    public  class Platform
    {
     
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Cost { get; set; }
        [Required]
        public string Publisher { get; set; }
    }
}
