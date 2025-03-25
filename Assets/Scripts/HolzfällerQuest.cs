using UnityEngine;

public class HolzfällerQuest : MonoBehaviour
{
    // Speichert den Status der Axt-Quest
    public bool AxtQuestErledigt = false;
    
    // Markiert die Axt-Quest als erledigt
    public void HolzfällerQuestErledigt()
    {
        AxtQuestErledigt = true;
    }

}
