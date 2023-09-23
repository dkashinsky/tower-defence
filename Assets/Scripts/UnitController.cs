using UnityEngine;

public class UnitController : MonoBehaviour
{
    public int health;
    public int power;

    private GameManager gameManager;

    public void Awake()
    {
        gameManager = GameObject
            .Find("GameManager")
            .GetComponent<GameManager>();
    }

    public void ApplyDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            gameManager.UpdateMoney(power * 50);

            Destroy(gameObject);
        }
    }
}
