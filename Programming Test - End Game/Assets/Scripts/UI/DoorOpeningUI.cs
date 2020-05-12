using System.Collections.Generic;
using UnityEngine;

public class DoorOpeningUI : MonoBehaviour
{
    [SerializeField] private string doorOpenedText = "Door opened!";
    [SerializeField] private string cantOpenText = "You don't have the correct key\n Go find it in loot boxes";
    [SerializeField] private string keyFoundText = "Key found!";
    [SerializeField] private string keyNotFoundText = "No keys here :(";

    private void Start()
    {       
        DoorManager.OnDoorOpened += OnDoorOpened;
        PlayerInventoryManager.OnKeyFound += OnKeyFound;
    }

    private void OnDestroy()
    {
        DoorManager.OnDoorOpened -= OnDoorOpened;
        PlayerInventoryManager.OnKeyFound -= OnKeyFound;
    }

    private void OnKeyFound(List<Key> obj)
    {
        if (obj.Count > 0)
            Show(keyFoundText);
        else
            Show(keyNotFoundText);
    }

    private void OnDoorOpened(bool _value)
    {
        if (_value)
        {
            Show(doorOpenedText);
            AudioManager.PlaySFX(SFXAudioClips.Instance.successSFX, false);
        }
        else
        {
            Show(cantOpenText);
            AudioManager.PlaySFX(SFXAudioClips.Instance.failSFX, false);
        }
    }

    private void Show(string _text) => Notification.Show(_text);
}
