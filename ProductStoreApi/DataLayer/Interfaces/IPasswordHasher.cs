﻿
namespace StoreDAL.Interfaces
{
	public interface IPasswordHasher
	{
		string HashPassword(string password);
		bool VerifyPassword(string password, string hash);
	}
}
