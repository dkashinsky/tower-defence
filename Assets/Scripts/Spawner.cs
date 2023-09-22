using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] units;

    [SerializeField]
    private Transform[] pathOptions;

    [SerializeField]
    private float spawnCountdown = 5f;

    private int waveIndex = 0;

    void Start()
    {
        spawnCountdown = WaveConfig.WavesConfig[waveIndex].delay;
    }

    void Update()
    {
        spawnCountdown -= Time.deltaTime;

        if (spawnCountdown <= 0f && waveIndex < WaveConfig.WavesConfig.Count)
        {
            StartCoroutine(SpawnWave(WaveConfig.WavesConfig[waveIndex]));
            spawnCountdown = WaveConfig.WavesConfig[waveIndex + 1].delay;
            waveIndex++;
        }
    }

    private IEnumerator SpawnWave(WaveConfig config)
    {
        for (int i = 0; i < config.spawnCount; i++)
        {
            SpawnUnit(config);
            yield return new WaitForSeconds(0.75f);
        }
    }

    private void SpawnUnit(WaveConfig config)
    {
        var unitType = (int)config.unitType;
        var newUnit = Instantiate(units[unitType], transform.position, Quaternion.identity, transform);
        newUnit.GetComponent<PathFollower>().path = pathOptions[Random.Range(0, pathOptions.Length)];
    }
}
