using Lab03MiniprojektAPI.Data;
using Lab03MiniprojektAPI.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Lab03MiniprojektAPI.Models
{
    public class WebsiteLink
    {
        public int Id { get; set; }
        public string Link { get; set; }

        public virtual Interest Interests { get; set; }
        public virtual Person Persons { get; set; }
 
    }
}
