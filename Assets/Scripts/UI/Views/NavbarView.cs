using System;
using System.Collections.Generic;
using UI.Navbar;
using UnityEngine;

namespace UI.Views
{
    public class NavbarView : BaseView
    {
        [SerializeField] private List<NavbarButton> _navbarButtons;
        
        public event Action<NavbarButtonType> ButtonClicked;
        
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
            ButtonClicked?.Invoke(type);
        }
    }
}