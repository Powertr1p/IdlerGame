using System.Collections.Generic;
using Scriptable;
using UnityEngine;

namespace Inventory.EquipmentItems
{
    public class EquipmentRepository : MonoBehaviour
    {
        [SerializeField] private List<ToolData> _toolData;

        public ToolData GetEquipment(int id)
        {
            for (int i = 0; i < _toolData.Count; i++)
            {
                if (_toolData[i].ItemId == id)
                {
                    return _toolData[i];
                }
            }
            
            return null;
        }
    }
}