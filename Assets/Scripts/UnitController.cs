using UnityEngine;

public class UnitController : MonoBehaviour
{
    public int health;
    public int power;

    public bool IsAlive { get => health > 0; }

    private GameManager gameManager;
    private Animator animator;

    public void Awake()
    {
        gameManager = GameObject
            .Find("GameManager")
            .GetComponent<GameManager>();

        // set walk animation
        animator = GetComponent<Animator>();
        animator.SetInteger("moving", 1);
    }

    public void ApplyDamage(int damage)
    {
        if (IsAlive)
        {
            // Only apply damage if unit is alive
            health -= damage;

            if (health <= 0)
            {
                // increase money if unit died
                gameManager.UpdateMoney(power * 50);

                // run die animation
                animator.SetInteger("moving", 12);

                // destroy object in 3 seconds
                Destroy(gameObject, 3f);
            }
        }
    }
}
