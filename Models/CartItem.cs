using App.DBModels;

namespace DAFW_IS220.Models
{
    public class CartItem
    {
        public string userid {set;get;}
        public int quantity { set; get; }
        public ProductDetailModel product { set; get; }
    }
}