using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.DTO_EXT
{
    public class AnnouncementDTO_Ext
    {
        public long IdEn { get; set; }
        public long IdAr { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime CreatedAt{ get; set; }
        public string Icon { get; set; }
        public string Image { get; set; }
        public string TitleEn { get; set; }
        public string TitleAr { get; set; }
        public long LanguageId { get; set; }

    }
}
