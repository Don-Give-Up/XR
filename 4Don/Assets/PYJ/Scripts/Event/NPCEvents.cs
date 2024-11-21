using System;
using UnityEngine;

public class NPCEvents
{
    public event Action<string, string, string> onDetectored;
    
    public void Detector(string npcName, string quest, string state)
    { 
        onDetectored?.Invoke(npcName, quest, state);
    }
    
    //state는 바뀐다.
    //발견하면 달려온다
    //클릭하면 말을 시작한다. 
    
}
