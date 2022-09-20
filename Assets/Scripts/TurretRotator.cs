using UnityEngine;

public class TurretRotator : MonoBehaviour
{
    private Transform aimTransform;

    private void Start()
    {
        aimTransform = GameObject.Find("Aim").transform;
    }
    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(aimTransform.position, Vector3.up);
    }
}
