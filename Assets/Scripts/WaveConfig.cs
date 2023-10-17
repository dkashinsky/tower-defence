using System.Collections.Generic;

public class WaveConfig
{
    public UnitTypeEnum unitType;
    public int health;
    public int power;
    public float speed;
    public int spawnCount;
    public float delay;

    public static List<WaveConfig> WavesConfig = new List<WaveConfig>
    {
        new WaveConfig { unitType = UnitTypeEnum.Frog, health = 300, power = 1, speed = 6f, spawnCount = 6, delay = 30f },
        new WaveConfig { unitType = UnitTypeEnum.Iguana, health = 300, power = 1, speed = 6f, spawnCount = 6, delay = 30f },
        new WaveConfig { unitType = UnitTypeEnum.Rabbit, health = 300, power = 1, speed = 6f, spawnCount = 6, delay = 30f },
        new WaveConfig { unitType = UnitTypeEnum.Frog, health = 600, power = 1, speed = 4f, spawnCount = 6, delay = 30f },
        new WaveConfig { unitType = UnitTypeEnum.Iguana, health = 600, power = 1, speed = 4f, spawnCount = 6, delay = 30f },
        new WaveConfig { unitType = UnitTypeEnum.Rabbit, health = 600, power = 1, speed = 4f, spawnCount = 6, delay = 30f },
        new WaveConfig { unitType = UnitTypeEnum.Boss, health = 5400, power = 10, speed = 3f, spawnCount = 1, delay = 15f },
        new WaveConfig { unitType = UnitTypeEnum.Frog, health = 900, power = 1, speed = 9f, spawnCount = 6, delay = 20f },
        new WaveConfig { unitType = UnitTypeEnum.Iguana, health = 900, power = 1, speed = 9f, spawnCount = 6, delay = 25f },
        new WaveConfig { unitType = UnitTypeEnum.Rabbit, health = 900, power = 1, speed = 9f, spawnCount = 6, delay = 60f },
    };
}