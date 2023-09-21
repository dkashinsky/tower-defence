using UnityEngine;

public class TowerManager : MonoBehaviour
{
    private GameManager gameManager;

    public void Awake()
    {
        gameManager = GameObject
            .Find("GameManager")
            .GetComponent<GameManager>();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            var enemy = collider
                .gameObject
                .GetComponent<UnitController>();

            gameManager.DeductLives(enemy.power);
            gameManager.UpdateMoney(enemy.power * 50);

            Destroy(collider.gameObject);
        }
    }
}
