using UnityEngine;

// Diese Klasse steuert das Fallenlassen des Bogens durch einen Gegner
public class DropBogen : MonoBehaviour
{
    public bool alive = true;          // Status, ob der Gegner noch lebt
    public bool dropped = false;       // Zeigt an, ob der Bogen bereits fallengelassen wurde
    public GameObject BogenAufsammeln; // Prefab des aufsammelbaren Bogens
    
    // Wird jeden Frame aufgerufen
    void Update()
    {
        alive = GetComponent<Skelett_BogenScript>().alive;  // Aktualisiert den Lebensstatus vom Skelett-Skript
        if (alive == false && dropped == false)             // Wenn der Gegner tot ist und noch nichts fallengelassen hat
        {
            Drop();                                         // Lässt den Bogen fallen
        }
    }
    
    // Lässt den Bogen an der Position des Gegners fallen
    public void Drop()
    {
         Instantiate(BogenAufsammeln, transform.position, Quaternion.identity);  // Erzeugt den aufsammelbaren Bogen
         dropped = true;                                                         // Markiert, dass der Bogen fallengelassen wurde
    }
}
