using UnityEngine;

namespace UI
{
    public class BaseView : MonoBehaviour, IView
    {
        public virtual bool IsVisible => gameObject.activeInHierarchy;
        public virtual void Show() => gameObject.SetActive(true);
        public virtual void Hide() => gameObject.SetActive(false);
    }
}