using StoreDAL.Entities;

namespace StoreDAL.Infrastructure.Data
{
	public class TestDataFactory : AbstractDataFactory
	{
		public override Contact[] GetContactData()
		{
			return new Contact[]
			{
				new Contact(1) { Name = "Email", Value = "email1@main.com", PersonId = 1 },
				new Contact(2) { Name = "Phone", Value = "+380111111111", PersonId = 1 },
				new Contact(3) { Name = "Phone", Value = "+380222222222", PersonId = 2 },
				new Contact(4) { Name = "Email", Value = "email2@main.com", PersonId = 3 },
				new Contact(5) { Name = "Viber", Value = "+380333333333", PersonId = 4 },
			};
		}

		public override Person[] GetPersonData()
		{
			return new Person[]
			{
				new Person(1) { FirstName = "John", LastName = "Smith", Address = "New York, 123 Broadway St", Discount = 0.1m },
				new Person(2) { FirstName = "Emily", LastName = "Johnson", Address = "Los Angeles, 456 Sunset Blvd", Discount = 0.15m },
				new Person(3) { FirstName = "Michael", LastName = "Brown", Address = "Houston, 1011 Main St", Discount = 0.05m },
				new Person(4) { FirstName = "Jessica", LastName = "Davis", Address = "Phoenix, 1213 Central Ave", Discount = 0.05m },
				new Person(5) { FirstName = "David", LastName = "Wilson", Address = "Philadelphia, 1415 Walnut St", Discount = 0.2m },
			};
		}

		public override User[] GetUserData()
		{
			return new User[]
			{
				new User(1) { PersonId = 1, Username = "johnsmith", Password = "$2a$11$QxP6GG54zUOzEhLs2CORNuNOfokAkM09Op5MFN0oLkTeX3rS/vjsO", IsActive = true },
				new User(2) { PersonId = 2, Username = "emilyjohnson", Password = "$2a$11$nVp1qAK9YwOi5LNpFAXfR.SK07.DRBt4KnvIfdpOnahEQO6wJouOy", IsActive = true },
				new User(3) { PersonId = 3, Username = "michaelbrown", Password = "$2a$11$yD69ysc/wPKgIWpLJq2HjOLfDRPymOhlz8jRVoj4sv57L9G.jjSsq", IsActive = true },
				new User(4) { PersonId = 4, Username = "jessicadavis", Password = "$2a$11$sj9d8CY2L0ozSoQfdvce5.TpCiI.dr6iejTOmWBAE1xqJ9rj/wRw6", IsActive = true },
				new User(5) { PersonId = 5, Username = "davidwilson", Password = "$2a$11$n/bFeF3Xh.QHYwQ1RL6GyuvNe.tytw2NnWlQA0RSEVe6cMFbXRQeu", IsActive = false },
			};
		}

		public override Position[] GetPositionData()
		{
			return new Position[]
			{
				new Position(1) { Name = "Administrator" },
				new Position(2) { Name = "Manager" },
				new Position(3) { Name = "Helper" },
			};
		}

		public override Employee[] GetEmployeeData()
		{
			return new Employee[]
			{
				new Employee(1) { UserId = 4, PositionId = 1 },
				new Employee(2) { UserId = 3, PositionId = 2 },
			};
		}

		public override Category[] GetCategoryData()
		{
			return new Category[]
			{
				new Category(1) { Name = "Alcohol" },
				new Category(2) { Name = "Bakery" },
				new Category(3) { Name = "Meat" },
				new Category(4) { Name = "Beverages" },
				new Category(5) { Name = "Snacks" },
				new Category(6) { Name = "Frozen Foods" },
				new Category(7) { Name = "Canned" },
				new Category(8) { Name = "Pasta" },
				new Category(9) { Name = "Sauces" },
				new Category(10) { Name = "Energy Drinks" },
			};
		}

