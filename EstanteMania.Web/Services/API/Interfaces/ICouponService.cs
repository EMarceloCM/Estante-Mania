using EstanteMania.Web.Models.DTO_s;

namespace EstanteMania.Web.Services.API.Interfaces
{
    public interface ICouponService
    {
        Task<CouponDTO?> GetCouponByCode(string couponCode);
    }
}
