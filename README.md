<div align="center">

# 📒 Mokes.API

**REST API для управления личными записями с JWT-аутентификацией**

![.NET](https://img.shields.io/badge/.NET_8-512BD4?style=flat-square&logo=dotnet&logoColor=white)
![EF Core](https://img.shields.io/badge/EF_Core-512BD4?style=flat-square&logo=dotnet&logoColor=white)
![SQLite](https://img.shields.io/badge/SQLite-003B57?style=flat-square&logo=sqlite&logoColor=white)
![JWT](https://img.shields.io/badge/JWT-000000?style=flat-square&logo=jsonwebtokens&logoColor=white)
![Swagger](https://img.shields.io/badge/Swagger-85EA2D?style=flat-square&logo=swagger&logoColor=black)

</div>

---

## ✨ Возможности

- 🔐 **Аутентификация** — регистрация и вход с хешированием паролей через BCrypt
- 🍪 **Безопасные cookie** — JWT передаётся в `HttpOnly`, `Secure`, `SameSite=Strict`
- 📝 **CRUD записей** — полное управление своими записями, чужие недоступны
- ✅ **Валидация** — через MiniValidation
- 📖 **Swagger UI** — интерактивная документация для тестирования

---

## 🚀 Быстрый старт

### 1. Клонировать репозиторий

```bash
git clone https://github.com/mmk0420/Mokes.API.git
cd Mokes.API
```

### 2. Создать `secret.json`

В папке `Mokes.API/` (рядом с `appsettings.json`) создай файл `secret.json`:

```json
{
  "Jwt": {
    "Key": "ВАШ-СЕКРЕТНЫЙ-КЛЮЧ-МИНИМУМ-64-СИМВОЛА"
  },
  "Cookie": {
    "Auth": "ЧТО-ТО-РАНДОМНОЕ-Я-ХЗ",
    "Refresh": "ТУТ-ТОЖЕ"
  }
}
```

> [!WARNING]
> Ключ должен быть **64+ символов** и случайным. Файл добавлен в `.gitignore` — в репозиторий не попадёт.

<details>
<summary>💡 Как сгенерировать ключ</summary>

**PowerShell:**
```powershell
[Convert]::ToBase64String((1..64 | ForEach-Object { Get-Random -Maximum 256 }))
```

**Через .NET User Secrets:**
```bash
dotnet user-secrets set "Jwt:Key" "ВАШ_КЛЮЧ"
```

</details>

### 3. Применить миграции

```bash
dotnet ef database update
```

### 4. Запустить

```bash
dotnet run
```

> Swagger откроется по адресу: `https://localhost:{PORT}/swagger`

---

## 📡 Эндпоинты

### Аутентификация — `/api/auth`

| Метод | Путь | Описание |
|:---:|---|---|
| `POST` | `/api/auth/register` | Регистрация |
| `POST` | `/api/auth/login` | Вход, устанавливает cookie с токеном |
| `POST` | `/api/auth/logout` | Выход, удаляет cookie |

<details>
<summary>📋 Тело запроса для register / login</summary>

```json
{
  "username": "mmk",
  "password": "yourpassword"
}
```

</details>

### Записи — `/api/entries` 🔒

> Все маршруты требуют авторизации (JWT в cookie)

| Метод | Путь | Описание |
|:---:|---|---|
| `GET` | `/api/entries` | Все свои записи |
| `GET` | `/api/entries/{id}` | Одна запись по ID |
| `POST` | `/api/entries` | Создать запись |
| `PUT` | `/api/entries/{id}` | Обновить запись |
| `DELETE` | `/api/entries/{id}` | Удалить запись |

---

## 🗂 Структура проекта

```
Mokes.API/
├── Data/               # SQLite база данных
├── DTOs/               # Request/Response модели
├── Endpoints/          # Minimal API маршруты
├── Extensions/         # Настройка аутентификации
├── Migrations/         # EF Core миграции
├── Models/             # Сущности БД
├── Repositories/       # Слой доступа к данным
├── Services/           # Бизнес-логика
├── Utils/              # JWTGenerator и прочее
├── appsettings.json
├── secret.json         # ← создать вручную (не в git)
└── Program.cs
```

---

## 📦 Зависимости

| Пакет | Версия |
|---|:---:|
| `Microsoft.AspNetCore.Authentication.JwtBearer` | `8.0.25` |
| `Microsoft.EntityFrameworkCore.Sqlite` | `8.0.25` |
| `BCrypt.Net-Next` | `4.1.0` |
| `MiniValidation` | `0.9.2` |
| `Swashbuckle.AspNetCore` | `6.6.2` |
