using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerSellController : MonoBehaviour
{
    private GameManager gameManager;
    private Button button;
    private TMP_Text priceText;
    private TowerController selectedTower;

    void Awake()
    {
        // initialize prefab components
        gameManager = GameObject
            .Find("GameManager")
            .GetComponent<GameManager>();

        button = transform
            .Find("Button")
            .GetComponent<Button>();

        priceText = transform
            .Find("PriceTag/Icon/PriceText")
            .GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        gameManager.OnSelectedTowerChange += OnTowerChangeHandler;
        button.onClick.AddListener(OnButtonClick);
        
        // syncronize initial button availability
        OnTowerChangeHandler(gameManager.GetSelectedTower());
    }

    private void OnDisable()
    {
        gameManager.OnSelectedTowerChange -= OnTowerChangeHandler;
        button.onClick.RemoveListener(OnButtonClick);
    }

    private void OnTowerChangeHandler(GameObject tower)
    {
        selectedTower = tower != null
            ? tower.GetComponent<TowerController>()
            : null;

        if (selectedTower != null)
            priceText.text = $"+{selectedTower.SellPrice}";
    }

    private void OnButtonClick()
    {
        if (selectedTower != null)
            selectedTower.Sell();
    }
}
