using UnityEngine;
public class DamageController : MonoBehaviour
{
    private int damage;
    private int knockbackForce;
    private Animator animator;
    public bool alive = true;
    public int Health;
    public int PlayerHealth;
    public GameObject HealthPotion;
    public Rigidbody2D PlayerRigidbody;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        PlayerRigidbody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        PlayerHealth = PlayerPrefs.GetInt("PlayerHealth", 120);
        Debug.Log("PlayerHealth: " + PlayerHealth);
        UnityEngine.Object.FindFirstObjectByType<Healthbar>().UpdateHealthbar(PlayerHealth);
    }

    public void DealDamage(int damage)
    {
        
        if (gameObject.CompareTag("Enemy"))
        {
            Health -= damage;
            if (Health <= 0)
            {
                GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                animator.SetTrigger("death");
                alive = false;
                float randomChance = Random.Range(0f, 1f);
                if (randomChance <= 0.1f)
                {
                    Instantiate(HealthPotion, transform.position, Quaternion.identity);
                }
            }
        }
        if (gameObject.CompareTag("Player"))
        {
            PlayerHealth -= damage;
            UnityEngine.Object.FindFirstObjectByType<Healthbar>().UpdateHealthbar(PlayerHealth);
            if (PlayerHealth <= 0)
            {
                alive = false;
                animator.SetTrigger("death");
                Debug.Log("Spieler ist tot");
                GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            }
        }
        if (gameObject.CompareTag("Boss"))
        {
            Health -= damage;
            UnityEngine.Object.FindFirstObjectByType<Bossbar>().UpdateBossbar(Health);
            Debug.Log("Boss Health: " + Health);
            if (Health <= 0)
            {
                GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                animator.SetTrigger("death");

            }
        }
    }
    public void KnockbackEnemy(int knockbackForce)
    {
        Rigidbody2D enemyRb = GetComponent<Rigidbody2D>();
        if ((gameObject.CompareTag("Enemy") || gameObject.CompareTag("BogenQuest")) && enemyRb != null)
        {
            Vector2 knockbackDirection = (transform.position - PlayerRigidbody.transform.position).normalized;
            Debug.Log("knockbackDirection: " + knockbackDirection);
            float appliedForce = knockbackForce * 75f;
            Debug.Log("appliedForce: " + appliedForce);
            GetComponent<Rigidbody2D>().AddForce(knockbackDirection * appliedForce, ForceMode2D.Impulse);
            Debug.Log("KnockbackEnemy");
        }
    }
    public void KnockbackPlayer(int knockbackForce, Transform enemy)
    {
        if (gameObject.CompareTag("Player"))
        {
            Vector2 knockbackDirection = (transform.position - enemy.position).normalized;
            Debug.Log("knockbackForce: " + knockbackForce);
            float appliedForce = knockbackForce * 75f;
            Debug.Log("knockbackDirection: " + knockbackDirection);
            GetComponent<Rigidbody2D>().AddForce(knockbackDirection * appliedForce, ForceMode2D.Impulse);
            Debug.Log("KnockbackPlayer");
            Debug.Log("appliedForce: " + appliedForce);
        }
    }
    public void KnockbackBothEnemy(int knockbackForce, Transform enemy)
    {
        if (gameObject.CompareTag("Enemy") && (enemy != null))
        {
            Vector2 knockbackDirection = (transform.position - enemy.position).normalized;
            Debug.Log("transform.position: " + transform.position);
            Debug.Log("enemy.position: " + enemy.position);
            Debug.Log("knockbackDirection: " + knockbackDirection);
            float appliedForce = knockbackForce * 75f;
            GetComponent<Rigidbody2D>().AddForce(knockbackDirection * appliedForce, ForceMode2D.Impulse);
            Debug.Log("KnockbackBothEnemy");
            Debug.Log("appliedForce: " + appliedForce);
        }
    }
    private void DestroyEntity()
    {
        Destroy(gameObject);
    }
}
