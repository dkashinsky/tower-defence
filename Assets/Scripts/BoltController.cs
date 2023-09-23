public class BoltController : ProjectileController
{
    protected override void HitTarget() 
    {
        target
            .GetComponent<UnitController>()
            .ApplyDamage(power);

        Destroy(gameObject);
    }
}
