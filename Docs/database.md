# Database Design

Версия: 2.3
Последна актуализация: 01.09.2026

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

# Организационен модел

Основната организационна структура е:

```text
Department
   │
   └── Position
          │
          └── Employee
```

Връзките са директни:

```text
Department 1 ──── * Position
Position   1 ──── * Employee
```

Междинна таблица между `Department` и `Position` не се използва.

---

# Таблици

## Departments

Съдържа организационните звена.

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

В UI таблицата и entity-то се представят като **„Звена“**, въпреки че техническото име остава `Department`.

Ограничения:

* `Name` е задължително поле
* `Name` има максимална дължина 100 символа

Връзки:

```text
Department 1 ──── * Position
Department 1 ──── * Room
```

CRUD операциите за `Department` са реализирани.

Поддържат се:

* Create
* Read
* Update
* Delete
* Undelete

---

## Positions

Съдържа длъжностите в организационните звена.

```text
Positions
├── Id
├── Name
├── Notes
├── DepartmentId
├── IsDeleted
├── CreatedAt
├── CreatedById
├── UpdatedAt
├── UpdatedById
├── DeletedAt
└── DeletedById
```

### DepartmentId

`DepartmentId` представлява задължителен foreign key към `Departments`.

Връзка:

```text
Department 1 ──── * Position
```

### Name

Ограничения:

* `Name` е задължително поле
* `Name` има максимална дължина 100 символа

Името на длъжността не е глобално уникално.

Уникалността се гарантира в рамките на звеното чрез:

```text
DepartmentId + Name
```

Следователно е допустимо:

```text
Звено A
    └── Монтьор

Звено B
    └── Монтьор
```

но в едно и също звено не могат да съществуват две длъжности със същото име.

При изтриване на звено се използва:

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
├── PositionId
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

### WorkNumber

`WorkNumber` представлява работен номер.

Ограничения:

* задължителен
* от 1 до 4 цифри
* уникален

На ниво база данни се използва уникален индекс.

### PositionId

`PositionId` представлява задължителен foreign key към `Positions`.

Връзката е:

```text
Position 1 ──── * Employee
```

Служителят **няма собствен `DepartmentId`**.

Звеното на служителя се определя чрез неговата длъжност:

```text
Employee
   ↓
Position
   ↓
Department
```

Това предотвратява дублиране на организационната зависимост.

### Бизнес зависимост

Служителят не може да съществува без валидна длъжност.

Длъжността от своя страна не може да съществува без валидно звено.

Следователно организационната зависимост е:

```text
Department
    ↓
Position
    ↓
Employee
```

При изтриване на длъжност се използва:

```text
DeleteBehavior.Restrict
```

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
   ├── Position
   │      │
   │      └── Employee
   │             │
   │             └── Checker
   │
   └── Room
          │
          └── RoomRequirement
                       │
                       └── ExtinguisherType
                              │
                              └── Extinguisher
```

По-подробно:

```text
Department
    │
    ├── Position
    │      │
    │      └── Employee
    │             │
    │             └── Checker
    │
    └── Room
           │
           └── RoomRequirement
                    │
                    └── ExtinguisherType
                           │
                           └── Extinguisher
```

---

# Индекси и ограничения

В текущия модел са реализирани следните важни ограничения:

| Entity          | Ограничение                            |
| --------------- | -------------------------------------- |
| Employee        | уникален `WorkNumber`                  |
| Position        | уникален `DepartmentId + Name`         |
| Checker         | уникален `EmployeeId`                  |
| RoomRequirement | уникален `RoomId + ExtinguisherTypeId` |
| RoomRequirement | `RequiredCount` между 1 и 30           |

Няма глобален unique index върху `Position.Name`, тъй като една и съща длъжност може да съществува в различни звена.

---

# Referential Integrity

За основните зависимости се използва:

```text
DeleteBehavior.Restrict
```

Целта е да не се допуска автоматично каскадно физическо изтриване на свързани бизнес данни.

Това е особено важно при използването на Soft Delete.

Основните зависимости са:

```text
Department → Position
Position → Employee
Department → Room
Employee → Checker
Room → RoomRequirement
ExtinguisherType → RoomRequirement
ExtinguisherType → Extinguisher
```

---

# Global Query Filter

Entity-тата, наследяващи `SoftDeletableEntity`, използват Global Query Filter:

```text
IsDeleted == false
```

Следователно стандартните заявки работят само с активните спрямо Soft Delete записи.

Изтритите записи могат да бъдат достъпени чрез специализирани repository операции, използващи:

```text
IgnoreQueryFilters()
```

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

Примери:

```text
IDepartmentService
DepartmentService

IEmployeeService
EmployeeService
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
* промяна на foreign keys
* промяна на relationships
* промяна на database constraints

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

Към 01.09.2026 основният организационен модел е:

```text
Department
   ↓
Position
   ↓
Employee
```

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
* директна връзка `Department → Position`
* директна връзка `Position → Employee`
* `Department` CRUD
* `DepartmentService`
* `IDepartmentService`
* `Create`
* `Read`
* `Update`
* `Delete`
* `Undelete`

Междинният модел между звена и длъжности не е част от текущата база данни.

Следващият функционален етап е изграждането на **Position CRUD в контекста на конкретно звено**.