		public override Specification[] GetSpecificationData()
		{
			return new Specification[]
			{
				new Specification(1) { Name = "Weight", Value = "1 kg" },
				new Specification(2) { Name = "Weight", Value = "0.5 kg" },
				new Specification(3) { Name = "Volume", Value = "1 l" },
				new Specification(4) { Name = "Volume", Value = "0.75 l" },
				new Specification(5) { Name = "Volume", Value = "0.25 l" },
				new Specification(6) { Name = "Volume", Value = "0.5 l" },
				new Specification(7) { Name = "Weight", Value = "1 kg" },
				new Specification(8) { Name = "Size", Value = "15x2x3" },
				new Specification(9) { Name = "Manufacturer", Value = "United States" },
				new Specification(10) { Name = "Manufacturer", Value = "Canada" },
				new Specification(11) { Name = "Manufacturer", Value = "Japan" },
				new Specification(12) { Name = "Manufacturer", Value = "United Kingdom" },
				new Specification(13) { Name = "Manufacturer", Value = "Germany" },
				new Specification(14) { Name = "Manufacturer", Value = "France" },
				new Specification(15) { Name = "Manufacturer", Value = "Brazil" },
			};
		}

		public override Product[] GetProductData()
		{
			return new Product[]
			{
				new Product(1) {
					Name = "Martini Brut 11,5%, 0,75l",
					Description = "Martini Brut is a premium sparkling wine that boasts an alcohol content of 11.5%. Known for its fine bubbles and balanced flavor profile, this wine is perfect for celebratory occasions or simply enjoying a glass with friends. The 0.75 liter bottle is ideal for sharing and ensures you have enough to go around. Martini Brut combines elegance and a refreshing taste, making it a versatile choice for any event.",
					Price = 13.37m,
					CategoryId = 1,
					Discount = 0.35m,
					UnitMeasure = "bottle",
				},

				new Product(2) {
					Name = "Hankey Bannister, 1l",
					Description = "Hankey Bannister is a renowned blended Scotch whisky known for its smooth and balanced flavor profile. This 1-liter bottle of Hankey Bannister offers ample supply for your enjoyment or for sharing with friends and family. Crafted with a blend of carefully selected malt and grain whiskies, Hankey Bannister delivers a rich and satisfying taste experience with every sip. Whether enjoyed neat, on the rocks, or as part of your favorite cocktail, Hankey Bannister promises a delightful whisky experience.",
					Price = 24.78m,
					CategoryId = 1,
					Discount = 0.6m,
					UnitMeasure = "bottle",
				},

				new Product(3) {
					Name = "Corona Extra, 0,33l",
					Description = "Corona Extra is a popular Mexican beer known for its refreshing taste and iconic presentation. Brewed since 1925, this pale lager has become a symbol of relaxation and enjoyment worldwide. With its light and crisp flavor profile, Corona Extra is perfect for any occasion, whether you're lounging on the beach, hosting a barbecue, or simply unwinding after a long day. Enjoyed best when served ice-cold with a wedge of lime, Corona Extra delivers a smooth and satisfying drinking experience that pairs effortlessly with sunny days and good company.",
					Price = 1.54m,
					CategoryId = 1,
					Discount = 0.37m,
					UnitMeasure = "bottle",
				},

				new Product(4) {
					Name = "Sandwich with ham, 200g",
					Description = "Indulge in the timeless simplicity of the classic ham sandwich. Made with thinly sliced ham nestled between two slices of your favorite bread, this sandwich is a satisfying combination of flavors and textures. Whether you're packing a lunch, need a quick bite, or craving a comforting snack, the classic ham sandwich is always a delicious choice. Add your favorite condiments and toppings like lettuce, tomato, cheese, or mustard to customize it to your taste preferences. With its savory ham and soft bread, this sandwich is a timeless favorite that never fails to hit the spot.",
					Price = 1.44m,
					CategoryId = 2,
					Discount = 0.2m,
					UnitMeasure = "unit",
				},

				new Product(5) {
					Name = "Bun with cherry",
					Description = "Savor the delightful combination of sweet cherries nestled within a soft, fluffy bun with our Cherry Bun Delight. Perfect for breakfast, dessert, or a delightful snack, this treat offers a burst of fruity flavor in every bite. Each bun is lovingly filled with succulent cherries, creating a harmonious blend of tartness and sweetness that tantalizes the taste buds. Whether enjoyed with a cup of coffee in the morning or as a delightful afternoon treat, our Cherry Bun Delight is sure to brighten your day with its irresistible flavor and comforting texture.",
					Price = 1.36m,
					CategoryId = 2,
					Discount = 0.2m,
					UnitMeasure = "unit",
				},

				new Product(6) {
					Name = "Beef steak Wet Aged, 100g",
					Description = "Indulge in the exquisite taste and tender texture of our Beef Steak Wet Aged, expertly crafted to perfection. Each succulent bite promises a symphony of flavors, from the rich umami of premium beef to the subtle hints of natural juices that enhance every mouthful. Wet aging ensures optimal tenderness and juiciness, allowing the meat to reach its peak flavor potential. Sourced from the finest cuts of beef, our steak is hand-selected for its marbling and quality, guaranteeing a dining experience that surpasses expectations. Whether seared to perfection on the grill or pan-fried to your desired level of doneness, our Beef Steak Wet Aged is a culinary masterpiece that elevates any meal.",
					Price = 12.90m,
					CategoryId = 3,
					Discount = 0.1m,
					UnitMeasure = "unit",
				},

				new Product(7) {
					Name = "Schweppes pomegranate, 1l",
					Description = "Description6",
					Price = 1.15m,
					CategoryId = 4,
					Discount = 0m,
					UnitMeasure = "bottle",
				},

				new Product(8) {
					Name = "Dr.Pepper Cherry, 0,33l",
					Description = "Indulge in the bold and refreshing flavor of Dr. Pepper Cherry, a delightful twist on the classic soda favorite. Bursting with the essence of ripe cherries combined with the signature blend of 23 flavors found in Dr. Pepper, each sip delivers a symphony of taste sensations that tantalize the palate. Whether enjoyed on its own over ice or used as a mixer in your favorite cocktails, Dr. Pepper Cherry is the perfect beverage choice for any occasion. With its distinctive cherry-infused aroma and satisfyingly fizzy texture, this iconic soda will leave you craving more with every sip.",
					Price = 0.92m,
					CategoryId = 4,
					Discount = 0.19m,
					UnitMeasure = "can",
				},

				new Product(9) {
					Name = "Mountain Dew, 0,33l",
					Description = "Embark on an exhilarating journey of citrusy refreshment with Mountain Dew, the iconic soft drink that energizes and invigorates with every sip. Bursting with bold flavors and a vibrant neon-green hue, Mountain Dew is a beloved beverage that ignites the senses and fuels adventure. With its unique blend of citrus flavors and a touch of sweetness, this fizzy drink delivers an unmistakable taste that's as exhilarating as scaling a mountain peak. Whether you're tackling the great outdoors or simply kicking back with friends, Mountain Dew is the perfect companion for those who thirst for excitement.",
					Price = 1.26m,
					CategoryId = 4,
					Discount = 0.22m,
					UnitMeasure = "can",
				},

				new Product(10) {
					Name = "Pringles with sour cream and onion flavor, 165g",
					Description = "Indulge in the irresistible crunch and savory taste of Pringles Sour Cream & Onion Flavor. Each bite-sized crisp is meticulously seasoned with a delectable blend of tangy sour cream and zesty onion flavors, creating a mouthwatering snack sensation that's hard to resist. Whether you're snacking solo, sharing with friends, or enjoying a movie night at home, Pringles Sour Cream & Onion Flavor offers the perfect combination of flavor and crunch to satisfy your snack cravings. Packed in a convenient resealable tube, these Pringles are ideal for on-the-go munching or as a flavorful addition to any party spread.",
					Price = 3.60m,
					CategoryId = 5,
					Discount = 0m,
					UnitMeasure = "unit",
				},

				new Product(11) {
					Name = "Pringles with hot chili-lime flavor, 158g",
					Description = "Indulge in the unmatched crispiness and flavorful taste of Pringles chips with chili-lime flavor. Each crispy chip is infused with the bold flavor of spicy chili and refreshing lime, creating an incredibly appetizing sensation that's hard to resist. Whether you're snacking alone or sharing with friends, Pringles with chili-lime flavor are sure to satisfy your taste buds with every bite.",
					Price = 3.60m,
					CategoryId = 5,
					Discount = 0m,
					UnitMeasure = "unit",
				},

				new Product(12) {
					Name = "Pringles cheese, 165g",
					Description = "Enjoy the delightful taste of Pringles chips with cheese flavor. Each crispy chip is bursting with rich and savory cheese flavor that will tantalize your taste buds with every bite. Perfect for snacking on the go or sharing with friends, Pringles cheese flavor chips are a delicious and satisfying treat for any occasion.",
					Price = 3.60m,
					CategoryId = 5,
					Discount = 0m,
					UnitMeasure = "unit",
				},

				new Product(13) {
					Name = "Croissants Shulstad, 480 g",
					Description = "Indulge in the delightful taste of Schulstad Croissants, made with traditional recipes and the finest ingredients. Each 480g pack contains perfectly baked, flaky croissants that are golden on the outside and soft on the inside. These croissants are ideal for breakfast, a quick snack, or as an accompaniment to your favorite meals.",
					Price = 6.230m,
					CategoryId = 6,
					Discount = 0.2m,
					UnitMeasure = "unit",
				},

				new Product(14) {
					Name = "Sorbet Tonitto with mango and passion fruit, 310g",
					Description = "Experience the refreshing and exotic flavors of Tonitto Sorbet with Mango and Passion Fruit. This 310g tub of sorbet is a perfect blend of tropical mango and tangy passion fruit, delivering a deliciously smooth and refreshing treat. Made with real fruit and crafted to perfection, Tonitto Sorbet offers a guilt-free indulgence for any time of the day.",
					Price = 3.60m,
					CategoryId = 6,
					Discount = 0m,
					UnitMeasure = "tube",
				},

				new Product(15) {
					Name = "Raspberries Franui in white and milk chocolate, fresh frozen, 150g",
					Description = "Discover a luxurious treat with Franui Raspberries in White and Milk Chocolate. Each 150g pack features premium fresh-frozen raspberries, perfectly coated in a layer of smooth white chocolate followed by a rich milk chocolate shell. This decadent combination of tangy raspberries and creamy chocolate offers an exquisite taste sensation, perfect for satisfying your sweet cravings.",
					Price = 5.68m,
					CategoryId = 6,
					Discount = 0.35m,
					UnitMeasure = "unit",
				},

				new Product(16) {
					Name = "Liveg Veg Cutlet from tofu and seitan, 160g",
					Description = "Savor the wholesome goodness of Liveg Veg Cutlet made from tofu and seitan. Each 160g pack offers a delicious and nutritious plant-based alternative that's perfect for anyone looking to incorporate more vegetarian options into their diet. These cutlets are expertly crafted to deliver a satisfying texture and rich flavor, making them an ideal choice for a quick and healthy meal.",
					Price = 4.19m,
					CategoryId = 6,
					Discount = 0.14m,
					UnitMeasure = "unit",
				},

				new Product(17) {
					Name = "Ice cream \"Belgian chocolate\", 300g",
					Description = "Indulge in the rich and decadent flavor of \"Belgian Chocolate\" Ice Cream. This 300g tub offers a luxurious dessert experience, featuring velvety smooth ice cream made with the finest Belgian chocolate. Perfect for chocolate lovers, this ice cream delivers an intense, creamy, and unforgettable taste sensation that will satisfy your sweet tooth.",
					Price = 6.67m,
					CategoryId = 6,
					Discount = 0m,
					UnitMeasure = "tube",
				},

				new Product(18) {
					Name = "Otto Botti pitted black olives, 300 ml",
					Description = "Enhance your culinary creations with Otto Botti Pitted Black Olives. Each 300ml jar is filled with carefully selected, high-quality black olives that are pitted for your convenience. These olives are perfect for a wide range of dishes, from salads and pizzas to pasta and tapas, offering a rich and savory flavor that complements any meal.",
					Price = 1.36m,
					CategoryId = 7,
					Discount = 0m,
					UnitMeasure = "can",
				},

				new Product(19) {
					Name = "Whole peeled Fiorino tomatoes in tomato juice, 400g",
					Description = "Bring the authentic taste of Italy to your kitchen with Fiorino Whole Peeled Tomatoes in Tomato Juice. Each 400g can is packed with sun-ripened tomatoes, carefully peeled and preserved in rich tomato juice to retain their natural flavor and freshness. Perfect for a variety of dishes, these tomatoes are a versatile and essential ingredient for any home cook.",
					Price = 1.61m,
					CategoryId = 7,
					Discount = 0m,
					UnitMeasure = "can",
				},

				new Product(20) {
					Name = "Iberica black mini stoneless olives, 300g",
					Description = "Discover the exquisite taste of Iberica Black Mini Stoneless Olives, a gourmet delight straight from the Mediterranean. Sourced from the famed olive groves of the Iberian Peninsula, these mini olives are hand-selected for their exceptional quality and flavor. Packed without stones, they offer a convenient and mess-free way to enjoy the rich and robust taste of premium black olives.",
					Price = 2.60m,
					CategoryId = 7,
					Discount = 0m,
					UnitMeasure = "can",
				},

				new Product(21) {
					Name = "Sauce Barilla Pesto alla Genovese, 190g",
					Description = "Elevate your dishes with the authentic taste of Barilla Pesto alla Genovese. This 190g jar of premium pesto sauce is made with fresh basil, Parmigiano Reggiano cheese, and rich olive oil, delivering a vibrant and aromatic flavor. Perfect for pasta, sandwiches, or as a marinade, Barilla Pesto alla Genovese adds a delightful Italian touch to any meal.",
					Price = 3.05m,
					CategoryId = 9,
					Discount = 0m,
					UnitMeasure = "unit",
				},

				new Product(22) {
					Name = "Pasta Garofalo \"Penne Rigate\" No. 70 Bio, 500g",
					Description = "Experience the authentic taste of Italy with Garofalo \"Penne Rigate\" No. 70 Bio Pasta. Crafted with care using organic ingredients, this 500g pack of penne rigate pasta offers a perfect blend of tradition and quality. Made from the finest durum wheat semolina, each piece of pasta is bronze-extruded to create a rough texture that holds sauces beautifully, ensuring a delightful dining experience with every bite.",
					Price = 2.95m,
					CategoryId = 8,
					Discount = 0.3m,
					UnitMeasure = "unit",
				},

				new Product(23) {
					Name = "Pasta Riscossa \"Serpentine\" No. 51, 500g",
					Description = "Introducing Riscossa \"Serpentine\" No. 51 Pasta, a unique and versatile addition to your culinary repertoire. This 500g pack features serpentine-shaped pasta meticulously crafted to hold sauces and flavors in every twist and turn. Made from premium durum wheat semolina, Riscossa pasta undergoes a traditional bronze extrusion process, ensuring an authentic texture that pairs perfectly with a variety of sauces.",
					Price = 1.49m,
					CategoryId = 8,
					Discount = 0.2m,
					UnitMeasure = "unit",
				},

				new Product(24) {
					Name = "Pasta Garofalo \"Orzo\", 500g",
					Description = "Discover the delightful Garofalo \"Orzo\" Pasta, a versatile and elegant addition to your culinary creations. This 500g pack of orzo pasta offers a unique grain-like shape that resembles barley, making it perfect for a variety of dishes, from soups and salads to casseroles and pilafs. Crafted with the finest durum wheat semolina, Garofalo ensures superior quality and taste in every bite.",
					Price = 2.46m,
					CategoryId = 8,
					Discount = 0m,
					UnitMeasure = "unit",
				},

				new Product(25) {
					Name = "Ketchup Heinz Mild, 570g",
					Description = "Experience the classic taste of Heinz Mild Ketchup, a beloved condiment enjoyed by families around the world. This 570g bottle features Heinz's signature ketchup recipe, crafted with ripe tomatoes, a blend of spices, and a hint of sweetness. Perfectly balanced and irresistibly smooth, Heinz Mild Ketchup is a versatile addition to burgers, fries, sandwiches, and more.",
					Price = 4.32m,
					CategoryId = 9,
					Discount = 0m,
					UnitMeasure = "unit",
				},

				new Product(26) {
					Name = "Sauce Mr. Caramba \"Sweet chili\", 300g",
					Description = "Get ready to add a zing to your meals with Mr. Caramba's \"Sweet Chili\" Sauce. This 300g bottle packs a punch of flavor with its perfect blend of sweetness and heat. Ideal as a dipping sauce, marinade, or condiment, Mr. Caramba's Sweet Chili Sauce adds a delicious kick to everything from chicken wings to spring rolls.",
					Price = 5.43m,
					CategoryId = 9,
					Discount = 0m,
					UnitMeasure = "unit",
				},

				new Product(27) {
					Name = "Tahini Achva, 500g",
					Description = "Experience the authentic taste of Achva Tahini, a rich and creamy sesame paste that's perfect for adding depth and flavor to your favorite dishes. Made from carefully selected sesame seeds, this 500g jar of tahini offers a smooth texture and a nutty taste that pairs well with both sweet and savory recipes. Whether you're making hummus, salad dressings, or baked goods, Achva Tahini is a versatile pantry staple that will elevate your cooking to new heights.",
					Price = 5.68m,
					CategoryId = 9,
					Discount = 0m,
					UnitMeasure = "unit",
				},

				new Product(28) {
					Name = "Sauce Heinz Garlic, 400 ml",
					Description = "Elevate your dishes with the rich and flavorful Heinz Garlic Sauce. This 400ml bottle is packed with the irresistible taste of garlic, offering a perfect balance of savory goodness and aromatic notes. Whether you're dipping, drizzling, or cooking, Heinz Garlic Sauce adds a delicious twist to a variety of meals, from sandwiches and salads to meats and vegetables.",
					Price = 4.19m,
					CategoryId = 9,
					Discount = 0m,
					UnitMeasure = "unit",
				},

				new Product(29) {
					Name = "Energy drink Burn mango, 250 ml",
					Description = "Fuel your day with the refreshing and invigorating Burn Mango Energy Drink. This 250ml can bursts with the exotic flavor of ripe mango, combined with the powerful energy boost of Burn's signature blend. Perfect for those moments when you need a pick-me-up, Burn Mango Energy Drink delivers a tropical taste sensation that revives your senses and keeps you going.",
					Price = 1m,
					CategoryId = 10,
					Discount = 0m,
					UnitMeasure = "can",
				},

				new Product(30) {
					Name = "Energy drink Monster Energy The Doctor, 0.5l",
					Description = "Unleash the beast with Monster Energy \"The Doctor\" Drink in a 500ml can. This exhilarating beverage offers a fusion of tropical citrus flavors, delivering a bold and electrifying taste experience. Packed with Monster's signature energy blend, \"The Doctor\" provides the perfect kick to fuel your day, whether you're hitting the gym, studying for exams, or need an extra boost during a long day at work.",
					Price = 1.7m,
					CategoryId = 10,
					Discount = 0m,
					UnitMeasure = "can",
				},

				new Product(31) {
					Name = "Energy drink Monster Energy Monarch, 0.5l",
					Description = "Unleash your inner royalty with Monster Energy \"Monarch\" Drink, the ultimate companion for conquering your day. This electrifying beverage embodies power, vitality, and a taste fit for kings and queens. With a symphony of exhilarating flavors and a potent energy blend, \"Monarch\" reigns supreme among energy drinks.",
					Price = 2.2m,
					CategoryId = 10,
					Discount = 0.25m,
					UnitMeasure = "can",
				},
			};
		}

