# Пътна карта

**Версия: 2.2**
**Последна актуализация: 01.09.2026**

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
* организационен модел **Звено → Длъжност → Служител**
* директна връзка `Department → Position`
* директна връзка `Position → Employee`
* уникалност на длъжност в рамките на звено

---

# Версия 0.1 — Основа

**Статус: Завършена**

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

**Статус: В процес**

## Звена

* [x] Department CRUD
* [x] Create
* [x] Read / Details
* [x] Update
* [x] Soft Delete
* [x] Deleted records
* [x] Undelete
* [x] CRUD result handling
* [x] Management view
* [x] Navigation към Длъжности
* [x] Navigation към Служители
* [ ] Отделен екран за Audit информация

## Длъжности

* [x] Position entity
* [x] Position configuration
* [x] `DepartmentId`
* [x] `Department` navigation
* [x] Unique `(DepartmentId, Name)`
* [ ] Position CRUD
* [ ] UI управление на длъжности в конкретно звено

## Служители

* [x] Employee entity
* [x] Employee configuration
* [x] `PositionId`
* [x] `Position` navigation
* [x] WorkNumber validation
* [x] Unique WorkNumber
* [ ] Employee CRUD
* [ ] UI управление на служители в конкретно звено

## Проверяващи лица

* [x] Checker entity
* [x] Checker configuration
* [x] Unique EmployeeId
* [ ] Checker CRUD

---

# Организационен модел

Текущата организационна структура е:

```text
Звено
  │
  └── Длъжност
        │
        └── Служител
```

В базата това се реализира чрез:

```text
Department
    │
    └── Position
          │
          └── Employee
```

`Employee` не съдържа директна връзка към `Department`.

Звеното на служителя се определя чрез неговата длъжност.

---

# Бизнес правило — Служители

Създаването на служител зависи от наличието на организационен контекст.

Служител трябва да бъде свързан с валидна длъжност.

Длъжността трябва да принадлежи към конкретно звено.

В UI основният поток е:

```text
Звена
   ↓
Конкретно звено
   ↓
Управление
   ├── Длъжности
   └── Служители
```

Не се предвижда глобално управление на служители извън организационния контекст.

Окончателното бизнес правило трябва да бъде защитено и на ниво Application/Service logic.

---

# Версия 0.3 — Помещения

**Статус: Domain и Infrastructure са подготвени**

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

**Статус: Domain и Infrastructure са подготвени**

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

**Статус: Планирана**

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

**Статус: Планирана**

* [ ] ASP.NET Core Identity
* [ ] Потребители
* [ ] Връзка Employee ↔ IdentityUser
* [ ] Роли
* [ ] Права
* [ ] Авторизация
* [ ] Ограничения по роли
* [ ] Ограничаване на административните функции

---

# Версия 0.7 — Одит

**Статус: Основата е реализирана**

* [x] CreatedAt
* [x] UpdatedAt
* [x] DeletedAt
* [x] CreatedById
* [x] UpdatedById
* [x] DeletedById
* [ ] Свързване с Identity
* [ ] Отделен екран за Audit информация
* [ ] Подробен Audit Log
* [ ] История на промените

---

# Версия 0.8 — Отчети

**Статус: Планирана**

* [ ] Word отчети
* [ ] PDF отчети
* [ ] Excel отчети
* [ ] Отчети за пожарогасители
* [ ] Отчети за проверки
* [ ] Отчети по помещения
* [ ] Отчети по звена

---

# Версия 0.9 — Допълнителни функционалности

**Статус: Планирана**

* [ ] Снимки към пожарогасители
* [ ] Снимки към проверки
* [ ] QR кодове
* [ ] Dashboard
* [ ] Статистики
* [ ] Известия
* [ ] Автоматично следене на предстоящи проверки

---

# Версия 1.0 — Първа производствена версия

**Статус: Бъдеща**

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

**Приоритет: Среден**

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

1. **Position CRUD**
2. UI за управление на длъжности в конкретно звено
3. **Employee CRUD**
4. UI за управление на служители в конкретно звено
5. **Checker CRUD**
6. **Room CRUD**
7. **RoomRequirement CRUD**
8. **ExtinguisherType CRUD**
9. **Extinguisher CRUD**

След това:

10. Общ Bootstrap модален прозорец за резултатите от CRUD операциите
11. Audit екран
12. Подготовка на Identity и авторизация

Модалният прозорец трябва да поддържа:

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

---

# Правило за документацията

Документацията трябва да описва реалното състояние на проекта.

При промяна на архитектурата се синхронизират:

```text
architecture.md
database.md
decisions.md
roadmap.md
index.md
```

Планираните функционалности се отбелязват като такива и не се представят като реализирани.

---

# Технически наблюдения

## Забавяне при първоначално отваряне

При първоначално отваряне на `Departments/Index` локално се наблюдава
забавяне приблизително 1–3 секунди, въпреки че базата данни е празна.

Към момента това не се счита за дефект.

Възможни причини:

* първоначална инициализация на приложението
* инициализация на Entity Framework Core
* установяване на Npgsql/PostgreSQL връзката
* мрежова латентност

При бъдещо хостване на приложението в Render с PostgreSQL в Neon
забавянето може да бъде по-осезаемо, особено при първоначално
стартиране или събуждане на приложението.

### Предвидено действие

* измерване на времето след deployment в Render
* локализиране на причината при наличие на реален проблем
* оптимизация само след измерване

**Статус: Наблюдение**