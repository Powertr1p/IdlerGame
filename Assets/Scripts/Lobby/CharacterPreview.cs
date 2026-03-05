using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Lobby
{
    public class CharacterPreview : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        [SerializeField] private Transform _characterRoot;
        [SerializeField] private float _rotationSpeed = 0.5f;
        
        private float _currentRotationY;
        private float _initialRotation;

        private void Awake()
        {
            _initialRotation = _characterRoot.eulerAngles.y;
        }

        private void Start()
        {
            _currentRotationY = _characterRoot.eulerAngles.y;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
        }
        
        public void OnPointerUp(PointerEventData eventData)
        {
        }

        public void OnDrag(PointerEventData eventData)
        {
            float dragDelta = eventData.delta.x * _rotationSpeed;
            _currentRotationY -= dragDelta;
            
            _characterRoot.rotation = Quaternion.Euler(0, _currentRotationY, 0);
        }
        
        public void ResetRotation()
        {
            _characterRoot.rotation = Quaternion.Euler(0, _initialRotation, 0);
            _currentRotationY = _characterRoot.eulerAngles.y;
        }
    }
}