		public override Order[] GetOrderData()
		{
			return new Order[]
			{
				new Order(1)
				{
					UserId = 1,
					EmployeeId = 1,
					StatusId = 1,
					UserComment = "Comment1",
				},
				new Order(2)
				{
					UserId = 2,
					EmployeeId = 2,
					StatusId = 2,
					UserComment = "Comment2",
				},
				new Order(3)
				{
					UserId = 2,
					EmployeeId = 1,
					StatusId = 3,
					UserComment = "Comment3",
				},
			};
		}

		public override OrderDetail[] GetOrderDetailData()
		{
			return new OrderDetail[]
			{
				new OrderDetail(1) { OrderId = 1, ProductId = 1, Quantity = 2, UnitPrice = 12 },
				new OrderDetail(1) { OrderId = 1, ProductId = 6, Quantity = 8, UnitPrice = 2.3m },
				new OrderDetail(1) { OrderId = 1, ProductId = 24, Quantity = 3, UnitPrice = 1.7m },
				new OrderDetail(2) { OrderId = 2, ProductId = 4, Quantity = 1, UnitPrice = 2 },
				new OrderDetail(3) { OrderId = 2, ProductId = 2, Quantity = 5, UnitPrice = 2.3m },
				new OrderDetail(4) { OrderId = 3, ProductId = 1, Quantity = 1, UnitPrice = 5 },
			};
		}

