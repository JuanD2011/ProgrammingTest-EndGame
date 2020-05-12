using Player;
using UnityEngine;

public class RoofManager : MonoBehaviour
{
    [SerializeField] private float fadeTime = 0f;
    [SerializeField] private LeanTweenType tweenType = LeanTweenType.notUsed;

    private void OnTriggerEnter(Collider other)
    {
        RigidMovement player = other.GetComponent<RigidMovement>();

        if (player != null)
            Hide();
    }

    private void OnTriggerExit(Collider other)
    {
        RigidMovement player = other.GetComponent<RigidMovement>();

        if (player != null)
            Show();
    }

    private void Hide() => LeanTween.alpha(gameObject, 0f, fadeTime).setEase(tweenType);

    private void Show() => LeanTween.alpha(gameObject, 1f, fadeTime).setEase(tweenType);
}
