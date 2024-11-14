using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class NPCDialogueManager : MonoBehaviour
{

    public GameObject dialogueObj;
    public TMP_Text dialogueText;
    public TMP_Text npcName;
    
    public GameObject selectdialogueObj;
    private Transform selectObjectPos;
    
    public Button selectObjectPrefab;
    private List<Button> choiceButtons = new();

    // 이벤트 유아이 저장
    public List<GameObject[]> eventUI = new List<GameObject[]>();
    
    // 받아온 정보를 
    [SerializeField] 
    private AllDialogueEvent usedAllDialogue;

    private QuestInfoSO currentQuestInfo; 

    Vector2 dialogueNum = Vector2.zero;
    
    private bool hasTalked = true; // 대화가 끝나는 순간
    private bool changeQuest = false; // 퀘스트가 바뀌는 순간

    private int beforeQuestState = 0;

    private bool dialogueOn = false;
    private bool selectDialogueOn = false;
    
    private int currentDialogueNum = 0; 
    
    private KingQuest kingQuest;
    //private QuestManager questManager;
    
    // 1 -> 0  // 퀘스트 끝남, 퀘스트 번호를 하나 옮기고 0 -> 1 될 떄 까지 안 보이게 한다.  
    // 0 -> 1  // 퀘스트 시작, 퀘스트를 발행하고 유지한다. 
    
    private void Awake()
    {
        //selectObjectPos = selectdialogueObj.transform;
        kingQuest = KingQuest.instance;
        hasTalked = true; 
        //questManager = QuestManager.instance;
        for (int i = 0; i < eventUI.Count; i++)
        {
            GameObject[] eventUIS = eventUI[i];

            for (int j = 0; j < eventUIS.Length; j++)
            {
                GameObject events = eventUIS[j]; 
                events.SetActive(false);
            }
        }
    }

    private void OnEnable()
    {
        GameEventsManager.instance.npcdialogEvents.onDialogueNumCheck += OnNPCDialogueCheck;
        // 일단 이벤트 바뀌는 순간 감지
        // 이벤트 끝나는 순간 감지
        //GameEventsManager.instance.npcdialogEvents.onShowDialoge += OnShowDialogue; 
        //GameEventsManager.instance.npcdialogEvents.onStartDialoge += OnNPCDialogueFinish; 
    }
    
    private void OnDisable()
    {
        GameEventsManager.instance.npcdialogEvents.onDialogueNumCheck -= OnNPCDialogueCheck; 
        // 끝날 떄, 끝나기
        //GameEventsManager.instance.npcdialogEvents.onShowDialoge -= OnShowDialogue; 
        //GameEventsManager.instance.npcdialogEvents.onStartDialoge -= OnNPCDialogueFinish; 
    }

    // 퀘스트의 상태를 확인하고 해댕 npc 대화 시작 가능 여부를 판단
    public void OnNPCDialogueCheck(string npcName) // 어떤 대화를 해야 할 지 선택하는 과정, npc 이름을 가지고 들어옴
    {
        // 클릭했을 때 NPC의 이름을 반환하고 
        // 현재 퀘스트 정보를 가지고 와서 
        // 이 친구가 말할 수 있는 지? 어떤 내용을 말해야하는 지 대답한다. 

        currentQuestInfo = kingQuest.GetQuestInfo(); // 현재 퀘스트 정보 가저옴 // 현재  퀘스트 정보를 어떻게 가져와야하지? 
        dialogueNum = Vector2.zero;
        
        switch (hasTalked)
        {
            case true:
                // 전 대화를 나누었어! 
                // 후 대화를 나눌차례
                dialogueNum = currentQuestInfo.dialogueLine[1];
                hasTalked = false;
                break;
            case false:
                // 전 대화를 나누지 않았어.
                // 후 대화를 나눌차례 
                dialogueNum = currentQuestInfo.dialogueLine[0];
                hasTalked = true; 
                break;
        }
        
        Debug.Log($"대화라인:{(int)dialogueNum.x}, {(int)dialogueNum.y}");
        currentDialogueNum = (int)dialogueNum.x;
        GetDialogueData(dialogueNum);
        
        if (usedAllDialogue.alldialogues[currentDialogueNum].name != npcName )
        {
            // 일단 상호작용 안 되게만 해놓음
            return;
        }

        OnShowDialogue(currentDialogueNum);
        currentDialogueNum++; 
    }

    private void Update() // bool 타입 값에 따라 어떤 창을 띄울까 표시
    {
        if (dialogueOn)
        {
            dialogueObj.SetActive(true); // 다이알로그 대화가 나오는 상황에 

            if (selectDialogueOn)
            {
                selectdialogueObj.SetActive(true);  // 선택지도 제공되어야 해
            }
            else
            {
                selectdialogueObj.SetActive(false); // 선택지는 제공되지 않아야 해
            }
        }
        else // 아무런 대화가 나오지 않고 있음
        {
            
            dialogueObj.SetActive(false); 
            selectdialogueObj.SetActive(false);
        }
        
    }

    // 필요한 대화 저장하기
    private Dictionary<int, AllDialogue> GetDialogueData(Vector2 dialogueNum)
    {
        usedAllDialogue.alldialogues = DataBaseManager.instance.GetAllDialogues(dialogueNum);
        return usedAllDialogue.alldialogues;
    }

    private void OnShowDialogue(int currentDial)
    {
        dialogueOn = true;
        
        string npcNameText = usedAllDialogue.alldialogues[currentDial].name;
        string npcText = usedAllDialogue.alldialogues[currentDial].content;

        dialogueText.text = npcText;
        npcName.text = npcNameText;
    }

    private void OnSelectDialogue(int selectNum)
    {
        // 어떤 선택인지 가지고 들어옴 
        selectDialogueOn = true;

        for (int i = 0; i < usedAllDialogue.alldialogues.Count; i++)
        {
            if (usedAllDialogue.alldialogues[i].selectLine == selectNum)
            {
                Button selectButton = Instantiate(selectObjectPrefab);
                choiceButtons.Add(selectButton);
                
                selectButton.transform.SetParent(selectdialogueObj.transform, false);
                
                selectButton.onClick.AddListener(() => OnselectClick(usedAllDialogue.alldialogues[i].skipLine) );
                
                TMP_Text selectText = selectButton.GetComponentInChildren<TMP_Text>();
                selectText.text = usedAllDialogue.alldialogues[i].content;
            }
        }
    }

    private void OnselectClick(int skipNum)
    {
        // 이거는 불리는 순간 
        //생성된 버튼 사라지게 하기
        choiceButtons.Clear();
        // 선택창 끄고
        selectDialogueOn = false; 
        //스킵 넘버 할달 후 
        currentDialogueNum = skipNum;
        // 표현하기
        OnShowDialogue(skipNum);
    }

    private void OnEventUI(int eventNum)
    {
        int eventUINum = eventNum - 1;
        dialogueObj.SetActive(false);
        
        GameObject[] eventObjects = eventUI[eventUINum]; // 해당하는 이벤트 게임오브젝트 반환
        
        eventObjects[0].gameObject.SetActive(true);
        
        //EventTrigger
        //OnPointClick
    }
    

    // 버튼 클릭 받으면 인덱스 하나 움직인 다음에 OnShowDialogue 부르기 
    public void MoveNext() // 메인 다이알로그와 연결 함
    {
        if (currentDialogueNum <= (int)dialogueNum.y)
        {
            // 현재 번호를 가지고 옴, 처음 클릭 시에는 처음 인덱스를 가지고 옴
            if (usedAllDialogue.alldialogues[currentDialogueNum].selectLine > 0) // 선택다이알로그이다. 
            {
                // 선택란을 만났다 -> 선택하는 거 몇 개, 어떤 내용으로 뜰 지 결정하는 곳으로 가야함. 여긴 아님
                OnSelectDialogue(usedAllDialogue.alldialogues[currentDialogueNum].selectLine);
            }
            else // 일반 다이알로그입니다.
            {
                // 이 땐 다음 버튼이 눌리면 어떻게 움직일지 하면 되긴함.
                if (usedAllDialogue.alldialogues[currentDialogueNum].choiceEventNum > 0) // 일반 다이알로그에서 발생해야 하는 이벤트가 있다면
                {
                    // 이벤트 발생하게 함
                    OnEventUI(usedAllDialogue.alldialogues[currentDialogueNum].choiceEventNum);
                    
                }
                else if (usedAllDialogue.alldialogues[currentDialogueNum].skipLine > 0) // 일반 다이알로그에서 스킵라인이 있다면 
                {
                    if (usedAllDialogue.alldialogues[currentDialogueNum].skipLine == 10000)
                    {
                        OnNPCDialogueFinish();
                    }
                    else
                    {
                        currentDialogueNum = usedAllDialogue.alldialogues[currentDialogueNum].skipLine;
                        OnShowDialogue(currentDialogueNum);
                    }
                }
                else
                {
                    // 이거면 특별한 이벤트나 스킵넘버 없업
                    OnShowDialogue(currentDialogueNum);
                    currentDialogueNum++; 
                }
            }
        }
        else
        {
            if (usedAllDialogue.alldialogues[currentDialogueNum].questState == 1)
            { 
                // 1 -> 0으로 가는 순간 
                // 안 끝내, 대화 계속해  , 다음 퀘스트로 넘어가 
                KingQuest.instance.onQuestEnd.Invoke();
                // 사실 이 떄 대화 계속 진행하는 코드가 있어야 하는데 아직 구현 안 됨 
            }
            else
            {
                // 0 -> 1으로 가는 순간 
                // 끝내, 퀘스트 시작 
                KingQuest.instance.onQuestStart.Invoke(); 
                //OnNPCDialogueFinish(); // 끝내!
            }
            
            OnNPCDialogueFinish(); // 끝내!

            // 다음 번호가 없다. 
        }
    }
    
    public void OnNPCDialogueFinish()
    {
        dialogueOn = false;
        selectDialogueOn = false;
        currentDialogueNum = 0; // 초기화 
        // 및 등록된 버튼들 삭제
    }
    
}
