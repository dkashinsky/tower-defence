using UnityEngine;

public class PathFollower : MonoBehaviour
{
    public Transform[] waypoints;
    public float moveSpeed = 20f;
    public float rotationSpeed = 1f;
    public bool isSlowedDown;

    private int waypointIndex;
    private float waypointThreshold = 1f;
    private UnitController unit = null;

    void Start()
    {
        unit = GetComponent<UnitController>();
        waypointIndex = 0;
    }

    void Update()
    {
        // only move object if unit is alive and not hit
        if (unit == null || (unit.IsAlive && !unit.IsHit))
            Move();
    }

    private void Move()
    {
        if (waypointIndex < waypoints.Length)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                waypoints[waypointIndex].position,
                moveSpeed * Time.deltaTime
            );

            var direction = waypoints[waypointIndex].position - transform.position;
            if (direction.normalized != Vector3.zero)
            {
                var lookRotation = Quaternion.LookRotation(direction, Vector3.up);
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    lookRotation,
                    rotationSpeed * Time.deltaTime
                );
            }

            if (direction.magnitude <= waypointThreshold)
            {
                waypointIndex += 1;
            }
        }
    }
}
