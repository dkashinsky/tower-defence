using System.Linq;
using UnityEngine;
using ExtensionMethods;

public class PathFollower : MonoBehaviour
{
    public Transform path;

    [SerializeField]
    private float moveSpeed = 20f;

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

            if (transform.position == waypoints[waypointIndex].position)
            {
                waypointIndex += 1;
            }
        }
        else 
        {
            Destroy(gameObject);
        }
    }
}
