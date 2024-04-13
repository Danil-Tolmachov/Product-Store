using StoreDAL.Infrastructure;
using StoreDAL.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreTest.DAL
{
    [TestFixture]
    internal class StoreDbContextTest
    {
        [Test]
        public void StoreDbFactory_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => new StoreDbFactory(new TestDataFactory()));
        }

        [Test]
        public void StoreDbFactory_CreatesContext_ReturnContext()
        {
            StoreDbContext result = new StoreDbFactory(new TestDataFactory()).CreateDbContext();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.GetType(), Is.EqualTo(typeof(StoreDbContext)));
        }

        [Test]
        public void StoreDbFactory_CreatesContext_DbSetsNotEmpty()
		{
			StoreDbContext result = new StoreDbFactory(new TestDataFactory()).CreateDbContext();

			Assert.Multiple(() =>
			{
				Assert.That(result.CartItems, Is.Not.Null);
				Assert.That(result.Carts, Is.Not.Null);
				Assert.That(result.Categories, Is.Not.Null);
				Assert.That(result.Contacts, Is.Not.Null);
				Assert.That(result.Employees, Is.Not.Null);
				Assert.That(result.Users, Is.Not.Null);
				Assert.That(result.Orders, Is.Not.Null);
				Assert.That(result.OrderDetails, Is.Not.Null);
				Assert.That(result.Specifications, Is.Not.Null);
				Assert.That(result.Statuses, Is.Not.Null);
				Assert.That(result.Products, Is.Not.Null);
				Assert.That(result.Positions, Is.Not.Null);
				Assert.That(result.Persons, Is.Not.Null);
			});
		}
	}
}
