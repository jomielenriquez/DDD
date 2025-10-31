## 🗓️ REST API Mastery Plan (for .NET Developer)

### **📅 Fundamentals & Core Concepts**

**🎯 Goal:** Understand REST principles deeply and build a basic API from scratch.

### 🧠 Learn

- What is REST (you already started!)
- HTTP fundamentals: verbs, headers, status codes
- REST constraints (statelessness, uniform interface)
- JSON structure and serialization

## 🧩 1. What is REST (Recap & Deeper View)

**REST (Representational State Transfer)** is not a technology — it’s a **design style** for web services.

When you build a REST API, you design it so clients can:

- Access **resources** via **URLs**
- Use **HTTP methods** to manipulate those resources
- Receive **representations** of those resources (usually JSON)

Example:

```
GET /api/products/5
```

means → “Get the representation of the product whose ID is 5.”

### 🧠 Key Ideas to Remember:

- REST = **Resource-oriented**
- URLs should represent **nouns**, not **verbs**
- Each resource has a **unique identifier (URI)**
- Communication is **stateless**
- Data exchange format is typically **JSON**

---

## 🌐 2. HTTP Fundamentals

You must be fluent in **HTTP** since REST is built on top of it.

### 🔹 HTTP Verbs (Methods)

| Verb | Description | Typical Use |
| --- | --- | --- |
| **GET** | Retrieve data | `GET /api/products` |
| **POST** | Create new resource | `POST /api/products` |
| **PUT** | Replace entire resource | `PUT /api/products/5` |
| **PATCH** | Partially update resource | `PATCH /api/products/5` |
| **DELETE** | Remove resource | `DELETE /api/products/5` |
| **OPTIONS** | Ask server which HTTP methods are allowed | used in CORS preflight |

✅ REST API design tip:

Don’t include verbs in your URL.

**Bad:** `/api/getAllProducts`

**Good:** `/api/products`

---

### 🔹 HTTP Headers

Headers carry metadata in requests and responses.

**Common Request Headers**

| Header | Purpose | Example |
| --- | --- | --- |
| `Content-Type` | Tells server the data format | `application/json` |
| `Accept` | Tells server what response format you expect | `application/json` |
| `Authorization` | Used for authentication tokens | `Bearer eyJhbGciOiJI...` |

**Common Response Headers**

| Header | Purpose |
| --- | --- |
| `Content-Type` | Format of the response body |
| `Cache-Control` | Caching rules |
| `Location` | URL of newly created resource (for POST) |

---

### 🔹 HTTP Status Codes

REST APIs use HTTP status codes to indicate what happened.

| Code | Meaning | Example |
| --- | --- | --- |
| **200 OK** | Request succeeded | `GET /api/products` |
| **201 Created** | Resource created successfully | `POST /api/products` |
| **204 No Content** | Successfully processed, no body | `DELETE /api/products/5` |
| **400 Bad Request** | Invalid input | Missing required field |
| **401 Unauthorized** | Missing/invalid auth token | No JWT provided |
| **403 Forbidden** | Authenticated but not allowed | User not admin |
| **404 Not Found** | Resource not found | Invalid ID |
| **500 Internal Server Error** | Server-side issue | DB error |

---

## ⚙️ 3. REST Constraints (The 6 Rules)

To be considered *truly RESTful*, an API should follow these **constraints** defined by Roy Fielding (the creator of REST):

| # | Constraint | Description | Why It Matters |
| --- | --- | --- | --- |
| 1️⃣ | **Client–Server** | Separation between client (UI) and server (data). | Allows independent evolution. |
| 2️⃣ | **Statelessness** | Each request must contain all the information the server needs (no session). | Simplifies scaling. |
| 3️⃣ | **Cacheable** | Responses must define if they are cacheable. | Improves performance. |
| 4️⃣ | **Uniform Interface** | Consistent use of resources, methods, and URLs. | Makes APIs predictable. |
| 5️⃣ | **Layered System** | API can use intermediaries (proxies, load balancers). | Improves scalability and security. |
| 6️⃣ | **Code on Demand (Optional)** | Server can send executable code (e.g., JS). | Rarely used, optional. |

