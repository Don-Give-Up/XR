using System;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogueManager : MonoBehaviour
{
    // 받아온 정보를 
    public int dialogueNum = 0; 
    private AllDialogueEvent usedDialogue;

    private void OnEnable()
    {
        GameEventsManager.instance.npcdialogEvents.onStartDialoge += OnNPCDialogueStart; 
        //GameEventsManager.instance.npcdialogEvents.onStartDialoge += OnNPCDialogueAdvance; 
        //GameEventsManager.instance.npcdialogEvents.onStartDialoge += OnNPCDialogueFinish; 
    }

    private void OnDisable()
    {
        GameEventsManager.instance.npcdialogEvents.onStartDialoge -= OnNPCDialogueStart; 
        //GameEventsManager.instance.npcdialogEvents.onStartDialoge -= OnNPCDialogueAdvance; 
        //GameEventsManager.instance.npcdialogEvents.onStartDialoge -= OnNPCDialogueFinish; 
    }

    public void OnNPCDialogueStart(string npcName)
    {
        // NPC 이름을 기준으로 can_start 인 퀘스트의 
    }

    public void OnNPCDialogueAdvance()
    {
        
    }

    public void OnNPCDialogueFinish()
    {
        
    }

}
