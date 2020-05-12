using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu]
public class PlayerInventory : ScriptableObject
{
    public List<Key> keys;

    public void ResetInventory()
    {
        keys.Clear();
    }
}