You’ll mainly focus on **statelessness** and **uniform interface** in backend coding.

---

## 🧾 4. JSON Structure and Serialization

### 🔹 What is JSON?

JSON (JavaScript Object Notation) is the standard format for REST API communication.

Example:

```json
{
  "id": 1,
  "name": "Laptop",
  "price": 1299.99,
  "tags": ["electronics", "computers"]
}
```

### 🔹 Key Rules

- Uses **key-value pairs**
- Strings use **double quotes**
- Arrays use `[ ]`
- Objects use `{ }`

---

### 🔹 Serialization

Serialization = converting a C# object → JSON text

Deserialization = converting JSON text → C# object

In .NET Core:

```csharp
var product = new Product { Id = 1, Name = "Laptop" };

// Serialize (C# → JSON)
string json = JsonSerializer.Serialize(product);

// Deserialize (JSON → C#)
var obj = JsonSerializer.Deserialize<Product>(json);
```

When you return an object from a controller in ASP.NET Core:

```csharp
return Ok(product);
```

The framework automatically **serializes** it to JSON for you.

---

### 🧠 Practical Tips:

- Always use **camelCase** in JSON: `{ "productName": "Laptop" }`
- Don’t expose sensitive fields (like passwords)
- Use consistent response formats (e.g., wrap results in `{ "data": ..., "message": ... }`)

---

Would you like me to continue the next batch of fundamentals?

The next ones should be:

- **How REST APIs handle authentication and authorization**
- **Versioning, pagination, and filtering**
- **Error handling and best practices**
- **Postman and testing tools overview**

### 🧑‍💻 Do (Coding)

1. Create a simple Web API:
    - `dotnet new webapi -n BookStoreApi`
    - Use `InMemoryDatabase` via EF Core
2. Implement endpoints:
    - `GET /api/books`
    - `POST /api/books`
    - `GET /api/books/{id}`
    - `PUT /api/books/{id}`
    - `DELETE /api/books/{id}`
3. Test all endpoints using **Swagger** or **Postman**

### 🧩 Reflect

- How are routes mapped to controllers?
- What does `CreatedAtAction()` do?
- How do HTTP status codes communicate success/failure?

---

### **📅 Best Practices & Structure**

**🎯 Goal:** Learn clean architecture, DTOs, dependency injection, and validation.

### 🧠 Learn

- Separation of concerns: Controller → Service → Repository layers
- DTOs (Data Transfer Objects)
- Model validation with `[Required]`, `[StringLength]`, etc.
- Dependency Injection (`builder.Services.AddScoped<>();`)
- Using AutoMapper (optional but good practice)

## 🧱 1️⃣ Separation of Concerns — *API Architecture Layers*

### **What it means**

Each part of your code should have **a single responsibility**.

In a REST API, that means separating your logic into clear **layers**:

| Layer | Responsibility | Example Folder |
| --- | --- | --- |
| **Controller** | Handle HTTP requests & responses | `/Controllers` |
| **Service (Business Logic)** | Apply business rules, validation, computations | `/Services` |
| **Repository (Data Access)** | Handle all DB interaction (CRUD) | `/Repositories` |
| **Models/Entities** | Represent your database tables | `/Models` |

**🧩 Example flow:**

```
Client → Controller → Service → Repository → Database

```

**Why it matters:**

- Easier to maintain
- Easier to test
- Swappable layers (e.g., change DB without touching controllers)

**In code (simplified):**

```csharp
// Controller
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _service;
    public ProductsController(IProductService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _service.GetAllAsync());
}

```

```csharp
// Service
public class ProductService : IProductService
{
    private readonly IProductRepository _repo;
    public ProductService(IProductRepository repo) => _repo = repo;

    public async Task<IEnumerable<Product>> GetAllAsync() =>
        await _repo.GetAllAsync();
}

```

