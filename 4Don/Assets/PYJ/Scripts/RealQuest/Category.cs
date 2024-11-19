using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Category", fileName = "Category_")]
public class Category : ScriptableObject, IEquatable<Category>
{
    [SerializeField]
    private string codeName; // 보통 이걸로 비교
    [SerializeField]
    private string displayName; // 게임상 보여짐

    public string CodeName => codeName;
    public string DisplayName => displayName;

    #region Operator
    public bool Equals(Category other)
    {
        if (other is null)
            return false;
        if (ReferenceEquals(other, this))
            return true;
        if (GetType() != other.GetType())
            return false;

        return codeName == other.CodeName;
    }

    public override int GetHashCode() => (CodeName, DisplayName).GetHashCode();

    public override bool Equals(object other) => base.Equals(other);

    public static bool operator ==(Category lhs, string rhs)
    {
        if (lhs is null)
            return ReferenceEquals(rhs, null);
        return lhs.CodeName == rhs || lhs.DisplayName == rhs;
    }

    public static bool operator !=(Category lhs, string rhs) => !(lhs == rhs);
    #endregion
}


// 뭘해야 하는 지에 대한 정보를 담고 분류 목적으로 사용