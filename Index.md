# FP Documentation Index

## Общ преглед

* README.md

---

## Проектиране

* architecture.md
* database.md
* decisions.md
* ideas.md
* roadmap.md

---

## Бизнес модули

### Администрация

* Звена
* Длъжности
* Служители
* Проверяващи
* Потребители
* Роли
* Права

### Пожарогасители

* Видове пожарогасители
* Регистър на пожарогасителите
* Изисквания за пожарогасители по помещения
* QR кодове

### Проверки

* Проверки
* История на проверките
* Снимки

### Отчети

* Word
* PDF
* Excel

### Одит

* Audit информация
* Audit Log
* История на промените

---

# Текущо реализирано

## Домейн модел

* `Department`
* `Position`
* `Employee`
* `Checker`
* `Room`
* `RoomRequirement`
* `ExtinguisherType`
* `Extinguisher`

### Организационна структура

```text
Department
    │
    └── Position
          │
          └── Employee
```

`Employee` е свързан с `Position`.

`Position` е свързана с `Department`.

По този начин звеното на служителя се определя чрез неговата длъжност.

---

## Общи базови класове

* `BaseEntity`
* `AuditableEntity`
* `SoftDeletableEntity`

---

## Infrastructure

* Entity configurations
* Generic repository
* Generic CRUD service
* Soft delete
* Restore на изтрити записи
* Global query filter за soft-deleted entities
* Audit timestamps
* PostgreSQL чрез Entity Framework Core и Npgsql
* EF Core migrations

---

# CRUD

## Звена

Реализирани:

* Index
* Details
* Create
* Edit
* Delete
* Deleted
* Undelete
* CRUD result handling
* Management view

От страницата на конкретно звено е достъпно управление на:

* Длъжности
* Служители
* Звеното

Одитната информация ще бъде достъпна чрез отделен екран.

---

## Длъжности

Реализирани:

* `Position` entity
* `Position` configuration
* връзка към `Department`
* `DepartmentId`
* уникалност на `(DepartmentId, Name)`

Предстои:

* Position CRUD
* UI за управление на длъжности в конкретно звено

---

## Служители

Реализирани:

* `Employee` entity
* `Employee` configuration
* връзка към `Position`
* `PositionId`
* WorkNumber validation
* Unique WorkNumber
* CRUD service
* Controller
* DI регистрация

Предстои:

* Employee CRUD UI
* управление на служители в контекста на конкретно звено

---

## Проверяващи

Реализирани:

* `Checker` entity
* `Checker` configuration
* Unique `EmployeeId`

Предстои:

* Checker CRUD

---

# Валидация и бизнес правила

## Звена

* Наименование: задължително
* Наименование: до 100 символа
* Забележка: до 1000 символа

## Длъжности

* Наименование: задължително
* Наименование: до 100 символа
* Забележка: до 1000 символа
* Длъжността принадлежи към конкретно звено
* Името на длъжността е уникално в рамките на звеното

## Служители

* Работен номер: задължителен
* Работен номер: от 1 до 4 цифри
* Работният номер е уникален
* Име: 2–100 символа
* Презиме: 2–100 символа, по желание
* Фамилия: 2–100 символа
* Забележка: до 1000 символа
* Служителят е свързан с валидна длъжност
* Длъжността принадлежи към валидно звено

## Помещения

* Наименование: задължително
* Наименование: до 100 символа
* Забележка: до 150 символа
* Помещението принадлежи към звено

## Изисквания за пожарогасители

* Изискването е свързано с конкретно помещение
* Изискването е свързано с конкретен вид пожарогасител
* Брой пожарогасители: от 1 до 30
* За едно помещение не може да има две еднакви изисквания за един и същ вид пожарогасител
* Забележка: до 500 символа

## Видове пожарогасители

* Наименование: задължително
* Наименование: до 30 символа
* Описание: до 500 символа

## Проверяващи

* Един служител може да бъде регистриран като проверяващ само веднъж

---

# База данни

## Основни таблици

* `Departments`
* `Positions`
* `Employees`
* `Checkers`
* `Rooms`
* `RoomRequirements`
* `ExtinguisherTypes`
* `Extinguishers`

---

## Основни връзки

```text
Departments
    │
    ├── Positions
    │      │
    │      └── Employees
    │
    └── Rooms
           │
           └── RoomRequirements
                  │
                  └── ExtinguisherTypes
```

---

## Реализирани ограничения

* Foreign Key ограничения
* `DeleteBehavior.Restrict`
* Unique indexes
* Composite unique index за `RoomRequirements`
* Composite unique index за `Positions (DepartmentId, Name)`
* Unique `WorkNumber`
* Unique `Checker.EmployeeId`
* Database check constraint за `RoomRequirements.RequiredCount`
* Максимални дължини на текстовите полета

---

# Soft Delete

Основните домейн entities използват `SoftDeletableEntity`.

При soft delete:

* записът не се изтрива физически;
* задава се `IsDeleted`;
* записват се `DeletedAt` и `DeletedById`;
* стандартните заявки не връщат изтритите записи;
* изтритите записи могат да бъдат извлечени чрез `GetDeletedAsync`;
* записите могат да бъдат възстановени чрез `Undelete`.

---

# Одит

В домейн модела е реализирана основа за audit tracking:

* `CreatedAt`
* `CreatedById`
* `UpdatedAt`
* `UpdatedById`
* `DeletedAt`
* `DeletedById`

Предстои:

* отделен Audit екран
* свързване с Identity
* подробен Audit Log
* история на промените

---

# Планирани бизнес правила

* Управлението на длъжности да се извършва в контекста на конкретно звено
* Управлението на служители да се извършва в контекста на конкретно звено
* Създаването на служител да изисква валидна длъжност
* Длъжността на служителя да принадлежи към избраното звено
* Бизнес правилата да бъдат защитени на ниво Application/Service logic
* Универсален Bootstrap modal за резултатите от CRUD операциите:

  * Success
  * Info
  * Warning
  * Error

---

# Бъдещи разширения

* Проверки на пожарогасители
* История на проверките
* Автоматично изчисляване на следваща проверка
* Проверка за изтичащи ревизии
* Български официални празници и почивни дни
* Снимки
* QR кодове
* Потребители
* Роли
* Права
* Word отчети
* PDF отчети
* Excel отчети
* Подробен Audit Log
* История на промените
* Dashboard
* Статистики
* Известия

---

# Работни принципи

* Бизнес логиката не се дублира между Controller-и
* CRUD операциите използват generic CRUD service
* Entity-specific логиката остава в съответния service
* Валидацията се прилага както на ниво приложение, така и на ниво база данни, когато е необходимо
* Fluent API е авторитетът за database/schema constraints
* Data Annotations се използват за MVC/UI validation
* Soft delete се използва вместо физическо изтриване
* Referential integrity се защитава чрез `DeleteBehavior.Restrict`
* Документацията описва реалното състояние на проекта
* Планираните функционалности се отделят ясно от реализираните
* При архитектурна промяна се синхронизират всички зависими документи

---

# Документационна структура

```text
README.md
    │
    └── index.md
          │
          ├── architecture.md
          ├── database.md
          ├── decisions.md
          ├── ideas.md
          └── roadmap.md
```

Документите имат различни роли:

```text
ideas.md
    ↓
Какво бихме могли да направим?

decisions.md
    ↓
Какво решихме?

architecture.md
    ↓
Как е устроена системата?

database.md
    ↓
Как е устроена базата?

roadmap.md
    ↓
Какво ще реализираме и кога?

index.md
    ↓
Къде се намира всичко?
```
