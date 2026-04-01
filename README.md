# Mokes.API

REST API для управления личными записями с JWT-аутентификацией.

**Стек:** ASP.NET Core 8 · Minimal API · Entity Framework Core · SQLite · BCrypt · JWT Bearer

---

## Возможности

- Регистрация и вход с хешированием паролей через BCrypt
- JWT-токен передаётся в cookie (`HttpOnly`, `Secure`, `SameSite=Strict`)
- CRUD для записей (entries) — только свои, чужие не видны
- Валидация через MiniValidation
- Swagger UI для тестирования

---

## Быстрый старт

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
    "Key": "ВАШ_СЕКРЕТНЫЙ_КЛЮЧ_МИНИМУМ_32_СИМВОЛА"
  }
}
```

> ⚠️ Ключ должен быть **достаточно длинным** (рекомендуется 64+ символа) и случайным.  
> Файл добавлен в `.gitignore` — в репозиторий не попадёт.

**Как сгенерировать ключ:**

В PowerShell:
```powershell
[Convert]::ToBase64String((1..64 | ForEach-Object { Get-Random -Maximum 256 }))
```

Или в терминале .NET:
```bash
dotnet user-secrets set "Jwt:Key" "ВАШ_КЛЮЧ"
```

Или просто придумай длинную случайную строку — главное, чтобы было 64+ символа.

### 3. Применить миграции

```bash
dotnet ef database update
```

### 4. Запустить

```bash
dotnet run
```

Swagger откроется по адресу: `https://localhost:{PORT}/swagger`

---

## Эндпоинты

### Аутентификация — `/api/auth`

| Метод | Путь | Описание |
|-------|------|----------|
| `POST` | `/api/auth/register` | Регистрация |
| `POST` | `/api/auth/login` | Вход, устанавливает cookie с токеном |
| `POST` | `/api/auth/logout` | Выход, удаляет cookie |

**Тело запроса для register / login:**
```json
{
  "username": "mmk",
  "password": "yourpassword"
}
```

---

### Записи — `/api/entries` 🔒

Все маршруты требуют авторизации (JWT в cookie).

| Метод | Путь | Описание |
|-------|------|----------|
| `GET` | `/api/entries` | Все свои записи |
| `GET` | `/api/entries/{id}` | Одна запись по ID |
| `POST` | `/api/entries` | Создать запись |
| `PUT` | `/api/entries/{id}` | Обновить запись |
| `DELETE` | `/api/entries/{id}` | Удалить запись |

---

## Структура проекта

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

## Зависимости

| Пакет | Версия |
|-------|--------|
| `Microsoft.AspNetCore.Authentication.JwtBearer` | 8.0.25 |
| `Microsoft.EntityFrameworkCore.Sqlite` | 8.0.25 |
| `BCrypt.Net-Next` | 4.1.0 |
| `MiniValidation` | 0.9.2 |
| `Swashbuckle.AspNetCore` | 6.6.2 |
