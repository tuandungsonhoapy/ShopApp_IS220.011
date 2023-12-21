using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DAFW_IS220.Models
{
    public class OrderModel {
        public string TENKH {set;get;}

        public string Phone {set;get;}

        public string Address {set;get;}

        public string Email {set;get;}

        public int TypePayment {set;get;}

        public int TypePaymentVN {set;get;}

        public decimal Price {set;get;}

        public string? Note {set;get;}

        public int Deliveryid {set;get;}
    }
}