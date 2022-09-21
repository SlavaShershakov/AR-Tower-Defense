using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Camera mainCamera;
    [HideInInspector] public Slider slider;

    private void Awake()
    {
        slider = GetComponentInChildren<Slider>();
    }
    private void Start()
    {
        GetComponent<Canvas>().worldCamera = mainCamera = Camera.main;
    }
    private void LateUpdate()
    {
        transform.LookAt(mainCamera.transform);
    }
}