using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    public GameObject prefabToBuild;

    private GameManager gameManager;
    private Button button;
    private TMP_Text priceText;
    private int price;

    public void Awake()
    {
        // initialize prefab components
        gameManager = GameObject
            .Find("GameManager")
            .GetComponent<GameManager>();

        button = transform
            .Find("InventoryIconButton")
            .GetComponent<Button>();

        priceText = transform
            .Find("PriceTag/Icon/PriceText")
            .GetComponent<TMP_Text>();

        price = prefabToBuild
            .GetComponent<TowerController>()
            .price;
    }

    private void OnMoneyChangeHandler(int money)
    {
        // validate user player can build tower
        button.interactable = money >= price;
    }

    private void OnButtonClick()
    {
        // set tower to build
        gameManager.SetSelectedTower(null);
        gameManager.SetPrefabToBuild(prefabToBuild);
    }

    private void OnEnable()
    {
        gameManager.OnMoneyChange += OnMoneyChangeHandler;
        button.onClick.AddListener(OnButtonClick);

        // setup price tag according to price variable
        priceText.text = price.ToString();

        // syncronize initial button availability
        OnMoneyChangeHandler(gameManager.GetMoney());
    }

    private void OnDisable()
    {
        gameManager.OnMoneyChange -= OnMoneyChangeHandler;
        button.onClick.RemoveListener(OnButtonClick);
    }
}
