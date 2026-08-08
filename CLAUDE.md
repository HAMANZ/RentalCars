# Rental cars and maintenance system

Main goal is to continue my project according to my Domain Models. and to continue according to my structure of my project,
Always-on rules for this project. For module checklists, C# templates, the full
domain model, the accounting engine, the phase plan, and concrete DB schema, consult
the ```
RentalCars.sln
├── src/
│   ├── RentalCars.DomainLayer/            # Entities, value objects, enums-as-lookups,DTOs, domain events
│   ├── RentalCars.ServiceLayer/       # Interfaces, , services, validators (per module)
│   ├── RentalCars.RepositoryLayer/    # EF Core DbContext, Migrations, Repositories,
│   ├── RentalCars.RentalCar/               # Controllers, Views, Auth, DI composition root

``` — read its

This file holds only what must be true in every session.

## What this system is

A Clean Architecture, Modular Monolith ERP (ASP.NET Core + EF Core + MySQL) for
companies that manage  investor-owned cars to drivers and Maintenance Center to maintaine theirs own cars and another cars.

## Tech stack (fixed — never substitute)

- Backend: ASP.NET Core (.NET, latest LTS)
- ORM: Entity Framework Core (fluent config, not data annotations)
- DB: MySQL (snake_case tables, DECIMAL(18,2) money, utf8mb4)
- Auth: JWT + database-driven Role-Based Access Control
- Frontend: server-rendered ASP.NET Core MVC / Razor views (no SPA/JS framework).
  Extend the existing Dashboard views in the presentation project — never redesign.
- Architecture: Clean Architecture, Modular Monolith (module-per-folder, NOT
  module-per-project)
- Patterns: Repository Layer, Domain Layer, Service Layer, DI. CQRS only when a module
  genuinely justifies it — default to plain service methods.

## Code conventions

I want you to continue a complete **Service Layer** for my ASP.NET Core project the business model in the domain layer and its the sql model.

### Architecture

Use this structure:


DomainLayer
└── Models
    ├── Car.cs
    ├── Customer.cs
    ├── RentalContract.cs
    ├── ...
    
ServiceLayer
├── Interfaces
│   ├── ICarService.cs
│   ├── ICustomerService.cs
│   ├── IRentalContractService.cs
│   └── ...
│
└── Implementations
    ├── CarService.cs
    ├── CustomerService.cs
    ├── RentalContractService.cs
    └── ...
```

### Main requirement

For **every model/entity in the Domain/Models folder**, create:

1. An interface:

   
   ICarService
   ICustomerService
   IRentalContractService
   ```

2. A service implementation:

   
   CarService
   CustomerService
   RentalContractService
   ```

The service name must be based on the model name.

For example:

```text
DomainLayer/Models/Car.cs
        ↓
ServiceLayer/Interface/ICarService.cs
ServiceLayer/Implementation/CarService.cs
```

### Generic CRUD operations

Each service interface should provide standard asynchronous CRUD operations:


Task<IEnumerable<Car>> GetAllAsync();
Task<Car?> GetByIdAsync(int id);
Task<Car> CreateAsync(Car entity);
Task<Car> UpdateAsync(Car entity);
Task<bool> DeleteAsync(int id);
```

Use the correct primary-key type from the model instead of assuming `int`.

If a model uses:


string Id
Guid Id
long Id
```

use the correct type.

### Implementation

Each service implementation must:

* Use `RentalCarDbContext`
* Use Entity Framework Core
* Use asynchronous methods
* Use `AsNoTracking()` for read-only queries where appropriate
* Use `Include()` when navigation properties are required
* Use `SaveChangesAsync()`
* Handle entity-not-found cases properly
* Avoid unnecessary database calls
* Follow clean coding practices

Example:


public interface ICarService
{
    Task<IEnumerable<Car>> GetAllAsync();
    Task<Car?> GetByIdAsync(int id);
    Task<Car> CreateAsync(Car entity);
    Task<Car> UpdateAsync(Car entity);
    Task<bool> DeleteAsync(int id);
}
```

