using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using App.DBModels;

namespace DAFW_IS220.Models
{
    public class OrderFeedback {
        public OrderDetailModel orderDetailModel {set;get;}

        public DANHGIA dANHGIA {set;get;}
    }
}