# Пътна карта

Версия: 1.0
Последна актуализация: 22.08.2026

---

## Дневник на проекта

### 22.08.2026

#### Документация

- [x] Създаден architecture.md
- [x] Създаден database.md
- [x] Създаден ideas.md
- [x] Създаден roadmap.md

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

## Версия 0.1 - Основа

Статус: В процес

### Документация

- [x] Архитектура
- [x] Дизайн на база данни
- [x] Идеи
- [x] Пътна карта

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
- [ ] Repository Pattern

---

## Версия 0.2 - Служители

- [ ] Employee
- [ ] Checker
- [ ] EmployeePhone

---

## Версия 1.0

- [ ] Първа производствена версия