using RepositoryLayer.RespositoryPattern;
using RentalCar.DomainLayer.CommonObjects;
using RentalCar.DomainLayer.DTO;
using RentalCar.DomainLayer.Models;
using RentalCar.ServiceLayer.Interface;
using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RentalCar.ServiceLayer.Implementation
{
    public class AppLabelServices : IAppLabel
    {
        private readonly IRepository<AppLabel> _repository;
        private RentalCarDbContext _dbContext;
        public AppLabelServices(IRepository<AppLabel> rep, RentalCarDbContext dbContext)
        {

            this._repository = rep;
            this._dbContext = dbContext;
        }


        #region DTOtoModel/ModeltoDTO 
        public AppLabel FromDTOtoModel(AppLabelDTO dto)
        {
            AppLabel Model = new AppLabel();
            Model.Id = dto.Id;
            Model.LabelName = dto.LabelName;
            Model.FriendlyName = dto.FriendlyName;
            Model.Value = dto.Value;
            Model.Desc = dto.Desc;
            Model.LanguagId = dto.LanguagId;
            Model.Is_deleted = dto.Is_deleted;
            Model.Created_at = dto.Created_at;
            return Model;
        }



        public AppLabelDTO FromModeltoDTO(AppLabel model)
        {
            AppLabelDTO DTO = new AppLabelDTO();
            DTO.Id = model.Id;
            DTO.Id = model.Id;
            DTO.LabelName = model.LabelName;
            DTO.FriendlyName = model.FriendlyName;
            DTO.Value = model.Value;
            DTO.Desc = model.Desc;
            DTO.Is_deleted = model.Is_deleted;
            DTO.Created_at = model.Created_at;
            return DTO;
        }

        #endregion


        #region Get
        public DynamicResponse<AppLabelDTO> Get(long Id)
        {
            DynamicResponse<AppLabelDTO> response = new DynamicResponse<AppLabelDTO>();

            try
            {
                AppLabel Model = _dbContext.AppLabels.Where(e => e.Id == Id).FirstOrDefault();
                response.Data = FromModeltoDTO(Model);
                response.HttpStatusCode = HttpStatusCode.OK;

                return response;
            }
            catch (Exception ex)
            {
                response.HttpStatusCode = HttpStatusCode.InternalServerError;
                response.Message = "Please try again later";
                response.ServerMessage = ex.Message;

                return response;
            }
        }

        #endregion


        #region GetAll
        public DynamicResponse<List<AppLabelDTO>> GetAll(long LangId)
        {
            DynamicResponse<List<AppLabelDTO>> response = new DynamicResponse<List<AppLabelDTO>>();

            try
            {
                List<AppLabel> listModel = _dbContext.AppLabels.Where(e => e.LanguagId == LangId).ToList();
                List<AppLabelDTO> listDTO = new List<AppLabelDTO>();
                if (listModel.Count != 0)
                {
                    foreach (var item in listModel)
                    {
                        listDTO.Add(FromModeltoDTO(item));
                    }
                }
                response.Data = listDTO;
                response.HttpStatusCode = HttpStatusCode.OK;

                return response;
            }
            catch (Exception ex)
            {
                response.HttpStatusCode = HttpStatusCode.InternalServerError;
                response.Message = "Please try again later";
                response.ServerMessage = ex.Message;

                return response;
            }
        }

        #endregion

        #region Get All By Language Id - added by Hanine
        public async Task<DynamicResponse<List<AppLabelDTO>>> GetAllByLanguageIdAsync(int languageId)
        {
            var response = new DynamicResponse<List<AppLabelDTO>>();

            try
            {
                var listModel = await _dbContext.AppLabels
                    .Where(e => !e.Is_deleted && e.LanguagId == languageId)
                    .ToListAsync();

                var listDTO = new List<AppLabelDTO>();

                foreach (var item in listModel)
                {
                    listDTO.Add(FromModeltoDTO(item));
                }

                response.Data = listDTO;
                response.HttpStatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.HttpStatusCode = HttpStatusCode.InternalServerError;
                response.Message = "Please try again later";
                response.ServerMessage = ex.Message;
            }

            return response;
        }
        #endregion

        #region Add
        public DynamicResponse<bool> Add(AppLabelDTO toAdd)
        {
            DynamicResponse<bool> response = new DynamicResponse<bool>();

            try
            {

                if (toAdd != null)
                {
                    AppLabel model = FromDTOtoModel(toAdd);
                    _repository.Insert(model);
                }

                response.Data = true;
                response.HttpStatusCode = HttpStatusCode.OK;

                return response;
            }
            catch (Exception ex)
            {
                response.HttpStatusCode = HttpStatusCode.InternalServerError;
                response.Message = "Please try again later";
                response.ServerMessage = ex.Message;

                return response;
            }
        }

        #endregion


        #region Update
        public DynamicResponse<bool> Update(AppLabelDTO ToUpdate)
        {
            DynamicResponse<bool> response = new DynamicResponse<bool>();

            try
            {
                AppLabel Model = _dbContext.AppLabels.Where(e => e.Id == ToUpdate.Id).FirstOrDefault();
                if (Model != null)
                {
                    _repository.Update(FromDTOtoModel(ToUpdate));

                    response.Data = true;
                    response.HttpStatusCode = HttpStatusCode.OK;
                    return response;
                }

                response.Data = false;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                return response;
            }
            catch (Exception ex)
            {
                response.HttpStatusCode = HttpStatusCode.InternalServerError;
                response.Message = "Please try again later";
                response.ServerMessage = ex.Message;

                return response;
            }
        }

        #endregion

        public async Task<DynamicResponse<bool>> UpdateAsync(AppLabelDTO ToUpdate)
        {
            var response = new DynamicResponse<bool>();

            try
            {
                var model = await _dbContext.AppLabels.FindAsync(ToUpdate.Id);
                if (model != null)
                {
                    _repository.Update(FromDTOtoModel(ToUpdate)); 
                    await _dbContext.SaveChangesAsync();         

                    response.Data = true;
                    response.HttpStatusCode = HttpStatusCode.OK;
                }
                else
                {
                    response.Data = false;
                    response.HttpStatusCode = HttpStatusCode.NotFound;
                }
            }
            catch (Exception ex)
            {
                response.Data = false;
                response.HttpStatusCode = HttpStatusCode.InternalServerError;
                response.Message = "Please try again later";
                response.ServerMessage = ex.Message;
            }

            return response;
        }

        public async Task<DynamicResponse<bool>> UpdateWebsiteLabel(AppLabelDTO en, AppLabelDTO ar)
        {
            var response = new DynamicResponse<bool>();

            using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                var modelEn = await _dbContext.AppLabels.FindAsync(en.Id);
                var modelAr = await _dbContext.AppLabels.FindAsync(ar.Id);

                if (modelEn == null || modelAr == null)
                {
                    response.Data = false;
                    response.HttpStatusCode = HttpStatusCode.NotFound;
                    return response;
                }

                // UPDATE TRACKED ENTITIES (NO new objects!)
                modelEn.LabelName = en.LabelName;
                modelEn.FriendlyName = en.FriendlyName;
                modelEn.Value = en.Value;
                modelEn.Desc = en.Desc;
                modelEn.Updated_at = en.Updated_at;

                modelAr.LabelName = ar.LabelName;
                modelAr.FriendlyName = ar.FriendlyName;
                modelAr.Value = ar.Value;
                modelAr.Desc = ar.Desc;
                modelAr.Updated_at = ar.Updated_at;

                var entries = _dbContext.ChangeTracker.Entries()
                .Where(e => e.State != EntityState.Unchanged)
                .ToList();

                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                response.Data = true;
                response.HttpStatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                response.Data = false;
                response.HttpStatusCode = HttpStatusCode.InternalServerError;
                response.Message = "Please try again later";
                response.ServerMessage = ex.Message;
            }

            return response;
        }


        #region Delete
        public DynamicResponse<bool> Delete(long Id)
        {
            DynamicResponse<bool> response = new DynamicResponse<bool>();

            try
            {
                AppLabel Model = _dbContext.AppLabels.Where(e => e.Id == Id).FirstOrDefault();
                if (Model != null)
                {
                    //_repository.Remove(Model);
                    Model.Is_deleted = true;
                    _dbContext.SaveChanges();
                    response.Data = true;
                    response.HttpStatusCode = HttpStatusCode.OK;
                    return response;
                }

                response.Data = false;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                return response;
            }
            catch (Exception ex)
            {
                response.HttpStatusCode = HttpStatusCode.InternalServerError;
                response.Message = "Please try again later";
                response.ServerMessage = ex.Message;

                return response;
            }
        }

        #endregion

        #region Get Value
        public async Task<DynamicResponse<string>> GetValAsync(string LabelName, long LangId)
        {
            DynamicResponse<string> response = new DynamicResponse<string>();

            try
            {
                AppLabel model = await _dbContext.AppLabels
                    .Where(e => e.LabelName == LabelName && e.LanguagId == LangId)
                    .FirstOrDefaultAsync();

                if (model != null)
                    response.Data = model.Value;

                response.HttpStatusCode = HttpStatusCode.OK;
                return response;
            }
            catch (Exception ex)
            {
                response.HttpStatusCode = HttpStatusCode.InternalServerError;
                response.Message = "Please try again later";
                response.ServerMessage = ex.Message;

                return response;
            }
        }
        #endregion
    }
}


