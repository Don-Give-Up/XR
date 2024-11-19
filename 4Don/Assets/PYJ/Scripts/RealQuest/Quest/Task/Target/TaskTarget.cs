using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TaskTarget : ScriptableObject
{
    public abstract object Value { get; } // 타겟을 외부로 가지고 올 수 있는 속성

    public abstract bool IsEqual(object target); // QuestSystem 에 보고된 타겟이 Task에 설정한 타겟과 같은지 확인하는 함수 
    
}