using UnityEngine;

public class CannonballController : ProjectileController
{
    public float explosionRadius;
    private Vector3 targetPosition;

    public override void FireAt(Transform target)
    {
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
        // TODO: Implement explosion and damage to enemies within the esplosion radius
    }
}
