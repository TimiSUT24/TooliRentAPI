# TooliRent API

TooliRent is a REST-based web API for a small makerspace where members can book, pick up, and return tools. The API is built with [ASP.NET Core] using Entity Framework and a relational database (SQL Server). Clients, such as web or mobile applications, can interact with the API to manage tool rentals

---

## Features

1. **Authentication**
   - Register and log in members
   - JWT token-based authentication
   - Token refresh

2. **Tool Management**
   - List all tools
   - Filter tools by category, status, or availability
   - View tool details

3. **Booking**
   - Book one or more tools for a specific period
   - View own bookings and status
   - Cancel bookings

4. **Pickup / Return**
   - Mark tools as picked up
   - Mark tools as returned
   - Handle late returns

5. **Admin Features**
   - CRUD operations on tools and categories
   - Manage users (activate/deactivate)
   - View tool usage statistics
     
---

## API Endpoints

### AuthController
| Method | Endpoint | Authentication | Description |
|--------|----------|----------------|----------------|
| POST   | `/Auth/register` | ❌ Not Authenticated | Register as user | 
| POST   | `/Auth/login` | ❌ Not Authenticated | Login As User | 
| POST   | `/Auth/refresh-token` | ✅ User/Admin | Get a new refresh token/access token | 

### ToolController
| Method | Endpoint | Authentication | Description |
|--------|----------|----------------|----------------|
| GET    | `/Tool/available-tools` | ❌ NOT Authenticated | Get all available tools |
| GET    | `/Tool/{toolName}` | ❌ NOT Authenticated | Get tool info by toolname | 
| GET    | `/Tool/filterTools` | ❌ NOT Authenticated | Get tools via filtering  | 

### BookingController
| Method | Endpoint | Authentication | Description |
|--------|----------|----------------|----------------|
| POST   | `/Booking/create-booking` | ✅ User | Book available tools |
| GET    | `/Booking/user-bookings` | ✅ User | Get info about own bookings | 
| DELETE | `/Booking/cancel-booking` | ✅ User | Cancel a pending booking | 
| PUT    | `/Booking/pick-up` | ✅ User | Pick up ur tool | 
| PUT    | `/Booking/return` | ✅ User | Return tool |
| PUT    | `/Booking/pay-late-fee` | ✅ User | Pay late fee if return was late |

### AdminController
| Method | Endpoint | Authentication | Description |
|--------|----------|----------------|----------------|
| POST   | `/Admin/add-tool` | ✅ Admin | Create new tool model |
| GET    | `/Admin/tool` | ✅ Admin | Get detailed info about a tool | 
| PUT    | `/Admin/update-tools` | ✅ Admin | Update a tool | 
| DELETE | `/Admin/delete-toolItem` | ✅ Admin | Delete a specific toolitem within a tool model | 
| DELETE | `/Admin/delete-tool` | ✅ Admin | Delete the whole tool model including toolitems |
| POST   | `/Admin/create-category` | ✅ Admin | Create a category model |
| GET    | `/Admin/categories` | ✅ Admin | Get all categories |
| PUT    | `/Admin/update-category` | ✅ Admin | Update a category |
| DELETE | `/Admin/delete-category` | ✅ Admin | Delete a category model |
| PUT    | `/Admin/inactivate/reactivate-user` | ✅ Admin | inactivate/reactivate a user for a period of time |
| GET    | `/Admin/statistics-usage-tools` | ✅ Admin | Get statistics of how many times tools are getting booked |
| GET    | `/Admin/statistics-borrowed-tools` | ✅ Admin | Get statistics of how many tools are currently borrowed |

---

## Architecture

The TooliRent API uses an **N-tier architecture** with clear separation:

- **Presentation Layer:** API Controllers that handle HTTP requests  
- **Application Layer:** Services containing application logic  
- **Domain Layer:** Entities, enums, and interfaces  
- **Infrastructure Layer:** Repositories and DbContext for data access  

**Design Patterns:**

- Service pattern for business logic  
- Repository pattern for data access  
- AutoMapper for DTO ↔ Entity mapping  
- FluentValidation for validating incoming DTOs  
- JWT Bearer authentication with role-based authorization (**Member** and **Admin**)  

---

## Database

- Relational database (SQL Server)  
- Code-first approach with migrations  
- Seed data for development and testing  

**Key models:**

- **Tool:** Represents a tool with name, description, category, and ToolItems  
- **ToolItem:** Individual unit of a tool with a status (Available, Borrowed, etc.)  
- **Booking:** Booking with start and end dates, status, associated tools, latefee, islate and user
- **Category:** Category with name and associated tool
- **ApplicationUser:** Admin/user with role and login information also associated with bookings

---




