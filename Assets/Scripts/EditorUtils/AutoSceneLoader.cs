using UnityEngine;
using UnityEngine.SceneManagement;

namespace EditorUtils
{
    public class AutoSceneLoader : MonoBehaviour
    {
        private const string MAIN_SCENE_NAME = "Main";

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void TryLoadMainScene()
        {
#if UNITY_EDITOR
            var activeScene = SceneManager.GetActiveScene();
            
            if (activeScene.name != MAIN_SCENE_NAME)
            {
                SceneManager.LoadScene(MAIN_SCENE_NAME);
            }
#endif
        }
    }
}
