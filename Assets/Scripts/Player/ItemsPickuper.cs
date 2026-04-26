using Inventory.RaidInventory;
using ResourceItems.Core;
using UnityEngine;

public class ItemsPickuper : MonoBehaviour
{
    [SerializeField] private Transform _attractor;
    [SerializeField] private RaidInventory _raidInventory;
        
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IAttractable attractable) && HasSlots())
        {
            attractable.Attract(_attractor);
            _raidInventory.TryAdd(attractable.Type, attractable.Quality);
        }
    }

    private bool HasSlots()
    {
        return _raidInventory.CanAdd();
    }
}