using System;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public NPCInfoSO npcinfo;

    private void Start()
    {
        SetNPCInfo();
    }

    private void SetNPCInfo()
    {
        if (npcinfo != null)
        {
            gameObject.name = npcinfo.name; 
        }
    }
}
