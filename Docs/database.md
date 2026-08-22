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
- position
- department_id
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

## inspectors

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
Създадена

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

## extinguisher_types

Видове пожарогасители.

Статус:
Създадена

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
Създадена

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
- QR код
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
- inspector_id
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

## users

Бъдеща таблица за потребителски акаунти.

Статус:
За обсъждане

Предназначение:

- вход в системата
- роли
- права
- управление на достъпа

Връзка:

employees → users

---

# Връзки

departments

↓

employees

↓

inspectors

employees

↓

users

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

inspectors

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

# Бъдещи разширения

- снимки
- роли
- права
- Word отчети
