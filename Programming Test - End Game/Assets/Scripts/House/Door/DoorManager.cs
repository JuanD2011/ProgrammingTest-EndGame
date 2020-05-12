using UnityEngine;
using System.Linq;
using System;

public class DoorManager : MonoBehaviour
{
    [SerializeField]
    private int doorId = 0;

    private bool opened = false;

    private Animator animator = null;
    private int openDoorTriggerAnimator = Animator.StringToHash("Open");
    private int closeDoorTriggerAnimator = Animator.StringToHash("Close");

    private new Collider collider = null;

    public static event Action<bool> OnDoorOpened = null;
    public static event Action<bool> OnDoorClosed = null;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        PlayerInventoryManager playerInventory = collision.gameObject.GetComponent<PlayerInventoryManager>();

        if (playerInventory != null && playerInventory.Inventory.keys.Count > 0)
        {
            if (!opened && playerInventory.Inventory.keys.Any(x => x.doorId == doorId))
            {
                OpenDoor();
            }
            else
                OnDoorOpened?.Invoke(false);
        }
        else
            OnDoorOpened?.Invoke(false);
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerInventoryManager playerInventory = other.GetComponent<PlayerInventoryManager>();

        if (playerInventory != null && playerInventory.Inventory.keys.Count > 0)
        {
            if (opened)
                CloseDoor();
            else
                OnDoorClosed?.Invoke(false);
        }
    }


    private void OpenDoor()
    {
        OnDoorOpened?.Invoke(true);
        animator.SetTrigger(openDoorTriggerAnimator);
        collider.isTrigger = true;
        opened = true;
    }

    private void CloseDoor()
    {
        OnDoorClosed?.Invoke(true);
        animator.SetTrigger(closeDoorTriggerAnimator);
        collider.isTrigger = false;
        opened = false;
    }
}
