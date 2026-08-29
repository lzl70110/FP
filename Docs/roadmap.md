# Пътна карта

Версия: 2.1
Последна актуализация: 30.08.2026

---

# Текущо състояние

Проектът е в етап на изграждане на основната бизнес функционалност.

Реализирани са:

* архитектура на решението
* Domain модел
* PostgreSQL база данни
* Entity Framework Core
* EF Core Configurations
* Repository Pattern
* Generic CRUD Service
* Soft Delete
* Global Query Filter
* Audit Tracking основа
* Department CRUD
* Bootstrap базов UI
* общ Layout и вертикална навигация
* CRUD резултати чрез TempData и общ модел за съобщения

---

# Версия 0.1 — Основа

Статус: Завършена

### Документация

* [x] Architecture.md
* [x] Database.md
* [x] Ideas.md
* [x] Roadmap.md
* [x] README.md
* [x] Index.md

### Структура на решението

* [x] FP.sln
* [x] FP.Domain
* [x] FP.Application
* [x] FP.Infrastructure
* [x] FP.Web
* [x] FP.Tests

### Domain

* [x] BaseEntity
* [x] AuditableEntity
* [x] SoftDeletableEntity
* [x] Department
* [x] Position
* [x] DepartmentPosition
* [x] Employee
* [x] Checker
* [x] Room
* [x] RoomRequirement
* [x] ExtinguisherType
* [x] Extinguisher

### Infrastructure

* [x] PostgreSQL
* [x] Entity Framework Core
* [x] Npgsql
* [x] AppDbContext
* [x] EF Core Migrations
* [x] Entity Configurations
* [x] Repository Pattern
* [x] Generic Repository
* [x] Global Query Filter
* [x] Soft Delete

---

# Версия 0.2 — Администрация

Статус: В процес

## Отдели

* [x] Department CRUD
* [x] Create
* [x] Read / Details
* [x] Update
* [x] Soft Delete
* [x] Deleted records
* [x] Undelete
* [x] CRUD result handling

## Длъжности

* [x] Position entity
* [x] Position configuration
* [ ] Position CRUD

## Връзка отдели — длъжности

* [x] DepartmentPosition entity
* [x] DepartmentPosition configuration
* [x] Unique constraint за Department + Position
* [ ] UI за управление

## Служители

* [x] Employee entity
* [x] Employee configuration
* [x] WorkNumber validation
* [x] Unique WorkNumber
* [ ] Employee CRUD

## Проверяващи лица

* [x] Checker entity
* [x] Checker configuration
* [x] Unique EmployeeId
* [ ] Checker CRUD

---

# Бизнес правило — Служители

Създаването на служител зависи от наличието на организационна структура.

Служител може да бъде създаден само когато съществуват:

* поне един отдел
* поне една длъжност

В UI:

* менюто за служители не се показва, ако липсва необходимата структура
* окончателното бизнес правило трябва да бъде защитено и на ниво application/service logic

Това правило предотвратява създаването на служители без валиден организационен контекст.

---

# Версия 0.3 — Помещения

Статус: Domain и Infrastructure са подготвени

* [x] Room entity
* [x] Room configuration
* [x] RoomRequirement entity
* [x] RoomRequirement configuration
* [x] RequiredCount constraint
* [x] Unique Room + ExtinguisherType
* [ ] Room CRUD
* [ ] RoomRequirement CRUD
* [ ] UI за управление на помещенията

---

# Версия 0.4 — Пожарогасители

Статус: Domain и Infrastructure са подготвени

* [x] ExtinguisherType entity
* [x] ExtinguisherType configuration
* [x] Extinguisher entity
* [x] Extinguisher configuration
* [ ] ExtinguisherType CRUD
* [ ] Extinguisher CRUD
* [ ] Търсене
* [ ] Филтриране
* [ ] Статуси
* [ ] Годност
* [ ] Ревизии

---

# Версия 0.5 — Проверки

Статус: Планирана

* [ ] Inspection entity
* [ ] Inspection configuration
* [ ] Inspection CRUD
* [ ] Дата на проверка
* [ ] Следваща проверка
* [ ] Проверяващо лице
* [ ] История на проверките
* [ ] Проверка на работни дни
* [ ] Официални празници

---

# Версия 0.6 — Потребители и сигурност

Статус: Планирана

* [ ] ASP.NET Core Identity
* [ ] Потребители
* [ ] Роли
* [ ] Права
* [ ] Авторизация
* [ ] Ограничения по роли
* [ ] Ограничаване на административните функции

---

# Версия 0.7 — Одит

Статус: Основата е реализирана

* [x] CreatedAt
* [x] UpdatedAt
* [x] DeletedAt
* [x] CreatedById
* [x] UpdatedById
* [x] DeletedById
* [ ] Свързване с Identity
* [ ] Подробен Audit Log
* [ ] История на промените

---

# Версия 0.8 — Отчети

Статус: Планирана

* [ ] Word отчети
* [ ] PDF отчети
* [ ] Excel отчети
* [ ] Отчети за пожарогасители
* [ ] Отчети за проверки
* [ ] Отчети по помещения
* [ ] Отчети по отдели

---

# Версия 0.9 — Допълнителни функционалности

Статус: Планирана

* [ ] Снимки към пожарогасители
* [ ] Снимки към проверки
* [ ] QR кодове
* [ ] Dashboard
* [ ] Статистики
* [ ] Известия
* [ ] Автоматично следене на предстоящи проверки

---

# Версия 1.0 — Първа производствена версия

Статус: Бъдеща

* [ ] Финален преглед
* [ ] Пълно тестване
* [ ] Integration Tests
* [ ] Поправка на дефекти
* [ ] Проверка на сигурността
* [ ] Проверка на бизнес правилата
* [ ] Финализиране на UI
* [ ] Подготовка за deployment
* [ ] Production Release

---

# FP.Tools

Приоритет: Среден

Помощен проект за разработка и поддръжка.

* [ ] Project Tree Generator
* [ ] Class Locator
* [ ] Entity Report Generator
* [ ] Documentation Helper
* [ ] Database Model Report

FP.Tools не е част от основната бизнес функционалност и може да бъде разработван независимо.

---

# Архитектурни зависимости

Развитието на системата следва приблизително следния ред:

```text
Domain
   ↓
Infrastructure
   ↓
Application
   ↓
Web
   ↓
UI / Business Modules
```

Бизнес модулите се изграждат върху вече реализираната обща инфраструктура.

---

# Следващ непосредствен етап

След приключване на документацията следва:

1. Position CRUD
2. DepartmentPosition UI
3. Employee CRUD
4. Checker CRUD
5. Room CRUD
6. RoomRequirement CRUD
7. ExtinguisherType CRUD
8. Extinguisher CRUD

След приключване на основните CRUD модули ще бъде реализиран общ Bootstrap модален прозорец за резултатите от CRUD операциите:

* Success
* Info
* Warning
* Error

Целта е да няма отделна логика за съобщенията във всеки View.

---

# Правило за Git

Преди по-големи или рискови промени се създава Git commit.

При завършване на функционален етап се препоръчва:

```text
Build → Test → Commit → Push
```

Документацията също се актуализира като част от завършването на значим етап.
