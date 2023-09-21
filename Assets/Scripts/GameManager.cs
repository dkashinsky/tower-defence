using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TMP_Text healthText;
    public TMP_Text moneyText;

    public event Action<int> OnMoneyChange;
    public event Action<GameObject> OnPrefabToBuildChange;

    private int health = 20;
    private int money = 150;
    private GameObject prefabToBuild;

    public void DeductLives(int lives)
    {
        health -= lives;
        healthText.text = health.ToString();

        if (health <= 0)
        {
            SceneManager.LoadScene(Scenes.GameOver);
        }
    }

    public int GetMoney()
    {
        return money;
    }

    public void UpdateMoney(int moneyDelta)
    {
        money += moneyDelta;
        moneyText.text = money.ToString();

        OnMoneyChange?.Invoke(money);
    }

    public void SetPrefabToBuild(GameObject prefab)
    {
        prefabToBuild = prefab;
        OnPrefabToBuildChange?.Invoke(prefab);
    }
}
