using System;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace UI.NavbarView
{
    public class NavbarView : BaseView
    {
        [SerializeField] private List<NavbarButton> _navbarButtons;

        public event Action<NavbarButtonType> NavbarButtonClicked;

        private NavbarButtonType _currentOpenedNavbar;
        
        private void OnEnable()
        {
            foreach (var navbarButton in _navbarButtons)
            {
                navbarButton.OnClicked += HandleClick;
            }
        }

        private void OnDisable()
        {
            foreach (var navbarButton in _navbarButtons)
            {
                navbarButton.OnClicked -= HandleClick;
            }
        }

        private void HandleClick(NavbarButtonType type)
        {
            //если нажать на то что открыто сейчас, то надо закрыть
            if (_currentOpenedNavbar == type) return;
            
            NavbarButtonClicked?.Invoke(type);

            _currentOpenedNavbar = type;
            
            if (type == NavbarButtonType.Inventory)
            {
                LobbyUIEventBus.RequestInventoryOpen();
            }
        }
    }
}