using UnityEngine;

// Diese Klasse steuert das Verhalten der Schockwelle
public class shockwave : MonoBehaviour
{
    public int damage = 0;             // Schaden, den die Schockwelle verursacht
    public int knockbackForce = 10;    // Stärke des Rückstoßes bei Treffern
    private float lastHitTime = 0f;    // Zeitpunkt des letzten Treffers
    private float hitCooldown = 1f;    // Abklingzeit zwischen Treffern

    // Wird aufgerufen, wenn die Schockwelle mit einem anderen Objekt kollidiert
    void OnTriggerEnter2D(Collider2D collider)
    {
        // Überprüft, ob der Spieler getroffen wurde
        if (collider.gameObject.CompareTag("Player"))
        {
                
                // Prüft, ob die Abklingzeit abgelaufen ist
                if (Time.time >= lastHitTime + hitCooldown)
                {
                    DamageController damageComponent = collider.gameObject.GetComponent<DamageController>();
                    Debug.Log("BossAttack getroffen");
                    if (damageComponent != null)
                    {
                        damageComponent.DealDamage(damage);                          // Fügt dem Spieler Schaden zu
                        Debug.Log("Player getroffen");
                        damageComponent.KnockbackPlayer(knockbackForce, transform);  // Wendet Rückstoß auf den Spieler an
                        Debug.Log("knockbackForce: " + knockbackForce);
                        lastHitTime = Time.time;                                     // Aktualisiert den Zeitpunkt des letzten Treffers
                    }
                }
        }
    }
}
