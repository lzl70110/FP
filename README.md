# FP

Система за управление и контрол на пожарогасители.

## Описание

FP е ASP.NET Core MVC приложение за управление на пожарогасители, проверки, помещения, изисквания и служители.

Системата позволява:

- Управление на цехове
- Управление на служители
- Управление на длъжности
- Управление на помещения
- Управление на пожарогасители
- Управление на проверки
- Проследяване на история на промените
- Генериране на отчети
- Управление на потребители и роли
- QR кодове за пожарогасители

---

## Технологии

- ASP.NET Core 8 MVC
- Entity Framework Core
- SQL Server
- ASP.NET Core Identity
- Bootstrap
- xUnit

---

## Структура на проекта

```text
FP
├── FP.Domain
├── FP.Infrastructure
├── FP.Web
├── FP.Tests
└── Docs
```

### FP.Domain

Съдържа:

- Entities
- Enums
- Contracts
- Constants
- Base класове

### FP.Infrastructure

Съдържа:

- Data Access
- Repositories
- Services
- Configurations
- Seed данни

### FP.Web

Съдържа:

- Controllers
- Views
- Identity
- Authentication
- Authorization

### FP.Tests

Съдържа:

- Unit Tests
- Integration Tests

### Docs

Съдържа проектната документация.

---

## Основни модули

### Администрация

- Цехове
- Служители
- Длъжности
- Потребители
- Роли
- Права за достъп

### Пожарогасители

- Видове пожарогасители
- Регистър на пожарогасители
- Местоположение
- QR кодове

### Проверки

- Проверки
- Инспектори
- Снимки
- Констатации

### Отчети

- Word отчети
- PDF отчети
- Excel отчети

### Одит

- Audit Log
- История на промените

---

## База данни

Основни таблици:

- Departments
- Employees
- Positions
- DepartmentPositions
- Rooms
- Checkers
- ExtinguisherTypes
- RoomRequirements
- Extinguishers
- Inspections
- Identity Users

## Документация

Проектната документация се намира в папка:

```text
Docs/
```

Основни файлове:

- Index.md
- Architecture.md
- Database.md
- Roadmap.md
- Decisions.md

---

## Бъдещи разширения

- Качване на снимки
- QR кодове
- Word отчети
- PDF отчети
- Excel отчети
- Dashboard
- Статистики
- Email известия
- Audit Log разширения

---

## Лиценз

Проектът се разпространява под лиценза, описан във файла:

LICENSE.txt
