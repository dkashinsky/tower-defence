using System;
using System.Linq;
using ExtensionMethods;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    public int price;
    public float range;

    public int SellPrice { get => price / 2; }
    public int UpgradePrice { get => price + 20; }
    public bool IsUpgradable { get => towerLevel < maxTowerLevel; }

    public event Action OnTowerSell;
    public event Action<int> OnTowerUpgrade;

    protected int towerLevel;
    private int maxTowerLevel;
    protected GameObject[] towerLevelPrefabs;

    public virtual void Awake()
    {
        towerLevelPrefabs = transform
            .GetChildrenByTag(ObjectTags.Tower)
            .Select(t => t.gameObject)
            .ToArray();
        maxTowerLevel = towerLevelPrefabs.Length - 1;
        towerLevelPrefabs[towerLevel].SetActive(true);
    }

    public void Sell()
    {
        OnTowerSell?.Invoke();
    }

    public virtual void Upgrade()
    {
        if (IsUpgradable)
        {
            // store price
            var upgradePrice = UpgradePrice;

            // visually upgrade the tower
            towerLevelPrefabs[towerLevel].SetActive(false);
            towerLevel += 1;
            towerLevelPrefabs[towerLevel].SetActive(true);

            // increase specs
            price += 20;
            range += 10;

            // notify subscribers
            OnTowerUpgrade?.Invoke(upgradePrice);
        }
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
