using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DoorOpeningUI : MonoBehaviour
{
    [SerializeField] private string doorOpenedText = "Door opened!";
    [SerializeField] private string cantOpenText = "You don't have the correct key\n Go find it in loot boxes";
    [SerializeField] private string keyFoundText = "Key found!";
    [SerializeField] private string keyNotFoundText = "No keys here :(";

    private Transform notification = null;
    private TextMeshProUGUI notificationText = null;

    [Header("Animation")]
    [SerializeField] private Vector3 openedScale = Vector3.zero;
    [SerializeField] private Vector3 closedScale = Vector3.zero;
    [SerializeField] private LeanTweenType tweenType = LeanTweenType.notUsed;
    [SerializeField] private float animationTime = 0f;
    [SerializeField] private float notificationStayTime = 0f;

    private WaitForSeconds timeToHide; 

    private void Start()
    {
        notification = transform.GetChild(0);
        notificationText = notification.GetComponentInChildren<TextMeshProUGUI>();
        timeToHide = new WaitForSeconds(notificationStayTime);
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
            Show(doorOpenedText);
        else
            Show(cantOpenText);
    }

    private void Show(string _text)
    {
        notificationText.text = _text;
        notification.gameObject.SetActive(true);
        notification.LeanScale(openedScale, animationTime).setEase(tweenType).setOnComplete(() => StartCoroutine(Hide()));
    }

    private IEnumerator Hide()
    {
        yield return timeToHide;
        notification.LeanScale(closedScale, animationTime).setEase(tweenType).setOnComplete(() => notification.gameObject.SetActive(false));
    }
}
