using UnityEngine;

namespace BGD.ThirdPersonMovement_2
{

    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(Animator))]
    public class ThirdPersonMovement : MonoBehaviour
    {
        [SerializeField] private Transform _camera;
        [SerializeField] private MovementCharacteristics _characteristics;

        private float _vertical, _horizontal, _run;

        private readonly string STR_VERTICAL = "Vertical";
        private readonly string STR_HORIZONTAL = "Horizontal";
        private readonly string STR_SPEED = "Speed";

        private readonly string STR_RUN = "Run";
        private readonly string STR_JUMP = "Jump";

        private const float DISTANCE_OFFSET_CAMERA = 5f;

        private CharacterController _controller;
        private Animator _animator;

        private Vector3 _direction;
        private Quaternion _look;

        private Vector3 TargetRotate => _direction;
        private bool Idle => _horizontal == 0.0f && _vertical == 0.0f;


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

        private void Movement()
        {
            if (_controller.isGrounded)
            {
                _horizontal = Input.GetAxis(STR_HORIZONTAL);
                _vertical = Input.GetAxis(STR_VERTICAL);
                _run = Input.GetAxis(STR_RUN);

                _direction = _camera.TransformDirection(_horizontal, 0, _vertical).normalized;

                PlayAnimation();
                Jump();
            }

            _direction.y -= _characteristics.Gravity * Time.deltaTime;

            float speed = _run * _characteristics.RunSpeed + _characteristics.MovementSpeed;
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
            if (Input.GetButtonDown(STR_JUMP))
            {
                _animator.SetTrigger(STR_JUMP);
                _direction.y += _characteristics.JumpForce;
            }
        }

        private void PlayAnimation()
        {
            float maxMovementValue = Mathf.Max(Mathf.Abs(_horizontal), Mathf.Abs(_vertical));
            float speed = _run * maxMovementValue + maxMovementValue;

            _animator.SetFloat(STR_SPEED, speed);
        }
    }
}