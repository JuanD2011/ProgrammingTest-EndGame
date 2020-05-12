using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryManager : MonoBehaviour
{
    [SerializeField]
    private PlayerInventory inventory = null;

    public PlayerInventory Inventory { get => inventory; private set => inventory = value; }

    public event Action<List<Key>> OnKeyFound = null;

    private void OnCollisionEnter(Collision collision)
    {
        LootBox box = collision.gameObject.GetComponent<LootBox>();

        if (box != null)
        {
            if (!box.Opened)
                OpenBox(box);
        }
    }

    private void OpenBox(LootBox _box)
    {
        _box.Opened = true;
        if (_box.Keys.Count > 0)
        {
            foreach (Key item in _box.Keys)
            {
                inventory.keys.Add(item);
            } 
        }
        OnKeyFound?.Invoke(_box.Keys);
    }
}
