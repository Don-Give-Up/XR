using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Task/Target/String", fileName = "Target_")]
public class StringTarget : TaskTarget
{
    [SerializeField]
    private string value; // 값을 받을 변수

    public override object Value => value;

    public override bool IsEqual(object target)
    {
        string targetAsString = target as string; // 타입으로 캐스팅 해주기
        if (targetAsString == null)
            return false;
        return value == targetAsString;
    }
}