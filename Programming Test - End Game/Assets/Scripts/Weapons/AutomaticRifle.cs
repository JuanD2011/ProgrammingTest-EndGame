using UnityEngine;
using Utilities.GenericPool;

namespace Weapons
{
    public class AutomaticRifle : Gun
    {
        protected override void OnUpdate()
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
        
        protected override void Shoot()
        {
            Bullet bullet = PoolHandler.Pop<GameObject>(bulletData.poolName).GetComponent<Bullet>();
            bullet.transform.SetParent(null);
            bullet.Rigidbody.AddForce(aim.transform.forward * weaponSettings.weaponPower * Time.deltaTime, ForceMode.Impulse);
            
            shot = true;
        }

        protected override void OnAwake() { }
    } 
}
