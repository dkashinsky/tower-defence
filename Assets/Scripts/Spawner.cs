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
    private float spawnCountdown = 3f;

    private int waveIndex = 0;
    private float waveInterval = 5f;

    void Update()
    {
        spawnCountdown -= Time.deltaTime;

        if (spawnCountdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            spawnCountdown = waveInterval;
        }
    }

    private IEnumerator SpawnWave()
    {
        waveIndex++;

        for (int i = 0; i < waveIndex; i++)
        {
            SpawnUnit();
            yield return new WaitForSeconds(0.75f);
        }
    }

    private void SpawnUnit()
    {
        var newUnit = Instantiate(unit, transform.position, Quaternion.identity, transform);
        newUnit.GetComponent<PathFollower>().path = pathOptions[Random.Range(0, pathOptions.Length)];
    }
}
