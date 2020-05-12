using UnityEngine;
using Player;
using Utilities.GenericPool;

namespace Weapons
{
    public abstract class Gun : MonoBehaviour
    {
        [SerializeField] protected WeaponSettings weaponSettings = null;
        [SerializeField] protected Aiming aim = null;
        [SerializeField] protected Enemy.EnemyAIShooting enemyShooting = null;

        protected bool shot = true;
        protected bool canShoot = false;

        protected Transform muzzle = null;
        protected string bulletPoolName = string.Empty;

        protected float elapsedTime = 0f;

        protected Pool<GameObject> bulletPool = new Pool<GameObject>();
        protected BulletData bulletData = null;

        [SerializeField] private Animator animator = null;

        protected ParticleSystem flashEffect = null;

        private void Awake()
        {
            bulletData = weaponSettings.bulletPrefab.GetComponent<Bullet>().BulletData;
            OnAwake();
        }

        protected void Update()
        {
            canShoot = animator.GetCurrentAnimatorStateInfo(1).IsName("Shooting") ? true : false;
            OnUpdate();
        }

        /// <summary>
        /// Sets bullet pool settings and populate the pool
        /// </summary>
        protected abstract void SetAndPopulateBulletPool();

        protected abstract void OnAwake();
        protected abstract void OnUpdate();
        protected abstract void Shoot();
        protected abstract void ShootEnemyGun();
    } 
}