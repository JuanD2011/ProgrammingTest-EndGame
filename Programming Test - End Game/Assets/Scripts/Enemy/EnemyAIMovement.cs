using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class EnemyAIMovement : MonoBehaviour
    {
        private NavMeshAgent agent = null;

        [SerializeField] private Transform target = null;
        [SerializeField] private EnemySettings enemySettings;

        //private bool canShoot = false;
        private bool canPursuit = false;
        private bool guard = false;

        private float distanceToTarget = 0f;
        private float elapsedTime = 0f;

        private void Awake() => agent = GetComponent<NavMeshAgent>();

        private void Update()
        {
            if (Time.frameCount % 60 == 0)
            {
                distanceToTarget = (target.position - transform.position).sqrMagnitude;
                canPursuit = distanceToTarget <= enemySettings.maxPursuitDistance && distanceToTarget >= enemySettings.shootDistance ? true : false;
                //canShoot = distanceToTarget <= enemySettings.shootDistance ? true : false;
                UpdateMovement();
            }

            if (!canPursuit && guard)
            {
                elapsedTime += Time.deltaTime;
                if (elapsedTime >= enemySettings.guardTime)
                    Guard();
            }
        }

        private void UpdateMovement()
        {
            if (canPursuit)
            {
                guard = false;
                agent.speed = enemySettings.pursuitSpeed;
                agent.destination = target.position;
                agent.isStopped = false;
            }
            else if (!guard)
            {
                guard = true;
                agent.speed = 0f;
                agent.isStopped = true;
            }
        }

        private Vector3 GetRandomNavMeshPointOnGuardRadius()
        {
            Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * enemySettings.guardWalkRadius;
            randomDirection += transform.position;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, enemySettings.guardWalkRadius, 1);
            return hit.position;
        }

        private void Guard()
        {
            elapsedTime = 0f;
            agent.speed = enemySettings.guardSpeed;
            agent.destination = GetRandomNavMeshPointOnGuardRadius();
            agent.isStopped = false;
        }
    } 
}
