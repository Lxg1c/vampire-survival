using UnityEngine;
using Utils;

namespace Player
{
    public class AutoShooter : MonoBehaviour
    {
        public GameObject projectilePrefab;
        public Transform shootPoint;
        public PlayerStats stats;
        public float fireRate = 1f;

        public float attackRadius = 8f;

        private float _timer;

        private void Update()
        {
            _timer -= Time.deltaTime;

            if (_timer <= 0f)
            {
                TryShoot();
                _timer = fireRate;
            }
        }

        void TryShoot()
        {
            Transform target = FindNearestEnemyInRadius();
            if (target == null) return;

            GameObject proj = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);

            Projectile p = proj.GetComponent<Projectile>();
            p.SetTarget(target);

            // ВАЖНО: берём актуальный урон из stats
            p.damage = stats.damage;
        }

        Transform FindNearestEnemyInRadius()
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            Transform nearest = null;
            float minDist = Mathf.Infinity;

            foreach (var e in enemies)
            {
                float dist = Vector2.Distance(transform.position, e.transform.position);

                if (dist <= attackRadius && dist < minDist)
                {
                    minDist = dist;
                    nearest = e.transform;
                }
            }

            return nearest;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRadius);
        }
    }
}