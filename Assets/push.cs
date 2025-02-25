using UnityEngine;

public class push : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
     void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            push2();
        }
    }
     void push2()
    {
         GetComponent<Rigidbody2D>().AddForce(transform.up * 1000, ForceMode2D.Impulse);
        Debug.Log("Pushed");
    }
}
