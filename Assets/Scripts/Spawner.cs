using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject unit;

    [SerializeField]
    private Transform[] pathOptions;

    [SerializeField]
    private float spawnInterval = 5f;

    private float timer;

    void Start()
    {
        timer = spawnInterval;
    }

    void Update()
    {
        SpawnUnit();
    }

    private void SpawnUnit()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            var newUnit = Instantiate(unit, transform.position, Quaternion.identity, transform);
            newUnit.GetComponent<PathFollower>().path = pathOptions[Random.Range(0, pathOptions.Length)];

            timer = 0;
        }
    }
}
