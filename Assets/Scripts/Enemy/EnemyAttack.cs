using Player;
using UnityEngine;

namespace Enemy
{
    public class EnemyAttack : MonoBehaviour
    {
        public float damage = 5f;
        public float attackRange = 1.2f;
        public float attackCooldown = 1f;

        private Transform _player;
        private float _cooldownTimer;

        private void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player")?.transform;
        }

        private void Update()
        {
            if (_player == null) return;

            _cooldownTimer -= Time.deltaTime;

            float distance = Vector2.Distance(transform.position, _player.position);

            if (distance <= attackRange)
            {
                TryAttack();
            }
        }

        private void TryAttack()
        {
            if (_cooldownTimer > 0f) return;
            
            PlayerHealth health = _player.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }

            _cooldownTimer = attackCooldown;
        }
    }
}
