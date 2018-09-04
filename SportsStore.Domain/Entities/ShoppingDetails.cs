using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SportsStore.Domain.Entities
{
    public class ShippingDetails
    {
        [Required(ErrorMessage = "请选择一个产品名称")]
        public string Name { get; set; }
        [Required(ErrorMessage = "请输入第一个地址栏")]
        [Display(Name = "地址栏1")]
        public string Line1 { get; set; }
        [Display(Name = "地址栏2")]
        public string Line2 { get; set; }
        [Display(Name = "联系方式：")]
        public string Line3 { get; set; }
        [Required(ErrorMessage = "请选择一个国家名")]
        [Display(Name = "国家")]
        public string Country { get; set; }
        [Required(ErrorMessage = "请选择一个省份名称")]
        [Display(Name = "省份")]
        public string City { get; set; }
        [Required(ErrorMessage = "请选择一个城市名称")]
        [Display(Name = "市")]
        public string State { get; set; }
        [Display(Name = "区")]
        public string Zip { get; set; }
        public bool GiftWrap { get; set; }
    }
}
