using System.Linq;
using UnityEngine;
using ExtensionMethods;

public class PathFollower : MonoBehaviour
{
    public Transform path;

    public float moveSpeed = 20f;

    public float rotationSpeed = 1f;

    private Transform[] waypoints;
    private int waypointIndex;

    void Start()
    {
        waypoints = path.GetChildrenByTag("Waypoint").ToArray();
        waypointIndex = 0;
    }

    void Update()
    {
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

            var direction = (waypoints[waypointIndex].position - transform.position).normalized;
            if (direction != Vector3.zero)
            {
                var lookRotation = Quaternion.LookRotation(direction, Vector3.up);
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    lookRotation,
                    rotationSpeed * Time.deltaTime
                );
            }

            if (transform.position == waypoints[waypointIndex].position)
            {
                waypointIndex += 1;
            }
        }
    }
}
