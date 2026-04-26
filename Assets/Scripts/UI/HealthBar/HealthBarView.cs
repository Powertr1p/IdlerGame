using ShareComponents;
using UnityEngine;
using UnityEngine.UI;

namespace UI.HealthBar
{
    public class HealthBarView : MonoBehaviour
    {
        [SerializeField] private Health _health;
        [SerializeField] private Image _fillImage;
        [SerializeField] private bool _hideOnDeath = true;

        private void OnEnable()
        {
            if (ReferenceEquals(_health, null)) return;

            _health.OnTakeDamage += HandleHealthChanged;
            _health.OnDie += HandleDie;
        }

        private void OnDisable()
        {
            if (ReferenceEquals(_health, null)) return;

            _health.OnTakeDamage -= HandleHealthChanged;
            _health.OnDie -= HandleDie;
        }

        private void HandleHealthChanged() => Refresh();

        private void HandleDie()
        {
            if (!_hideOnDeath) return;
            gameObject.SetActive(false);
        }

        private void Refresh()
        {
            if (_health.MaxHealth <= 0) return;
            _fillImage.fillAmount = (float)_health.CurrentHealth / _health.MaxHealth;
        }
    }
}
