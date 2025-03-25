using UnityEngine;

public class HealPotion : MonoBehaviour
{
    // Heilungswert (negativ für Heilung im DamageController)
    public int damage;
    // Speichert den aktuellen Gesundheitswert des Spielers
    public int PlayerHealth;
    
    // Wird ausgelöst, wenn ein Objekt mit dem Trank kollidiert
    void OnTriggerEnter2D(Collider2D collider)
    {
        // Prüft, ob der Spieler den Trank berührt hat
        if (collider.gameObject.CompareTag("Player"))
        {
            // Holt die DamageController-Komponente des Spielers
            DamageController damageComponent = collider.gameObject.GetComponent<DamageController>();
            // Speichert die aktuelle Gesundheit des Spielers
            PlayerHealth = damageComponent.PlayerHealth;
            Debug.Log("Health: " + PlayerHealth);
                // Prüft, ob die DamageController-Komponente existiert
                if (damageComponent != null)
                {
                    // Wenn Spieler weniger als 60 Gesundheit hat, volle Heilung
                    if (PlayerHealth <= 60)
                    {
                        damageComponent.DealDamage(damage);
                    }
                    // Wenn Spieler mehr als 60 Gesundheit hat, heilt nur bis zum Maximum (120)
                    else 
                    {
                        damage = -120 + PlayerHealth;
                        damageComponent.DealDamage(damage);
                    }
                    // Zerstört den Heiltrank nach Benutzung
                    Destroy(gameObject);
                }
        }
    }
}
