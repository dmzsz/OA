using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        [Display(Name = "目的港口中名")]
        public string EndNameCN { get; set; }

        [Display(Name = "起运港口英文名")]
        public string StartNameEN { get; set; }

        [Display(Name = "中转港口英文名")]
        public string TransferNameEN { get; set; }

        [Display(Name = "目的港口英文名")]
        public string EndNameEN { get; set; }

        // 1:直航 2:转港
        [Display(Name = "是否有中转港")]
        public int TransferFlag { get; set; }

        [Display(Name = "船公司logo")]
        public string ShippingLogo { get; set; }


        [Display(Name = "挂港图")]
        public string RouteImg { get; set; }

        // 1:充足;2:紧张;3:爆仓
        [Display(Name = "舱位情况")]
        public int SpaceStatus { get; set; }

        [Display(Name = "舱位总数")]
        public long SpaceTotal { get; set; }

        [Display(Name = "舱位剩余数")]
        public long SpaceResidue { get; set; }

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
        public string Description { get; set; }

        [Display(Name = "价格发布时间")]
        public string[] PriceDates { get; set; }

        [Display(Name = "20GP历史价格")]
        public int[] GP20 { get; set; }

        [Display(Name = "40GP历史价格")]
        public int[] GP40 { get; set; }

        [Display(Name = "40HC历史价格")]
        public int[] HC40 { get; set; }

        public int Limit { get; set; }

        public int Offset { get; set; }

        // 存储的是id 已经对应的 发布时间 GP20 GP40 HC40
        public static IDictionary<int, ArrayList> ChartDatas { get; set; }
    }
}
