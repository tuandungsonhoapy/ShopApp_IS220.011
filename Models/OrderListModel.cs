using App.DBModels;

namespace DAFW_IS220.Models
{
    public class OrderListModel
    {
        public int MADH {set;get;}

        public string OrderStatus {set;get;}

        public decimal TONGTIEN {set;get;}

        public List<OrderDetailModel> OrderDetails {set;get;}
    }
}