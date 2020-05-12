using System.Collections;
using TMPro;
using UnityEngine;

public class DoorOpeningUI : MonoBehaviour
{
    private string doorOpenedText = "Door opened!";
    private string cantOpenText = "You don't have the correct key\n Go find it in loot boxes";

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
    }

    private void OnDestroy()
    {
        DoorManager.OnDoorOpened -= OnDoorOpened;
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
