using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class QuestManager : MonoBehaviour
{
 private Dictionary<string, Quest> questMap;

 private void Awake()
 {
  questMap = CreateQuestMap();
 }

 private void OnEnable()
 {
  GameEventManager.instance.questEvent.onStartQuest += StartQuest;
  GameEventManager.instance.questEvent.onAdvanceQuest += AdvanceQuest;
  GameEventManager.instance.questEvent.onFinishQuest += FinishQuest;
 }

 private void OnDisable()
 {
  GameEventManager.instance.questEvent.onStartQuest -= StartQuest;
  GameEventManager.instance.questEvent.onAdvanceQuest -= AdvanceQuest;
  GameEventManager.instance.questEvent.onFinishQuest -= FinishQuest;
 }

 private void Start()
 {
  foreach (Quest quest in questMap.Values)
  {
   //GameEventManager.instance.questEvent.QuestStateChange(quest);
  }
 }

 private void StartQuest(string id)
 {
  Debug.Log("Start Quest:" + id);
 }

 private void AdvanceQuest(string id)
 {
  Debug.Log("Advance Quest:" + id);

 }

 private void FinishQuest(string id)
 {
  Debug.Log("Finish Quest:" + id);
 }

 private Dictionary<string, Quest> CreateQuestMap()
 {
  QuestInfoSO[] allQuest = Resources.LoadAll<QuestInfoSO>("Quest");
  Dictionary<string, Quest> idToQuestMap = new SerializedDictionary<string, Quest>();

  foreach (QuestInfoSO questInfoSo in allQuest)
  {
   if (idToQuestMap.ContainsKey(questInfoSo.id))
   {
    Debug.Log("몰랑");
   }

   idToQuestMap.Add(questInfoSo.id, new Quest(questInfoSo));
  }

  return idToQuestMap;
 }

 private Quest GetQuestById(string id)
 {
  Quest quest = questMap[id];
  if (quest == null)
  {
   Debug.Log("id 가 없어요");
  }

  return quest;
 }
}

