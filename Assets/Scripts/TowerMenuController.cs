using UnityEngine;

public class TowerMenuController : MonoBehaviour
{
    public GameObject[] menuItems;
    private GameManager gameManager;

    void Awake()
    {
        // initialize prefab components
        gameManager = GameObject
            .Find("GameManager")
            .GetComponent<GameManager>();
    }

    private void OnTowerChangeHandler(GameObject tower)
    {
        bool isActive = tower != null;

        foreach (var menuItem in menuItems)
            menuItem.SetActive(isActive);
    }

    private void OnEnable()
    {
        gameManager.OnSelectedTowerChange += OnTowerChangeHandler;

        // syncronize initial button availability
        OnTowerChangeHandler(gameManager.GetSelectedTower());
    }

    private void OnDisable()
    {
        gameManager.OnSelectedTowerChange -= OnTowerChangeHandler;
    }
}
