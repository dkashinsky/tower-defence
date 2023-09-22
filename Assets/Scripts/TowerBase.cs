using UnityEngine;

public class TowerBase : MonoBehaviour
{
    public bool rangeModifier;
    public bool damageModifier;
    public bool speedModifier;

    private GameManager gameManager;
    private GameObject auraRing;
    private Transform level;
    private TowerController tower;

    public bool IsInUse { get => tower != null; }

    public void Awake()
    {
        gameManager = GameObject
            .Find("GameManager")
            .GetComponent<GameManager>();

        auraRing = transform
            .Find("AuraRingLight")
            .gameObject;

        level = transform.Find("Level");
    }

    public void OnMouseDown()
    {
        if (IsInUse)
        {
            gameManager.SetSelectedTower(tower.gameObject);
            return;
        }

        var prefab = gameManager.GetPrefabToBuild();
        if (!IsInUse && prefab != null)
        {
            BuildTower(prefab);
        }
    }

    private void OnPefabToBuildChange(GameObject prefab)
    {
        if (!IsInUse && prefab != null)
        {
            var particleSystem = auraRing
                .GetComponent<ParticleSystem>()
                .main;
            var towerRange = prefab
                .GetComponent<TowerController>()
                .range;

            particleSystem.startSize = new ParticleSystem.MinMaxCurve(towerRange, towerRange);
            auraRing.SetActive(true);
        }
        else
        {
            auraRing.SetActive(false);
        }
    }

    private void BuildTower(GameObject prefab)
    {
        var gameObject = Instantiate(prefab, level.position, Quaternion.identity, transform);
        tower = gameObject.GetComponent<TowerController>();
        tower.OnTowerSell += OnTowerSellHandler;

        auraRing.SetActive(false);
        gameManager.UpdateMoney(-tower.price);
        gameManager.SetPrefabToBuild(null);
    }

    private void OnTowerSellHandler()
    {
        // temporarilty store price variable to not deal with object reference as it will be destroyed
        var sellPrice = tower.SellPrice;
        
        // cleanup tower base
        tower.OnTowerSell -= OnTowerSellHandler;
        Destroy(tower.gameObject);
        tower = null;

        // management update
        gameManager.UpdateMoney(sellPrice);
        gameManager.SetSelectedTower(null);
    }

    private void OnEnable()
    {
        gameManager.OnPrefabToBuildChange += OnPefabToBuildChange;
    }

    private void OnDisable()
    {
        gameManager.OnPrefabToBuildChange -= OnPefabToBuildChange;
    }
}
