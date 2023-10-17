using System.Collections;
using System.Linq;
using ExtensionMethods;
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

    void Update()
    {
        spawnCountdown -= Time.deltaTime;

        if (spawnCountdown <= 0f && waveIndex < WaveConfig.WavesConfig.Count)
        {
            StartCoroutine(SpawnWave(WaveConfig.WavesConfig[waveIndex]));
            spawnCountdown = WaveConfig.WavesConfig[waveIndex].delay;
            waveIndex++;
        }
    }

    private IEnumerator SpawnWave(WaveConfig config)
    {
        for (int i = 0; i < config.spawnCount; i++)
        {
            SpawnUnit(config);
            yield return new WaitForSeconds(Random.Range(1.5f, 2.5f));
        }
    }

    private void SpawnUnit(WaveConfig config)
    {
        var unitType = (int)config.unitType;
        var path = pathOptions[Random.Range(0, pathOptions.Length)];
        var waypoints = path.GetChildrenByTag("Waypoint").ToArray();
        var unit = Instantiate(units[unitType], waypoints[0].position, Quaternion.identity, transform);
        unit.transform.localScale = new Vector3(config.scale, config.scale, config.scale);

        if (unit.TryGetComponent<PathFollower>(out var pathFollower))
        {
            pathFollower.waypoints = waypoints;
            pathFollower.moveSpeed = config.speed;
        }

        if (unit.TryGetComponent<UnitController>(out var unitCtrl))
        {
            unitCtrl.health = config.health;
            unitCtrl.power = config.power;
        }
    }
}
