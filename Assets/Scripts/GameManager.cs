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
    public event Action<GameObject> OnSelectedTowerChange;

    [SerializeField]
    private int health = 20;
    [SerializeField]
    private int money = 150;
    private GameObject prefabToBuild;
    private GameObject selectedTower;

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

    public GameObject GetPrefabToBuild()
    {
        return prefabToBuild;
    }

    public void SetPrefabToBuild(GameObject prefab)
    {
        // deselect if same prefab is already selected
        prefabToBuild = prefabToBuild == prefab 
            ? null 
            : prefab;

        OnPrefabToBuildChange?.Invoke(prefabToBuild);
    }

    public GameObject GetSelectedTower()
    {
        return selectedTower;
    }

    public void SetSelectedTower(GameObject tower)
    {
        // deselect if same tower is already selected
        selectedTower = selectedTower == tower 
            ? null 
            : tower;

        OnSelectedTowerChange?.Invoke(selectedTower);
    }
}