```csharp
// Repository
public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;
    public ProductRepository(ApplicationDbContext context) => _context = context;

    public async Task<IEnumerable<Product>> GetAllAsync() =>
        await _context.Products.ToListAsync();
}

```

---

## 🧳 2️⃣ DTOs — *Data Transfer Objects*

### **What they are**

DTOs are **lightweight objects** used to transfer data between layers — especially **Controller ↔ Service** or **Controller ↔ Client**.

You use DTOs to:

- Hide database entity details (like passwords or internal IDs)
- Control what data is returned or accepted
- Simplify request/response models

**Example:**

```csharp
// Entity (database model)
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal CostPrice { get; set; }
    public decimal SellingPrice { get; set; }
}

```

```csharp
// DTO (response)
public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal SellingPrice { get; set; }
}

```

Then in your controller:

```csharp
[HttpGet("{id}")]
public async Task<IActionResult> Get(int id)
{
    var product = await _service.GetByIdAsync(id);
    if (product == null) return NotFound();

    var dto = new ProductDto
    {
        Id = product.Id,
        Name = product.Name,
        SellingPrice = product.SellingPrice
    };
    return Ok(dto);
}

```

---

## 🧰 3️⃣ Model Validation (Data Annotations)

When your API receives input (e.g., `POST /api/products`), you must validate it before saving to the database.

### Common Validation Attributes

| Attribute | Purpose |
| --- | --- |
| `[Required]` | Field cannot be null or empty |
| `[StringLength(50)]` | Maximum string length |
| `[Range(1, 100)]` | Numeric range |
| `[EmailAddress]` | Validates email format |
| `[RegularExpression(...)]` | Validates with regex |

Example DTO:

```csharp
public class CreateProductDto
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [Range(0.1, double.MaxValue)]
    public decimal Price { get; set; }
}

```

In your controller:

```csharp
[HttpPost]
public async Task<IActionResult> Create(CreateProductDto dto)
{
    if (!ModelState.IsValid)
        return BadRequest(ModelState);

    await _service.CreateAsync(dto);
    return CreatedAtAction(nameof(Get), new { id = dto.Id }, dto);
}

```

---

## 🧩 4️⃣ Dependency Injection (DI)

**DI** is how .NET Core provides objects (dependencies) to your classes automatically.

Instead of doing this:

```csharp
var repo = new ProductRepository(); // tightly coupled

```

You do this:

```csharp
private readonly IProductRepository _repo;
public ProductService(IProductRepository repo) => _repo = repo;

```

And register the dependency in `Program.cs`:

```csharp
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

```

Now .NET automatically provides instances where needed.

This makes your app **modular**, **testable**, and **maintainable**.

---

## 🔄 5️⃣ AutoMapper (Optional but Great)

Manually mapping DTOs ↔ Entities can get repetitive.

AutoMapper automates this.

### Installation:

```bash
dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection

```

### Setup:

```csharp
// Create a mapping profile
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductDto>();
        CreateMap<CreateProductDto, Product>();
    }
}

```

Register it in `Program.cs`:

```csharp
builder.Services.AddAutoMapper(typeof(Program));

```

Then use it in your service:

```csharp
var dto = _mapper.Map<ProductDto>(product);

```

---

## ✅ Summary — What You Should Know & Practice

| Concept | Why It Matters | What You Should Practice |
| --- | --- | --- |
| **Layered Architecture** | Keeps code organized & scalable | Build CRUD with Controller → Service → Repo |
| **DTOs** | Protect data & control payloads | Use Create/Update DTOs for all endpoints |
| **Validation** | Prevent bad data early | Apply `[Required]` etc. + test ModelState |
| **Dependency Injection** | Enables clean, testable code | Register & inject interfaces |
| **AutoMapper** | Reduces boilerplate | Map between Entities and DTOs automatically |

### 🧑‍💻 Do (Coding)

