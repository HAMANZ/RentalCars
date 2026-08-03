using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.DTO_EXT
{
    public class AboutUs_EXT
    {
        public long AboutUsEnId { get; set; }
        public string AboutUsEnTitle { get; set; }
        public string AboutUsEnDescription { get; set; }
        public long AboutUsArId { get; set; }
        public string AboutUsArTitle { get; set; }
        public string AboutUsArDescription { get; set; }
        public string AboutUsImage { get; set; }
        public IFormFile AboutUsUploadImage { get; set; }
    }
}
