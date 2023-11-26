using App.DBModels;

namespace DAFW_IS220.Models
{
    public class CartItem
    {
        public int quantity { set; get; }
        public ProductDetailModel product { set; get; }
    }
}