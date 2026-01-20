using UnityEngine;
using Zenject;

namespace UI.Factories
{
    public static class ViewFactory
    {
        private static DiContainer _container;
        private static bool _isInitialized = false;

        public static void BindContainer(DiContainer container)
        {
            if (_isInitialized) return;
            
            _container = container;
            _isInitialized = true;
        }
        
        public static T Create<T>(T prefab, Transform parent) where T : BaseView
        {
            if (!_isInitialized)
            {
                Debug.LogError("ViewFactory is not initialized");
                return null;
            }
            
            return _container.InstantiatePrefabForComponent<T>(prefab, parent);
        }
    }
}