using UnityEngine;

public class HealPotion : MonoBehaviour
{
    public int damage;
    public int Health;
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            Damage damageComponent = collider.gameObject.GetComponent<Damage>();
            // Get the current health from the player's Damage component
            Health = damageComponent.Health;
            Debug.Log("Health: " + Health);
                if (damageComponent != null)
                {
                    if (Health <= 60)
                    {
                        damageComponent.DealDamage(damage);
                    }
                    else 
                    {
                        damage = -120 + Health;
                        damageComponent.DealDamage(damage);
                    }
                    Destroy(gameObject);
                }
        }
    }
}
