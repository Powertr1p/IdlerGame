using System;
using System.Collections;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utilities
{
    public class SceneLoader : MonoBehaviour
    {
        [CanBeNull] private string _additivelyLoadedSceneName = null;

        public event Action OnSceneLoaded;

        private bool _isSceneLoading;

        public void LoadSceneAsync(string sceneName)
        {
            if (_isSceneLoading) return;

            StartCoroutine(LoadYourAsyncScene());

            IEnumerator LoadYourAsyncScene()
            {
                _isSceneLoading = true;

                if (_additivelyLoadedSceneName.NullIfEmpty() != null)
                {
                    yield return UnloadScene();
                    yield return CleanResources();
                }

                _additivelyLoadedSceneName = sceneName;

                yield return LoadScene();

                _isSceneLoading = false;
            }
        }
        
        public void UnloadCurrentScene()
        {
            StartCoroutine(UnloadScene());
            StartCoroutine(CleanResources());
        }

        private IEnumerator LoadScene()
        {
            var asyncLoad = SceneManager.LoadSceneAsync(_additivelyLoadedSceneName, LoadSceneMode.Additive);
            
            while (!asyncLoad.isDone)
            {
                yield return null;
            }

            OnSceneLoaded?.Invoke();
            
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(_additivelyLoadedSceneName));
        }

        private IEnumerator CleanResources()
        {
            yield return Resources.UnloadUnusedAssets();
        }

        private IEnumerator UnloadScene()
        {
            var asyncUnLoad = SceneManager.UnloadSceneAsync(_additivelyLoadedSceneName);
            _additivelyLoadedSceneName = null;

            while (!asyncUnLoad.isDone)
            {
                yield return null;
            }
        }
    }
}