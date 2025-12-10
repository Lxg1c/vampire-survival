using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    public class PlayerMovement : MonoBehaviour
    {
        private PlayerInputActions _input;
        private Rigidbody2D _rb;
        private Animator _animator;

        public PlayerStats stats;
        private Vector2 _moveInput;
        private bool _canMove = true;

        private void Awake()
        {
            _input = new PlayerInputActions();
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();

            if (stats == null)
                stats = GetComponent<PlayerStats>();

            _input.Player.Move.performed += ctx => _moveInput = ctx.ReadValue<Vector2>();
            _input.Player.Move.canceled += ctx => _moveInput = Vector2.zero;
        }

        private void OnEnable() => _input.Enable();
        private void OnDisable() => _input.Disable();

        private void FixedUpdate()
        {
            HandleMovement();
        }

        private void HandleMovement()
        {
            if (!_canMove)
            {
                _rb.linearVelocity = Vector2.zero;
                _animator.SetBool("Move", false);
                return;
            }
            
            float speed = stats.moveSpeed;

            _rb.MovePosition(_rb.position + _moveInput * speed * Time.fixedDeltaTime);

            bool isMoving = _moveInput.sqrMagnitude > 0.01f;
            _animator.SetBool("Move", isMoving);

            if (_moveInput.x > 0.01f)
                transform.localScale = new Vector3(4, 4, 4);
            else if (_moveInput.x < -0.01f)
                transform.localScale = new Vector3(-4, 4, 4);
        }
    }
}