Add the **Car Details page** so that each related section is displayed as a separate AdminLTE card.

IMPORTANT: Use the **actual existing model properties and relationships in the project**. Do NOT invent properties, fields, columns, or relationships.

## Car Details Layout

The page should have:

### 1. Car Information Card

Use the existing `Car` properties:

* `Id`
* `VIN`
* `EngineNo`
* `Model`
* `ChassisNumber`
* `Year`
* `Color`
* `Image`
* `PurchasePrice`
* `CurrentKM`
* `Description`
* `InvestorId`
* `Branch`
* `FuelType`
* `LicensePlate`
* `CarOwner`
* `Brand`
* `Investor`

Do NOT add a `Type` field.

Display the important information in a clean responsive AdminLTE card.

---

### 2. Oil Schedules Card

Use the existing `OilChangeSchedule` model.

The card/table must use these actual properties:

* `Id`
* `Car`
* `LastChangeDate`
* `LastChangeKM`
* `ChangeIntervalKM`
* `NextChangeKM`
* `OilType`
* `Cost`
* `Notes`

`NextChangeKM` is a calculated property and should be displayed as the next required oil-change mileage.

Do not invent fields such as `NextChangeDate` because they do not exist in the model.

Display the records in a responsive table inside an AdminLTE card.

Suggested columns:

| Last Change Date | Last Change KM | Interval KM | Next Change KM | Oil Type | Cost | Notes |

---

### 3. Tire Schedules Card

Use the existing `TireSchedule` model already present in the project.

Before implementing this card, inspect the actual `TireSchedule` model and use ONLY its existing properties.

Do not invent properties.

Display the available Tire Schedule information in a responsive AdminLTE table.

---

### 4. Battery Schedules Card

Use the existing `BatterySchedule` model already present in the project.

Before implementing this card, inspect the actual `BatterySchedule` model and use ONLY its existing properties.

Do not invent properties.

Display the available Battery Schedule information in a responsive AdminLTE table.

---

### 5. Rental Contracts Card

Use the existing `RentalContract` model and its existing relationship with `Car`.

Before implementing this card, inspect the actual model and use ONLY its existing properties.

Display the relevant rental contract information in a responsive AdminLTE table.

Do not invent fields such as `ContractNumber`, `RentalAmount`, etc. unless they actually exist in the model.

---

### 6. Work Orders Card

Use the existing `WorkOrder` model and its relationship with the Car.

Inspect the actual model first.

Display ONLY existing WorkOrder properties.

Use a responsive AdminLTE table.

Do not invent fields.

---

### 7. Insurance Card

Use the actual existing `Insurance` model.

The model contains these properties:

* `Id`
* `PolicyNumber`
* `StartDate`
* `EndDate`
* `Premium`
* `CoverageAmount`
* `Deductible`
* `CoverageDetails`
* `Notes`
* `RenewalReminderSent`
* `InsuranceCompany`
* `Car`
* `InsuranceType`
* `Status`
* `Documents`

Display the insurance information using an AdminLTE card.

Suggested columns:

| Policy Number | Insurance Company | Insurance Type | Start Date | End Date | Premium | Coverage Amount | Deductible | Status |

Use the existing relationships for `InsuranceCompany`, `InsuranceType`, and `Status`.

Do not create duplicate fields.

---

### 8. Car Documents Card

Use the actual existing `CarDocuments` model.

The model contains:

* `Id`
* `FilePath`
* `ExpiresAt`
* `IsExpired`
* `IsActive`
* `DocumentType`
* `Car`

Display the documents in an AdminLTE card.

Suggested columns:

| Document Type | File | Expires At | Status | Active |

Use the existing `DocumentType` relationship.

`IsExpired` is a calculated property and should be used to display an appropriate status badge.

For example:

* Expired → danger badge
* Active/valid → success badge

If `FilePath` contains a valid document path, provide an appropriate View/Open action using the existing project conventions.

Do not invent a `DocumentNumber`, `IssueDate`, or other document fields because they do not exist in this model.

---

## Card Layout

Use a responsive Bootstrap/AdminLTE layout.

Suggested structure:

```text
Car Details

┌──────────────────────────────────────────────┐
│ Car Information                              │
│ VIN | Engine | Brand | Model | Plate | ... │
└──────────────────────────────────────────────┘

┌────────────────────────┐  ┌────────────────────────┐
│ Oil Schedules          │  │ Tire Schedules         │
│ table                  │  │ table                  │
└────────────────────────┘  └────────────────────────┘

┌────────────────────────┐  ┌────────────────────────┐
│ Battery Schedules      │  │ Rental Contracts       │
│ table                  │  │ table                  │
└────────────────────────┘  └────────────────────────┘

┌────────────────────────┐  ┌────────────────────────┐
│ Work Orders            │  │ Insurance              │
│ table                  │  │ table                  │
└────────────────────────┘  └────────────────────────┘

┌──────────────────────────────────────────────┐
│ Car Documents                                │
│ table                                        │
└──────────────────────────────────────────────┘
```

### Important Implementation Rules

1. Inspect the existing models before writing the views.
2. Use actual property names from the models.
3. Use existing navigation properties and EF Core relationships.
4. Do not create fake ViewModel properties just to make the UI work.
5. If a ViewModel is required, map only existing entity properties.
6. Do not add database columns.
7. Do not modify existing business logic.
8. Do not create duplicate models.
9. Do not invent routes.
10. Reuse the existing AdminLTE layout, CSS, Bootstrap, icons, and components.
11. Preserve existing authentication and authorization.
12. Use `Include` / `ThenInclude` where required to load the related data efficiently.
13. Avoid N+1 database queries.
14. Each card should show `No records found` when there are no related records.
15. Use status badges where the actual model provides a status or calculated state.
16. Make the page responsive on desktop, tablet, and mobile.
17. Keep the implementation minimal and consistent with the existing project architecture.

### Final Verification

After implementation:

* Build the project.
* Fix compilation errors.
* Verify all property names against the actual models.
* Verify all EF Core relationships.
* Verify the Cars Index → Car Details navigation.
* Verify each card displays data for the selected Car.
* Ensure there are no references to a nonexistent `Type` property.
* Ensure no invented fields or database relationships were introduced.
