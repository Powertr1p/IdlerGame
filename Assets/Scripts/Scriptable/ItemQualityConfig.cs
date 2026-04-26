using Inventory.Core;
using UnityEngine;

namespace Scriptable
{
    [CreateAssetMenu(fileName = "ItemQualityConfig", menuName = "Create Item Quality Config", order = 0)]
    public class ItemQualityConfig : ScriptableObject
    {
        [SerializeField] private QualityColor[] _qualities;

        public Color GetColor(ItemQuality quality)
        {
            foreach (var entry in _qualities)
            {
                if (entry.Quality == quality)
                {
                    return entry.Color;
                }
            }

            return Color.white;
        }

        [System.Serializable]
        private class QualityColor
        {
            public ItemQuality Quality;
            public Color Color = Color.white;
        }
    }
}
