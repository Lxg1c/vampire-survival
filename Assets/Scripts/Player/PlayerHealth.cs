using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerHealth : MonoBehaviour
    {
        public PlayerStats stats;
        public Slider healthSlider;
        private Animator _animator;

        public GameObject loseCanvas;

        public void Start()
        {
            _animator = GetComponent<Animator>();

            if (stats == null)
                stats = GetComponent<PlayerStats>();

            stats.health = stats.maxHealth;

            healthSlider.maxValue = stats.maxHealth;
            healthSlider.value = stats.health;
            
            Time.timeScale = 1f;
        }

        public void TakeDamage(float damage)
        {
            stats.health -= damage;
            if (stats.health <= 0)
            {
                stats.health = 0;
                Die();
            }

            healthSlider.value = stats.health;
        }

        public void Heal(float amount)
        {
            stats.health = Mathf.Min(stats.health + amount, stats.maxHealth);
            healthSlider.value = stats.health;
        }

        private void Die()
        {
            Debug.Log("Player died!");

            if (_animator != null)
                _animator.SetTrigger("Die");

            if (loseCanvas != null)
                loseCanvas.SetActive(true);

            Time.timeScale = 0f;
        }
    }
}