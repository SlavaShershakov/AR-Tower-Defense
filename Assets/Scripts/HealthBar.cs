using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Camera mainCamera;
    [HideInInspector] public Slider slider;

    private void Start()
    {
        slider = GetComponentInChildren<Slider>();
        GetComponent<Canvas>().worldCamera = mainCamera = Camera.main;
    }
    private void LateUpdate()
    {
        transform.LookAt(mainCamera.transform);
    }
}