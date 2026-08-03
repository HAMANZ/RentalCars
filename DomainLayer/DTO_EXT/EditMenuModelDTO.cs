using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.DTO_EXT
{
    public class EditMenuModel
    {
        public int MenuEnId { get; set; }
        public int MenuArId { get; set; }
        public string TitleEn { get; set; }
        public string TitleAr { get; set; }
        public string Action { get; set; }
        public string is_subTest { get; set; }
        public string PositionNumber { get; set; }

    }
}
