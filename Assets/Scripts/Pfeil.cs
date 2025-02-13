using UnityEngine;

public class pfeilPrefab : MonoBehaviour
{
    public float damage = 1f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Schaden am Spieler verursachen
            other.SendMessage("OnHit", damage, SendMessageOptions.DontRequireReceiver);
            Destroy(gameObject);
        }
    }
}