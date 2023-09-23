using UnityEngine;

public class BoltController : ProjectileController
{
    protected override void HitTarget() 
    {
        Destroy(target.gameObject);
        Destroy(gameObject);
    }
}
