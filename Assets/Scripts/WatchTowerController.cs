using UnityEngine;

public class WatchTowerController : TowerController
{
    public float slowdownFactor;

    public void Start()
    {
        InvokeRepeating(nameof(SelectTarget), 0f, 0.5f);
    }


    public void SelectTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(ObjectTags.Enemy);
        foreach (var enemy in enemies)
        {
            var unit = enemy.GetComponent<PathFollower>();
            var distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance <= range && !unit.isSlowedDown)
            {
                unit.moveSpeed /= slowdownFactor;
                unit.isSlowedDown = true;
            }

            if (distance > range && unit.isSlowedDown)
            {
                unit.moveSpeed *= slowdownFactor;
                unit.isSlowedDown = false;
            }
        }
    }

    public override void Upgrade()
    {
        base.Upgrade();
        // change slowdown factor. for example if slowdown factor is 1.2 for level1 tower, it will become 1.5 for level2 tower
        // thus if default unit speed is 20, it will be slowed down to 16.66 for level 1 tower, and 13.33 for level2 tower.
        slowdownFactor *= 1.25f;
    }
}
