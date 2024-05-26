# <p align="center">API Documentation</p>

## Auth Endpoints

| Endpoint              | Method | Params          | Description                                             | Responses                                                     |
| --------------------- | ------ | --------------- | ------------------------------------------------------- | ------------------------------------------------------------- |
| /api/v1/auth/         | GET    |                 | Check if user is authorized                             | 200: Success(string)<br>401: Unauthorized                     |
| /api/v1/auth/user     | GET    |                 | Retrieves the current authenticated user's information. | 200: Success(UserDto)<br>401: Unauthorized                    |
| /api/v1/auth/login    | POST   | LoginModel      | Allows a user to log in with their credentials.         | 200: Success(TokensDto)<br>401: Unauthorized                  |
| /api/v1/auth/refresh  | POST   | RefreshModel    | Refreshes the authentication token.                     | 200: Success(TokensDto)<br>401: Unauthorized                  |
| /api/v1/auth/register | POST   | RegisterModel   | Registers a new user.                                   | 200: Success(string)<br>400: Bad Request                      |
| /api/v1/auth/update   | PUT    | UpdateUserModel | Updates the user's information.                         | 200: Success(string)<br>400: Bad Request<br>401: Unauthorized |

## Category Endpoints

| Endpoint              | Method | Description                              | Responses                                   |
| --------------------- | ------ | ---------------------------------------- | ------------------------------------------- |
| /api/v1/category      | GET    | Retrieves all categories.                | 200: Success(IEnumerable<CategoryDto>)      |
| /api/v1/category/{id} | GET    | Retrieves a specific category by its ID. | 200: Success(CategoryDto)<br>404: Not Found |

## Product Endpoints

| Endpoint                      | Method | Params                                         | Description                                                                                                 | Responses                                                     |
| ----------------------------- | ------ | ---------------------------------------------- | ----------------------------------------------------------------------------------------------------------- | ------------------------------------------------------------- |
| /api/v1/product               | GET    | [FromQuery] page: int?, [FromQuery] count: int | Retrieves products page. By default contains all products on one page.                                      | 200: Success(IEnumerable<ProductPageDto>)                     |
| /api/v1/product/{id}          | GET    | id: long                                       | Retrieves a specific product by its ID.                                                                     | 200: Success(ProductDto)<br>404: Not Found                    |
| /api/v1/product/category/{id} | POST   | [FromQuery] page: int?, [FromQuery] count: int | Retrieves products page with products from specific category. By default contains all products on one page. | 200: Success(IEnumerable<ProductPageDto>)<br>400: Bad Request |

## Order Endpoints [Authorize]

| Endpoint             | Method | Params           | Description                                      | Responses                                                     |
| -------------------- | ------ | ---------------- | ------------------------------------------------ | ------------------------------------------------------------- |
| /api/v1/order        | GET    |                  | Retrieves all orders for the authenticated user. | 200: Success(IEnumerable<OrderDto>)<br>401: Unauthorized      |
| /api/v1/order/{id}   | GET    | id: long         | Retrieves a specific order by its ID.            | 200: Success(OrderDto)<br>404: Not Found<br>401: Unauthorized |
| /api/v1/order        | POST   | SubmitCartModel  | Creates a new order from cart items.             | 201: Created<br>400: Bad Request<br>401: Unauthorized         |
| /api/v1/order/cancel | post   | CancelOrderModel | Cancels user order.                              | 200: Success<br>400: Bad Request<br>401: Unauthorized         |

## Cart Endpoints [Authorize]

| Endpoint          | Method | Params           | Description                        | Responses                                                     |
| ----------------- | ------ | ---------------- | ---------------------------------- | ------------------------------------------------------------- |
| /api/v1/cart      | GET    |                  | Retrieves the current user's cart. | 200: Success(CartDto)<br>401: Unauthorized                    |
| /api/v1/cart      | POST   | AddCartItemModel | Adds an item to the cart.          | 201: Created<br>400: Bad Request(string)<br>401: Unauthorized |
| /api/v1/cart      | DELETE |                  | Clear user cart.                   | 200: Success<br>401: Unauthorized                             |
| /api/v1/cart/{id} | DELETE | id: long         | Removes an item from the cart.     | 200: Success<br>400: Bad Request(string)<br>401: Unauthorized |

## Image Endpoints

| Endpoint                   | Method | Params     | Description                              | Responses                               |
| -------------------------- | ------ | ---------- | ---------------------------------------- | --------------------------------------- |
| /api/v1/image/product/{id} | GET    | id: string | Gets an image by its ID(Base64 encoded). | 200: Success(bytes[])<br>404: Not Found |

# <p align="center">Schemas</p>

### Dtos

1. UserDto

| Property  | Type                       | Description                        |
| --------- | -------------------------- | ---------------------------------- |
| id        | long                       | Unique identifier for the user.    |
| username  | string                     | Username of the user.              |
| firstName | string                     | First name of the user.            |
| lastName  | string                     | Last name of the user.             |
| discount  | string                     | Discount associated with the user. |
| address   | string                     | Address of the user.               |
| cart      | CartDto                    | User's shopping cart.              |
| contacts  | IEnumerable<ContactDto>    | List of user's contacts.           |
| orders    | IEnumerable<OrderBriefDto> | List of user's orders.             |

