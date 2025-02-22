using UnityEngine;

public class pfeilPrefab : MonoBehaviour
{
    public int damage;
    public int knockbackForce = 3;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            Damage damageComponent = collider.gameObject.GetComponent<Damage>();
            if (damageComponent != null)
            {
                damageComponent.DealDamage(damage);
                Debug.Log("Enemy hit");
                damageComponent.Knockback(knockbackForce);
                Debug.Log("knockbackForce: " + knockbackForce);
            }
            Destroy(gameObject);
        }
    }
}