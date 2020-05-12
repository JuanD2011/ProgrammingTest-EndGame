using System.Collections.Generic;
using UnityEngine;

public class LootBox : MonoBehaviour
{
    [SerializeField]
    private List<Key> keys = new List<Key>();

    public bool Opened { get; set; } = false;

    public List<Key> Keys { get => keys; private set => keys = value; }

    public void Fill()
    {
        Opened = false;
        keys.Clear();
        Key key = new Key();
        key.doorId = 1;
        Keys.Add(key);
    }
}
