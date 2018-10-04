using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OA.WebApp.Models
{
    [Table("st_user_company")]
    public class UserCompany : BaseEntity
    {
        [Key, Column(Order = 0)]
        public int UserID { get; set; }

        [Key, Column(Order = 1)]
        public int CompanyID { get; set; }

        //导航属性
        public virtual User User { get; set; }

        public virtual Company Company { get; set; }
    }
}
