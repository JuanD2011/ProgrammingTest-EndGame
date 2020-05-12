using System.Collections.Generic;
using UnityEngine;

public class LootBox : MonoBehaviour
{
    [SerializeField]
    private List<Key> keys = new List<Key>();

    public bool Opened { get; set; } = false;

    public List<Key> Keys { get => keys; private set => keys = value; }
}
