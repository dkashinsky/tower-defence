using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] units;

    [SerializeField]
    private Transform[] pathOptions;

    [SerializeField]
    private float spawnCountdown = 3f;

    private int waveIndex = 0;
    private float waveInterval = 5f;

    void Update()
    {
        spawnCountdown -= Time.deltaTime;

        if (spawnCountdown <= 0f && waveIndex < WaveConfig.Level1WaveConfig.Count)
        {
            StartCoroutine(SpawnWave());
            spawnCountdown = waveInterval;
        }
    }

    private IEnumerator SpawnWave()
    {
        for (int i = 0; i < WaveConfig.Level1WaveConfig[waveIndex].spawnCount; i++)
        {
            SpawnUnit(waveIndex);
            yield return new WaitForSeconds(0.75f);
        }

        waveIndex++;
    }

    private void SpawnUnit(int waveIndex)
    {
        var unitType = (int)WaveConfig.Level1WaveConfig[waveIndex].unitType;
        var newUnit = Instantiate(units[unitType], transform.position, Quaternion.identity, transform);
        newUnit.GetComponent<PathFollower>().path = pathOptions[Random.Range(0, pathOptions.Length)];
    }
}
