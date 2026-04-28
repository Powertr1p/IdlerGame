using UnityEngine;

namespace UI.HealthBar
{
    public class Billboard : MonoBehaviour
    {
        private Camera _camera;
        
        private void Awake()
        {
            _camera = Camera.main;
        }

        private void LateUpdate()
        {
            Transform cameraTransform = _camera.transform;
            transform.LookAt(transform.position + cameraTransform.forward, cameraTransform.up);
        }
    }
}
