using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerUpgradeController : MonoBehaviour
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

    private void OnTowerChangeHandler(GameObject tower)
    {
        selectedTower = tower != null
            ? tower.GetComponent<TowerController>()
            : null;

        if (selectedTower != null)
        {
            priceText.text = $"-{selectedTower.UpgradePrice}";
            button.interactable = selectedTower.UpgradePrice <= gameManager.GetMoney();
        }
    }

    private void OnMoneyChangeHandler(int money)
    {
        if (selectedTower != null)
            button.interactable = selectedTower.UpgradePrice <= money;
    }

    private void OnButtonClick()
    {
        if (selectedTower != null)
            selectedTower.Upgrade();
    }

    private void OnEnable()
    {
        gameManager.OnSelectedTowerChange += OnTowerChangeHandler;
        gameManager.OnMoneyChange += OnMoneyChangeHandler;
        button.onClick.AddListener(OnButtonClick);

        // syncronize initial button availability
        OnTowerChangeHandler(gameManager.GetSelectedTower());
        OnMoneyChangeHandler(gameManager.GetMoney());
    }

    private void OnDisable()
    {
        gameManager.OnSelectedTowerChange -= OnTowerChangeHandler;
        gameManager.OnMoneyChange -= OnMoneyChangeHandler;
        button.onClick.RemoveListener(OnButtonClick);
    }
}
