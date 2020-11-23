using UnityEngine;

namespace BGD.ThirdPersonMovement_1
{

    [CreateAssetMenu(fileName = "Characteristics", menuName = "Movement/MovementCharacteristics", order = 1)]
    public class MovementCharacteristics : ScriptableObject
    {
        [SerializeField] private bool _visibleCursor = false;
        
        [SerializeField] private float _movementSpeed = 1f;
        [SerializeField] private float _angularSpeed = 150f;

        [SerializeField] private float _gravity = 15f;


        public bool VisibleCursor => _visibleCursor;

        public float MovementSpeed => _movementSpeed;
        public float AngularSpeed => _angularSpeed;

        public float Gravity => _gravity;
    }
}