﻿
using StoreDAL.Entities;

namespace StoreBLL.Models
{
	public class UserModel
	{
		public long Id { get; set; }
		public bool IsActive { get; set; }
		public string Username { get; set; } = string.Empty;
		public string? Password { get; set; }

		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;

		public decimal Discount { get; set; }
		public string Address { get; set; } = string.Empty;

		public long CartId { get; set; }
		public IEnumerable<CartItemModel> CartItems { get; set; } = new List<CartItemModel>();

		public IEnumerable<OrderModel> Orders { get; set; } = new List<OrderModel>();
		public IEnumerable<ContactModel> Contacts { get; set; } = new List<ContactModel>();
	}
}
