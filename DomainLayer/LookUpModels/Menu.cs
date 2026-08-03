using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.LookUpModels
{
    public class Menu
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Action { get; set; }
        public string is_subTest { get; set; }
        public string PositionNumber { get; set; }
    }
}
