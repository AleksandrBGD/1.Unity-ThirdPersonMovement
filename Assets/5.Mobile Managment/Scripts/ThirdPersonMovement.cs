using UnityEngine;

namespace BGD.ThirdPersonMovement_2
{

    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(Animator))]
    public class ThirdPersonMovement : MonoBehaviour
    {
        [SerializeField] private Transform _camera;
        [SerializeField] private MovementCharacteristics _characteristics;

        [SerializeField] private JoystickInput _joystick;

        private bool _canJumping;

        private float _vertical, _horizontal;

        private readonly string STR_SPEED = "Speed";
        private readonly string STR_JUMP = "Jump";

        private CharacterController _controller;
        private Animator _animator;

        private Vector3 _direction;
        private Quaternion _look;


        private Vector3 TargetRotate => _direction;

        private bool Idle => _horizontal == 0.0f && _vertical == 0.0f;
        private float MaxMovementValue => Mathf.Max(Mathf.Abs(_horizontal), Mathf.Abs(_vertical));


        private void Start()
        {
            _controller = GetComponent<CharacterController>();
            _animator = GetComponent<Animator>();

            Cursor.visible = _characteristics.VisibleCursor;
        }

        private void Update()
        {
            Movement();
            Rotate();
        }

        public void EnabledJumping(bool isActive) => _canJumping = isActive;

        private void Movement()
        {
            if (_controller.isGrounded)
            {
                _horizontal = _joystick.Horizontal;
                _vertical = _joystick.Vertical;

                _direction = _camera.TransformDirection(_horizontal, 0, _vertical).normalized;

                PlayAnimation();
                Jump();
            }

            _direction.y -= _characteristics.Gravity * Time.deltaTime;

            float speed = _characteristics.MovementSpeed * MaxMovementValue;
            Vector3 dir = _direction * speed * Time.deltaTime;

            dir.y = _direction.y;

            _controller.Move(dir);
        }

        private void Rotate()
        {
            if (Idle) return;

            Vector3 target = TargetRotate;
            target.y = 0;

            _look = Quaternion.LookRotation(target);

            float speed = _characteristics.AngularSpeed * Time.deltaTime;

            transform.rotation = Quaternion.RotateTowards(transform.rotation, _look, speed);
        }

        private void Jump()
        {
            if (_canJumping)
            {
                _animator.SetTrigger(STR_JUMP);
                _direction.y += _characteristics.JumpForce;
                _canJumping = false;
            }
        }

        private void PlayAnimation()
        {
            _animator.SetFloat(STR_SPEED, MaxMovementValue);
        }
    }
}