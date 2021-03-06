﻿using System;
using System.ComponentModel.DataAnnotations;

namespace OA.WebApp.Models
{
    public class FreightFcl
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "船名")]
        public string FCL_ID{ get; set; }

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
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true, NullDisplayText = "暂无")]
        public DateTime? ETD { get; set; }

        [Display(Name = "内外贸")]
        public bool Trade { get; set; }
    }
}
