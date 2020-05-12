using UnityEngine;
using Utilities.GenericPool;

namespace Weapons
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private BulletData bulletData = null;

        public BulletData BulletData { get => bulletData; private set => bulletData = value; }

        public string PoolName { get; set; }

        public Rigidbody Rigidbody { get; private set; }

        private void Awake() => Rigidbody = GetComponent<Rigidbody>();

        private void OnEnable() => Invoke("PoolBullet", AmmoSettings.Instance.lifeTime);

        private void OnDisable() => CancelInvoke("PoolBullet");

        private void PoolBullet() => gameObject.Pool(PoolName);

        private void OnTriggerEnter(Collider other)
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
            if (damageable != null)
                damageable.DoDamage(bulletData.Damage);

            gameObject.Pool(PoolName);
        }
    }
}
