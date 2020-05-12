using UnityEngine;
using Utilities.Factory;
using Utilities.GenericPool;

namespace Weapons
{
    public class AutomaticRifle : Gun
    {
        protected override void OnAwake()
        {
            muzzle = transform.GetChild(0);
            flashEffect = GetComponentInChildren<ParticleSystem>();
            bulletPoolName = string.Format("{0} {1}", transform.name, bulletData.poolName);
            SetAndPopulateBulletPool();
        }

        protected override void OnUpdate()
        {
            if (aim != null)
                UpdatePlayerRifle();
            else
                UpdateEnemyRifle();
        }

        private void UpdatePlayerRifle()
        {
            if (aim.InputAiming.magnitude.Equals(1f) && !shot && canShoot)
                Shoot();
            else if (!aim.InputAiming.magnitude.Equals(1f))
            {
                elapsedTime = 0f;
                shot = false;
            }

            if (shot)
            {
                elapsedTime += Time.deltaTime;
                if (elapsedTime >= weaponSettings.FiringRate)
                {
                    elapsedTime = 0f;
                    shot = false;
                }
            }
        }

        private void UpdateEnemyRifle()
        {
            if (enemyShooting.CanShoot && !shot && canShoot)
                ShootEnemyGun();
            else if (!enemyShooting.CanShoot)
            {
                elapsedTime = 0f;
                shot = false;
            }

            if (shot)
            {
                elapsedTime += Time.deltaTime;
                if (elapsedTime >= weaponSettings.FiringRate)
                {
                    elapsedTime = 0f;
                    shot = false;
                }
            }
        }

        protected override void Shoot()
        {
            Bullet bullet = PoolHandler.Pop<GameObject>(bulletPoolName).GetComponent<Bullet>();
            bullet.transform.SetParent(null);
            bullet.Rigidbody.AddForce(aim.transform.forward * weaponSettings.weaponPower * Time.deltaTime, ForceMode.Impulse);

            AudioManager.PlaySFX(weaponSettings.fireSFX, true);
            flashEffect.Play();

            shot = true;
        }

        protected override void ShootEnemyGun()
        {
            Bullet bullet = PoolHandler.Pop<GameObject>(bulletPoolName).GetComponent<Bullet>();
            bullet.transform.SetParent(null);
            bullet.Rigidbody.AddForce(enemyShooting.transform.forward * weaponSettings.weaponPower * Time.deltaTime, ForceMode.Impulse);
            
            AudioManager.PlaySFX(weaponSettings.fireSFX, true);
            flashEffect.Play();

            shot = true;
        }

        protected override void SetAndPopulateBulletPool()
        {
            bulletPool = PoolHandler.GetPool<GameObject>(bulletPoolName);
            bulletPool.OnPush = (_bullet) =>
            {
                _bullet.gameObject.SetActive(false);
                _bullet.transform.SetParent(transform.GetChild(1));
                _bullet.GetComponent<Rigidbody>().velocity = Vector3.zero;
                _bullet.GetComponent<Bullet>().PoolName = bulletPoolName;
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
    } 
}
