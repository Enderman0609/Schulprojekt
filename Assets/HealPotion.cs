using UnityEngine;

public class HealPotion : MonoBehaviour
{
    public int damage;
    public int PlayerHealth;
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            DamageController damageComponent = collider.gameObject.GetComponent<DamageController>();
            // Get the current health from the player's Damage component
            PlayerHealth = damageComponent.PlayerHealth;
            Debug.Log("Health: " + PlayerHealth);
                if (damageComponent != null)
                {
                    if (PlayerHealth <= 60)
                    {
                        damageComponent.DealDamage(damage);
                    }
                    else 
                    {
                        damage = -120 + PlayerHealth;
                        damageComponent.DealDamage(damage);
                    }
                    Destroy(gameObject);
                }
        }
    }
}
