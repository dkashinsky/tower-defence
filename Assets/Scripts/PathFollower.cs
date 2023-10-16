using System.Linq;
using UnityEngine;
using ExtensionMethods;

public class PathFollower : MonoBehaviour
{
    public Transform path;
    public float moveSpeed = 20f;
    public float rotationSpeed = 1f;
    public bool isSlowedDown;

    private Transform[] waypoints;
    private int waypointIndex;
    private float waypointThreshold = 1f;
    private UnitController unit = null;

    void Start()
    {
        unit = GetComponent<UnitController>();
        waypoints = path.GetChildrenByTag("Waypoint").ToArray();
        waypointIndex = 0;
    }

    void Update()
    {
        if (unit == null || unit.IsAlive)
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
