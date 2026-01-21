using Core;
using UI.Navbar;
using UI.Views;
using UnityEngine;
using Zenject;

namespace UI.Presenters
{
    public class NavbarPresenter : BasePresenter<NavbarView>
    {
        private NavbarButtonType _currentOpenedNavbar;
        
        private readonly INavigationService _navigationService;
        
        [Inject]
        public NavbarPresenter(
            INavigationService navigationService, 
            [Inject(Id = "NavbarView")] NavbarView navbarView, 
            [Inject(Id = "uiRoot")] Transform uiRoot) 
            : base(navbarView, uiRoot)
        {
            _navigationService = navigationService;
        }
        
        protected override void OnViewCreated()
        {
            View.ButtonClicked += HandleButtonClick;
        }
       
        protected override void OnViewDestroy()
        {
            View.ButtonClicked -= HandleButtonClick;
        }
        
        private void HandleButtonClick(NavbarButtonType type)
        {
            if (_currentOpenedNavbar == type)
            {
                Open(NavbarButtonType.Lobby);
                return; 
            }

            Open(type);
        }

        private void Open(NavbarButtonType type)
        {
            _currentOpenedNavbar = type;
            _navigationService.Open(type);
        }
    }
}