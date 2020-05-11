using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace Player
{
    public class Aiming : MonoBehaviour
    {
        private Vector2 inputAiming = Vector2.zero;

        //Rotation
        private float rotation = 0f, targetRotation = 0f;
        private float currentVelocityRotation = 0f; //SmoothDamp ref

        private RigidMovement movement = null;

        public Vector2 InputAiming { get => inputAiming; private set => inputAiming = value; }

        private void Awake()
        {
            movement = GetComponent<RigidMovement>();
        }

        private void FixedUpdate()
        {
            Rotate();
        }

        /// <summary>
        /// Input direction assignment via PlayerInput Move UnityEvent
        /// </summary>
        /// <param name="_context"></param>
        public void OnAiming(CallbackContext _context) => InputAiming = _context.ReadValue<Vector2>();

        private void Rotate()
        {
            if (inputAiming != Vector2.zero)
            {
                targetRotation = Mathf.Atan2(InputAiming.x, InputAiming.y) * Mathf.Rad2Deg;
                rotation = Mathf.SmoothDampAngle(rotation, targetRotation, ref currentVelocityRotation, movement.PlayerSettings.timeToMaxSpeedRotation);
                movement.Rigidbody.MoveRotation(Quaternion.Euler(Vector3.up * rotation));
            }
        }
    } 
}
