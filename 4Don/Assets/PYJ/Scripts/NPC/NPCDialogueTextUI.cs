using System;
using TMPro;
using UnityEngine;

public class NPCDialogueTextUI : MonoBehaviour
{
  public GameObject npcName;
  public GameObject npcContent;
  public GameObject selectDialogue;

  public GameObject selectDialoguePrefabs;
  
  private TMP_Text npcNameText;
  private TMP_Text npcContentText;
  private TMP_Text selectDialogueText;

  private void Start()
  {
    npcNameText = npcName.GetComponentInChildren<TMP_Text>();
    npcContentText = npcContentText.GetComponentInChildren<TMP_Text>();
  }
  
  public void NPCDialogueText(string npcName, string npcContent)
  {
    npcNameText.text = npcName;
    npcContentText.text = npcContent;
  }

  public void SelectDialogueText(string selectDialogue)
  {
    // DataBase 에 있는 부분 참고 
  }
}
