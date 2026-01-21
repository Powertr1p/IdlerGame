using UI.Presenters;
using UnityEngine;
using Zenject;

namespace UI
{
    public class UIBootstrap : MonoBehaviour
    {
        private NavbarPresenter _navbarPresenter;
        
        [Inject]
        private void Construct(NavbarPresenter navbarPresenter)
        {
            _navbarPresenter = navbarPresenter;
        }

        private void Start()
        {
            _navbarPresenter.Show();
        }
    }
}