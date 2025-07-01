using UnityEngine;
using UnityEngine.UI;
using Utilities;


namespace UI
{
    public class StartButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private SceneLoader _sceneLoader;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            _sceneLoader.LoadSceneAsync("GameScene");
        }
    }
}