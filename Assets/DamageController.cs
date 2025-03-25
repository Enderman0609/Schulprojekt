using UnityEngine;
public class DamageController : MonoBehaviour
{
    // Schadensparameter
    private int damage;
    private int knockbackForce;
    private Animator animator;
    public bool alive = true;
    public int Health; // Gesundheit des Objekts
    public int PlayerHealth; // Gesundheit des Spielers
    public GameObject HealthPotion; // Referenz auf das Heiltrank-Prefab
    public Rigidbody2D PlayerRigidbody; // Referenz auf die Physikkomponente des Spielers

    // Initialisierung der Komponenten
    private void Awake()
    {
        animator = GetComponent<Animator>();
        PlayerRigidbody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    // Lädt die Spielergesundheit aus den gespeicherten Daten
    void Start()
    {
        PlayerHealth = PlayerPrefs.GetInt("PlayerHealth", 120);
        Debug.Log("PlayerHealth: " + PlayerHealth);
        UnityEngine.Object.FindFirstObjectByType<Healthbar>().UpdateHealthbar(PlayerHealth);
    }

    // Verarbeitet Schadenszufügung für verschiedene Entitäten (Feind, Spieler, Boss)
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
                // 10% Chance einen Heiltrank fallen zu lassen
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

    // Wendet Rückstoßkraft auf Feinde an, wenn sie getroffen werden
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

    // Wendet Rückstoßkraft auf den Spieler an, wenn er getroffen wird
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

    // Wendet Rückstoßkraft auf Feinde an, wenn sie mit anderen Objekten kollidieren
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

    // Entfernt das Objekt aus der Szene
    private void DestroyEntity()
    {
        Destroy(gameObject);
    }
}
