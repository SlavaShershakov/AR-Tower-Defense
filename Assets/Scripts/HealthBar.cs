using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    private Camera mainCamera;

    private void Start()
    {
        GetComponent<Canvas>().worldCamera = mainCamera = Camera.main;
    }
    private void LateUpdate()
    {
        transform.LookAt(mainCamera.transform);
    }
}