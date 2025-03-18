using UnityEngine;

public class Bogenfreischalten : MonoBehaviour
{
    public bool BogenQuestErledigt = false;
    void Start()
    {
       GetComponent<Collider2D>().enabled = false;
       Invoke("EnableCollider", 3f);
    }
    void EnableCollider()
    {
        GetComponent<Collider2D>().enabled = true;  
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
        
        Destroy(gameObject);
        collider.gameObject.GetComponent<PlayerControls>().BogenQuest();
        Debug.Log("Bogenfreischalten");
        }
    }
}
