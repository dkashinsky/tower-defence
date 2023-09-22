using System;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    public int price;
    public float range;

    public int SellPrice { get => price / 2; }
    public int UpgradePrice { get => price + 20; }

    public event Action OnTowerSell;

    public void Sell()
    {
        OnTowerSell?.Invoke();
    }

    public void Upgrade()
    {
        Debug.Log("Should upgrade if possible");
    }
}
