using UnityEngine;

public class TowerBase : MonoBehaviour
{
    public bool rangeModifier;
    public bool damageModifier;
    public bool speedModifier;

    private GameManager gameManager;
    private GameObject auraRing;
    private Transform level;
    private GameObject tower;

    public bool IsInUse { get => tower != null; }

    public void Awake()
    {
        gameManager = GameObject
            .Find("GameManager")
            .GetComponent<GameManager>();

        auraRing = transform
            .Find("AuraRingLight")
            .gameObject;

        level = transform.Find("Level");
    }

    public void OnMouseDown()
    {
        var prefab = gameManager.GetPrefabToBuild();
        if (prefab != null && !IsInUse)
        {
            tower = Instantiate(prefab, level.position, Quaternion.identity, transform);
            auraRing.SetActive(false);

            var towerPrice = prefab
                .GetComponent<TowerController>()
                .price;

            gameManager.UpdateMoney(-towerPrice);
            gameManager.SetPrefabToBuild(null);
        }
    }

    private void OnPefabToBuildChange(GameObject prefab)
    {
        if (!IsInUse && prefab != null)
        {
            var particleSystem = auraRing
                .GetComponent<ParticleSystem>()
                .main;
            var towerRange = prefab
                .GetComponent<TowerController>()
                .range;

            particleSystem.startSize = new ParticleSystem.MinMaxCurve(towerRange, towerRange);
            auraRing.SetActive(true);
        }
        else
        {
            auraRing.SetActive(false);
        }
    }

    private void OnEnable()
    {
        gameManager.OnPrefabToBuildChange += OnPefabToBuildChange;
    }

    private void OnDisable()
    {
        gameManager.OnPrefabToBuildChange -= OnPefabToBuildChange;
    }
}
