using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TMP_Text healthText;
    public TMP_Text moneyText;

    private int health = 20;
    private int money = 150;

    public void DeductLives(int lives)
    {
        health -= lives;
        healthText.text = health.ToString();

        if (health <= 0)
        {
            SceneManager.LoadScene(Scenes.GameOver);
        }
    }

    public void UpdateMoney(int moneyDelta)
    {
        money += moneyDelta;
        moneyText.text = money.ToString();
    }
}
