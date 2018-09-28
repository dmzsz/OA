using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OA.WebApp.ViewModels
{
    public class T1DataDto
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "起运港口中文名")]
        public string StartNameCN { get; set; }

        [Display(Name = "中转港口中文名")]
        public string TransferNameCN { get; set; }

        [Display(Name = "目的港口英文名")]
        public string EndNameEN { get; set; }

        [Display(Name = "起运港口英文名")]
        public string StartNameEN { get; set; }

        [Display(Name = "中转港口英文名")]
        public string TransferNameEN { get; set; }

        [Display(Name = "船公司logo")]
        public string ShippingLogo { get; set; }

        // 1充足2紧张3爆仓
        [Display(Name = "舱位情况")]
        public int SpaceStatus { get; set; }

        [Display(Name = "舱位总数")]
        public int SpaceTotal { get; set; }

        [Display(Name = "舱位剩余数")]
        public int SpaceResidue { get; set; }

        [Display(Name = "20GP")]
        public Decimal Price20 { get; set; }

        [Display(Name = "40GP")]
        public Decimal Price40 { get; set; }

        [Display(Name = "40HC")]
        public Decimal Price40HQ { get; set; }

        [Display(Name = "生效日期")]
        public DateTime BeginDate { get; set; }

        [Display(Name = "失效日期")]
        public DateTime EndDate { get; set; }

        [Display(Name = "备注信息")]
        public DateTime Description { get; set; }

        //[Display(Name = "备注信息")]
        //public ICollection<> Description { get; set; }
    }
}
