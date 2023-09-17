using System.Collections;
using System.Collections.Generic;

public class WaveConfig
{
    public UnitTypeEnum unitType;
    public int health;
    public float speed;
    public int spawnCount;

    public static List<WaveConfig> Level1WaveConfig = new List<WaveConfig>
    {
        new WaveConfig { unitType = UnitTypeEnum.Frog, health = 300, speed = 20f, spawnCount = 6 },
        new WaveConfig { unitType = UnitTypeEnum.Iguana, health = 300, speed = 20f, spawnCount = 6 },
        new WaveConfig { unitType = UnitTypeEnum.Rabbit, health = 300, speed = 20f, spawnCount = 6 },
        new WaveConfig { unitType = UnitTypeEnum.Frog, health = 600, speed = 20f, spawnCount = 6 },
        new WaveConfig { unitType = UnitTypeEnum.Iguana, health = 600, speed = 20f, spawnCount = 6 },
        new WaveConfig { unitType = UnitTypeEnum.Rabbit, health = 600, speed = 20f, spawnCount = 6 },
        new WaveConfig { unitType = UnitTypeEnum.Boss, health = 5400, speed = 20f, spawnCount = 1 },
        new WaveConfig { unitType = UnitTypeEnum.Frog, health = 900, speed = 20f, spawnCount = 6 },
        new WaveConfig { unitType = UnitTypeEnum.Iguana, health = 900, speed = 20f, spawnCount = 6 },
        new WaveConfig { unitType = UnitTypeEnum.Rabbit, health = 900, speed = 20f, spawnCount = 6 },
    };
}