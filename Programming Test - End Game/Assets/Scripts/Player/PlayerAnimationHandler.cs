using UnityEngine;

namespace Player
{
	[RequireComponent(typeof(Animator))]
	public class PlayerAnimationHandler : MonoBehaviour
	{
		private Aiming aim = null;
		private RigidMovement movement = null;

		private bool aiming = false;

		//Animation
		private int shootingAnimatorHash = Animator.StringToHash("Shooting");
		private int speedAnimatorHash = Animator.StringToHash("Speed");
		private float speed = 0f;
		public Animator Animator { get; private set; } = null;

		private void Awake()
		{
			aim = GetComponent<Aiming>();
			movement = GetComponent<RigidMovement>();
			Animator = GetComponent<Animator>();
		}

		private void Update()
		{
			UpdateMovementAnimation();
			UpdateShootingAnimation();
		}

		private void UpdateShootingAnimation()
		{
			aiming = aim.InputAiming.Equals(Vector2.zero) ? false : true;
			Animator.SetBool(shootingAnimatorHash, aiming);
		}

		private void UpdateMovementAnimation()
		{
			speed = movement.InputDirection.magnitude;

			if (!aim.InputAiming.Equals(Vector2.zero) && Vector2.Dot(aim.InputAiming, movement.InputDirection) < 0)
				speed *= -1;

			Animator.SetFloat(speedAnimatorHash, speed, movement.PlayerSettings.timetoMaxSpeedMovement, Time.deltaTime);
		}
	} 
}
