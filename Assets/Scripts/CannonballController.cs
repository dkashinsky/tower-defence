using UnityEngine;

public class CannonballController : ProjectileController
{
    public float explosionRadius;
    public GameObject explosionPrefab;
    private Vector3 targetPosition;

    public override void FireAt(Transform target, int power)
    {
        base.FireAt(target, power);

        targetPosition = target.position;
    }

    public override void Update()
    {
        var distanceThisFrame = projectileSpeed * Time.deltaTime;
        var direction = targetPosition - transform.position;

        if (direction.magnitude <= distanceThisFrame)
        {
            Explode();
        }

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
    }

    private void Explode()
    {
        var explosion = Instantiate(explosionPrefab, targetPosition, transform.rotation);
        Destroy(gameObject);
        Destroy(explosion, 3f);
        ApplyDamageToEnemies();
    }

    private void ApplyDamageToEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(ObjectTags.Enemy);
        foreach (var enemy in enemies)
        {
            var distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance <= explosionRadius)
            {
                // apply damage depending on the distance from the epicenter of explosion
                enemy
                    .GetComponent<UnitController>()
                    .ApplyDamage(Mathf.FloorToInt(power * distance / explosionRadius));
            }
        }
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
