# Пътна карта

Версия: 1.0
Последна актуализация: 23.08.2026

---

## Дневник на проекта

### 22.08.2026

#### Документация

- [x] Създаден Architecture.md
- [x] Създаден Database.md
- [x] Създаден Ideas.md
- [x] Създаден Roadmap.md

#### Архитектурни решения

- [x] Избран Repository Pattern
- [x] Inspector е заменен с Checker
- [x] Въведена е структура Department → Position → DepartmentPosition
- [x] Решено е длъжностите да се съхраняват в отделна таблица
- [x] Решено е длъжностите по цехове да се съхраняват в DepartmentPosition
- [x] Планирана е поддръжка на множество телефони чрез EmployeePhone

#### Domain

- [x] BaseEntity
- [x] AuditableEntity
- [x] SoftDeletableEntity
- [x] Department
- [x] Position
- [x] DepartmentPosition

#### Инфраструктура

- [x] Git Repository
- [x] GitHub Repository
- [x] Development Branch

---

### 23.08.2026

#### Документация

- [x] Обновен README.md
- [x] Създаден Index.md
- [x] Актуализиран .gitignore
- [x] Прегледана структура на проекта

#### Инфраструктура

- [x] Създадена папка Repositories

---

## Версия 0.1 - Основа

Статус: В процес

### Документация

- [x] Архитектура
- [x] Дизайн на база данни
- [x] Идеи
- [x] Пътна карта
- [x] README
- [x] Index

### Структура на решението

- [x] FP.sln
- [x] FP.Domain
- [x] FP.Infrastructure
- [x] FP.Web
- [x] FP.Tests

### Domain

- [x] BaseEntity
- [x] AuditableEntity
- [x] SoftDeletableEntity
- [x] Department
- [x] Position
- [x] DepartmentPosition

### Infrastructure

- [ ] PostgreSQL
- [ ] Entity Framework Core
- [ ] ApplicationDbContext
- [ ] Първа миграция
- [х] Repository Pattern

---

## Версия 0.2 - Служители

### Domain

- [ ] Employee
- [ ] EmployeePhone
- [ ] Checker

### Infrastructure

- [ ] Employee Repository
- [ ] Checker Repository

### Web

- [ ] Employee CRUD
- [ ] Checker CRUD

---

## Версия 0.3 - Помещения

- [ ] Room
- [ ] RoomRequirement
- [ ] Room CRUD
- [ ] Room Requirement CRUD

---

## Версия 0.4 - Пожарогасители

- [ ] ExtinguisherType
- [ ] Extinguisher
- [ ] Extinguisher CRUD
- [ ] Търсене и филтриране

---

## Версия 0.5 - Проверки

- [ ] Inspection
- [ ] Inspection CRUD
- [ ] История на проверките

---

## Версия 0.6 - Потребители

- [ ] ASP.NET Core Identity
- [ ] Роли
- [ ] Права за достъп
- [ ] Ограничения по роли

---

## Версия 0.7 - Отчети

- [ ] Word отчети
- [ ] PDF отчети
- [ ] Excel отчети

---

## Версия 0.8 - Одит

- [ ] Audit Log
- [ ] История на промените
- [ ] Проследяване на действията

---

## Версия 0.9 - Допълнителни функционалности

- [ ] Снимки към пожарогасители
- [ ] Снимки към проверки
- [ ] QR кодове
- [ ] Dashboard
- [ ] Статистики

---

## Версия 1.0

### Първа производствена версия

- [ ] Финален преглед
- [ ] Тестване
- [ ] Поправка на дефекти
- [ ] Подготовка за публикуване
- [ ] Production Release  
 
 ## FP.Tools

Приоритет: Среден

- [ ] Project Tree Generator
- [ ] Class Locator
- [ ] Entity Report Generator
- [ ] Documentation Helper