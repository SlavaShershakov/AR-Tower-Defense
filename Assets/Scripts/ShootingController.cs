using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class ShootingController : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [Range(0f, 89.9f)]
    [SerializeField] private float barrelAngle = 45.0f;
    [SerializeField] private float reloadTime;
    
    private Transform launchPointTransform;
    private Transform barrelTrnasform;
    private Transform aimTransform;

    private readonly float g = Physics.gravity.y;

    private ObjectPool<GameObject> projectilePool;

    private Camera mainCamera;

    private void Start()
    {
        launchPointTransform = GameObject.Find("LaunchPoint").transform;
        barrelTrnasform = GameObject.Find("TurretBarrel").transform;
        aimTransform = GameObject.Find("Aim").transform;

        projectilePool = ObjectPooler.Instance.FindPool<Projectile>();

        mainCamera = Camera.main;

        StartCoroutine(ShootingRoutine());
    }
    private IEnumerator ShootingRoutine()
    {
        RaycastHit hit = new RaycastHit();

        while (true)
        {
            yield return new WaitUntil(() => Input.touchCount > 0 && Physics.Raycast(mainCamera.ScreenPointToRay(Input.GetTouch(0).position), out hit));

            aimTransform.position = hit.point;
            RotateTurret();
            Shoot(LoadNewProjetile());

            yield return new WaitForSeconds(reloadTime);
        }

        void RotateTurret()
        {
            transform.rotation = Quaternion.LookRotation(aimTransform.position, Vector3.up);
        }
    }
    private GameObject LoadNewProjetile()
    {
        var newProjectile = projectilePool.Get();
        newProjectile.transform.position = launchPointTransform.position;
        newProjectile.SetActive(true);
        return newProjectile;
    }
    private void Shoot(GameObject projectile)
    {
        barrelTrnasform.localEulerAngles = new Vector3(-barrelAngle, 0.0f, 0.0f);

        var projectileRb = projectile.GetComponent<Rigidbody>();
        projectileRb.velocity = launchPointTransform.forward * CalculateShootForce();

        float CalculateShootForce()
        {
            Vector3 diraction = aimTransform.position - projectile.transform.position;
            Vector3 directionXZ = new Vector3(diraction.x, 0.0f, diraction.z);

            float x = directionXZ.magnitude;
            float y = diraction.y;

            float angleInRadians = barrelAngle * Mathf.Deg2Rad;

            float v2 = (g * x * x) / (2 * (y - Mathf.Tan(angleInRadians) * x) * Mathf.Pow(Mathf.Cos(angleInRadians), 2));
            float v = Mathf.Sqrt(Mathf.Abs(v2));
            return v;
        }
    }
}