using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DAFW_IS220.Models
{
    public class OrderInfo {
        public int OrderId {set;get;}

        public decimal Amount {set;get;}

        public string Status {set;get;}

        public DateTime CreatedDate {set;get;}
    }
}