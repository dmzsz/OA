using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OA.WebApp.Models
{
    public class Vessel
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "船名")]
        public string Name { get; set; }

        [Display(Name = "中文船名")]
        public string LocalName { get; set; }

        [Display(Name = "航次")]
        public string Voy { get; set; }

        [Display(Name = "码头")]
        public string Port{ get; set; }

        [Display(Name = "开港时间")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime OpDate { get; set; }

        [Display(Name = "截关时间")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime CloseDate { get; set; }

        [Display(Name = "船期")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime? ETD { get; set; }

        [Display(Name = "内外贸")]
        public bool Trade { get; set; }
    }
}
