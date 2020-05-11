using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace Player
{
	[RequireComponent(typeof(Rigidbody))]
	public class RigidMovement : MonoBehaviour
	{
		[SerializeField]
		private PlayerSettings playerSettings = null;

		//Input
		private Vector2 inputDirection = Vector2.zero;

		//Speed
		private Vector3 direction = Vector3.zero;
		private float speed = 0f, targetSpeed = 0f;
		private float currentVelocityMovement = 0f; //SmoothDamp ref 

		//Rotation
		private float rotation = 0f, targetRotation = 0f;
		private float currentVelocityRotation = 0f; //SmoothDamp ref
		private Aiming aim = null;

		public Vector2 InputDirection { get => inputDirection; private set => inputDirection = value; }
		public PlayerSettings PlayerSettings { get => playerSettings; private set => playerSettings = value; }
        public Rigidbody Rigidbody { get; private set; } = null;

		private void Awake()
		{
			Rigidbody = GetComponent<Rigidbody>();
			aim = GetComponent<Aiming>();
		}

		private void FixedUpdate()
		{
			Rotate();
			Move();
		}

		/// <summary>
		/// Input direction assignment via PlayerInput Move UnityEvent
		/// </summary>
		/// <param name="_context"></param>
		public void OnMoveAction(CallbackContext _context) => InputDirection = _context.ReadValue<Vector2>();

		/// <summary>
		/// Finds the target rotation on y axis and rotate the player smoothly to it
		/// </summary>
		private void Rotate()
		{
			if (InputDirection != Vector2.zero && aim.InputAiming.magnitude.Equals(0f))
			{
				targetRotation = Mathf.Atan2(InputDirection.x, InputDirection.y) * Mathf.Rad2Deg;
				rotation = Mathf.SmoothDampAngle(rotation, targetRotation, ref currentVelocityRotation, PlayerSettings.timeToMaxSpeedRotation);
				Rigidbody.MoveRotation(Quaternion.Euler(Vector3.up * rotation));
			}
		}

		/// <summary>
		/// Calculates the speed to move the player based on the input magnitude
		/// </summary>
		private void Move()
		{
			targetSpeed = PlayerSettings.maxNormalSpeed * InputDirection.magnitude;
			direction = new Vector3(InputDirection.x, 0f, InputDirection.y);
			speed = Mathf.SmoothDamp(speed, targetSpeed, ref currentVelocityMovement, PlayerSettings.timetoMaxSpeedMovement);
			Rigidbody.MovePosition(transform.position + direction * speed * Time.deltaTime);
		}
	}
}
