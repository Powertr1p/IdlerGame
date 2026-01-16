using AssetLoader;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _spawnPoint;
    
    private AssetsLoader _assetsLoader;
    private AssetsConfig _assetsConfig;

    private void Start()
    {
        SpawnPlayer();
    }
    
    private void SpawnPlayer()
    {
        _player.transform.position = _spawnPoint.position;
        _player.gameObject.SetActive(true);
    }
}