1. Create layers:
    
    ```
    ├── BookStore.Api
    ├── BookStore.Services
    └── BookStore.Data
    
    ```
    
2. Implement:
    - Repository class for EF Core
    - Service layer for business logic
    - Controller using injected service
3. Add model validation
4. Test how invalid data returns `400 Bad Request`

### 🧩 Reflect

- Why should the controller not directly access the DbContext?
- How does dependency injection improve testability?

---

### **📅 Authentication, Middleware, and Error Handling**

**🎯 Goal:** Make your API secure and robust.

### 🧠 Learn

- How JWT (JSON Web Token) authentication works
- Adding authentication middleware
- Role-based authorization (`[Authorize(Roles="Admin")]`)
- Global exception handling (`UseExceptionHandler`)
- Logging requests and errors

## 🧩 1. How JWT Authentication Works

**JWT (JSON Web Token)** is a compact way to securely transmit information between parties (like a client and server).

Here’s how it works:

1. The **client** logs in (e.g., by sending username/password to `/api/auth/login`).
2. The **server validates credentials** and issues a **JWT** (a signed token containing claims like username and roles).
3. The **client stores the token** (usually in `localStorage` or `sessionStorage`).
4. For each request, the client sends the token in the header:
    
    ```
    Authorization: Bearer <token>
    ```
    
5. The **server validates the token** on each request — no session needed.

---

## 🔐 2. Adding JWT Authentication Middleware

### Step 1 — Install NuGet packages

Run this in your API project:

```bash
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer

```

---

### Step 2 — Add authentication configuration

In your `Program.cs`:

```csharp
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// JWT Configuration
var jwtKey = builder.Configuration["Jwt:Key"];
var jwtIssuer = builder.Configuration["Jwt:Issuer"];

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtIssuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

// Authorization + Controllers
builder.Services.AddAuthorization();
builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();

// Add Authentication and Authorization middleware
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
```

---

### Step 3 — Add JWT settings in `appsettings.json`

```json
"Jwt": {
  "Key": "YourSecretKeyHere12345",
  "Issuer": "BookStoreApi"
}
```

---

### Step 4 — Create a token generator

Example `AuthController.cs`:

```csharp
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;

        public AuthController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost("login")]
        public IActionResult Login(string username, string password)
        {
            // 🔐 Normally you'd check against the database
            if (username != "admin" || password != "123") return Unauthorized();

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds);

            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        }
    }
}
```

---

## 👮‍♂️ 3. Role-Based Authorization

Once a user is logged in and the JWT includes a role (like `Admin`), you can secure endpoints like this:

```csharp
using Microsoft.AspNetCore.Authorization;

[Authorize(Roles = "Admin")]
[HttpPost("add-book")]
public IActionResult AddBook(BookDto book)
{
    // only Admins can access this
    return Ok();
}

```

You can also secure an entire controller:

```csharp
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    // ...
}
```

---

## ✅ Step-by-Step: Add Bearer Token Support in Swagger

### 🧩 Step 1 — Add Swagger configuration in `Program.cs`

Find this part:

```csharp
builder.Services.AddSwaggerGen();
```

Replace it with this:

```csharp
using Microsoft.OpenApi.Models;

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BookStore API", Version = "v1" });

    // 🔑 Add JWT Bearer Definition
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' followed by your JWT token. Example: 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...'"
    });

    // 🔒 Make sure Swagger UI applies this globally
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});
```

---

### 🧩 Step 2 — Keep the middleware order correct

Ensure your app has:

```csharp
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();  // ✅ must come before authorization
app.UseAuthorization();

app.MapControllers();

app.Run();

```

---

### 🧩 Step 3 — Run your app

