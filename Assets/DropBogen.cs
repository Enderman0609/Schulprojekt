using UnityEngine;

public class DropBogen : MonoBehaviour
{
    public bool alive = true;
    public bool dropped = false;
    public GameObject BogenAufsammeln;
    void Update()
    {
        alive = GetComponent<Skelett_BogenScript>().alive;
        if (alive == false && dropped == false)
        {
            Drop();
        }
    }
    public void Drop()
    {
         Instantiate(BogenAufsammeln, transform.position, Quaternion.identity);
         dropped = true;
    }
}
