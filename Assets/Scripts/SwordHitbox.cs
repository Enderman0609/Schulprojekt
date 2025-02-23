using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SwordHitbox : MonoBehaviour
{
    public int damage;
    public int knockbackForce = 3;
    public Vector3 faceBot = new Vector3(0, 0, 0);
    public Vector3 faceTop = new Vector3(0, 1, 0);
    public Vector3 faceLeft = new Vector3(0, 0, 0);
    public Vector3 faceRight = new Vector3(0, 0, 0);
    public Collider2D swordCollider;
    void Start()
    {
        if (swordCollider == null)
        {
            Debug.LogWarning("Sword Collider not set");
        }
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Enemy"))
        {
            Damage damageComponent = collider.gameObject.GetComponent<Damage>();
            if (damageComponent != null)
            {
                damageComponent.DealDamage(damage);
                Debug.Log("Schlag");
                damageComponent.KnockbackEnemy(knockbackForce);
            }
        }
    }
    void FacingTop(bool FacingTop)
    {
        if (FacingTop == true)
        {
            gameObject.transform.localPosition = faceTop;
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
    void FacingBot(bool FacingBot)
    {
        if (FacingBot == true)
        {
            gameObject.transform.localPosition = faceBot;
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
    void FacingLeft(bool FacingLeft)
    {
        if (FacingLeft == true)
        {
            gameObject.transform.localPosition = faceLeft;
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 270
        );
        }
    }
    void FacingRight(bool FacingRight)
    {
        if (FacingRight == true)
        {
            gameObject.transform.localPosition = faceRight;
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 90);
        }
    }
}
