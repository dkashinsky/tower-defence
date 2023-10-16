using System;
using UnityEngine;

public class TowerBase : MonoBehaviour
{
    public TowerModifierEnum modifier;
    public float modifierPercentage;

    public GameObject defaultAuraRingPrefab;
    public GameObject rangeAuraRingPrefab;
    public GameObject powerAuraRingPrefab;
    public GameObject speedAuraRingPrefab;

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

        auraRing = GetAuraRing(
            transform.Find("AuraPlacement"));

        level = transform.Find("Level");
    }

    public void OnMouseDown()
    {
        if (IsInUse)
        {
            // when base in use, select current tower
            gameManager.SetPrefabToBuild(null);
            gameManager.SetSelectedTower(tower.gameObject);
            return;
        }

        var towerPrefab = gameManager.GetPrefabToBuild();
        if (!IsInUse && towerPrefab != null)
        {
            // when base is not in use and prefab selected, build tower and set it selected
            tower = BuildTower(towerPrefab);
            ApplyModifiers(tower);

            gameManager.UpdateMoney(-tower.price);
            gameManager.SetPrefabToBuild(null);
            gameManager.SetSelectedTower(tower.gameObject);
        }
        else
        {
            gameManager.SetSelectedTower(null);
        }
    }

    private void OnPefabToBuildChange(GameObject prefab)
    {
        if (!IsInUse && prefab != null)
        {
            var towerRange = prefab
                .GetComponent<TowerController>()
                .range;

            towerRange = modifier == TowerModifierEnum.Range
                ? GetModified(towerRange)
                : towerRange;

            auraRing.SetActive(true);
            auraRing.transform.localScale = GetAuraScale(towerRange);
        }
        else
        {
            auraRing.SetActive(false);
        }
    }

    private TowerController BuildTower(GameObject prefab)
    {
        var towerGO = Instantiate(prefab, level.position, Quaternion.identity, transform);
        var newTower = towerGO.GetComponent<TowerController>();
        newTower.OnTowerSell += OnTowerSellHandler;
        newTower.OnTowerUpgrade += OnTowerUpgradeHandler;

        return newTower;
    }

    private void OnTowerSellHandler()
    {
        // temporarilty store price variable to not deal with object reference as it will be destroyed
        var sellPrice = tower.SellPrice;

        // cleanup tower base
        tower.OnTowerSell -= OnTowerSellHandler;
        tower.OnTowerUpgrade -= OnTowerUpgradeHandler;
        Destroy(tower.gameObject);
        tower = null;

        // management update
        gameManager.UpdateMoney(sellPrice);
        gameManager.SetSelectedTower(null);
    }

    private void OnTowerUpgradeHandler(int price)
    {
        gameManager.UpdateMoney(-price);

        // update aura ring to reflect upgrade in UI
        auraRing.transform.localScale = GetAuraScale(tower.range);
    }

    private void OnSelectedTowerChangeHandler(GameObject towerGO)
    {
        auraRing.SetActive(towerGO != null && tower != null && tower.gameObject == towerGO);
        if (auraRing.activeSelf)
            auraRing.transform.localScale = GetAuraScale(tower.range);
    }

    private Vector3 GetAuraScale(float range)
    {
        float auraScaleFactor = 0.16428f;
        var scale = range * auraScaleFactor;

        return new Vector3(scale, scale, scale);
    }

    private void ApplyModifiers(TowerController tower)
    {
        var shootingTower = tower as ShootingTowerController;

        if (shootingTower != null && modifier == TowerModifierEnum.Power)
            shootingTower.firePower = Mathf.CeilToInt(GetModified(shootingTower.firePower));

        if (shootingTower != null && modifier == TowerModifierEnum.Speed)
            shootingTower.fireRate = GetModified(shootingTower.fireRate);

        if (modifier == TowerModifierEnum.Range)
            tower.range = GetModified(tower.range);
    }

    private float GetModified(float value)
    {
        return value + value * modifierPercentage / 100;
    }

    private GameObject GetAuraRing(Transform placement)
    {
        var auraRingPrefab = defaultAuraRingPrefab;

        switch (modifier)
        {
            case TowerModifierEnum.Power:
                auraRingPrefab = powerAuraRingPrefab;
                break;

            case TowerModifierEnum.Range:
                auraRingPrefab = rangeAuraRingPrefab;
                break;

            case TowerModifierEnum.Speed:
                auraRingPrefab = speedAuraRingPrefab;
                break;
        }

        return Instantiate(auraRingPrefab, placement.position, placement.rotation, transform);
    }

    private void OnEnable()
    {
        gameManager.OnPrefabToBuildChange += OnPefabToBuildChange;
        gameManager.OnSelectedTowerChange += OnSelectedTowerChangeHandler;
    }

    private void OnDisable()
    {
        gameManager.OnPrefabToBuildChange -= OnPefabToBuildChange;
        gameManager.OnSelectedTowerChange -= OnSelectedTowerChangeHandler;
    }
}
