using UnityEngine;

public class WorldCanvasBillboard : MonoBehaviour
{
    [SerializeField]
    private Transform cameraTransform = null;

    private Quaternion startingRotation = Quaternion.identity;

    private void Start() => startingRotation = transform.rotation;

    private void LateUpdate() => transform.rotation = cameraTransform.rotation * startingRotation;
}
