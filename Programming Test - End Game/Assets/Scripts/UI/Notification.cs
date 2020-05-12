using System.Collections;
using TMPro;
using UnityEngine;

public class Notification : Singleton<Notification>
{
    private Transform notification = null;
    private TextMeshProUGUI notificationText = null;

    [Header("Animation")]
    [SerializeField] private Vector3 openedScale = Vector3.zero;
    [SerializeField] private Vector3 closedScale = Vector3.zero;
    [SerializeField] private LeanTweenType tweenType = LeanTweenType.notUsed;
    [SerializeField] private float animationTime = 0f;
    [SerializeField] private float notificationStayTime = 0f;

    private WaitForSeconds timeToHide;

    protected override void OnAwake()
    {
        notification = transform.GetChild(0);
        notificationText = notification.GetComponentInChildren<TextMeshProUGUI>();
        timeToHide = new WaitForSeconds(notificationStayTime);
    }

    public static void Show(string _text)
    {
        Instance.notificationText.text = _text;
        Instance.notification.gameObject.SetActive(true);
        Instance.notification.LeanScale(Instance.openedScale, Instance.animationTime).setEase(Instance.tweenType).setOnComplete(() => Instance.StartCoroutine(Instance.Hide()));
    }

    private IEnumerator Hide()
    {
        yield return timeToHide;
        notification.LeanScale(closedScale, animationTime).setEase(tweenType).setOnComplete(() => notification.gameObject.SetActive(false));
    }
}
