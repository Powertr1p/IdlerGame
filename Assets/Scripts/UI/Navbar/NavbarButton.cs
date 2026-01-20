using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Navbar
{
    [RequireComponent(typeof(Button))]
    public class NavbarButton : MonoBehaviour
    {
        [SerializeField] private NavbarButtonType _navbarButtonType;
     
        public event Action<NavbarButtonType> OnClicked;
        
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(ButtonClicked);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(ButtonClicked);
        }

        private void ButtonClicked()
        {
            OnClicked?.Invoke(_navbarButtonType);
        }
    }
}