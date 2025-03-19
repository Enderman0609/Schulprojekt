using UnityEngine;

public class shockwave : MonoBehaviour
{
    public int damage = 0;
    public int knockbackForce = 10;
    private float lastHitTime = 0f;
    private float hitCooldown = 1f;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
                

                if (Time.time >= lastHitTime + hitCooldown)
                {
                    Damage damageComponent = collider.gameObject.GetComponent<Damage>();
                    Debug.Log("BossAttack getroffen");
                    if (damageComponent != null)
                    {
                        damageComponent.DealDamage(damage);
                        Debug.Log("Player getroffen");
                        damageComponent.KnockbackPlayer(knockbackForce, transform);
                        Debug.Log("knockbackForce: " + knockbackForce);
                        lastHitTime = Time.time;
                    }
                }
        }
    }
}
