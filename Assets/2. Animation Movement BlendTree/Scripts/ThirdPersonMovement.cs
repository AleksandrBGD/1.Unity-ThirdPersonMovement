using UnityEngine;

namespace BGD.ThirdPersonMovement_1
{

    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(Animator))]
    public class ThirdPersonMovement : MonoBehaviour
    {
        [SerializeField] private Transform _camera;
        [SerializeField] private MovementCharacteristics _characteristics;

        private float _vertical, _horizontal;

        private readonly string STR_VERTICAL = "Vertical";
        private readonly string STR_HORIZONTAL = "Horizontal";

        private const float DISTANCE_OFFSET_CAMERA = 5f;

        private CharacterController _controller;
        private Animator _animator;

        private Vector3 _direction;
        private Quaternion _look;

        private Vector3 TargetRotate => _camera.forward * DISTANCE_OFFSET_CAMERA;
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

                _direction = transform.TransformDirection(_horizontal, 0, _vertical).normalized;

                PlayAnimation();
            }

            _direction.y -= _characteristics.Gravity * Time.deltaTime;
            Vector3 dir = _direction * _characteristics.MovementSpeed * Time.deltaTime;

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

        private void PlayAnimation()
        {
            _animator.SetFloat(STR_VERTICAL,_vertical);
            _animator.SetFloat(STR_HORIZONTAL, _horizontal);
        }
    }
}