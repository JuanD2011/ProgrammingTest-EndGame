using UnityEngine;
using Player;

public class AimingAid : MonoBehaviour
{
    private Aiming aim = null;

    private float targetRotation = 0f, rotation = 0f;
    private float currentVelocityRotation = 0f;

    private GameObject aidImageObject = null;

    private void Awake()
    {
        aim = GetComponentInParent<Aiming>();
        aidImageObject = transform.GetChild(0).gameObject;
    }

    private void LateUpdate() => RotateToAim();

    /// <summary>
    /// Rotate the canvas in the direction the player is pointing 
    /// </summary>
    private void RotateToAim()
    {
        if (aim.InputAiming != Vector2.zero)
        {
            targetRotation = Mathf.Atan2(aim.InputAiming.x, aim.InputAiming.y) * Mathf.Rad2Deg;
            rotation = Mathf.SmoothDampAngle(rotation, targetRotation, ref currentVelocityRotation, 0.1f);
            transform.eulerAngles = Vector3.up * rotation;
            if (!aidImageObject.activeInHierarchy) aidImageObject.SetActive(true);
        }
        else if (aidImageObject.activeInHierarchy) aidImageObject.SetActive(false);
    }
}
