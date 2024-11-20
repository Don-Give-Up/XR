using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsUtility : MonoBehaviour
{
    [ContextMenu("DeleteSaveData")] // 데이터 삭제하는데 사용
    private void DeleteSaveData()
    {
        PlayerPrefs.DeleteAll();
    }
}