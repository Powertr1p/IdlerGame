using System.Collections;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [CanBeNull] 
    private string _additivelyLoadedSceneName;

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
                yield return UnloadCurrentScene();
                yield return CleanResources();
            }

            _additivelyLoadedSceneName = sceneName;

            yield return LoadScene();

            _isSceneLoading = false;
        }
    }

    private IEnumerator LoadScene()
    {
        var asyncLoad = SceneManager.LoadSceneAsync(_additivelyLoadedSceneName, LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(_additivelyLoadedSceneName));
    }

    private IEnumerator CleanResources()
    {
        yield return Resources.UnloadUnusedAssets();
    }

    private IEnumerator UnloadCurrentScene()
    {
        var asyncUnLoad = SceneManager.UnloadSceneAsync(_additivelyLoadedSceneName);
        _additivelyLoadedSceneName = null;

        while (!asyncUnLoad.isDone)
        {
            yield return null;
        }
    }
}