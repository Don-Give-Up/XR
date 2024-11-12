using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPCInfoSO", menuName = "ScriptableObjects/NPCInfoSO", order = 2)]
public class NPCInfoSO : ScriptableObject
{
    [field: SerializeField] 
    [Header("NPC 이름")]
    public string name;
}
