using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Utilities
{
    public class SceneLoader : IDisposable
    {
        public event Action OnSceneLoaded;
        public event Action OnSceneUnloaded;
        
        private SceneInstance? _currentScene;
        private bool _isSceneLoading;

        public async UniTask LoadSceneAsync(string sceneKey)
        {
            if (_isSceneLoading) return;
            _isSceneLoading = true;

            try
            {
                if (_currentScene.HasValue)
                {
                    await UnloadCurrentSceneAsync();
                }

                var handle = Addressables.LoadSceneAsync(sceneKey, LoadSceneMode.Additive);

                while (!handle.IsDone)
                {
                    await UniTask.Yield();
                }

                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    _currentScene = handle.Result;
                    SceneManager.SetActiveScene(handle.Result.Scene);
                    OnSceneLoaded?.Invoke();
                }
            }
            finally
            {
                _isSceneLoading = false;
            }
        }
        
        public UniTask UnloadCurrentAsync()
        {
            return UnloadCurrentSceneAsync();
        }

        private async UniTask UnloadCurrentSceneAsync()
        {
            if (!_currentScene.HasValue) return;
            
            var unloadHandle = Addressables.UnloadSceneAsync(_currentScene.Value);
            await unloadHandle.ToUniTask();
            
            await Resources.UnloadUnusedAssets().ToUniTask();
            
            _currentScene = null;
            OnSceneUnloaded?.Invoke();
        }

        public void Dispose()
        {
            if (_currentScene.HasValue)
            {
                Addressables.UnloadSceneAsync(_currentScene.Value);
            }
        }
    }
}