		public override Status[] GetStatusData()
		{
			return new Status[]
			{
				new Status(1) { Name = "Waiting for approve" },
				new Status(2) { Name = "In process" },
				new Status(3) { Name = "Delivered" },
				new Status(4) { Name = "Canceled" },
			};
		}

		public override Cart[] GetCartData()
		{
			return new Cart[]
			{
				new Cart(1) { UserId = 1 },
				new Cart(2) { UserId = 2 },
				new Cart(3) { UserId = 3 },
				new Cart(4) { UserId = 4 },
				new Cart(5) { UserId = 5 },
			};
		}

		public override CartItem[] GetCartItemData()
		{
			return Array.Empty<CartItem>();
		}

		public override ProductImage[] GetProductImageData()
		{
			string? variable = Environment.GetEnvironmentVariable("IsDockerContainer");

			// Path for local 
			string seedDir = "..\\ProductStore.Data\\SeedImages\\";

			// Path for Docker container
			if (bool.TryParse(variable, out bool isDockerContainer) && isDockerContainer)
			{
				seedDir = "./ProductStore.Data/SeedImages/";
			}

			return new ProductImage[]
			{
				new ProductImage(1) { ProductId = 1, Image = File.ReadAllBytes($"{seedDir}{1}.webp") },
				//new ProductImage(2) { ProductId = 2, Image = File.ReadAllBytes($"{seedDir}{2}.webp") },
				//new ProductImage(3) { ProductId = 3, Image = File.ReadAllBytes($"{seedDir}{3}.webp") },
				//new ProductImage(4) { ProductId = 4, Image = File.ReadAllBytes($"{seedDir}{4}.webp") },
				//new ProductImage(5) { ProductId = 5, Image = File.ReadAllBytes($"{seedDir}{5}.webp") },
				//new ProductImage(6) { ProductId = 6, Image = File.ReadAllBytes($"{seedDir}{6}.webp") },
				//new ProductImage(7) { ProductId = 7, Image = File.ReadAllBytes($"{seedDir}{7}.webp") },
				//new ProductImage(8) { ProductId = 8, Image = File.ReadAllBytes($"{seedDir}{8}.webp") },
				//new ProductImage(9) { ProductId = 9, Image = File.ReadAllBytes($"{seedDir}{9}.webp") },
				//new ProductImage(10) { ProductId = 10, Image = File.ReadAllBytes($"{seedDir}{10}.webp") },
				//new ProductImage(11) { ProductId = 11, Image = File.ReadAllBytes($"{seedDir}{11}.webp") },
				//new ProductImage(12) { ProductId = 12, Image = File.ReadAllBytes($"{seedDir}{12}.webp") },
				//new ProductImage(13) { ProductId = 13, Image = File.ReadAllBytes($"{seedDir}{13}.webp") },
				//new ProductImage(14) { ProductId = 14, Image = File.ReadAllBytes($"{seedDir}{14}.webp") },
				//new ProductImage(15) { ProductId = 15, Image = File.ReadAllBytes($"{seedDir}{15}.webp") },
				//new ProductImage(16) { ProductId = 16, Image = File.ReadAllBytes($"{seedDir}{16}.webp") },
				//new ProductImage(17) { ProductId = 17, Image = File.ReadAllBytes($"{seedDir}{17}.webp") },
				//new ProductImage(18) { ProductId = 18, Image = File.ReadAllBytes($"{seedDir}{18}.webp") },
				//new ProductImage(19) { ProductId = 19, Image = File.ReadAllBytes($"{seedDir}{19}.webp") },
				//new ProductImage(20) { ProductId = 20, Image = File.ReadAllBytes($"{seedDir}{20}.webp") },
				//new ProductImage(21) { ProductId = 21, Image = File.ReadAllBytes($"{seedDir}{21}.webp") },
				//new ProductImage(22) { ProductId = 22, Image = File.ReadAllBytes($"{seedDir}{22}.webp") },
				//new ProductImage(23) { ProductId = 23, Image = File.ReadAllBytes($"{seedDir}{23}.webp") },
				//new ProductImage(24) { ProductId = 24, Image = File.ReadAllBytes($"{seedDir}{24}.webp") },
				//new ProductImage(25) { ProductId = 25, Image = File.ReadAllBytes($"{seedDir}{25}.webp") },
				//new ProductImage(26) { ProductId = 26, Image = File.ReadAllBytes($"{seedDir}{26}.webp") },
				//new ProductImage(27) { ProductId = 27, Image = File.ReadAllBytes($"{seedDir}{27}.webp") },
				//new ProductImage(28) { ProductId = 28, Image = File.ReadAllBytes($"{seedDir}{28}.webp") },
				//new ProductImage(29) { ProductId = 29, Image = File.ReadAllBytes($"{seedDir}{29}.webp") },
				//new ProductImage(30) { ProductId = 30, Image = File.ReadAllBytes($"{seedDir}{30}.webp") },
				//new ProductImage(31) { ProductId = 31, Image = File.ReadAllBytes($"{seedDir}{31}.webp") },
			};
		}

