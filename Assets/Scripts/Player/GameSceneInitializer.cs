using DefaultNamespace;
using UnityEngine;

public class GameSceneInitializer : MonoBehaviour
{
    [SerializeField] private Transform _toolContainer;

    private void Start()
    {
        EquipmentChanger equipmentChanger = FindObjectOfType<EquipmentChanger>();
        
        if (equipmentChanger != null)
        {
            equipmentChanger.SetGameSceneMode(_toolContainer);
        }
    }
}
