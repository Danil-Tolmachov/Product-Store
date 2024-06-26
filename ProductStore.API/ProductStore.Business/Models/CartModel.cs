﻿
using ProductStore.Data.Entities;

namespace ProductStore.Business.Models
{
	public class CartModel
	{
		public int Id { get; set; }
		public long UserId { get; set; }

		public IEnumerable<CartItemModel> CartItems { get; set; } = new List<CartItemModel>();
	}
}
