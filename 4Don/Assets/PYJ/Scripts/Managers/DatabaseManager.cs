using System;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
   public static DatabaseManager instance;

   [SerializeField] private string csv_FileName;

   private Dictionary<int, Dialgoue> dialogueDic = new Dictionary<int, Dialgoue>();
   private Dictionary<int, SelectDialogue> selectdialogueDic = new Dictionary<int, SelectDialogue>();

   public static bool isFinish = false;

   private void Awake()
   {
      if (instance == null)
      {
         instance = this;
         DialgoueParser theParser = GetComponent<DialgoueParser>();
         Dialgoue[] dialgoues = theParser.Parse(csv_FileName);

         for (int i = 0; i < dialgoues.Length; i++)
         {
            dialogueDic.Add(i+1, dialgoues[i]);
         }

         isFinish = true;
      }
   }

   public Dialgoue[] GetDialgoues(int _StartNum, int _EndNum)
   {
      List<Dialgoue> dialogueList = new List<Dialgoue>();

      for (int i = 0; i <= _EndNum - _StartNum; i++)
      {
         dialogueList.Add(dialogueDic[_StartNum + i]);
      }

      return dialogueList.ToArray();
   }
   
   public SelectDialogue[] GetSelects(int _StartNum, int _EndNum)
   {
      List<SelectDialogue> selectDialogues = new List<SelectDialogue>();

      for (int i = 0; i <= _EndNum - _StartNum; i++)
      {
         selectDialogues.Add(selectdialogueDic[_StartNum + i]);
      }

      return selectDialogues.ToArray();
   }
   
}
