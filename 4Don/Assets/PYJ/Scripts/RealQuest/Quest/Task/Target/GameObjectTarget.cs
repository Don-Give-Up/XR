using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Task/Target/GameObject", fileName = "Target_")]
public class GameObjectTarget : TaskTarget
{
    [SerializeField]
    private GameObject value; // 이건 프리팹

    public override object Value => value;

    public override bool IsEqual(object target) // 프리팹 혹은 게임 오브젝트 두 개가 가능
    {
        var targetAsGameObject = target as GameObject;
        if (targetAsGameObject == null)
            return false;
        return targetAsGameObject.name.Contains(value.name); // 이름을 통해 비교하는 방법
    }
}