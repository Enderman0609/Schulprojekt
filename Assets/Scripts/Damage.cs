using UnityEngine;

public class Damage : MonoBehaviour
{
    public int damage;
    public int knockbackForce;
    private Animator animator;
    public int Health;
    public Rigidbody2D rbp;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void Start()
    {
        rbp = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }
    public void DealDamage(int damage)
    {
        Debug.Log(damage);
        Health -= damage;
        Debug.Log(Health);
        if (gameObject.CompareTag("Enemy"))
        {
            if (Health <= 0)
            {
                animator.SetTrigger("death");
                GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            }
        }
        if (gameObject.CompareTag("Player"))
        {
            if (Health <= 0)
            {
                animator.SetTrigger("death");
                Debug.Log("Spieler ist tot");
                GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            }
        }
    }

    public void Knockback(int knockbackForce)
    {
        if (gameObject.CompareTag("Enemy"))
        {
            // Hole die Rigidbody2D-Komponente einmal und speichere sie
            Rigidbody2D enemyRb = GetComponent<Rigidbody2D>();

            // Überprüfe ob der Rigidbody2D nicht kinematic ist und die Masse nicht zu hoch
            if (enemyRb != null)
            {
                Vector2 knockbackDirection = (transform.position - rbp.transform.position).normalized;

                // Erhöhe die Kraft durch Multiplikation
                float appliedForce = knockbackForce * 75f;
                enemyRb.AddForce(knockbackDirection * appliedForce, ForceMode2D.Impulse);
            }
        }
        if (gameObject.CompareTag("Player"))
        {
            // Hole die Rigidbody2D-Komponente des Spielers
            Rigidbody2D playerRb = GetComponent<Rigidbody2D>();

            // Überprüfe ob der Rigidbody2D nicht null ist
            if (playerRb != null)
            {
                // Berechne die Richtung weg vom Enemy
                Vector2 knockbackDirection = (playerRb.transform.position - transform.position).normalized;

                // Erhöhe die Kraft durch Multiplikation
                float appliedForce = knockbackForce * 75f;
                playerRb.AddForce(knockbackDirection * appliedForce, ForceMode2D.Impulse);
            }
        }
    }
    private void DestroyEntity()
    {
        Destroy(gameObject);
    }
}
