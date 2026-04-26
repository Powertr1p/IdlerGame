using UnityEngine;

namespace UI.HealthBar
{
    public class Billboard : MonoBehaviour
    {
        private Camera _camera;

        private void LateUpdate()
        {
            if (ReferenceEquals(_camera, null))
            {
                _camera = Camera.main;
                if (ReferenceEquals(_camera, null)) return;
            }

            Transform cameraTransform = _camera.transform;
            transform.LookAt(transform.position + cameraTransform.forward, cameraTransform.up);
        }
    }
}