Start your API and open Swagger (usually at https://localhost:xxxx/swagger).

You’ll now see an **“Authorize”** button 🔒 at the top-right.

When you click it, you’ll get a popup like this:

```
Value: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

Just paste your JWT token **with** the word `Bearer` in front:

```
Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

Swagger will then automatically include this token in the `Authorization` header for all secured endpoints.

---

## 🧠 Notes & Best Practices

| Tip | Why |
| --- | --- |
| ✅ Always use “Bearer” scheme | Swagger automatically sends header: `Authorization: Bearer <token>` |
| ✅ Use `UseAuthentication()` before `UseAuthorization()` | Middleware order matters |
| ⚠️ Don’t hardcode your tokens | Tokens expire and should be generated dynamically |
| 🔐 Protect `/swagger` in production | Either hide it or require authentication |

## ⚙️ 4. Global Exception Handling

This ensures **unhandled errors** don’t crash your app and return clean responses instead.

Add in `Program.cs` (before `app.UseHttpsRedirection()`):

```csharp
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error");
}
```

Then create a global error controller:

```csharp
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApi.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        [Route("/error")]
        public IActionResult HandleError()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context?.Error;

            return Problem(
                title: "An error occurred while processing your request.",
                detail: exception?.Message,
                statusCode: StatusCodes.Status500InternalServerError);
        }
    }
}

```

---

## 🧾 5. Logging Requests and Errors

Use **built-in logging** via `ILogger<T>` and **middleware** for request tracking.

### Example: Inject `ILogger` in controller

```csharp
private readonly ILogger<BooksController> _logger;

public BooksController(ILogger<BooksController> logger)
{
    _logger = logger;
}

[HttpGet]
public IActionResult GetBooks()
{
    _logger.LogInformation("Fetching all books at {time}", DateTime.UtcNow);
    return Ok();
}

```

### Or create global logging middleware:

```csharp
public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        _logger.LogInformation("Incoming request: {method} {path}", context.Request.Method, context.Request.Path);
        await _next(context);
        _logger.LogInformation("Response status: {statusCode}", context.Response.StatusCode);
    }
}

```

Register it in `Program.cs`:

```csharp
app.UseMiddleware<RequestLoggingMiddleware>();

```

---

## ✅ Summary Checklist

| Feature | Middleware | Setup |
| --- | --- | --- |
| JWT Authentication | ✅ `UseAuthentication()` | Add JwtBearer config |
| Role Authorization | ✅ `UseAuthorization()` | Add `[Authorize(Roles="Admin")]` |
| Global Error Handling | ✅ `UseExceptionHandler("/error")` | Add `ErrorController` |
| Logging | ✅ `ILogger` + custom middleware | Monitor requests & errors |

## 🧾 1️⃣ **Console (default during development)**

By default, ASP.NET Core logs go to the **console** (the terminal window where you run `dotnet run` or F5 in Visual Studio).

You’ll see messages like:

```
info: BookStoreApi.Middlewares.RequestLoggingMiddleware[0]
      Incoming request: GET /api/books
info: BookStoreApi.Middlewares.RequestLoggingMiddleware[0]
      Response status: 200

```

If you run inside **Visual Studio**, check the **Output** window:

- In the dropdown, choose: **Show output from: Application**
    
    (or **ASP.NET Core Web Server** depending on version)
    

---

## 🪵 2️⃣ **Add file logging (optional)**

If you also want logs written to a file (e.g., for production or debugging), add a logging provider.

The simplest way is using **Serilog**.

### 👉 Step 1: Install Serilog packages

Run this in the terminal:

```bash
dotnet add package Serilog.AspNetCore
dotnet add package Serilog.Sinks.File

```

### 👉 Step 2: Configure Serilog in `Program.cs`

Add this **before `var app = builder.Build();`**:

```csharp
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

```

This will create a `Logs` folder with daily log files like:

```
Logs/
 ┣ log-2025-10-27.txt
 ┣ log-2025-10-28.txt

```

---

## 📁 3️⃣ **External logging platforms (advanced)**

When you deploy to production, you can forward logs to:

- **AWS CloudWatch** (if hosted on AWS)
- **Azure Application Insights**
- **Elasticsearch / Kibana**
- **Seq** (popular local log viewer for structured logs)

These are configured through logging providers, e.g.:

```csharp
builder.Logging.AddApplicationInsights();

```

---

### ✅ **Summary**

| Environment | Where logs appear | Notes |
| --- | --- | --- |
| Local dev (`dotnet run`) | Console output | Default behavior |
| Visual Studio | Output window | Select "ASP.NET Core Web Server" |
| With Serilog | Log files under `/Logs` | Great for debugging & production |
| Cloud-hosted | Cloud log service | Depends on your provider |

### 🧑‍💻 Do (Coding)

1. Add JWT authentication (register/login endpoints)
2. Secure product endpoints — only logged-in users can `POST`, `PUT`, `DELETE`
3. Add custom middleware to log each request
4. Add global error handling to return consistent error JSON

### 🧩 Reflect

- What’s inside a JWT token?
- Why is middleware order important?
- How can you handle errors consistently?

---

### **📅 Advanced Topics & Real-World Patterns**

**🎯 Goal:** Level up your API for production-grade scenarios.

### 🧠 Learn

- Pagination, filtering, and sorting
- API versioning
- CORS policy (cross-origin)
- Rate limiting (using middleware)
- Swagger documentation (customized)
- Unit testing with `xUnit` and `Moq`

In this lesson, you’ll learn to:

✅ Add pagination, filtering, and sorting

✅ Manage API versions

✅ Configure CORS (Cross-Origin Resource Sharing)

✅ Implement rate limiting

✅ Customize Swagger documentation

✅ Write unit tests using **xUnit** and **Moq**

Each section includes:

- **Concept** (what it is & why it matters)
- **Implementation** (step-by-step guide)
- **Tips & pitfalls**

---

## 🧮 1️⃣ Pagination, Filtering, and Sorting

### 🎯 **Concept**

When you query data (e.g., `/api/books`), returning **thousands of records** can slow performance.

Pagination, filtering, and sorting make APIs:

- Faster
- Easier for frontends to consume
- Scalable for large datasets

---

### ⚙️ **Implementation**

### Step 1: Modify Repository

```csharp
public async Task<IEnumerable<Book>> GetBooksAsync(string? search, string? sortBy, int page = 1, int pageSize = 10)
{
    var query = _context.Books.AsQueryable();

    // Filtering
    if (!string.IsNullOrEmpty(search))
        query = query.Where(b => b.Title.Contains(search) || b.Author.Contains(search));

    // Sorting
    query = sortBy switch
    {
        "title" => query.OrderBy(b => b.Title),
        "author" => query.OrderBy(b => b.Author),
        _ => query.OrderBy(b => b.Id)
    };

    // Pagination
    return await query
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();
}

```

### Step 2: Controller

```csharp
[HttpGet]
public async Task<IActionResult> GetBooks([FromQuery] string? search, [FromQuery] string? sortBy, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
{
    var books = await _bookRepository.GetBooksAsync(search, sortBy, page, pageSize);
    return Ok(books);
}

```

### ✅ Tips

- Always use `Skip` and `Take` with `OrderBy` (EF needs a deterministic order).
- Return pagination metadata (like total count) via custom headers or response objects.

---

## 🧭 2️⃣ API Versioning

### 🎯 **Concept**

When your API evolves (e.g., `/api/v1/books` → `/api/v2/books`), you don’t want to break existing clients.

API versioning lets multiple versions coexist.

---

### ⚙️ **Implementation**

### Step 1: Install

```bash
dotnet add package Microsoft.AspNetCore.Mvc.Versioning

```

### Step 2: Configure in `Program.cs`

```csharp
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
});

```

### Step 3: Apply to Controller

```csharp
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    ...
}
```

---

## 🧭 What You’ll Learn

You’ll create:

- ✅ `v1` and `v2` endpoints (side by side)
- ✅ Versioned Swagger documentation (v1 and v2 tabs)
- ✅ Controller version attributes (`[ApiVersion("1.0")]`)
- ✅ Version-based routing (`api/v{version:apiVersion}/[controller]`)

---

## 🪜 Step-by-Step Implementation

---

### **1️⃣ Install the required NuGet packages**

Run these commands in your main API project:

```bash
dotnet add package Microsoft.AspNetCore.Mvc.Versioning
dotnet add package Microsoft.AspNetCore.Mvc.Versioning.ApiExplorer

```

> 🔍 ApiExplorer helps Swagger display multiple versions.
> 

---

### **2️⃣ Configure versioning in `Program.cs`**

Below your `builder.Services.AddControllers();` line, add this:

```csharp
builder.Services.AddApiVersioning(options =>
{
    // Default API version
    options.DefaultApiVersion = new ApiVersion(1, 0);

    // If no version is specified in request, use default (v1.0)
    options.AssumeDefaultVersionWhenUnspecified = true;

    // Report supported versions in response headers
    options.ReportApiVersions = true;
});

builder.Services.AddVersionedApiExplorer(options =>
{
    // Format: v1, v2, etc.
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

```

---

### **3️⃣ Update Swagger to support versions**

Add this **after** your `AddSwaggerGen()`:

```csharp
builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();

```

Now create a new class `ConfigureSwaggerOptions.cs` (anywhere in your project, e.g., under `Helpers` folder):

```csharp
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Models;

public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;

    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
    {
        _provider = provider;
    }

    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in _provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(
                description.GroupName,
                new OpenApiInfo
                {
                    Title = "BookStore API",
                    Version = description.ApiVersion.ToString(),
                    Description = "Sample API with versioning support"
                });
        }
    }
}

```

Then update your Swagger middleware in `Program.cs`:

```csharp
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                description.GroupName.ToUpperInvariant());
        }
    });
}

```

---

### **4️⃣ Update controller routes**

Now, modify your controllers to include version attributes.

### Example: `BooksController` (v1)

```csharp
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApi.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BooksController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetV1()
        {
            return Ok(new { message = "Books from API v1" });
        }
    }
}

```

### Example: `BooksController` (v2)

Create a new file under a `v2` folder:

```csharp
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApi.Controllers.v2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BooksController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetV2()
        {
            return Ok(new { message = "Books from API v2" });
        }
    }
}

```

---

### **5️⃣ Run your project**

✅ Swagger will now show:

- `v1` API
- `v2` API

✅ You can call:

- `GET /api/v1/books`
- `GET /api/v2/books`

Each will hit its own controller.

---

## 🧠 Extra Notes

- **Backward compatibility:**
    
    Old clients can continue using `v1` while new clients switch to `v2`.
    
- **Header-based versioning (optional):**
    
    You can also version via header instead of URL. Example:
    
    ```csharp
    options.ApiVersionReader = new HeaderApiVersionReader("x-api-version");
    
    ```
    
    Then call the API with a header like `x-api-version: 2.0`.
    

## 🌐 3️⃣ CORS (Cross-Origin Resource Sharing)

### 🎯 **Concept**

CORS controls **which domains** can access your API.

For example, allow `https://schoolwebsite.com` but block others.

---

### ⚙️ **Implementation**

### Step 1: Configure in `Program.cs`

```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSchoolFrontEnd",
        policy => policy.WithOrigins("https://schoolwebsite.com")
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

```

### Step 2: Use it

```csharp
var app = builder.Build();

app.UseCors("AllowSchoolFrontEnd");

```

### ✅ Tip

- Use `AllowAnyOrigin()` **only in development**.
- Always restrict origins in production for security.

---

## 🚦 4️⃣ Rate Limiting (Middleware)

### 🎯 **Concept**

Rate limiting prevents abuse (e.g., bots or DoS attacks) by limiting how many requests a client can make per minute.

---

### ⚙️ **Implementation**

### Step 1: Install

```bash
dotnet add package AspNetCoreRateLimit

```

### Step 2: Configure `appsettings.json`

```json
"IpRateLimiting": {
  "EnableEndpointRateLimiting": true,
  "GeneralRules": [
    {
      "Endpoint": "*",
      "Period": "1m",
      "Limit": 30
    }
  ]
}

```

### Step 3: Add in `Program.cs`

```csharp
builder.Services.AddMemoryCache();
builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
builder.Services.AddInMemoryRateLimiting();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

var app = builder.Build();
app.UseIpRateLimiting();

```

### ✅ Tip

You can apply per-endpoint limits, or use JWT claims to limit by user instead of IP.

---

## 📚 5️⃣ Swagger (Customized Documentation)

### 🎯 **Concept**

Swagger helps developers explore and test your API easily.

You can customize it for better clarity and authentication support.

---

### ⚙️ **Implementation**

### Step 1: Add Package

```bash
dotnet add package Swashbuckle.AspNetCore

```

### Step 2: Configure in `Program.cs`

```csharp
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "BookStore API", Version = "v1" });

    // Add JWT Bearer Auth Support
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            Array.Empty<string>()
        }
    });
});

```

Now Swagger UI will show an **“Authorize”** button for tokens.

---

## 🧪 6️⃣ Unit Testing with xUnit and Moq

### 🎯 **Concept**

Unit tests ensure your code behaves correctly and remains reliable during refactoring.

---

### ⚙️ **Implementation**

### Step 1: Create a Test Project

```bash
dotnet new xunit -n BookStore.Tests
dotnet add BookStore.Tests reference BookStore.Services
dotnet add BookStore.Tests package Moq

```

### Step 2: Example Test

```csharp
using Xunit;
using Moq;
using BookStore.Services;
using BookStore.Data.Models;
using BookStore.Data.Repository;

public class BookServiceTests
{
    [Fact]
    public async Task GetBookById_ShouldReturnBook()
    {
        // Arrange
        var mockRepo = new Mock<IBookRepository>();
        mockRepo.Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(new Book { Id = 1, Title = "Clean Code" });

        var service = new BookService(mockRepo.Object);

        // Act
        var book = await service.GetBookByIdAsync(1);

        // Assert
        Assert.NotNull(book);
        Assert.Equal("Clean Code", book.Title);
    }
}

```

---

### ✅ Tips

- Use **Moq** to isolate services and mock dependencies.
- Follow naming pattern: `MethodName_Condition_ExpectedResult`.
- Add test coverage for error paths, not just success cases.

---

## 🧠 Summary Table

| Feature | Purpose | Package |
| --- | --- | --- |
| Pagination / Sorting | Efficient data fetching | — |
| API Versioning | Support multiple API versions | Microsoft.AspNetCore.Mvc.Versioning |
| CORS | Secure cross-domain access | Built-in |
| Rate Limiting | Prevent API abuse | AspNetCoreRateLimit |
| Swagger | API docs & testing | Swashbuckle.AspNetCore |
| Unit Testing | Ensure code quality | xUnit, Moq |

### 🧑‍💻 Do (Coding)

1. Add pagination to `GET /api/books`
2. Implement API Versioning (v1, v2)
3. Configure CORS (allow only specific origins)
4. Write 3–5 unit tests for your service layer

### 🧩 Reflect

- Why is versioning important?
- How do you prevent performance bottlenecks?
- How can you make APIs backward compatible?

---

## 📚 Recommended Tools & Topics (for Deep Mastery)

| Category | Tools / Libraries |
| --- | --- |
| HTTP Testing | Postman, Swagger UI |
| Database | EF Core, SQL Server |
| Security | JWT, Identity |
| Docs | Swagger (Swashbuckle) |
| Testing | xUnit, Moq |
| Monitoring | Serilog, Seq, Application Insights |
| Hosting | IIS, AWS, or Azure App Service |

---

## 🧠 Bonus Challenges (Optional)

- Build a **todo app** API from scratch (CRUD + JWT)
- Connect your API to a **React or plain JavaScript frontend**
- Deploy it to **AWS EC2 or Azure App Service**
- Add **CI/CD** using GitHub Actions