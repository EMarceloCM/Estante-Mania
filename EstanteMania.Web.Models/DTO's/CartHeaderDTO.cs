﻿using EstanteMania.API.DTO_s;

namespace EstanteMania.Web.Models.DTO_s
{
    public class CartHeaderDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string CouponCode { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime DateTime { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string CardNumber { get; set; }
        public string CVV { get; set; }
        public string ExpiryMonthYear { get; set; }
        public int CartTotalItens { get; set; }
        public IEnumerable<CarrinhoItemDTO> CartDetails { get; set; }
        public int cartId { get; set; }
    }
}
