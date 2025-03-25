using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Diese Klasse steuert das Verhalten des Slime-Gegners
public class Slimescript : MonoBehaviour
{
    public int knockbackForce;         // Stärke des Rückstoßes bei Kollisionen
    public int damage;                 // Schaden, den der Slime verursacht
    private bool canMove = true;       // Kontrolliert, ob sich der Slime bewegen kann
    public bool isMoving;              // Zeigt an, ob der Slime sich gerade bewegt
    private Animator animator;         // Referenz zum Animator-Komponenten
    public float speed = 1;            // Bewegungsgeschwindigkeit des Slimes
    public Collider2D SlimeCollider;   // Collider des Slimes für Kollisionserkennung
    private Transform target;          // Ziel des Slimes (normalerweise der Spieler)
    private Rigidbody2D rb;            // Rigidbody für physikbasierte Bewegungen
    public bool alive = true;          // Status, ob der Slime noch lebt

    // Wird beim Start aufgerufen
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;  // Findet den Spieler als Ziel
        animator = GetComponent<Animator>();                            // Holt die Animator-Komponente
        rb = GetComponent<Rigidbody2D>();                               // Holt die Rigidbody-Komponente
    }

    // Update wird einmal pro Frame aufgerufen
    void Update()
    {
        // Bewegt den Slime zum Spieler, wenn dieser in Reichweite ist und der Slime sich bewegen kann
        if (Vector2.Distance(transform.position, target.position) < 8 && canMove && alive)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            animator.SetBool("isMoving", true);  // Aktiviert die Bewegungsanimation
        }
        else
        {
            animator.SetBool("isMoving", false); // Deaktiviert die Bewegungsanimation
        }
    }

    // Sperrt die Bewegung des Slimes
    void LockMovement()
    {
        canMove = false;
    }

    // Wird aufgerufen, wenn der Slime mit einem anderen Objekt kollidiert
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Wenn der Slime mit dem Spieler kollidiert
        if (collision.gameObject.CompareTag("Player") && alive == true)
        {
            DamageController damageComponent = collision.gameObject.GetComponent<DamageController>();
            if (damageComponent != null)
            {
                damageComponent.DealDamage(damage);                         // Fügt dem Spieler Schaden zu
                Debug.Log("Enemy hit");
                damageComponent.KnockbackPlayer(knockbackForce, transform); // Wendet Rückstoß auf den Spieler an
            }
        }
        // Wenn der Slime mit einem anderen Gegner kollidiert
        if (collision.gameObject.CompareTag("Enemy"))
        {
            DamageController damageComponent = collision.gameObject.GetComponent<DamageController>();
            if (damageComponent != null)
            {
                Debug.Log("Enemy hit");
                damageComponent.KnockbackBothEnemy(knockbackForce, transform); // Wendet Rückstoß auf beide Gegner an
            }
        }
    }
}
