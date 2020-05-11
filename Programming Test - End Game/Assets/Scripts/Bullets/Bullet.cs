using UnityEngine;
using Utilities.GenericPool;

namespace Weapons
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private BulletData bulletData = null;

        public BulletData BulletData { get => bulletData; private set => bulletData = value; }

        public Rigidbody Rigidbody { get; private set; }

        private void Awake() => Rigidbody = GetComponent<Rigidbody>();

        private void OnEnable() => Invoke("PoolBullet", AmmoSettings.Instance.lifeTime);

        private void OnDisable() => CancelInvoke("PoolBullet");

        private void PoolBullet() => gameObject.Pool(bulletData.poolName);
    }
}
