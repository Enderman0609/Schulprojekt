using UnityEngine;

public class pfeilPrefab : MonoBehaviour
{
    public int damage;
    public int knockbackForce;

    private float lastHitTime = 0f;
    private float hitCooldown = 0.5f;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            if (Time.time >= lastHitTime + hitCooldown)
            {
                DamageController damageComponent = collider.gameObject.GetComponent<DamageController>();
                if (damageComponent != null)
                {
                    damageComponent.DealDamage(damage);
                    Debug.Log("Player getroffen");
                    damageComponent.KnockbackPlayer(knockbackForce, transform);
                    Debug.Log("knockbackForce: " + knockbackForce);
                    lastHitTime = Time.time;
                }
                Destroy(gameObject);
            }
        }
        if (collider.gameObject.CompareTag("Enemy"))
        {
            if (Time.time >= lastHitTime + hitCooldown)
            {
                DamageController damageComponent = collider.gameObject.GetComponent<DamageController>();
                if (damageComponent != null)
                {
                    damageComponent.DealDamage(damage);
                    Debug.Log("BothEnemy getroffen");
                    damageComponent.KnockbackBothEnemy(knockbackForce, transform);
                    Debug.Log("knockbackForce: " + knockbackForce);
                    lastHitTime = Time.time;
                }
                Destroy(gameObject);
            }
        }
        if (collider.gameObject.CompareTag("Schwert"))
        {
            lastHitTime = Time.time;
            Destroy(gameObject);
        }
        if (collider.gameObject.CompareTag("Gegenstand"))
        {
            lastHitTime = Time.time;
            Destroy(gameObject);
        }
    }
}