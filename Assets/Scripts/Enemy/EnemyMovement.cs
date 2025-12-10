using UnityEngine;

namespace Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        public float moveSpeed = 3f;
        public float stopDistance = 1f;

        private Transform _target;
        private Rigidbody2D _rb;


        private void Start()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                _target = player.transform;
            }

            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (_target == null) return;

            float distanceToPlayer = Vector2.Distance(transform.position, _target.position);

            if (distanceToPlayer > stopDistance)
            {
                Vector2 direction = (_target.position - transform.position).normalized;
                _rb.linearVelocity = direction * moveSpeed;
            }
            else
            {
                _rb.linearVelocity = Vector2.zero;

            }
        }
    }
}
