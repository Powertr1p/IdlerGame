# IdlerGame

Казуальная idle-игра на Unity, где игрок собирает ресурсы и обменивает их на снаряжение.

## 📋 Описание

IdlerGame — это простая казуальная игра в жанре idle/incremental, где игрок:
- Собирает различные ресурсы с помощью инструментов
- Меняет снаряжение для более эффективного сбора
- Управляет инвентарем и экипировкой
- Проходит рейды для добычи ресурсов

## 🎮 Основной геймплей

- **Сбор ресурсов**: Игрок взаимодействует с ресурсными узлами, используя различные инструменты
- **Управление инвентарем**: Собранные ресурсы сохраняются в инвентаре игрока
- **Смена экипировки**: Возможность менять инструменты для сбора разных типов ресурсов
- **Навигация**: Переключение между лобби, инвентарем и игровыми сценами

## 🏗️ Архитектурные паттерны

### 1. **Dependency Injection (Zenject)**
Проект использует Zenject для управления зависимостями:
- `ProjectInstaller` — глобальные зависимости (сохранения, загрузка ассетов)
- `BootstrapInstaller` — зависимости для лобби (UI, навигация)
- `GameSceneInstaller` — зависимости для игровой сцены

```csharp
// Пример инъекции зависимостей
[Inject]
public void Construct(IPlayerLoadout loadout)
{
    _loadout = loadout;
}
```

### 2. **MVP (Model-View-Presenter)**
UI построен на паттерне MVP:
- **View** (`BaseView`) — отображение UI элементов
- **Presenter** (`BasePresenter<TView>`) — логика управления View
- **Model** — данные (инвентарь, экипировка)

```csharp
public abstract class BasePresenter<TView> where TView : BaseView
{
    protected TView View { get; private set; }
    public virtual void Show() { ... }
    public virtual void Hide() { ... }
}
```

### 3. **Factory Pattern**
`ViewFactory` — создание UI элементов через DI контейнер:
```csharp
public static T Create<T>(T prefab, Transform parent) where T : BaseView
{
    return _container.InstantiatePrefabForComponent<T>(prefab, parent);
}
```

### 4. **Object Pool**
Переиспользование объектов для оптимизации производительности:
```csharp
public class ObjectPool<T> where T : Component
{
    public T Get() { ... }
    public void Return(T obj) { ... }
}
```

### 5. **Observer Pattern (Events)**
Система событий для слабой связанности компонентов:
- `OnResourceChanged` — изменение ресурсов в инвентаре
- `OnSceneLoaded` — загрузка сцены
- `LobbyUIEventBus` — события UI лобби

### 6. **Service Locator**
Интерфейсы для доступа к сервисам:
- `INavigationService` — навигация между экранами
- `IPlayerLoadout` — управление экипировкой игрока

### 7. **Repository Pattern**
`EquipmentRepository` — управление данными снаряжения

### 8. **ScriptableObject Pattern**
Хранение конфигурационных данных:
- `ToolData` — данные инструментов
- `ResourceData` — данные ресурсов
- `ItemsViewDatabase` — база данных визуальных элементов
- `AssetsConfig` — конфигурация ассетов

## 🛠️ Технологический стек

### Основные технологии
- **Unity 2022+** — игровой движок
- **C# 9.0+** — язык программирования
- **Universal Render Pipeline (URP)** — графический пайплайн

### Библиотеки и пакеты

#### Dependency Injection
- **Zenject (Extenject)** — DI контейнер для управления зависимостями

#### Асинхронность
- **UniTask** — высокопроизводительная альтернатива async/await для Unity
  - Асинхронная загрузка сцен
  - Загрузка ассетов через Addressables

#### Управление ассетами
- **Addressables** — динамическая загрузка и выгрузка ассетов
  - Загрузка сцен
  - Инстанцирование префабов
  - Управление памятью

#### UI
- **Unity UI (uGUI)** — система пользовательского интерфейса

