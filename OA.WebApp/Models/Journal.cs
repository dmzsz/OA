using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OA.WebApp.Models
{
    [Table("fm_journal")]
    public class Journal
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        //[Display(Name = "记录日期")]
        //public DateTime RecordDate { get; set; }

        //[Display(Name = "摘要")]
        //public string Summary { get; set; }

        //[Display(Name = "结算单位")]
        //public string ClientID { get; set; }

        //[Display(Name = "金额")]
        //public double Amount { get; set; }

        //[Display(Name = "收支")]
        //public string Borrowing { get; set; }

        //[Display(Name = "销账金额")]
        //public double SalesAmount { get; set; }

        //[Display(Name = "付款方式")]
        //public string Paytype { get; set; }

        //[Display(Name = "销账日期")]
        //public DateTime PayDate { get; set; }

        //[Display(Name = "发票号")]
        //public string ReceiptNo { get; set; }

        //[Display(Name = "账户类型")]
        //public string AccountType { get; set; }

        //[Display(Name = "删除")]
        //public bool Deleted { get; set; }

        //[Display(Name = "创建人")]
        //public bool CreatedBy { get; set; }

        //[Display(Name = "创建日期")]
        //public bool CreatedAt { get; set; }

        //[Display(Name = "修改人")]
        //public bool ModifiedBy { get; set; }

        //[Display(Name = "修改日期")]
        //public bool ModifiedAt { get; set; }

    }
}
