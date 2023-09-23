using UnityEngine;

public class CannonballController : ProjectileController
{
    public float explosionRadius;
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

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);

        if (direction.magnitude <= distanceThisFrame)
            Explode();
    }

    private void Explode()
    {
        Destroy(gameObject);
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