#### Ввод
- **New Input System** — современная система ввода Unity

#### Навигация
- **AI Navigation** — система навигации для NPC (если используется)

#### Сериализация
- **Newtonsoft.Json** — сериализация данных для сохранений

#### Другие пакеты
- **Unity Timeline** — система анимаций и кат-сцен
- **Visual Scripting** — визуальное программирование

### Графика и эффекты
Проект включает сторонние ассеты:
- **Toony Colors Pro** — стилизованные шейдеры
- **Cartoon FX Remaster** — визуальные эффекты
- **Kino Bloom** — пост-обработка
- **Skybox Cubemap Extended** — скайбоксы

## 📁 Структура проекта

```
Assets/Scripts/
├── AssetLoader/          # Загрузка ассетов через Addressables
├── Inventory/            # Система инвентаря и экипировки
│   ├── Core/            # Базовые классы инвентаря
│   ├── EquipmentItems/  # Предметы экипировки
│   └── RaidInventory/   # Инвентарь для рейдов
├── Player/              # Логика игрока
│   ├── PlayerMovement   # Передвижение
│   ├── PlayerGathering  # Сбор ресурсов
│   └── EquipmentChanger # Смена экипировки
├── ResourceItems/       # Ресурсные объекты
├── UI/                  # Пользовательский интерфейс
│   ├── Views/          # View компоненты
│   ├── Presenters/     # Presenter компоненты
│   └── Factories/      # Фабрики для создания UI
├── Lobby/              # Система лобби и навигации
├── LevelSystems/       # Системы уровней
├── Scriptable/         # ScriptableObject данные
└── Utilities/          # Утилиты и инсталлеры
```

## 🔑 Ключевые системы

### Система инвентаря
- `PlayerInventory` — управление ресурсами игрока
- `PlayerLoadout` — текущая экипировка
- Автоматическое сохранение через `PlayerInventorySaveBox`

### Система сбора ресурсов
- `PlayerGathering` — логика сбора ресурсов
- `IGatherable` — интерфейс для ресурсных узлов
- Анимации сбора через `PlayerAnimator`

### Система навигации
- `LobbyNavigator` — управление переходами между экранами
- `SceneLoader` — асинхронная загрузка сцен через Addressables

### Система UI
- MVP архитектура для всех UI элементов
- `ViewFactory` для создания View через DI
- Презентеры управляют жизненным циклом View

## 🎯 Принципы разработки

- **SOLID принципы** — разделение ответственности, инверсия зависимостей
- **Слабая связанность** — использование интерфейсов и событий
- **Переиспользование кода** — базовые классы и дженерики
- **Оптимизация памяти** — Object Pool, Addressables
- **Асинхронность** — UniTask для неблокирующих операций

## 🔄 CI/CD и автоматизация сборки

Проект поддерживает автоматическую сборку через CI/CD пайплайны для различных платформ.

### GitHub Actions

Рекомендуемая конфигурация для автоматической сборки:

```yaml
# .github/workflows/build.yml
name: Unity Build

on:
  push:
    branches: [ main, develop ]
  pull_request:
    branches: [ main ]

jobs:
  build:
    name: Build for ${{ matrix.targetPlatform }}
    runs-on: ubuntu-latest
    strategy:
      fail-fast: false
      matrix:
        targetPlatform:
          - StandaloneWindows64
          - StandaloneLinux64
          - Android
          - iOS
          - WebGL
    
    steps:
      - name: Checkout repository
        uses: actions/checkout@v4
        with:
          lfs: true
      
      - name: Cache Library
        uses: actions/cache@v3
        with:
          path: Library
          key: Library-${{ matrix.targetPlatform }}
          restore-keys: Library-
      
      - name: Build Addressables
        uses: game-ci/unity-builder@v4
        with:
          targetPlatform: ${{ matrix.targetPlatform }}
          buildMethod: UnityBuilderAction.BuildScript.BuildAddressables
        env:
          UNITY_LICENSE: ${{ secrets.UNITY_LICENSE }}
          UNITY_EMAIL: ${{ secrets.UNITY_EMAIL }}
          UNITY_PASSWORD: ${{ secrets.UNITY_PASSWORD }}
      
      - name: Build project
        uses: game-ci/unity-builder@v4
        with:
          targetPlatform: ${{ matrix.targetPlatform }}
          buildName: IdlerGame
        env:
          UNITY_LICENSE: ${{ secrets.UNITY_LICENSE }}
          UNITY_EMAIL: ${{ secrets.UNITY_EMAIL }}
          UNITY_PASSWORD: ${{ secrets.UNITY_PASSWORD }}
      
      - name: Upload Build
        uses: actions/upload-artifact@v3
        with:
          name: Build-${{ matrix.targetPlatform }}
          path: build/${{ matrix.targetPlatform }}
```

