using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SlimeScript : MonoBehaviour
{
    public float Health
    {
        get { return health; }
        set
        {
            health = value;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
    public float speed;
    public float health = 3;
    private Transform target;

    void OnHit(float damage)
    {
        Debug.Log("Slime hit for" + damage);
        Health -= (int)damage;
    }
    void Start()
    {
        target = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
       if (Vector2.Distance(transform.position, target.position) < 8) {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
       }
        
    }
}