2. CartDto

| Property | Type                     | Description                                    |
| -------- | ------------------------ | ---------------------------------------------- |
| items    | IEnumerable<CartItemDto> | Array of CartItemDto Items in the user's cart. |

3. CartItemDto

| Property | Type       | Description               |
| -------- | ---------- | ------------------------- |
| product  | ProductDto | The product in the cart.  |
| quantity | int        | Quantity of the products. |

4. ContactDto

| Property | Type   | Description                           |
| -------- | ------ | ------------------------------------- |
| type     | string | Type of contact (e.g., email, phone). |
| value    | string | Contact value.                        |

5. OrderBriefDto

| Property    | Type   | Description                            |
| ----------- | ------ | -------------------------------------- |
| id          | int    | Unique identifier for the order.       |
| employeeId  | long   | ID of the employee handling the order. |
| userComment | string | User's comments about the order.       |
| status      | string | Current status of the order.           |

6. OrderDetailDto

| Property  | Type       | Description                      |
| --------- | ---------- | -------------------------------- |
| quantity  | int        | Quantity of the product ordered. |
| unitPrice | decimal    | Unit price of the product.       |
| product   | ProductDto | The product details.             |

7. OrderDto

| Property    | Type                        | Description                            |
| ----------- | --------------------------- | -------------------------------------- |
| id          | int                         | Unique identifier for the order.       |
| employeeId  | long                        | ID of the employee handling the order. |
| total       | decimal                     | Total amount of the order.             |
| userComment | string                      | User's comments about the order.       |
| status      | string                      | Current status of the order.           |
| isCanceled  | boolean                     | Indicates if the order is canceled.    |
| isCompleted | boolean                     | Indicates if the order is completed.   |
| details     | IEnumerable<OrderDetailDto> | Detailed items in the order.           |

8. ProductDto

| Property       | Type                          | Description                        |
| -------------- | ----------------------------- | ---------------------------------- |
| id             | long                          | Unique identifier for the product. |
| name           | string                        | Name of the product.               |
| price          | decimal                       | Price of the product.              |
| discount       | decimal                       | Discount on the product.           |
| description    | string                        | Description of the product.        |
| unitMeasure    | string                        | Unit of measure for the product.   |
| category       | CategoryBriefDto              | Category of the product.           |
| specifications | IEnumerable<SpecificationDto> | Specifications of the product.     |
| images         | IEnumerable<ImageDto>         | Images of the product.             |

9. CategoryBriefDto

| Property | Type   | Description                         |
| -------- | ------ | ----------------------------------- |
| id       | long   | Unique identifier for the category. |
| name     | string | Name of the category.               |

10. CategoryDto

| Property | Type                    | Description                         |
| -------- | ----------------------- | ----------------------------------- |
| id       | long                    | Unique identifier for the category. |
| name     | string                  | Name of the category.               |
| products | IEnumerable<ProductDto> | Products in the category.           |

11. SpecificationDto

| Property | Type   | Description                 |
| -------- | ------ | --------------------------- |
| name     | string | Name of the specification.  |
| value    | string | Value of the specification. |

12. ImageDto

| Property | Type   | Description             |
| -------- | ------ | ----------------------- |
| path     | string | Path to the image.      |
| alt      | string | Alt text for the image. |

13. TokensDto

| Property     | Type   | Description            |
| ------------ | ------ | ---------------------- |
| token        | string | Jwt session token.     |
| refreshToken | string | Refresh session token. |

14. ProductPageDto

| Property    | Type                    | Description                                     |
| ----------- | ----------------------- | ----------------------------------------------- |
| products    | IEnumerable<ProductDto> | Products of the page.                           |
| currentPage | int                     | Current page.                                   |
| totalCount  | int                     | Total products count.                           |
| pagesCount  | int                     | Total pages count(depends on current pageSize). |
| pageSize    | int                     | Current page size.                              |

### Models

1. LoginModel

| Property | Type   | Description          |
| -------- | ------ | -------------------- |
| username | string | The user's username. |
| password | string | The user's password. |

2. RefreshModel

| Property | Type   | Description        |
| -------- | ------ | ------------------ |
| token    | string | The refresh token. |

3. RegisterModel

| Property  | Type   | Description                         |
| --------- | ------ | ----------------------------------- |
| username  | string | The user's username.                |
| password  | string | The user's password.                |
| firstName | string | The user's first name.              |
| lastName  | string | The user's last name.               |
| address   | string | The user's address (optional).      |
| phone     | string | The user's phone number (optional). |

4. UpdateUserModel

| Property  | Type   | Description                       |
| --------- | ------ | --------------------------------- |
| firstName | string | The user's first name (optional). |
| lastName  | string | The user's last name (optional).  |
| address   | string | The user's address (optional).    |

5. AddCartItemModel

| Property  | Type | Description           |
| --------- | ---- | --------------------- |
| productId | long | The product id.       |
| quantity  | int  | Quantity of products. |

6. CancelOrderModel

| Property | Type | Description             |
| -------- | ---- | ----------------------- |
| orderId  | long | The order to cancel id. |