### Этапы автоматической сборки

1. **Подготовка окружения**
   - Checkout кода из репозитория
   - Кэширование Library для ускорения сборки
   - Настройка Unity лицензии

2. **Сборка Addressables**
   - Автоматическая сборка Addressables перед основной сборкой
   - Критично для корректной работы динамической загрузки ассетов

3. **Компиляция проекта**
   - Сборка для целевой платформы
   - Валидация кода и зависимостей
   - Генерация билда

4. **Публикация артефактов**
   - Загрузка собранных билдов как артефакты
   - Доступны для скачивания и тестирования

### Локальная автоматизация

Для локальной сборки можно использовать скрипты:

```csharp
// Editor/BuildScript.cs
public static class BuildScript
{
    [MenuItem("Build/Build All Platforms")]
    public static void BuildAll()
    {
        // Сначала собираем Addressables
        BuildAddressables();
        
        // Затем собираем для каждой платформы
        BuildForPlatform(BuildTarget.StandaloneWindows64);
        BuildForPlatform(BuildTarget.Android);
    }
    
    public static void BuildAddressables()
    {
        AddressableAssetSettings.BuildPlayerContent();
    }
    
    private static void BuildForPlatform(BuildTarget target)
    {
        string path = $"Builds/{target}/IdlerGame";
        BuildPipeline.BuildPlayer(GetScenes(), path, target, BuildOptions.None);
    }
}
```

### Требования для CI/CD

**Секреты GitHub (Secrets):**
- `UNITY_LICENSE` — лицензия Unity (Personal/Pro)
- `UNITY_EMAIL` — email аккаунта Unity
- `UNITY_PASSWORD` — пароль аккаунта Unity

**Зависимости:**
- Unity версии 2022 или выше
- Git LFS для больших файлов
- Достаточно места для кэша (рекомендуется 5+ GB)

### Оптимизация сборки

- **Кэширование Library** — сокращает время сборки на 70-80%
- **Параллельная сборка** — одновременная сборка для разных платформ
- **Инкрементальная сборка** — только измененные ассеты
- **Addressables** — уменьшение размера основного билда

### Дополнительные возможности

**Автоматическое тестирование:**
```yaml
- name: Run Tests
  uses: game-ci/unity-test-runner@v4
  with:
    testMode: EditMode
```

**Деплой на платформы:**
- **Google Play** — автоматическая публикация Android билдов
- **App Store** — публикация iOS билдов
- **Itch.io / Steam** — деплой desktop версий
- **WebGL хостинг** — автоматическая публикация на GitHub Pages

## 🚀 Запуск проекта

1. Откройте проект в Unity 2022 или новее
2. Убедитесь, что все пакеты установлены (см. `Packages/manifest.json`)
3. Откройте стартовую сцену
4. Нажмите Play

## 📝 Примечания

- Проект использует URP, убедитесь что настройки графики соответствуют
- Для работы Addressables необходимо собрать ассеты (Build > Addressables)
- Сохранения хранятся локально через систему сериализации

## 📄 Лицензия

См. файл LICENSE
