using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float projectileSpeed;

    protected Transform target;
    protected int power;

    public virtual void FireAt(Transform target, int power)
    {
        this.target = target;
        this.power = power;
    }

    public virtual void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        var distanceThisFrame = projectileSpeed * Time.deltaTime;
        var direction = target.position - transform.position;

        if (direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
    }

    protected virtual void HitTarget()
    {
    }
}
