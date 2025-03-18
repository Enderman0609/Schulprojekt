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
            //Health = GetComponent<Collider>().gameObject.GetComponent<Damage>().Health;
                if (damageComponent != null)
                {
                    if (Health < 80)
                    {
                        Debug.Log("Health: " + Health);
                        damageComponent.DealDamage(damage);
                    }
                    else 
                    {
                        damage = 120 - Health;
                        damageComponent.DealDamage(damage);
                    }
                    Destroy(gameObject);
                }
        }
    }
}
