using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
	[RequireComponent(typeof(Animator))]
	public class EnemyAnimationHandler : MonoBehaviour
	{
		//Animation
		private int shootingAnimatorHash = Animator.StringToHash("Shooting");
		private int speedAnimatorHash = Animator.StringToHash("Speed");
		private float speed = 0f;

		private NavMeshAgent agent = null;
		private EnemyAIShooting shooting = null;

		public Animator Animator { get; private set; } = null;

		private void Awake()
		{
			Animator = GetComponent<Animator>();
			agent = GetComponent<NavMeshAgent>();
			shooting = GetComponent<EnemyAIShooting>();
		}

		private void Update()
		{
			UpdateMovementAnimation();
			UpdateShootAnimation();
		}

		private void UpdateMovementAnimation()
		{
			speed = agent.speed / 2f;
			Debug.Log(agent.speed);
			Animator.SetFloat(speedAnimatorHash, speed, 0.25f, Time.deltaTime);
		}

		private void UpdateShootAnimation()
		{
			if (shooting.CanShoot && !Animator.GetBool(shootingAnimatorHash))
				Animator.SetBool(shootingAnimatorHash, true);
			else if (!shooting.CanShoot && Animator.GetBool(shootingAnimatorHash))
				Animator.SetBool(shootingAnimatorHash, false);
		}
	} 
}
