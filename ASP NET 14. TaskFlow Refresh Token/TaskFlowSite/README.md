# TaskFlow Client — пошаговое создание (React + Vite + Tailwind)

Одностраничное приложение для API **08_TaskFlowWithRefreshToken**: проекты, доска задач с перетаскиванием, JWT + refresh token, всплывающие уведомления.

**Что получится:** SPA с логином/регистрацией, списком проектов, доской задач (колонки To do / In progress / Done) с drag-and-drop, тостами при успехе и ошибках (в т.ч. 403 «только для Managers и Admins»).

**Порядок создания:** от инициализации Vite до сборки для продакшена — по шагам ниже.

---

## Пошаговое создание клиента

### Шаг 1. Инициализация проекта

В корне репозитория (рядом с папкой `08_TaskFlowWithRefreshToken`):

```bash
cd 08_TaskFlowWithRefreshToken
npm create vite@latest client -- --template react-ts
cd client
```

Или: создайте папку `client` внутри `08_TaskFlowWithRefreshToken`, откройте её в терминале и выполните `npm create vite@latest . -- --template react-ts`.

### Шаг 2. Зависимости

```bash
npm install react-router-dom @dnd-kit/core @dnd-kit/utilities
npm install -D tailwindcss @tailwindcss/vite
```

- **react-router-dom** — маршрутизация.
- **@dnd-kit/core**, **@dnd-kit/utilities** — перетаскивание задач между колонками.
- **tailwindcss**, **@tailwindcss/vite** — стили.

### Шаг 3. Конфигурация Vite

В `vite.config.ts` добавьте плагин Tailwind и прокси для API:

```ts
import tailwindcss from '@tailwindcss/vite';

export default defineConfig({
  plugins: [react(), tailwindcss()],
  server: {
    port: 3000,
    proxy: {
      '/api': { target: 'http://localhost:5120', changeOrigin: true },
    },
  },
});
```

### Шаг 4. Стили Tailwind

Создайте `src/index.css`:

```css
@import "tailwindcss";

@layer base {
  body {
    @apply min-h-screen bg-[#f4f5f7] text-[#172b4d] font-sans text-sm antialiased;
  }
  #root { @apply min-h-screen; }
}
```

Подключите в `src/main.tsx`: `import './index.css';`

### Шаг 5. API-клиент и типы

- **src/types/api.ts** — интерфейсы ApiResponse&lt;T&gt;, AuthResponse, Project, TaskItem (и при необходимости PagedResult).
- **src/api/client.ts** — хранение access/refresh токенов, `apiFetch` с автоматическим обновлением токена при 401 (вызов `/api/auth/refresh`), возврат `{ ok, status, data, problem }`.
- **src/api/auth.ts** — login, register, revokeRefresh; при успехе вызов setAuthTokens.
- **src/api/projects.ts** — getProjects, getProject, createProject, updateProject, deleteProject; при ошибках возвращать `status` для обработки 403.
- **src/api/tasks.ts** — getTasksByProject, getTask, createTask, updateTask, deleteTask; при ошибках возвращать `status`.

### Шаг 6. Контексты

- **src/auth/AuthContext.tsx** — AuthProvider, useAuth; состояние user (email, roles), isLoading; login, register, logout; сохранение токенов в localStorage, восстановление при загрузке страницы; setOnTokensUpdated для обновления хранилища после refresh.
- **src/context/ToastContext.tsx** — ToastProvider, useToast; toast(message, type), toastError, toastSuccess; отображение уведомлений в правом верхнем углу (error/success/info), автоскрытие через 5 секунд.

### Шаг 7. Роутинг и защищённые маршруты

В **App.tsx**:

- Подключить BrowserRouter с future-флагами: `future={{ v7_startTransition: true, v7_relativeSplatPath: true }}`.
- Обернуть приложение в AuthProvider и ToastProvider.
- Маршруты: `/login`, `/register` — публичные; `/` — защищённый (ProtectedRoute), внутри Layout с вложенными маршрутами: индекс — список проектов, `project/:projectId` — доска.
- Lazy-загрузка страниц и Suspense с fallback «Loading…».
- ProtectedRoute: при isLoading — «Loading…»; при отсутствии user — Navigate to `/login`; иначе — children.

### Шаг 8. Страницы

- **LoginPage** — форма email/password, вызов login(), при успехе navigate('/'), при ошибке — отображение и опционально toast.
- **RegisterPage** — форма firstName, lastName, email, password, confirmPassword; проверка совпадения паролей; вызов register(); тосты при ошибке/успехе по желанию.
- **ProjectsPage** — список проектов (карточки), кнопка «Create project»; модальное окно создания (name, description); вызов createProject; при 403 — toast «Creating projects is available only to Managers and Admins»; при успехе — toast «Project created» и обновление списка.
- **ProjectBoardPage** — заголовок проекта, кнопки «Back», «Create task»; доска с колонками To do / In progress / Done; DndContext, useDraggable для карточек задач, useDroppable для колонок; при drop в другую колонку — updateTask с новым status, оптимистичное обновление, при ошибке — откат и toast (при 403 — «Creating and editing tasks is available only to Managers and Admins»); модалки редактирования задачи и создания задачи; тосты при успехе/ошибке (create, update, delete, drag).

Все тексты интерфейса — на английском.

### Шаг 9. Layout и компоненты

- **Layout** — боковая панель (Projects, Board при открытом проекте), шапка (Task management, email, Sign out), Outlet для вложенных страниц. Стили Tailwind в духе Jira (#172b4d, #0052cc, #f4f5f7).
- Кнопки, поля ввода, модалки — единообразные классы Tailwind (rounded, shadow, focus:ring и т.д.).

### Шаг 10. Запуск

1. Запустите API (порт 5120):

   ```bash
   cd 08_TaskFlowWithRefreshToken
   dotnet run
   ```

2. Запустите клиент:

   ```bash
   cd client
   npm install
   npm run dev
   ```

3. Откройте http://localhost:3000. Логин: `admin@taskflow.com` / `Admin123!`.

При необходимости задайте в корне `client` файл `.env`:

```env
VITE_API_URL=http://localhost:5120
```

При использовании прокси в Vite (см. шаг 3) переменная не обязательна.

---

## Сборка для продакшена

```bash
npm run build
```

Результат в `dist/`. Предпросмотр: `npm run preview`.

---

## Стек

- React 18, TypeScript
- Vite 5
- React Router 6 (с future-флагами v7)
- Tailwind CSS v4
- @dnd-kit (drag-and-drop)
- Без дополнительных UI-библиотек; тосты и layout — свои компоненты

---

## Краткий чеклист поэтапного создания

| Шаг | Действие |
|-----|----------|
| 1 | Инициализация Vite (React + TypeScript) |
| 2 | Установка зависимостей (react-router-dom, @dnd-kit, Tailwind) |
| 3 | Настройка Vite (Tailwind, прокси `/api` → API) |
| 4 | Стили Tailwind в `index.css` |
| 5 | Типы и API-клиент (client.ts, auth, projects, tasks) |
| 6 | Контексты Auth и Toast |
| 7 | Роутинг, ProtectedRoute, lazy/Suspense |
| 8 | Страницы Login, Register, Projects, ProjectBoard |
| 9 | Layout и общие компоненты |
| 10 | Запуск API + клиент, проверка |