		public override object[] GetProductSpecificationData()
		{
			return new[]
			{
				// Manufacturers
				new { ProductsId = 1L, SpecificationsId = 9L },
				new { ProductsId = 2L, SpecificationsId = 10L },
				new { ProductsId = 3L, SpecificationsId = 11L },
				new { ProductsId = 4L, SpecificationsId = 12L },
				new { ProductsId = 5L, SpecificationsId = 13L },
				new { ProductsId = 6L, SpecificationsId = 14L },
				new { ProductsId = 7L, SpecificationsId = 15L },
				new { ProductsId = 8L, SpecificationsId = 9L },
				new { ProductsId = 9L, SpecificationsId = 10L },
				new { ProductsId = 10L, SpecificationsId = 11L },
				new { ProductsId = 11L, SpecificationsId = 12L },
				new { ProductsId = 12L, SpecificationsId = 13L },
				new { ProductsId = 13L, SpecificationsId = 14L },
				new { ProductsId = 14L, SpecificationsId = 15L },
				new { ProductsId = 15L, SpecificationsId = 9L },
				new { ProductsId = 16L, SpecificationsId = 10L },
				new { ProductsId = 17L, SpecificationsId = 11L },
				new { ProductsId = 18L, SpecificationsId = 12L },
				new { ProductsId = 19L, SpecificationsId = 13L },
				new { ProductsId = 20L, SpecificationsId = 14L },
				new { ProductsId = 21L, SpecificationsId = 15L },
				new { ProductsId = 22L, SpecificationsId = 9L },
				new { ProductsId = 23L, SpecificationsId = 10L },
				new { ProductsId = 24L, SpecificationsId = 11L },
				new { ProductsId = 25L, SpecificationsId = 12L },
				new { ProductsId = 26L, SpecificationsId = 13L },
				new { ProductsId = 27L, SpecificationsId = 14L },
				new { ProductsId = 28L, SpecificationsId = 15L },
				new { ProductsId = 29L, SpecificationsId = 13L },
				new { ProductsId = 30L, SpecificationsId = 14L },
				new { ProductsId = 31L, SpecificationsId = 15L },

				// Volume
				new { ProductsId = 1L, SpecificationsId = 4L },
				new { ProductsId = 2L, SpecificationsId = 3L },
				new { ProductsId = 3L, SpecificationsId = 6L },
				new { ProductsId = 7L, SpecificationsId = 3L },
				new { ProductsId = 8L, SpecificationsId = 6L },
				new { ProductsId = 9L, SpecificationsId = 6L },
				new { ProductsId = 29L, SpecificationsId = 6L },
				new { ProductsId = 30L, SpecificationsId = 5L },
				new { ProductsId = 31L, SpecificationsId = 5L },
				new { ProductsId = 25L, SpecificationsId = 5L },
				new { ProductsId = 26L, SpecificationsId = 6L },
				new { ProductsId = 27L, SpecificationsId = 4L },
				new { ProductsId = 28L, SpecificationsId = 4L },

				// Weight

				//new Specification(1) { Name = "Weight", Value = "1 kg" },
				//new Specification(2) { Name = "Weight", Value = "0.5 kg" },

				new { ProductsId = 4L, SpecificationsId = 2L },
				new { ProductsId = 5L, SpecificationsId = 2L },
				new { ProductsId = 6L, SpecificationsId = 2L },
				new { ProductsId = 10L, SpecificationsId = 2L },
				new { ProductsId = 11L, SpecificationsId = 2L },
				new { ProductsId = 12L, SpecificationsId = 2L },
				new { ProductsId = 13L, SpecificationsId = 1L },
				new { ProductsId = 14L, SpecificationsId = 2L },
				new { ProductsId = 15L, SpecificationsId = 2L },
				new { ProductsId = 16L, SpecificationsId = 2L },
				new { ProductsId = 17L, SpecificationsId = 2L },
				new { ProductsId = 18L, SpecificationsId = 2L },
				new { ProductsId = 19L, SpecificationsId = 2L },
				new { ProductsId = 20L, SpecificationsId = 2L },
				new { ProductsId = 21L, SpecificationsId = 2L },
				new { ProductsId = 22L, SpecificationsId = 2L },
				new { ProductsId = 23L, SpecificationsId = 2L },
				new { ProductsId = 24L, SpecificationsId = 2L },
			};
		}
	}
}
