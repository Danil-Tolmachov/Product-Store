using Microsoft.EntityFrameworkCore.Migrations;
using ProductStore.Data.Entities;



#nullable disable
#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProductStore.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(5,4)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Specifications",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specifications", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "smallmoney", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(5,4)", nullable: false),
                    UnitMeasure = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.id);
                    table.ForeignKey(
                        name: "FK_Contacts_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                    table.ForeignKey(
                        name: "FK_Users_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductImage",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    Image = table.Column<byte[]>(type: "image", nullable: false),
                    Alt = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImage", x => x.id);
                    table.ForeignKey(
                        name: "FK_ProductImage_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductSpecification",
                columns: table => new
                {
                    ProductsId = table.Column<long>(type: "bigint", nullable: false),
                    SpecificationsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSpecification", x => new { x.ProductsId, x.SpecificationsId });
                    table.ForeignKey(
                        name: "FK_ProductSpecification_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductSpecification_Specifications_SpecificationsId",
                        column: x => x.SpecificationsId,
                        principalTable: "Specifications",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.id);
                    table.ForeignKey(
                        name: "FK_Carts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    PositionId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.id);
                    table.ForeignKey(
                        name: "FK_Employees_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    CartId = table.Column<long>(type: "bigint", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => new { x.ProductId, x.CartId });
                    table.ForeignKey(
                        name: "FK_CartItems_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    EmployeeId = table.Column<long>(type: "bigint", nullable: false),
                    StatusId = table.Column<long>(type: "bigint", nullable: false),
                    UserComment = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.id);
                    table.ForeignKey(
                        name: "FK_Orders_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "smallmoney", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => new { x.ProductId, x.OrderId });
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "id", "Name" },
                values: new object[,]
                {
                    { 1L, "Alcohol" },
                    { 2L, "Bakery" },
                    { 3L, "Meat" },
                    { 4L, "Beverages" },
                    { 5L, "Snacks" },
                    { 6L, "Frozen Foods" },
                    { 7L, "Canned" },
                    { 8L, "Pasta" },
                    { 9L, "Sauces" },
                    { 10L, "Energy Drinks" }
                });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "id", "Address", "Discount", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1L, "New York, 123 Broadway St", 0.1m, "John", "Smith" },
                    { 2L, "Los Angeles, 456 Sunset Blvd", 0.15m, "Emily", "Johnson" },
                    { 3L, "Houston, 1011 Main St", 0.05m, "Michael", "Brown" },
                    { 4L, "Phoenix, 1213 Central Ave", 0.05m, "Jessica", "Davis" },
                    { 5L, "Philadelphia, 1415 Walnut St", 0.2m, "David", "Wilson" }
                });

            migrationBuilder.InsertData(
                table: "Positions",
                columns: new[] { "id", "Name" },
                values: new object[,]
                {
                    { 1L, "Administrator" },
                    { 2L, "Manager" },
                    { 3L, "Helper" }
                });

            migrationBuilder.InsertData(
                table: "Specifications",
                columns: new[] { "id", "Name", "Value" },
                values: new object[,]
                {
                    { 1L, "Weight", "1 kg" },
                    { 2L, "Weight", "0.5 kg" },
                    { 3L, "Volume", "1 l" },
                    { 4L, "Volume", "0.75 l" },
                    { 5L, "Volume", "0.25 l" },
                    { 6L, "Volume", "0.5 l" },
                    { 7L, "Weight", "1 kg" },
                    { 8L, "Size", "15x2x3" },
                    { 9L, "Manufacturer", "United States" },
                    { 10L, "Manufacturer", "Canada" },
                    { 11L, "Manufacturer", "Japan" },
                    { 12L, "Manufacturer", "United Kingdom" },
                    { 13L, "Manufacturer", "Germany" },
                    { 14L, "Manufacturer", "France" },
                    { 15L, "Manufacturer", "Brazil" }
                });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "id", "Name" },
                values: new object[,]
                {
                    { 1L, "Waiting for approve" },
                    { 2L, "In process" },
                    { 3L, "Delivered" },
                    { 4L, "Canceled" }
                });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "id", "Name", "PersonId", "Value" },
                values: new object[,]
                {
                    { 1L, "Email", 1L, "email1@main.com" },
                    { 2L, "Phone", 1L, "+380111111111" },
                    { 3L, "Phone", 2L, "+380222222222" },
                    { 4L, "Email", 3L, "email2@main.com" },
                    { 5L, "Viber", 4L, "+380333333333" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "id", "CategoryId", "Description", "Discount", "Name", "Price", "UnitMeasure" },
                values: new object[,]
                {
                    { 1L, 1L, "Martini Brut is a premium sparkling wine that boasts an alcohol content of 11.5%. Known for its fine bubbles and balanced flavor profile, this wine is perfect for celebratory occasions or simply enjoying a glass with friends. The 0.75 liter bottle is ideal for sharing and ensures you have enough to go around. Martini Brut combines elegance and a refreshing taste, making it a versatile choice for any event.", 0.35m, "Martini Brut 11,5%, 0,75l", 13.37m, "bottle" },
                    { 2L, 1L, "Hankey Bannister is a renowned blended Scotch whisky known for its smooth and balanced flavor profile. This 1-liter bottle of Hankey Bannister offers ample supply for your enjoyment or for sharing with friends and family. Crafted with a blend of carefully selected malt and grain whiskies, Hankey Bannister delivers a rich and satisfying taste experience with every sip. Whether enjoyed neat, on the rocks, or as part of your favorite cocktail, Hankey Bannister promises a delightful whisky experience.", 0.6m, "Hankey Bannister, 1l", 24.78m, "bottle" },
                    { 3L, 1L, "Corona Extra is a popular Mexican beer known for its refreshing taste and iconic presentation. Brewed since 1925, this pale lager has become a symbol of relaxation and enjoyment worldwide. With its light and crisp flavor profile, Corona Extra is perfect for any occasion, whether you're lounging on the beach, hosting a barbecue, or simply unwinding after a long day. Enjoyed best when served ice-cold with a wedge of lime, Corona Extra delivers a smooth and satisfying drinking experience that pairs effortlessly with sunny days and good company.", 0.37m, "Corona Extra, 0,33l", 1.54m, "bottle" },
                    { 4L, 2L, "Indulge in the timeless simplicity of the classic ham sandwich. Made with thinly sliced ham nestled between two slices of your favorite bread, this sandwich is a satisfying combination of flavors and textures. Whether you're packing a lunch, need a quick bite, or craving a comforting snack, the classic ham sandwich is always a delicious choice. Add your favorite condiments and toppings like lettuce, tomato, cheese, or mustard to customize it to your taste preferences. With its savory ham and soft bread, this sandwich is a timeless favorite that never fails to hit the spot.", 0.2m, "Sandwich with ham, 200g", 1.44m, "unit" },
                    { 5L, 2L, "Savor the delightful combination of sweet cherries nestled within a soft, fluffy bun with our Cherry Bun Delight. Perfect for breakfast, dessert, or a delightful snack, this treat offers a burst of fruity flavor in every bite. Each bun is lovingly filled with succulent cherries, creating a harmonious blend of tartness and sweetness that tantalizes the taste buds. Whether enjoyed with a cup of coffee in the morning or as a delightful afternoon treat, our Cherry Bun Delight is sure to brighten your day with its irresistible flavor and comforting texture.", 0.2m, "Bun with cherry", 1.36m, "unit" },
                    { 6L, 3L, "Indulge in the exquisite taste and tender texture of our Beef Steak Wet Aged, expertly crafted to perfection. Each succulent bite promises a symphony of flavors, from the rich umami of premium beef to the subtle hints of natural juices that enhance every mouthful. Wet aging ensures optimal tenderness and juiciness, allowing the meat to reach its peak flavor potential. Sourced from the finest cuts of beef, our steak is hand-selected for its marbling and quality, guaranteeing a dining experience that surpasses expectations. Whether seared to perfection on the grill or pan-fried to your desired level of doneness, our Beef Steak Wet Aged is a culinary masterpiece that elevates any meal.", 0.1m, "Beef steak Wet Aged, 100g", 12.90m, "unit" },
                    { 7L, 4L, "Description6", 0m, "Schweppes pomegranate, 1l", 1.15m, "bottle" },
                    { 8L, 4L, "Indulge in the bold and refreshing flavor of Dr. Pepper Cherry, a delightful twist on the classic soda favorite. Bursting with the essence of ripe cherries combined with the signature blend of 23 flavors found in Dr. Pepper, each sip delivers a symphony of taste sensations that tantalize the palate. Whether enjoyed on its own over ice or used as a mixer in your favorite cocktails, Dr. Pepper Cherry is the perfect beverage choice for any occasion. With its distinctive cherry-infused aroma and satisfyingly fizzy texture, this iconic soda will leave you craving more with every sip.", 0.19m, "Dr.Pepper Cherry, 0,33l", 0.92m, "can" },
                    { 9L, 4L, "Embark on an exhilarating journey of citrusy refreshment with Mountain Dew, the iconic soft drink that energizes and invigorates with every sip. Bursting with bold flavors and a vibrant neon-green hue, Mountain Dew is a beloved beverage that ignites the senses and fuels adventure. With its unique blend of citrus flavors and a touch of sweetness, this fizzy drink delivers an unmistakable taste that's as exhilarating as scaling a mountain peak. Whether you're tackling the great outdoors or simply kicking back with friends, Mountain Dew is the perfect companion for those who thirst for excitement.", 0.22m, "Mountain Dew, 0,33l", 1.26m, "can" },
                    { 10L, 5L, "Indulge in the irresistible crunch and savory taste of Pringles Sour Cream & Onion Flavor. Each bite-sized crisp is meticulously seasoned with a delectable blend of tangy sour cream and zesty onion flavors, creating a mouthwatering snack sensation that's hard to resist. Whether you're snacking solo, sharing with friends, or enjoying a movie night at home, Pringles Sour Cream & Onion Flavor offers the perfect combination of flavor and crunch to satisfy your snack cravings. Packed in a convenient resealable tube, these Pringles are ideal for on-the-go munching or as a flavorful addition to any party spread.", 0m, "Pringles with sour cream and onion flavor, 165g", 3.60m, "unit" },
                    { 11L, 5L, "Indulge in the unmatched crispiness and flavorful taste of Pringles chips with chili-lime flavor. Each crispy chip is infused with the bold flavor of spicy chili and refreshing lime, creating an incredibly appetizing sensation that's hard to resist. Whether you're snacking alone or sharing with friends, Pringles with chili-lime flavor are sure to satisfy your taste buds with every bite.", 0m, "Pringles with hot chili-lime flavor, 158g", 3.60m, "unit" },
                    { 12L, 5L, "Enjoy the delightful taste of Pringles chips with cheese flavor. Each crispy chip is bursting with rich and savory cheese flavor that will tantalize your taste buds with every bite. Perfect for snacking on the go or sharing with friends, Pringles cheese flavor chips are a delicious and satisfying treat for any occasion.", 0m, "Pringles cheese, 165g", 3.60m, "unit" },
                    { 13L, 6L, "Indulge in the delightful taste of Schulstad Croissants, made with traditional recipes and the finest ingredients. Each 480g pack contains perfectly baked, flaky croissants that are golden on the outside and soft on the inside. These croissants are ideal for breakfast, a quick snack, or as an accompaniment to your favorite meals.", 0.2m, "Croissants Shulstad, 480 g", 6.230m, "unit" },
                    { 14L, 6L, "Experience the refreshing and exotic flavors of Tonitto Sorbet with Mango and Passion Fruit. This 310g tub of sorbet is a perfect blend of tropical mango and tangy passion fruit, delivering a deliciously smooth and refreshing treat. Made with real fruit and crafted to perfection, Tonitto Sorbet offers a guilt-free indulgence for any time of the day.", 0m, "Sorbet Tonitto with mango and passion fruit, 310g", 3.60m, "tube" },
                    { 15L, 6L, "Discover a luxurious treat with Franui Raspberries in White and Milk Chocolate. Each 150g pack features premium fresh-frozen raspberries, perfectly coated in a layer of smooth white chocolate followed by a rich milk chocolate shell. This decadent combination of tangy raspberries and creamy chocolate offers an exquisite taste sensation, perfect for satisfying your sweet cravings.", 0.35m, "Raspberries Franui in white and milk chocolate, fresh frozen, 150g", 5.68m, "unit" },
                    { 16L, 6L, "Savor the wholesome goodness of Liveg Veg Cutlet made from tofu and seitan. Each 160g pack offers a delicious and nutritious plant-based alternative that's perfect for anyone looking to incorporate more vegetarian options into their diet. These cutlets are expertly crafted to deliver a satisfying texture and rich flavor, making them an ideal choice for a quick and healthy meal.", 0.14m, "Liveg Veg Cutlet from tofu and seitan, 160g", 4.19m, "unit" },
                    { 17L, 6L, "Indulge in the rich and decadent flavor of \"Belgian Chocolate\" Ice Cream. This 300g tub offers a luxurious dessert experience, featuring velvety smooth ice cream made with the finest Belgian chocolate. Perfect for chocolate lovers, this ice cream delivers an intense, creamy, and unforgettable taste sensation that will satisfy your sweet tooth.", 0m, "Ice cream \"Belgian chocolate\", 300g", 6.67m, "tube" },
                    { 18L, 7L, "Enhance your culinary creations with Otto Botti Pitted Black Olives. Each 300ml jar is filled with carefully selected, high-quality black olives that are pitted for your convenience. These olives are perfect for a wide range of dishes, from salads and pizzas to pasta and tapas, offering a rich and savory flavor that complements any meal.", 0m, "Otto Botti pitted black olives, 300 ml", 1.36m, "can" },
                    { 19L, 7L, "Bring the authentic taste of Italy to your kitchen with Fiorino Whole Peeled Tomatoes in Tomato Juice. Each 400g can is packed with sun-ripened tomatoes, carefully peeled and preserved in rich tomato juice to retain their natural flavor and freshness. Perfect for a variety of dishes, these tomatoes are a versatile and essential ingredient for any home cook.", 0m, "Whole peeled Fiorino tomatoes in tomato juice, 400g", 1.61m, "can" },
                    { 20L, 7L, "Discover the exquisite taste of Iberica Black Mini Stoneless Olives, a gourmet delight straight from the Mediterranean. Sourced from the famed olive groves of the Iberian Peninsula, these mini olives are hand-selected for their exceptional quality and flavor. Packed without stones, they offer a convenient and mess-free way to enjoy the rich and robust taste of premium black olives.", 0m, "Iberica black mini stoneless olives, 300g", 2.60m, "can" },
                    { 21L, 9L, "Elevate your dishes with the authentic taste of Barilla Pesto alla Genovese. This 190g jar of premium pesto sauce is made with fresh basil, Parmigiano Reggiano cheese, and rich olive oil, delivering a vibrant and aromatic flavor. Perfect for pasta, sandwiches, or as a marinade, Barilla Pesto alla Genovese adds a delightful Italian touch to any meal.", 0m, "Sauce Barilla Pesto alla Genovese, 190g", 3.05m, "unit" },
                    { 22L, 8L, "Experience the authentic taste of Italy with Garofalo \"Penne Rigate\" No. 70 Bio Pasta. Crafted with care using organic ingredients, this 500g pack of penne rigate pasta offers a perfect blend of tradition and quality. Made from the finest durum wheat semolina, each piece of pasta is bronze-extruded to create a rough texture that holds sauces beautifully, ensuring a delightful dining experience with every bite.", 0.3m, "Pasta Garofalo \"Penne Rigate\" No. 70 Bio, 500g", 2.95m, "unit" },
                    { 23L, 8L, "Introducing Riscossa \"Serpentine\" No. 51 Pasta, a unique and versatile addition to your culinary repertoire. This 500g pack features serpentine-shaped pasta meticulously crafted to hold sauces and flavors in every twist and turn. Made from premium durum wheat semolina, Riscossa pasta undergoes a traditional bronze extrusion process, ensuring an authentic texture that pairs perfectly with a variety of sauces.", 0.2m, "Pasta Riscossa \"Serpentine\" No. 51, 500g", 1.49m, "unit" },
                    { 24L, 8L, "Discover the delightful Garofalo \"Orzo\" Pasta, a versatile and elegant addition to your culinary creations. This 500g pack of orzo pasta offers a unique grain-like shape that resembles barley, making it perfect for a variety of dishes, from soups and salads to casseroles and pilafs. Crafted with the finest durum wheat semolina, Garofalo ensures superior quality and taste in every bite.", 0m, "Pasta Garofalo \"Orzo\", 500g", 2.46m, "unit" },
                    { 25L, 9L, "Experience the classic taste of Heinz Mild Ketchup, a beloved condiment enjoyed by families around the world. This 570g bottle features Heinz's signature ketchup recipe, crafted with ripe tomatoes, a blend of spices, and a hint of sweetness. Perfectly balanced and irresistibly smooth, Heinz Mild Ketchup is a versatile addition to burgers, fries, sandwiches, and more.", 0m, "Ketchup Heinz Mild, 570g", 4.32m, "unit" },
                    { 26L, 9L, "Get ready to add a zing to your meals with Mr. Caramba's \"Sweet Chili\" Sauce. This 300g bottle packs a punch of flavor with its perfect blend of sweetness and heat. Ideal as a dipping sauce, marinade, or condiment, Mr. Caramba's Sweet Chili Sauce adds a delicious kick to everything from chicken wings to spring rolls.", 0m, "Sauce Mr. Caramba \"Sweet chili\", 300g", 5.43m, "unit" },
                    { 27L, 9L, "Experience the authentic taste of Achva Tahini, a rich and creamy sesame paste that's perfect for adding depth and flavor to your favorite dishes. Made from carefully selected sesame seeds, this 500g jar of tahini offers a smooth texture and a nutty taste that pairs well with both sweet and savory recipes. Whether you're making hummus, salad dressings, or baked goods, Achva Tahini is a versatile pantry staple that will elevate your cooking to new heights.", 0m, "Tahini Achva, 500g", 5.68m, "unit" },
                    { 28L, 9L, "Elevate your dishes with the rich and flavorful Heinz Garlic Sauce. This 400ml bottle is packed with the irresistible taste of garlic, offering a perfect balance of savory goodness and aromatic notes. Whether you're dipping, drizzling, or cooking, Heinz Garlic Sauce adds a delicious twist to a variety of meals, from sandwiches and salads to meats and vegetables.", 0m, "Sauce Heinz Garlic, 400 ml", 4.19m, "unit" },
                    { 29L, 10L, "Fuel your day with the refreshing and invigorating Burn Mango Energy Drink. This 250ml can bursts with the exotic flavor of ripe mango, combined with the powerful energy boost of Burn's signature blend. Perfect for those moments when you need a pick-me-up, Burn Mango Energy Drink delivers a tropical taste sensation that revives your senses and keeps you going.", 0m, "Energy drink Burn mango, 250 ml", 1m, "can" },
                    { 30L, 10L, "Unleash the beast with Monster Energy \"The Doctor\" Drink in a 500ml can. This exhilarating beverage offers a fusion of tropical citrus flavors, delivering a bold and electrifying taste experience. Packed with Monster's signature energy blend, \"The Doctor\" provides the perfect kick to fuel your day, whether you're hitting the gym, studying for exams, or need an extra boost during a long day at work.", 0m, "Energy drink Monster Energy The Doctor, 0.5l", 1.7m, "can" },
                    { 31L, 10L, "Unleash your inner royalty with Monster Energy \"Monarch\" Drink, the ultimate companion for conquering your day. This electrifying beverage embodies power, vitality, and a taste fit for kings and queens. With a symphony of exhilarating flavors and a potent energy blend, \"Monarch\" reigns supreme among energy drinks.", 0.25m, "Energy drink Monster Energy Monarch, 0.5l", 2.2m, "can" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "IsActive", "Password", "PersonId", "RefreshToken", "Username" },
                values: new object[,]
                {
                    { 1L, true, "$2a$11$QxP6GG54zUOzEhLs2CORNuNOfokAkM09Op5MFN0oLkTeX3rS/vjsO", 1L, "", "johnsmith" },
                    { 2L, true, "$2a$11$nVp1qAK9YwOi5LNpFAXfR.SK07.DRBt4KnvIfdpOnahEQO6wJouOy", 2L, "", "emilyjohnson" },
                    { 3L, true, "$2a$11$yD69ysc/wPKgIWpLJq2HjOLfDRPymOhlz8jRVoj4sv57L9G.jjSsq", 3L, "", "michaelbrown" },
                    { 4L, true, "$2a$11$sj9d8CY2L0ozSoQfdvce5.TpCiI.dr6iejTOmWBAE1xqJ9rj/wRw6", 4L, "", "jessicadavis" },
                    { 5L, false, "$2a$11$n/bFeF3Xh.QHYwQ1RL6GyuvNe.tytw2NnWlQA0RSEVe6cMFbXRQeu", 5L, "", "davidwilson" }
                });

            migrationBuilder.InsertData(
                table: "Carts",
                columns: new[] { "id", "UserId" },
                values: new object[,]
                {
                    { 1L, 1L },
                    { 2L, 2L },
                    { 3L, 3L },
                    { 4L, 4L },
                    { 5L, 5L }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "id", "PositionId", "UserId" },
                values: new object[,]
                {
                    { 1L, 1L, 4L },
                    { 2L, 2L, 3L }
                });

			string variable = Environment.GetEnvironmentVariable("IsDockerContainer") ?? string.Empty;

			// Path for local 
			string seedDir = "..\\ProductStore.Data\\SeedImages\\";

			// Path for Docker container
			if (bool.TryParse(variable, out bool isDockerContainer) && isDockerContainer)
			{
				seedDir = "./ProductStore.Data/SeedImages/";
			}

            migrationBuilder.InsertData(
                table: "ProductImage",
                columns: new[] { "id", "Alt", "Image", "ProductId" },
                values: new object[,]
                {
                    { 1L, null, File.ReadAllBytes($"{seedDir}{1}.webp"), 1L},
                    { 2L, null, File.ReadAllBytes($"{seedDir}{2}.webp"), 2L},
                    { 3L, null, File.ReadAllBytes($"{seedDir}{3}.webp"), 3L},
                    { 4L, null, File.ReadAllBytes($"{seedDir}{4}.webp"), 4L},
                    { 5L, null, File.ReadAllBytes($"{seedDir}{5}.webp"), 5L},
                    { 6L, null, File.ReadAllBytes($"{seedDir}{6}.webp"), 6L},
                    { 7L, null, File.ReadAllBytes($"{seedDir}{7}.webp"), 7L},
                    { 8L, null, File.ReadAllBytes($"{seedDir}{8}.webp"), 8L},
                    { 9L, null, File.ReadAllBytes($"{seedDir}{9}.webp"), 9L},
                    { 10L, null, File.ReadAllBytes($"{seedDir}{10}.webp"), 10L},
                    { 11L, null, File.ReadAllBytes($"{seedDir}{11}.webp"), 11L},
                    { 12L, null, File.ReadAllBytes($"{seedDir}{12}.webp"), 12L},
                    { 13L, null, File.ReadAllBytes($"{seedDir}{13}.webp"), 13L},
                    { 14L, null, File.ReadAllBytes($"{seedDir}{14}.webp"), 14L},
                    { 15L, null, File.ReadAllBytes($"{seedDir}{15}.webp"), 15L},
                    { 16L, null, File.ReadAllBytes($"{seedDir}{16}.webp"), 16L},
                    { 17L, null, File.ReadAllBytes($"{seedDir}{17}.webp"), 17L},
                    { 18L, null, File.ReadAllBytes($"{seedDir}{18}.webp"), 18L},
                    { 19L, null, File.ReadAllBytes($"{seedDir}{19}.webp"), 19L},
                    { 20L, null, File.ReadAllBytes($"{seedDir}{20}.webp"), 20L},
                    { 21L, null, File.ReadAllBytes($"{seedDir}{21}.webp"), 21L},
                    { 22L, null, File.ReadAllBytes($"{seedDir}{22}.webp"), 22L},
                    { 23L, null, File.ReadAllBytes($"{seedDir}{23}.webp"), 23L},
                    { 24L, null, File.ReadAllBytes($"{seedDir}{24}.webp"), 24L},
                    { 25L, null, File.ReadAllBytes($"{seedDir}{25}.webp"), 25L},
                    { 26L, null, File.ReadAllBytes($"{seedDir}{26}.webp"), 26L},
                    { 27L, null, File.ReadAllBytes($"{seedDir}{27}.webp"), 27L},
                    { 28L, null, File.ReadAllBytes($"{seedDir}{28}.webp"), 28L},
                    { 29L, null, File.ReadAllBytes($"{seedDir}{29}.webp"), 29L},
                    { 30L, null, File.ReadAllBytes($"{seedDir}{30}.webp"), 30L},
                    { 31L, null, File.ReadAllBytes($"{seedDir}{31}.webp"), 31L},
                });


			migrationBuilder.InsertData(
                table: "ProductSpecification",
                columns: new[] { "ProductsId", "SpecificationsId" },
                values: new object[,]
                {
                    { 1L, 4L },
                    { 1L, 9L },
                    { 2L, 3L },
                    { 2L, 10L },
                    { 3L, 6L },
                    { 3L, 11L },
                    { 4L, 2L },
                    { 4L, 12L },
                    { 5L, 2L },
                    { 5L, 13L },
                    { 6L, 2L },
                    { 6L, 14L },
                    { 7L, 3L },
                    { 7L, 15L },
                    { 8L, 6L },
                    { 8L, 9L },
                    { 9L, 6L },
                    { 9L, 10L },
                    { 10L, 2L },
                    { 10L, 11L },
                    { 11L, 2L },
                    { 11L, 12L },
                    { 12L, 2L },
                    { 12L, 13L },
                    { 13L, 1L },
                    { 13L, 14L },
                    { 14L, 2L },
                    { 14L, 15L },
                    { 15L, 2L },
                    { 15L, 9L },
                    { 16L, 2L },
                    { 16L, 10L },
                    { 17L, 2L },
                    { 17L, 11L },
                    { 18L, 2L },
                    { 18L, 12L },
                    { 19L, 2L },
                    { 19L, 13L },
                    { 20L, 2L },
                    { 20L, 14L },
                    { 21L, 2L },
                    { 21L, 15L },
                    { 22L, 2L },
                    { 22L, 9L },
                    { 23L, 2L },
                    { 23L, 10L },
                    { 24L, 2L },
                    { 24L, 11L },
                    { 25L, 5L },
                    { 25L, 12L },
                    { 26L, 6L },
                    { 26L, 13L },
                    { 27L, 4L },
                    { 27L, 14L },
                    { 28L, 4L },
                    { 28L, 15L },
                    { 29L, 6L },
                    { 29L, 13L },
                    { 30L, 5L },
                    { 30L, 14L },
                    { 31L, 5L },
                    { 31L, 15L }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "id", "EmployeeId", "StatusId", "UserComment", "UserId" },
                values: new object[,]
                {
                    { 1L, 1L, 1L, "Comment1", 1L },
                    { 2L, 2L, 2L, "Comment2", 2L },
                    { 3L, 1L, 3L, "Comment3", 2L }
                });

            migrationBuilder.InsertData(
                table: "OrderDetails",
                columns: new[] { "OrderId", "ProductId", "id", "Quantity", "UnitPrice" },
                values: new object[,]
                {
                    { 1L, 1L, 1L, 2, 12m },
                    { 3L, 1L, 4L, 1, 5m },
                    { 2L, 2L, 3L, 5, 2.3m },
                    { 2L, 4L, 2L, 1, 2m },
                    { 1L, 6L, 1L, 8, 2.3m },
                    { 1L, 24L, 1L, 3, 1.7m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartId",
                table: "CartItems",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_UserId",
                table: "Carts",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_PersonId",
                table: "Contacts",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PositionId",
                table: "Employees",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_UserId",
                table: "Employees",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrderDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_EmployeeId",
                table: "Orders",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_StatusId",
                table: "Orders",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImage_ProductId",
                table: "ProductImage",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSpecification_SpecificationsId",
                table: "ProductSpecification",
                column: "SpecificationsId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PersonId",
                table: "Users",
                column: "PersonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "ProductImage");

            migrationBuilder.DropTable(
                name: "ProductSpecification");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Specifications");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Persons");
        }
    }
}
