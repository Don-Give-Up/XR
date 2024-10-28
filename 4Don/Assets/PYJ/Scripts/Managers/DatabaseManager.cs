using System;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
   public static DatabaseManager instance;

   [SerializeField] private string csv_FileName;

   private Dictionary<int, Dialgoue> dialogueDic = new Dictionary<int, Dialgoue>(); // 대화 내용을 저장하는 데 사용 
   private Dictionary<int, SelectDialogue> selectdialogueDic = new Dictionary<int, SelectDialogue>();

   public static bool isFinish = false;

   private void Awake()
   {
      if (instance == null)
      {
         instance = this;
         DialgoueParser theParser = GetComponent<DialgoueParser>(); // 현재 게임 오브젝트에서 변환 컴포넌트를 가져와서 변수에 저장, 데이터 파일을 변환하는 데 사용 
         Dialgoue[] dialgoues = theParser.Parse(csv_FileName); // 매소드를 호출하여 파싱하고 결과를 반환받음 

         for (int i = 0; i < dialgoues.Length; i++)
         {
            dialogueDic.Add(i+1, dialgoues[i]);// 사전에 순서대로 추가
         }

         isFinish = true; // 초기화 끝났음을 알림
      }
   }

   public Dialgoue[] GetDialgoues(int _StartNum, int _EndNum) // 특정 범위의 대화를 가져오는 매소드
   {
      List<Dialgoue> dialogueList = new List<Dialgoue>();

      for (int i = 0; i <= _EndNum - _StartNum; i++)
      {
         dialogueList.Add(dialogueDic[_StartNum + i]);
      }

      return dialogueList.ToArray(); // 그떄의 대화를 리스트로 반환 
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
