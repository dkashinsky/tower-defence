using UnityEngine;

public class UnitController : MonoBehaviour
{
    public int health;
    public int power;

    public bool IsAlive { get => health > 0; }
    public bool IsHit { get => isHit; }

    private GameManager gameManager;
    private Animator animator;
    private bool isHit;

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

            // run hit animation
            if (!isHit)
            {
                isHit = true;
                animator.SetInteger("moving", 2);
            }

            if (health <= 0)
            {
                // increase money if unit died
                gameManager.UpdateMoney(power * 50);

                // destroy object in 5 seconds
                Destroy(gameObject, 5f);
            }
        }
    }

    public void OnHitAnimationEnd()
    {
        isHit = false;

        if (IsAlive)
            animator.SetInteger("moving", 1);
        else
            animator.SetInteger("moving", 12);
    }
}
