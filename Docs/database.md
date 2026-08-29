# Database Design

Версия: 2.2
Последна актуализация: 30.08.2026

Документът описва структурата на базата данни на системата за управление и проверка на пожарогасители.

Базата данни се реализира чрез:

* PostgreSQL
* Entity Framework Core
* Npgsql
* `AppDbContext`
* Repository Pattern
* Soft Delete
* Audit Tracking

---

# Основни принципи

Базата данни трябва да осигурява:

* уникалност на данните
* минимално дублиране
* преизползване на данните
* референтна цялост
* проследимост на промените
* възможност за разширяване
* Soft Delete вместо физическо изтриване, когато е приложимо

Основните бизнес entity-та наследяват `SoftDeletableEntity`.

---

# Базови Entity класове

## BaseEntity

Базовият клас съдържа:

```text
BaseEntity
└── Id
```

`Id` е първичен ключ на entity-то.

---

## AuditableEntity

Одитируемите entity-та наследяват:

```text
BaseEntity
├── CreatedAt
├── CreatedById
├── UpdatedAt
└── UpdatedById
```

Тези свойства позволяват проследяване на създаването и последната промяна на записа.

---

## SoftDeletableEntity

Бизнес entity-тата, за които е приложимо Soft Delete, наследяват:

```text
AuditableEntity
├── IsDeleted
├── DeletedAt
└── DeletedById
```

Изтриването не премахва физически записа от базата данни.

Изтритите записи се изключват от нормалните заявки чрез Global Query Filter.

За достъп до изтрити записи се използва `IgnoreQueryFilters()`.

---

# Таблици

## Departments

Съдържа организационните отдели.

```text
Departments
├── Id
├── Name
├── Notes
├── IsActive
├── IsDeleted
├── CreatedAt
├── CreatedById
├── UpdatedAt
├── UpdatedById
├── DeletedAt
└── DeletedById
```

Ограничения:

* `Name` е задължително поле
* `Name` има максимална дължина 100 символа

CRUD операциите за `Department` са реализирани.

Поддържат се:

* Create
* Read
* Update
* Delete
* Undelete

---

## Positions

Съдържа длъжностите.

```text
Positions
├── Id
├── Name
├── Notes
├── IsDeleted
├── CreatedAt
├── CreatedById
├── UpdatedAt
├── UpdatedById
├── DeletedAt
└── DeletedById
```

Ограничения:

* `Name` е задължително поле
* `Name` има максимална дължина 100 символа

---

## DepartmentPositions

Свързва отдели и длъжности.

```text
DepartmentPositions
├── Id
├── DepartmentId
├── PositionId
└── Soft Delete / Audit fields
```

Връзки:

```text
Department 1 ──── * DepartmentPosition * ──── 1 Position
```

Комбинацията:

```text
DepartmentId + PositionId
```

е уникална.

Това предотвратява дублиране на една и съща длъжност в един и същи отдел.

При изтриване на отдел или длъжност се използва:

```text
DeleteBehavior.Restrict
```

---

## Employees

Съдържа служителите.

```text
Employees
├── Id
├── WorkNumber
├── FirstName
├── MiddleName
├── LastName
├── DepartmentId
├── PositionId
├── Notes
├── IsActive
└── Soft Delete / Audit fields
```

### WorkNumber

`WorkNumber` представлява работен номер.

Ограничения:

* задължителен
* от 1 до 4 цифри
* уникален

На ниво база данни се използва уникален индекс.

### Връзки

```text
Department 1 ──── * Employee
Position   1 ──── * Employee
```

Служителят задължително принадлежи към:

* `Department`
* `Position`

Връзките се реализират чрез foreign keys.

При изтриване на свързан отдел или длъжност се използва:

```text
DeleteBehavior.Restrict
```

### Бизнес зависимост

Създаването на служител е зависимо от наличието на валидни:

* отдел
* длъжност

На ниво база данни тази зависимост се подсигурява чрез foreign keys.

Допълнителното правило за наличност на необходимите зависимости при работа с UI се реализира на application/UI ниво.

---

## Checkers

Съдържа служителите, които имат право да извършват проверки.

```text
Checkers
├── Id
├── EmployeeId
├── IsActive
└── Soft Delete / Audit fields
```

Връзка:

```text
Employee 1 ──── 0..1 Checker
```

`EmployeeId` е уникален.

Това гарантира, че един служител не може да бъде регистриран като `Checker` повече от веднъж.

При изтриване на служител се използва:

```text
DeleteBehavior.Restrict
```

---

## Rooms

Съдържа помещенията.

```text
Rooms
├── Id
├── Name
├── IsActive
├── Notes
├── DepartmentId
└── Soft Delete / Audit fields
```

Връзка:

```text
Department 1 ──── * Room
```

Ограничения:

* `Name` е задължително
* `Name` има максимална дължина 100 символа
* `Notes` има максимална дължина 150 символа

При изтриване на отдел се използва:

```text
DeleteBehavior.Restrict
```

---

## ExtinguisherTypes

Съдържа видовете пожарогасители.

```text
ExtinguisherTypes
├── Id
├── Name
├── Description
└── Soft Delete / Audit fields
```

Ограничения:

* `Name` е задължително
* `Name` има максимална дължина 30 символа
* `Description` има максимална дължина 500 символа

---

## Extinguishers