### Important: use the existing DbSet names

Do NOT assume the DbSet name from the class name.

Read the existing `RentalCarDbContext` and use the actual DbSet.

For example, if the context contains:


public virtual DbSet<EUser> EUser { get; set; }
```

then use:


_context.EUser
```

Do not change it to:


_context.EUsers
```

Similarly, respect all existing DbSet names.

### Relationships

For models with navigation properties, create service methods appropriate to the relationships.

For example, for:


RentalContract
```

load related:

```text
Customer
Car
RentalPayments
```

where appropriate.

For:


STransaction
```

respect the existing:

```text
DebitAccount
CreditAccount
```

relationships.

Do not modify the Domain models unless absolutely necessary.

### Do not duplicate business logic

The service layer should contain business operations and database access.

Controllers should NOT directly access `RentalCarDbContext`.

Use:

```text
Controller
    ↓
Service Interface
    ↓
Service Implementation
    ↓
RentalCarDbContext
    ↓
SQL Server
```

### Dependency Injection

Create/update the DI registration so every service is registered automatically.

Prefer assembly scanning if appropriate; otherwise explicitly register:


builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IRentalContractService, RentalContractService>();
```

Register all generated services.

### Naming

Use this exact naming convention:

```text
Model:
Car

Interface:
ICarService

Implementation:
CarService
```

