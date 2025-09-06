using UnityEngine;
using Utilities;
using Zenject;

public class LevelExit : MonoBehaviour
{
    [Inject] private SceneLoader _sceneLoader;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<PlayerMovement>(out var player)) return;
        
        _sceneLoader.UnloadCurrentScene();
        LobbyUIEventBus.RequestLobbyShow();
    }
}
