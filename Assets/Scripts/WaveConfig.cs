using System.Collections.Generic;

public class WaveConfig
{
    public UnitTypeEnum unitType;
    public int health;
    public float speed;
    public int spawnCount;
    public float delay;

    public static List<WaveConfig> WavesConfig = new List<WaveConfig>
    {
        new WaveConfig { unitType = UnitTypeEnum.Frog, health = 300, speed = 20f, spawnCount = 6, delay = 5f },
        new WaveConfig { unitType = UnitTypeEnum.Iguana, health = 300, speed = 20f, spawnCount = 6, delay = 15f },
        new WaveConfig { unitType = UnitTypeEnum.Rabbit, health = 300, speed = 20f, spawnCount = 6, delay = 15f },
        new WaveConfig { unitType = UnitTypeEnum.Frog, health = 600, speed = 14f, spawnCount = 6, delay = 15f },
        new WaveConfig { unitType = UnitTypeEnum.Iguana, health = 600, speed = 14f, spawnCount = 6, delay = 15f },
        new WaveConfig { unitType = UnitTypeEnum.Rabbit, health = 600, speed = 14f, spawnCount = 6, delay = 15f },
        new WaveConfig { unitType = UnitTypeEnum.Boss, health = 5400, speed = 8f, spawnCount = 1, delay = 15f },
        new WaveConfig { unitType = UnitTypeEnum.Frog, health = 900, speed = 16f, spawnCount = 6, delay = 5f },
        new WaveConfig { unitType = UnitTypeEnum.Iguana, health = 900, speed = 16f, spawnCount = 6, delay = 10f },
        new WaveConfig { unitType = UnitTypeEnum.Rabbit, health = 900, speed = 16f, spawnCount = 6, delay = 15f },
    };
}