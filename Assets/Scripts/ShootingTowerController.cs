using UnityEngine;

public class ShootingTowerController : TowerController
{
    public float fireRate;
    public int firePower;
    public float rotationSpeed;
    public GameObject projectilePrefab;

    private Transform target;
    private Transform gun;
    private Transform firePoint;
    private float fireCountdown;

    public void Start()
    {
        InvokeRepeating(nameof(SelectTarget), 0f, 0.5f);
        fireCountdown = GetFireCountdown();
    }

    public void Update()
    {
        if (target == null || !target.GetComponent<UnitController>().IsAlive)
            return;

        AimAtTarget();
        FireAtTarget();
    }

    private void AimAtTarget()
    {
        var direction = target.position - transform.position;
        var lookRotation = Quaternion.LookRotation(direction);
        var newRotation = Quaternion.Lerp(
            gun.rotation,
            lookRotation,
            rotationSpeed * Time.deltaTime
        ).eulerAngles;
        gun.rotation = Quaternion.Euler(0f, newRotation.y, 0f);
    }

    private void FireAtTarget()
    {
        fireCountdown -= Time.deltaTime;

        if (fireCountdown <= 0)
        {
            var projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            projectile
                .GetComponent<ProjectileController>()
                .FireAt(target, firePower);

            fireCountdown = GetFireCountdown();
        }
    }

    public void SelectTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(ObjectTags.Enemy);
        GameObject nearestEnemy = null;
        float nearestDistance = Mathf.Infinity;

        foreach (var enemy in enemies)
        {
            if (enemy.GetComponent<UnitController>().IsAlive)
            {
                var distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestEnemy = enemy;
                }
            }
        }

        if (nearestEnemy != null && nearestDistance <= range)
            target = nearestEnemy.transform;
        else
            target = null;
    }

    public override void Awake()
    {
        base.Awake();
        UpdateGunReference();
    }

    public override void Upgrade()
    {
        base.Upgrade();
        UpdateGunReference();

        // increase power by 50%
        firePower += firePower / 2;
    }

    private void UpdateGunReference()
    {
        gun = towerLevelPrefabs[towerLevel].transform.Find("head");
        firePoint = towerLevelPrefabs[towerLevel].transform.Find("head/firePoint");
    }

    private float GetFireCountdown()
    {
        return 1f / fireRate;
    }
}
