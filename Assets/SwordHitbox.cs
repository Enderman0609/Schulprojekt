using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Steuert das Verhalten der Schwert-Hitbox für Kampfaktionen
public class SwordHitbox : MonoBehaviour
{
    // Schadenswert, den das Schwert verursacht
    public int damage;
    // Stärke des Rückstoßes bei Treffern
    public int knockbackForce = 3;
    // Positionsvektoren für verschiedene Schwertausrichtungen
    public Vector3 faceBot = new Vector3(0, 0, 0);
    public Vector3 faceTop = new Vector3(0, 1, 0);
    public Vector3 faceLeft = new Vector3(0, 0, 0);
    public Vector3 faceRight = new Vector3(0, 0, 0);
    // Referenz auf den Collider des Schwerts
    public Collider2D swordCollider;

    // Wird beim Start aufgerufen
    void Start()
    {
        // Überprüft, ob der Schwert-Collider gesetzt wurde
        if (swordCollider == null)
        {
            Debug.LogWarning("Sword Collider not set");
        }
    }

    // Wird ausgelöst, wenn ein Objekt mit diesem Collider in Berührung kommt
    void OnTriggerEnter2D(Collider2D collider)
    {
        // Überprüft, ob ein Gegner getroffen wurde
        if (collider.gameObject.CompareTag("Enemy"))
        {
            DamageController damageComponent = collider.gameObject.GetComponent<DamageController>();
            if (damageComponent != null)
            {
                // Fügt dem Gegner Schaden zu
                damageComponent.DealDamage(damage);
                Debug.Log("Schlag");
                // Wendet Rückstoß auf den Gegner an
                damageComponent.KnockbackEnemy(knockbackForce);
            }
        }
        // Überprüft, ob ein Boss getroffen wurde
        if (collider.gameObject.CompareTag("Boss"))
        {
            DamageController damageComponent = collider.gameObject.GetComponent<DamageController>();
            if (damageComponent != null)
            {
                // Fügt dem Boss Schaden zu
                damageComponent.DealDamage(damage);
                Debug.Log("Schlag");
            }
            else
            {
                Debug.LogWarning("DamageController not found on Boss");
            }
        }
    }

    // Positioniert das Schwert nach oben
    void FacingTop(bool FacingTop)
    {
        if (FacingTop == true)
        {
            gameObject.transform.localPosition = faceTop;
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    // Positioniert das Schwert nach unten
    void FacingBot(bool FacingBot)
    {
        if (FacingBot == true)
        {
            gameObject.transform.localPosition = faceBot;
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    // Positioniert das Schwert nach links
    void FacingLeft(bool FacingLeft)
    {
        if (FacingLeft == true)
        {
            gameObject.transform.localPosition = faceLeft;
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 270
        );
        }
    }

    // Positioniert das Schwert nach rechts
    void FacingRight(bool FacingRight)
    {
        if (FacingRight == true)
        {
            gameObject.transform.localPosition = faceRight;
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 90);
        }
    }
}
