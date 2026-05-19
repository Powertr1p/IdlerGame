using UnityEngine;
using Zenject;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;

    [Inject] private PlayerMovement _player;

    private void Start()
    {
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        _player.gameObject.SetActive(true);
        _player.TeleportTo(_spawnPoint.position);
    }
}
