using System;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogueManager : MonoBehaviour
{
    // 받아온 정보를 
    public int dialogueNum = 0; 
    private AllDialogueEvent usedDialogue;

    [SerializeField] private AllDialogueEvent usedAllDialogue; 

    private void OnEnable()
    {
        GameEventsManager.instance.npcdialogEvents.onDialogueNumCheck += OnNPCDialogueCheck; 
        //GameEventsManager.instance.npcdialogEvents.onStartDialoge += OnNPCDialogueAdvance; 
        //GameEventsManager.instance.npcdialogEvents.onStartDialoge += OnNPCDialogueFinish; 
    }

    private void OnDisable()
    {
        GameEventsManager.instance.npcdialogEvents.onDialogueNumCheck -= OnNPCDialogueCheck; 
        //GameEventsManager.instance.npcdialogEvents.onStartDialoge -= OnNPCDialogueAdvance; 
        //GameEventsManager.instance.npcdialogEvents.onStartDialoge -= OnNPCDialogueFinish; 
    }

    public void OnNPCDialogueCheck(string npcName) // 어떤 대화를 해야 할 지 선택하는 과정, npc 이름을 가지고 들어옴
    {
       // can_Start 인 친구 중 

    }

    public void OnNPCDialogueAdvance()
    {
        
    }

    public void OnNPCDialogueFinish()
    {
        
    }
    
    

}
