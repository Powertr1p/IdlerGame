using System;
using Inventory.RaidInventory;
using UnityEngine;
using Utilities;
using Zenject;

namespace Inventory
{
    public class RaidLootApplier : MonoBehaviour
    {
        [SerializeField] private PlayerInventory _playerInventory;
        
        private RaidLootBuffer _raidLootBuffer;
        private SceneLoader _sceneLoader;
        
        [Inject]
        public void Construct(RaidLootBuffer raidLootBuffer, SceneLoader sceneLoader)
        {
            _raidLootBuffer = raidLootBuffer;
            _sceneLoader = sceneLoader;
        }

        private void OnEnable()
        {
            _sceneLoader.OnSceneUnloaded += TryApplyRaidLoot;
        }
        
        private void OnDisable()
        {
            _sceneLoader.OnSceneUnloaded -= TryApplyRaidLoot;
        }

        private void TryApplyRaidLoot()
        {
            if (!_raidLootBuffer.HasLoot) return;

            var raidLoot = _raidLootBuffer.Consume();
            _playerInventory.AddFromDtoList(raidLoot);
        }
    }
}