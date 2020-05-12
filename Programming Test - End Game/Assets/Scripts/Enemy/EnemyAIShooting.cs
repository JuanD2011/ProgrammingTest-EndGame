using UnityEngine;

namespace Enemy
{
    public class EnemyAIShooting : MonoBehaviour
    {
        [SerializeField] private Transform target = null;
        [SerializeField] private EnemySettings enemySettings;

        private float distanceToTarget = 0f;

        public bool CanShoot { get; private set; }

        private void Update()
        {
            if (Time.frameCount % 30 == 0)
            {
                distanceToTarget = (target.position - transform.position).sqrMagnitude;
                CanShoot = distanceToTarget <= enemySettings.shootDistance ? true : false;
            }

            if (CanShoot)
                UpdateRotation();
        }

        private void UpdateRotation() => transform.LookAt(target);
    } 
}
