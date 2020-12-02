using UnityEngine;

namespace BGD.ThirdPersonMovement_2
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] [Range(1f, 5f)] private float _angularSpeed = 1f;

        [SerializeField] private Transform _target;

        private float _angleY;

        private void Start()
        {
            _angleY = transform.rotation.y;
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Z)) _angleY -= _angularSpeed;
            if (Input.GetKey(KeyCode.X)) _angleY += _angularSpeed;

            transform.position = _target.transform.position;
            transform.rotation = Quaternion.Euler(0, _angleY, 0);
        }
    }
}