Съдържа конкретните пожарогасители.

```text
Extinguishers
├── Id
├── ExtinguisherTypeId
└── Soft Delete / Audit fields
```

Връзка:

```text
ExtinguisherType 1 ──── * Extinguisher
```

При изтриване на вид пожарогасител се използва:

```text
DeleteBehavior.Restrict
```

---

## RoomRequirements

Съдържа изискванията за пожарогасители в помещенията.

```text
RoomRequirements
├── Id
├── RoomId
├── ExtinguisherTypeId
├── RequiredCount
├── Notes
├── IsActive
└── Soft Delete / Audit fields
```

Връзки:

```text
Room 1 ──── * RoomRequirement
ExtinguisherType 1 ──── * RoomRequirement
```

### RequiredCount

`RequiredCount` представлява минималният необходим брой пожарогасители от даден вид.

Ограничението на ниво база данни е:

```text
RequiredCount BETWEEN 1 AND 30
```

### Уникалност

Комбинацията:

```text
RoomId + ExtinguisherTypeId
```

е уникална.

Това гарантира, че за едно помещение не може да има две отделни изисквания за един и същи вид пожарогасител.

При изтриване на свързаните записи се използва:

```text
DeleteBehavior.Restrict
```

---

# Основни връзки

Обобщена структура:

```text
Department
   │
   ├── Employee
   │      │
   │      └── Checker
   │
   ├── Room
   │      │
   │      └── RoomRequirement
   │                 │
   │                 └── ExtinguisherType
   │
   └── DepartmentPosition
              │
              └── Position
                     │
                     └── Employee


ExtinguisherType
   │
   ├── Extinguisher
   │
   └── RoomRequirement
```

---

# Индекси и ограничения

В текущия модел са реализирани следните важни ограничения:

| Entity             | Ограничение                            |
| ------------------ | -------------------------------------- |
| Employee           | уникален `WorkNumber`                  |
| Checker            | уникален `EmployeeId`                  |
| DepartmentPosition | уникален `DepartmentId + PositionId`   |
| RoomRequirement    | уникален `RoomId + ExtinguisherTypeId` |
| RoomRequirement    | `RequiredCount` между 1 и 30           |

---

# Referential Integrity

За основните зависимости се използва:

```text
DeleteBehavior.Restrict
```

Целта е да не се допуска автоматично каскадно физическо изтриване на свързани бизнес данни.

Това е особено важно при използването на Soft Delete.

---

# Global Query Filter

Entity-тата, наследяващи `SoftDeletableEntity`, използват Global Query Filter:

```text
IsDeleted == false
```

Следователно стандартните заявки работят само с незаличените записи.

Изтритите записи могат да бъдат достъпени чрез специализирани repository операции, използващи `IgnoreQueryFilters()`.

---

# Entity Framework Core Configurations

Конфигурацията на entity-тата е отделена от `AppDbContext`.

Използва се:

```text
IEntityTypeConfiguration<TEntity>
```

Конфигурациите се зареждат автоматично чрез:

```text
ApplyConfigurationsFromAssembly()
```

Основните конфигурации са:

```text
DepartmentConfiguration
PositionConfiguration
DepartmentPositionConfiguration
EmployeeConfiguration
CheckerConfiguration
RoomConfiguration
ExtinguisherConfiguration
ExtinguisherTypeConfiguration
RoomRequirementConfiguration
```

---

# Repository и CRUD слой

Достъпът до entity-тата е организиран чрез Repository Pattern.

Използва се generic repository:

```text
IRepository<TEntity>
Repository<TEntity>
```

Стандартните CRUD операции са централизирани чрез:

```text
ICrudService<TEntity>
CrudService<TEntity>
```

Поддържаните операции са:

```text
Create
Read
Update
Delete
Undelete
```

За entity-та със специфична бизнес логика могат да се използват entity-specific services.

Пример:

```text
IDepartmentService
DepartmentService
```

Този подход позволява общата CRUD логика да не се дублира между отделните модули.

---

# Миграции

Промените в структурата на базата данни се управляват чрез Entity Framework Core Migrations.

Миграциите се използват за:

* създаване на таблици
* промяна на колони
* добавяне на индекси
* добавяне на ограничения
* промяна на връзки
* преименуване на таблици и индекси

Базата данни използва PostgreSQL чрез Npgsql.

---

# Бъдещо развитие

Планирани разширения:

* телефонни номера на служителите
* проверки
* история на проверките
* снимки
* QR кодове
* потребители
* роли
* права
* подробен Audit Log
* отчети
* допълнителни бизнес правила

Моделът трябва да позволява тези разширения без ненужно преработване на съществуващите таблици.

---

# Текущ статус

Към версия 2.2 са реализирани основните entity модели и техните EF Core конфигурации.

Реализирани са:

* PostgreSQL
* Entity Framework Core
* Npgsql
* базови entity класове
* Soft Delete
* Global Query Filter
* Repository Pattern
* Generic CRUD service
* Entity configurations
* foreign keys
* unique indexes
* database check constraint за `RequiredCount`
* `Department` CRUD
* `DepartmentService`
* `IDepartmentService`
* `Create`
* `Read`
* `Update`
* `Delete`
* `Undelete`

За `Employee` е дефиниран модел със задължителни зависимости към `Department` и `Position`.

Следващите бизнес модули ще се изграждат върху тази основа.
