using UnityEngine;
using Player;
using Utilities.GenericPool;
using Utilities.Factory;

namespace Weapons
{
    public abstract class Gun : MonoBehaviour
    {
        [SerializeField] protected WeaponSettings weaponSettings = null;
        [SerializeField] protected Aiming aim = null;

        protected bool shot = true;
        protected bool canShoot = false;

        protected Transform muzzle = null;

        protected float elapsedTime = 0f;

        protected Pool<GameObject> bulletPool = new Pool<GameObject>();
        protected BulletData bulletData = null;

        private Animator animator = null;

        private void Awake()
        {
            muzzle = transform.GetChild(0);
            bulletData = weaponSettings.bulletPrefab.GetComponent<Bullet>().BulletData;
            SetAndPopulateBulletPool();
            OnAwake();
        }

        private void Start()
        {
            animator = aim.gameObject.GetComponent<CharacterAnimationHandler>().Animator;
            
        }

        protected void Update()
        {
            canShoot = animator.GetCurrentAnimatorStateInfo(1).IsName("Shooting") ? true : false;
            OnUpdate();
        }

        /// <summary>
        /// Sets bullet pool settings and populate the pool
        /// </summary>
        private void SetAndPopulateBulletPool()
        {
            bulletPool = PoolHandler.GetPool<GameObject>(bulletData.poolName);
            bulletPool.OnPush = (_bullet) =>
            {
                _bullet.gameObject.SetActive(false);
                _bullet.transform.SetParent(transform.GetChild(1));
                _bullet.GetComponent<Rigidbody>().velocity = Vector3.zero;
            };

            bulletPool.OnPop = (_bullet) =>
            {
                _bullet.gameObject.SetActive(true);
                _bullet.transform.position = muzzle.position;
                _bullet.transform.rotation = muzzle.rotation;
                _bullet.transform.SetParent(null);
            };

            bulletPool.Prefab = weaponSettings.bulletPrefab;
            bulletPool.Create = (_prefab) =>
            {
                GameObject newBullet = Factory<GameObject>.Create(weaponSettings.bulletPrefab, transform.GetChild(1));
                return newBullet;
            };

            bulletPool.Populate(10);
        }

        protected abstract void OnAwake();
        protected abstract void OnUpdate();
        protected abstract void Shoot();
    } 
}