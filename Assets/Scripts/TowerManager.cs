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
        if (collider.gameObject.CompareTag(ObjectTags.Enemy))
        {
            var enemy = collider
                .gameObject
                .GetComponent<UnitController>();

            gameManager.DeductLives(enemy.power);
            enemy.Kill();
        }
    }
}
