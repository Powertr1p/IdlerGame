# IdlerGame

Казуальная exctraction hybrid-casual на Unity с механиками сбора ресурсов, системой экипировки и рейдами.

## Основные механики

### Геймплей
- **Сбор ресурсов** - добыча различных типов ресурсов с помощью инструментов
- **Система инвентаря** - управление предметами с виртуализацией UI для оптимизации
- **Экипировка** - инструменты и рюкзаки с различными характеристиками
- **Рейды** - временные миссии со сбором лута и эвакуацией
- **Враги** - AI на основе State Machine (Idle, Chase, Attack, Death)
- **Система сохранения** - автосохранение инвентаря через PlayerPrefs

### UI системы
- **MVP архитектура** - разделение логики (Model), представления (View) и презентации (Presenter)
- **Виртуализация списков** - оптимизация отображения больших инвентарей через Object Pool
- **Навигация** - система переключения между UI экранами

## Архитектурные паттерны

- **Dependency Injection** (Zenject)
- **MVP** для UI (BaseView, BasePresenter) 
- **UI Factory** 
- **UI Lazy Initialization**
- **UI ScrollView Virtualization**
- **Object Pool** 
- **Observer/Event-driven**
- **State Machine**
- **Facade** (PlayerInventory.cs скрывает сложность работы с предметами)
- **Adapter** (DamageReceiver.cs адаптирует Health к интерфейсам IDamageable, IMortal)
- **DTO** (для сохранений)
- **Decorator** (InventoryItemDisplay оборачивает ItemData для UI)
- **Proxy** (AssetsLoader проксирует работу с Addressables)
- **Mediator** (LobbyNavigator координирует взаимодействие между UI компонентами)
- **Flyweight** (переиспользование слотов через Object Pool)

## Технологический стек

- **Unity 2022+**
- **C#** 
- **URP**
- **Zenject**
- **UniTask**
- **Addressables**
- **NavMesh**
- **TextMeshPro**

## Ключевые особенности

### Оптимизация
- Виртуализация UI списков - создаются только видимые элементы
- Object Pool для переиспользования UI слотов
- Addressables для ленивой загрузки ассетов
- Кэширование данных предметов через ItemRegistry

### Архитектура
- Чистое разделение ответственности через MVP
- Слабосвязанные системы через события
- Инъекция зависимостей для тестируемости
- Асинхронная загрузка через UniTask

## Требования

- Unity 2022.3+
- TextMeshPro
- Input System Package

## Как запустить

1. Открой проект в Unity 2022.3+
2. Запусти сцену `Main.unity` для лобби
3. Запусти сцену `GameScene.unity` для рейда

## Зависимости

- **Zenject** - DI контейнер
- **UniTask** - асинхронность
- **Addressables** - управление ассетами
- **Unity UI (uGUI)** - интерфейс
- **Input System** - ввод
- **NavMesh Components** - навигация AI
- **TextMeshPro** - текст

## CI/CD

Используются GitHub Actions для автоматических сборок.
