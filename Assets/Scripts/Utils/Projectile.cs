using UnityEngine;
using Enemy;

namespace Utils
{
    public class Projectile : MonoBehaviour
    {
        public float speed = 8f;
        public float damage = 10f;
        public float lifeTime = 5f;

        private Transform _target;

        private void Start()
        {
            Destroy(gameObject, lifeTime);
        }

        public void SetTarget(Transform t)
        {
            _target = t;
        }

        void Update()
        {
            if (_target == null)
            {
                Destroy(gameObject);
                return;
            }

            Vector2 dir = (_target.position - transform.position).normalized;

            transform.position += (Vector3)dir * speed * Time.deltaTime;
            
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player")) return;

            if (collision.CompareTag("Enemy"))
            {
                EnemyHealth hp = collision.GetComponent<EnemyHealth>();
                if (hp != null)
                {
                    hp.TakeDamage(damage);
                }

                Destroy(gameObject);
            }
        }
    }
}