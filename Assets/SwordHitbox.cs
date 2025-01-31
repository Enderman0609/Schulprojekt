using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SwordHitbox : MonoBehaviour
{
    public int swordDamage = 1;
    public Vector3 faceBot = new Vector3(0.545f, -0.2f, 0);
    public Vector3 faceTop = new Vector3(0.545f, 0.8f, 0);
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
      collider.SendMessage("OnHit", swordDamage);
    }
    void IsFacingRight(bool facingRight)
    {
        if (facingRight)
        {
            gameObject.transform.localPosition = faceTop;
        }
        else
        {
            gameObject.transform.localPosition = faceBot;
        }
    }
}
