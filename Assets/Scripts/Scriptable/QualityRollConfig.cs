using Inventory.Core;
using UnityEngine;

namespace Scriptable
{
    [CreateAssetMenu(fileName = "QualityRollConfig", menuName = "Data/QualityRollConfig")]
    public class QualityRollConfig : ScriptableObject
    {
        [SerializeField] private QualityWeight[] _weights;

        public ItemQuality Roll()
        {
            float total = 0f;
            foreach (var w in _weights) total += w.Weight;
            if (total <= 0f) return ItemQuality.Common;

            float pick = Random.Range(0f, total);
            float acc = 0f;
            foreach (var w in _weights)
            {
                acc += w.Weight;
                if (pick <= acc) return w.Quality;
            }

            return _weights[_weights.Length - 1].Quality;
        }

        [System.Serializable]
        private class QualityWeight
        {
            public ItemQuality Quality;
            public float Weight;
        }
    }
}
