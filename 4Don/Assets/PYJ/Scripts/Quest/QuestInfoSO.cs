using System;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestInfoSO", menuName = "ScriptableObjects/QuestInfoSO", order = 1)]
public class QuestInfoSO : ScriptableObject
{
    [field: SerializeField] 
    public string id { get; private set; }

    [Header("퀘스트 제목")] 
    public string diaplayName;

    [Header("요구사항")] 
    public int levelRequirement;
    public QuestInfoSO[] questPrerequisites;

    [Header("단계")] 
    public GameObject[] questStepPrefabs;

    [Header("반복")] 
    public int times; // 얼마 간의 시간 간격으로 반복할 것 인지, 혹은 반복을 몇 번 할 것 인지 ?

    [Header("보상")] 
    public int goldReward;
    public int experienceReward;

    private void OnValidate()
    {
#if UNITY_EDITOR
        id = this.name;
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }
}
