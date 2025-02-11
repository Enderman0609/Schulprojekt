using UnityEngine;
using System.Collections;

public class Skelett_BogenScript : MonoBehaviour
{
    private Transform player;
    private bool isMoving;              // Referenz zum Spieler
    private Rigidbody2D rb;               // Rigidbody des Skeletts
    private Animator animator;             // Animator des Skeletts
    
    public float detectionRange = 14f;     // Erkennungsreichweite
    public float preferredRange = 8f;      // Bevorzugte Kampfdistanz
    public float moveSpeed = 3f;           // Bewegungsgeschwindigkeit
    
    private bool playerInRange = false;    // Ist der Spieler in Reichweite?
    private Vector2 movement;              // Bewegungsrichtung
    
    public float health = 5f;
    public float knockbackResistance = 1f;  // Wie stark der Knockback reduziert wird

    public float Health
    {
        get { return health; }
        set { health = value; }
    }

    void Start()
    {
        // Komponenten initialisieren
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player == null) return;

        // Distanz zum Spieler berechnen
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        
        // Prüfen ob Spieler in Erkennungsreichweite ist
        if (distanceToPlayer <= detectionRange)
        {
            playerInRange = true;
            
            // Wenn weiter weg als bevorzugte Reichweite, nähere dich an
            if (distanceToPlayer > preferredRange)
            {
                // Bewegungsrichtung zum Spieler berechnen
                Vector2 direction = (player.position - transform.position).normalized;
                movement = direction * moveSpeed;
                
                // Animation Parameter setzen (falls vorhanden)
                if (animator != null)
                {
                    animator.SetFloat("moveX", direction.x);
                    animator.SetFloat("moveY", direction.y);
                    animator.SetBool("isMoving", true);
                }
            }
            else
            {
                // Wenn in bevorzugter Reichweite, stoppe Bewegung
                movement = Vector2.zero;
                if (animator != null)
                {
                    animator.SetBool("isMoving", false);
                }
            }
        }
        else
        {
            // Außerhalb der Erkennungsreichweite
            playerInRange = false;
            movement = Vector2.zero;
            if (animator != null)
            {
                animator.SetBool("isMoving", false);
            }
        }
    }

    void FixedUpdate()
    {
        if (playerInRange)
        {
            // Bewegung ausführen
            rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
        }
    }

    // Hilfsfunktion zum Visualisieren der Reichweiten im Editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, preferredRange);
    }

    void OnHit(float damage)
    {
        Debug.Log("Skelett hit for " + damage);
        Health -= (int)damage;
        if (health <= 0)
        {
            if (animator != null)
            {
                animator.SetTrigger("death");
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    void OnKnockback(float knockbackForce)
    {
        Debug.Log("Skelett knockbacked for " + knockbackForce);
        Vector2 direction = (transform.position - player.position).normalized;
        rb.linearVelocity = Vector2.zero; // Aktuelle Geschwindigkeit zurücksetzen
        rb.AddForce(direction * (knockbackForce / knockbackResistance), ForceMode2D.Impulse);
        StartCoroutine(KnockbackPause());
    }

    private IEnumerator KnockbackPause()
    {
        playerInRange = false;
        yield return new WaitForSeconds(0.2f);
        playerInRange = true;
    }
}
