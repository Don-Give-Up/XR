using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BankClerkDialogManager : MonoBehaviour // 은행 직원 
{
    // 아래에 있는 3개 나중에 클래스나 스크랩터블 오브젝트로 관리하기
    private Vector2 bankClerkDialogNum = new Vector2(1, 14);
    private Vector2 myDialogNum = new Vector2(1, 11);
    public int talkNum = 1; // 시작 대화 라인 = bankClerkDialogNum.x
    
    [SerializeField] DialogueEvent useddialgoue;
    [SerializeField] SeletDialogueEvent usedselect;
    // 일단 데이터 벵스에서 대화 받아와 

    private int eventNum; 
    
    private List<string> selectedChoice = new List<string>();
    private List<int> selectedChoiceNum = new List<int>(); 
    
    public static BankClerkDialogManager instance; 
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        GetDialogues();
        GetSeletDialogues();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Dialogue(talkNum);
        }
    }

    // 필요한 대화 불러오기
    private Dictionary<int, Dialogue> GetDialogues()
    {
        useddialgoue.dialogues = DataBaseManager.instance.GetDialogues((int)bankClerkDialogNum.x, (int)bankClerkDialogNum.y); // (int)dialogue.line.x, (int)dialogue.line.y
        return useddialgoue.dialogues;
    }

    // 필요한 대화 불러오기
    private Dictionary<int, SeletDialogue> GetSeletDialogues()
    {
        // 키값 1개에 다 들어가고 있어서 에러 발생
        usedselect.seletDialogues = DataBaseManager.instance.GetSeletDialogues((int)myDialogNum.x, (int)myDialogNum.y); // (int)dialogue.line.x, (int)dialogue.line.y
        return usedselect.seletDialogues;
    }
    
    // NPC 가 클릭되면 실행될 코드
    public void Dialogue(int talkNum) //번호 바뀌면 해당하는 대사 정해기 
    {
        this.talkNum = talkNum; // 여기서 talkNUm의 숫자 변겅
        string npcName = useddialgoue.dialogues[this.talkNum].npcname;
        string talkContent = useddialgoue.dialogues[talkNum].content;
        Debug.Log($"{talkContent}");
        
        DialogueManager.instance.DialogueText(npcName, talkContent);
    }

    private void Selet(int eventNum)
    {
        int sameEventNum = 0; // 내가 원하는 이벤트 개수 만큼 발생함 

        selectedChoice = new List<string>();
        selectedChoiceNum = new List<int>();
       
        for (int i = 1; i <= usedselect.seletDialogues.Count; i++) // key 값을 1부터 넣음
        {
            
            if (usedselect.seletDialogues[i].eventNum == eventNum)
            {
                sameEventNum++;
                selectedChoice.Add(usedselect.seletDialogues[i].choiceContent); //선택지에 뜨울 배열 추가
                selectedChoiceNum.Add(usedselect.seletDialogues[i].resumeNum);
            }
        }
        DialogueManager.instance.SeletDialogText(sameEventNum, selectedChoice.ToArray(), selectedChoiceNum.ToArray());
    }

    public void TalkNum() // 대화가 시작된 후, 버튼 클릭받으면 대사 뜨고 숫자 한게 증가시켜 놓기
    {
        DialogueManager.instance.EndSelectDialog();//선택지 창 꺼지게하기

        if (useddialgoue.dialogues[talkNum].eventNum > 0) //이벤트가 있으면 이벤트 쪽에 가서 이벤트 발생 
        {
            //Event
            eventNum = useddialgoue.dialogues[talkNum].eventNum;
            Selet(eventNum);
        }
        else if (useddialgoue.dialogues[talkNum].skipNum > 0) // 스킵라인이 있으면 가서 그 번호 실행 
        {
            //Skip
            talkNum = useddialgoue.dialogues[talkNum].skipNum;
        }
        else // 아무것도 해당 안 되면 번호만 뛰어넘어서 실행
        {
            if (talkNum == (int)bankClerkDialogNum.y) // 한개치 이상이면 대화 끝나게, 끝난 후 대화 리셋
            {
                //Reset
                DialogueManager.instance.EndDialog();
                DialogueManager.instance.EndSelectDialog();
                talkNum = 1;
                return;
            }
            else
            {
                talkNum++;
            }
        }
        
        // event -> dialoge = MoveDialoge 로 해도 될 듯
        Dialogue(talkNum); // 변경된 번호로 실행 
    }
    
    // 선택지에서 어떠한 선택지 고르면 그 선택지의 resumNum을 talkNum 으로 받는다. 
 
}
