using DG.Tweening;
using UnityEngine;

namespace GameItems.Core
{
    public class ResourceNodeAnimationTree : ResourceNodeAnimationBase
    {
        public override void AnimateOnHit()
        {
            transform.DOPunchPosition(Vector3.forward * _pulsePower, _pulseDuration);
            //test
        }
    }
}