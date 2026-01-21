# IdlerGame

Казуальная idle-игра на Unity: сбор ресурсов и обмен на снаряжение.

## 🏗️ Архитектурные паттерны

- **Dependency Injection** — Zenject (ProjectInstaller, BootstrapInstaller, GameSceneInstaller)
- **MVP** — Model-View-Presenter для UI (BaseView, BasePresenter)
- **Factory** — ViewFactory для создания UI через DI
- **Object Pool** — переиспользование объектов
- **Observer** — система событий (OnResourceChanged, OnSceneLoaded, LobbyUIEventBus)
- **Service Locator** — INavigationService, IPlayerLoadout
- **Repository** — EquipmentRepository
- **ScriptableObject** — конфигурационные данные (ToolData, ResourceData, ItemsViewDatabase)

## 🛠️ Технологический стек

### Основа
- **Unity 2022+**
- **C# 9.0+**
- **Universal Render Pipeline (URP)**

### Библиотеки
- **Zenject** — DI контейнер
- **UniTask** — асинхронность
- **Addressables** — динамическая загрузка ассетов
- **Unity UI (uGUI)** — интерфейс
- **New Input System** — ввод
- **Newtonsoft.Json** — сериализация
- **Unity Timeline** — анимации

### Графика
- **Toony Colors Pro** — шейдеры
- **Cartoon FX Remaster** — эффекты
- **Kino Bloom** — пост-обработка
- **Skybox Cubemap Extended** — скайбоксы

## 📁 Структура проекта

```
Assets/Scripts/
├── AssetLoader/      # Addressables
├── Inventory/        # Инвентарь и экипировка
├── Player/           # Логика игрока
├── ResourceItems/    # Ресурсы
├── UI/               # MVP интерфейс
├── Lobby/            # Навигация
├── Scriptable/       # ScriptableObject данные
└── Utilities/        # Инсталлеры Zenject
```

## 🔄 CI/CD

### GitHub Actions
- Платформы: Windows, Linux, Android, iOS, WebGL
- Триггеры: push/PR на main, develop
- Этапы: checkout → cache → build Addressables → build project → upload artifacts

### Требования
- Unity 2022+, Git LFS
- Secrets: UNITY_LICENSE, UNITY_EMAIL, UNITY_PASSWORD

### Оптимизация
- Кэширование Library (-70% времени)
- Параллельная сборка платформ
- Инкрементальная сборка
