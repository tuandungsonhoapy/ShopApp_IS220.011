using App.DBModels;

namespace DAFW_IS220.Models
{
    public class OrderDetailModel
    {
        public int MADH {set;get;}

        public SANPHAM sANPHAM {set;get;}

        public MAUSAC mAUSAC {set;get;}

        public SIZE sIZE {set;get;}

        public CHITIETSANPHAM cHITIETSANPHAM {set;get;}

        public int SoLuong {set;get;}

        public decimal Price {set;get;}
    }
}