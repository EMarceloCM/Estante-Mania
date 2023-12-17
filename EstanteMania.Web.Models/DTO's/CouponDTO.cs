namespace EstanteMania.Web.Models.DTO_s
{
    public class CouponDTO
    {
        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public decimal Discount { get; set; }
    }
}
