using UnityEngine;

// Diese Klasse steuert das Verhalten des Pfeil-Prefabs
public class pfeilPrefab : MonoBehaviour
{
    public int damage;                 // Schaden, den der Pfeil verursacht
    public int knockbackForce;         // Stärke des Rückstoßes bei Treffern

    private float lastHitTime = 0f;    // Zeitpunkt des letzten Treffers
    private float hitCooldown = 0.5f;  // Abklingzeit zwischen Treffern

    // Wird aufgerufen, wenn der Pfeil mit einem anderen Objekt kollidiert
    void OnTriggerEnter2D(Collider2D collider)
    {
        // Überprüft, ob der Spieler getroffen wurde
        if (collider.gameObject.CompareTag("Player"))
        {
            if (Time.time >= lastHitTime + hitCooldown)
            {
                DamageController damageComponent = collider.gameObject.GetComponent<DamageController>();
                if (damageComponent != null)
                {
                    damageComponent.DealDamage(damage);                          // Fügt dem Spieler Schaden zu
                    Debug.Log("Player getroffen");
                    damageComponent.KnockbackPlayer(knockbackForce, transform);  // Wendet Rückstoß auf den Spieler an
                    Debug.Log("knockbackForce: " + knockbackForce);
                    lastHitTime = Time.time;
                }
                Destroy(gameObject);  // Zerstört den Pfeil nach dem Treffer
            }
        }
        // Überprüft, ob ein Gegner getroffen wurde
        if (collider.gameObject.CompareTag("Enemy"))
        {
            if (Time.time >= lastHitTime + hitCooldown)
            {
                DamageController damageComponent = collider.gameObject.GetComponent<DamageController>();
                if (damageComponent != null)
                {
                    damageComponent.DealDamage(damage);                              // Fügt dem Gegner Schaden zu
                    Debug.Log("BothEnemy getroffen");
                    damageComponent.KnockbackBothEnemy(knockbackForce, transform);   // Wendet Rückstoß auf den Gegner an
                    Debug.Log("knockbackForce: " + knockbackForce);
                    lastHitTime = Time.time;
                }
                Destroy(gameObject);  // Zerstört den Pfeil nach dem Treffer
            }
        }
        // Überprüft, ob ein Schwert getroffen wurde
        if (collider.gameObject.CompareTag("Schwert"))
        {
            lastHitTime = Time.time;
            Destroy(gameObject);  // Zerstört den Pfeil bei Kontakt mit einem Schwert
        }
        // Überprüft, ob ein Gegenstand getroffen wurde
        if (collider.gameObject.CompareTag("Gegenstand"))
        {
            lastHitTime = Time.time;
            Destroy(gameObject);  // Zerstört den Pfeil bei Kontakt mit einem Gegenstand
        }
    }
}