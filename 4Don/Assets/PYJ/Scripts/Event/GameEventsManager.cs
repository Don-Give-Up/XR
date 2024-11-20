using System;
using UnityEngine;

public class GameEventsManager : MonoBehaviour
{
    public static GameEventsManager instance { get; private set; }

    public NPCDialogueEvents npcdialogEvents;
    public NPCEvents npcEvents; 

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Game Events Manager in the scene.");
        }
        instance = this;

        // initialize all events
        npcEvents = new NPCEvents();
        npcdialogEvents = new NPCDialogueEvents(); 

    }
}