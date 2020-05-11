using UnityEngine;

namespace Weapons
{
    [CreateAssetMenu]
    public class BulletData : ScriptableObject
    {
        public AmmoType ammoType;

        public string poolName;

        public float Damage
        {
            get
            {
                float damage = 0f;
                switch (ammoType)
                {
                    case AmmoType.Rifle:
                        damage = AmmoSettings.Instance.rifleAmmoDamage;
                        break;
                    case AmmoType.SMG:
                        damage = AmmoSettings.Instance.smgAmmoDamage;
                        break;
                    default:
                        break;
                }
                return damage;
            }
        }
    } 
}
