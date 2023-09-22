using UnityEngine;

public class ShootingTowerController : TowerController
{
    public float rotationSpeed;

    private Transform target;
    private Transform gun;

    public void Start()
    {
        InvokeRepeating(nameof(SelectTarget), 0f, 0.5f);
    }

    public void Update()
    {
        if (target == null)
            return;

        var direction = target.position - transform.position;
        var lookRotation = Quaternion.LookRotation(direction);
        var newRotation = Quaternion.Lerp(
            gun.rotation,
            lookRotation,
            rotationSpeed * Time.deltaTime
        ).eulerAngles;
        gun.rotation = Quaternion.Euler(0f, newRotation.y, 0f);
    }

    public void SelectTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(ObjectTags.Enemy);
        GameObject nearestEnemy = null;
        float nearestDistance = Mathf.Infinity;

        foreach (var enemy in enemies)
        {
            var distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestEnemy = enemy;
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
    }

    private void UpdateGunReference()
    {
        gun = towerLevelPrefabs[towerLevel].transform.Find("gun");
    }
}