```text
Model:
Customer

Interface:
ICustomerService

Implementation:
CustomerService
```

```text
Model:
RentalContract

Interface:
IRentalContractService

Implementation:
RentalContractService
```

For plural model names, preserve the actual class name.

### Existing project rules

Before generating code:

1. Inspect the entire `DomainLayer/Models` folder.
2. Inspect `RentalCarDbContext`.
3. Inspect existing ServiceLayer/services and interfaces.
4. Inspect the existing project namespaces.
5. Inspect existing primary keys.
6. Inspect existing DbSet names.
7. Inspect navigation properties and relationships.
8. Reuse existing patterns where they already exist.

Do NOT invent properties, DbSets, keys, or relationships.

### Avoid duplicate services

Before creating a service, search the entire solution.

If a service already exists:

* Do not create a duplicate.
* Improve/refactor the existing service if necessary.
* Preserve existing public APIs unless there is a strong reason to change them.

### Output

After analyzing the project, create all missing:

```text
Services/
├── Interfaces/
└── Implementations/
```

services for all Domain models.

Then:

1. Build the solution.
2. Fix all compilation errors.
3. Fix namespace issues.
4. Fix missing DI registrations.
5. Fix EF Core query issues.
6. Do not stop until the solution builds successfully.

At the end, provide a summary containing:

* Number of models found
* Number of services created
* Number of existing services reused
* Files created
* Files modified
* DI registrations added
* Any models that were intentionally skipped and why
* Final build result


## Build order



## Philosophy

KISS, YAGNI, DRY, SOLID. Build the simplest production-ready version of what's asked.
No features/entities/abstractions that weren't requested or implied. Explicit code
over clever code. When a request is genuinely new/ambiguous, ask one clarifying
question rather than guessing.
