# Database Design Draft

## Основна идея

Системата е предназначена за отчитане и проверка на пожарогасители.

Данните трябва да бъдат:

- преизползваеми
- проследими
- разширяеми

Документът представлява работен проект на структурата на базата данни и подлежи на допълнително развитие.

---

# Основни таблици

## departments

Цехове и организационни звена.

Статус:
Планирана

Основни полета:

- id
- name
- notes
- is_active

Audit:

- created_at
- created_by
- updated_at
- updated_by

---

## positions

Справочник с длъжности.

Статус:
Планирана

Основни полета:

- id
- name
- notes
- is_active

Audit:

- created_at
- created_by
- updated_at
- updated_by

---

## department_positions

Свързваща таблица между цехове и длъжности.

Статус:
Планирана

Основни полета:

- id
- department_id
- position_id
- is_active

Audit:

- created_at
- created_by
- updated_at
- updated_by

---

## employees

Обща таблица за всички служители.

Статус:
Планирана

Основни полета:

- id
- work_number
- first_name
- middle_name
- last_name
- department_position_id
- notes
- is_active

Audit:

- created_at
- created_by
- updated_at
- updated_by

Забележка:

Всеки служител принадлежи към точно един цех.

---

## employee_phones

Телефонни номера на служителите.

Статус:
Планирана

Основни полета:

- id
- employee_id
- phone_number
- notes

Audit:

- created_at
- created_by
- updated_at
- updated_by

Забележка:

Един служител може да има повече от един телефонен номер.

---

## checkers

Представлява подмножество от employees.

Само служители от тази таблица могат да извършват проверки.

Статус:
Планирана

Основни полета:

- id
- employee_id

Audit:

- created_at
- created_by
- updated_at
- updated_by

---

## rooms

Помещения и обекти.

Статус:
Планирана

Основни полета:

- id
- name
- notes
- is_active

Audit:

- created_at
- created_by
- updated_at
- updated_by

---

## holidays

Официални празници и неработни дни.

Статус:
Планирана

Основни полета:

- id
- holiday_date
- name
- is_official_holiday
- notes

Audit:

- created_at
- created_by
- updated_at
- updated_by

Предназначение:

- изчисляване на срокове
- проверки
- известия
- работни и неработни дни

Обновяване:

- автоматично
- първия работен ден на всеки месец
- чрез Background Service

---

## holiday_sync_logs

История на синхронизациите на календара.

Статус:
Планирана

Основни полета:

- id
- started_at
- finished_at
- imported_records
- status
- error_message

Предназначение:

- проследяване на синхронизациите
- диагностика на проблеми
- одит на импорта

---

## extinguisher_types

Видове пожарогасители.

Статус:
Планирана

Основни полета:

- id
- name
- notes
- is_active

Audit:

- created_at
- created_by
- updated_at
- updated_by

---

## room_requirements

Изисквания за помещение.

Пример:

Ремонтно хале → Прах 6 кг → 5 бр.

Статус:
Планирана

Основни полета:

- id
- room_id
- extinguisher_type_id
- required_count
- notes

Audit:

- created_at
- created_by
- updated_at
- updated_by

---

## extinguishers

Основна таблица на приложението.

Съдържа реалните пожарогасители, участващи в проверки и отчети.

Статус:
Планирана

Очаквани връзки:

- room
- extinguisher_type

Основни полета:

- id
- room_id
- extinguisher_type_id
- notes
- is_active

Audit:

- created_at
- created_by
- updated_at
- updated_by

Бъдещи възможности:

- снимки
- QR кодове
- архивиране

---

## inspections

Проверки.

Статус:
Планирана

Важно:

- inspection_date е датата на проверката
- created_at е датата на създаване на записа

Проверката може да бъде въведена по-късно, но се отнася за конкретна бизнес дата.

Основни полета:

- id
- extinguisher_id
- checker_id
- inspection_date
- notes

Audit:

- created_at
- created_by
- updated_at
- updated_by

Бъдещи възможности:

- снимки
- електронно подписване
- история на промените

---

## identity_users

ASP.NET Core Identity.

Статус:
Планирана

Предназначение:

- вход в системата
- роли
- права
- управление на достъпа

Връзка:

employees → identity_users

---

# Връзки

departments

↓

department_positions

↓

positions

departments

↓

employees

↓

checkers

employees

↓

identity_users

employees

↓

employee_phones

rooms

↓

room_requirements

↓

extinguisher_types

rooms

↓

extinguishers

↓

inspections

checkers

↓

inspections

---

# Notes

Всички основни бизнес таблици съдържат поле:

- notes

Полето е предназначено за свободен текст и допълнителна информация.

Свързващите таблици по подразбиране не съдържат поле notes.

Изключение се допуска само когато свързващата таблица съдържа собствена бизнес информация.

---

# Audit

Всички основни таблици трябва да предвиждат:

- created_at
- created_by
- updated_at
- updated_by

Цел:

- проследяване на промените
- установяване на автора на записа
- установяване на последния редактор

---

# Основни принципи

- уникалност на данните
- минимално дублиране
- преизползване на данните
- проследимост на промените
- защита на личните данни
- възможност за бъдещо разширяване
- използване на справочни данни в повече от едно приложение
- soft delete вместо физическо изтриване когато е възможно

---

# Технически решения

- PostgreSQL
- Entity Framework Core
- ASP.NET Core Identity
- Generic Repository Pattern
- Soft Delete
- Audit Tracking
- Background Services

---

# Бъдещи разширения

- снимки
- QR кодове
- роли
- права
- Word отчети
- PDF отчети
- Excel отчети
- Audit Log
- календар на официалните празници
- автоматична синхронизация на празниците
