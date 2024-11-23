using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RealDialogueManager : MonoBehaviour
{
    [SerializeField] 
    private GameObject dialogueObj;
    [SerializeField]
    private TMP_Text dialogueText;
    [SerializeField]
    private TMP_Text npcName; 
    
    private RealDialogueLine[] currentDialogue; // 대화 라인 배열
    private TaskGroup taskGroup; // 퀘스트 그룹
    private Task task; // 현재 할당된 Task
    private int currentDialogueIndex = 0;
    private int currentOptionDialogueIndex = 0; // 일단 이거 쓰는 곳 없음, 어떻게 해야할 지 모르겠음 
    private bool canFinish = false; 
    
    // dialogue 
    //option 의 구조 반복을 어떻게 처리할 지 확인해보기

    [SerializeField] 
    private Button optionDialogue;
    [SerializeField] 
    private GameObject optionParentsObj;
    private Transform optionParentPos;
    private List<Button> options;
    
    //대화 시작할 떄, 
    //대화 끝날 떄 
    // 두가지 상황에 대해서 이벤트??

    private void Start()
    {
        // 클릭 -> 비동기 보고 진행 -> 그 상태를 보고 대화 진행 
        //GameEventsManager.instance.npcdialogEvents.onShow += NPCInteraction;
        // "Body" 이름을 가진 자식 오브젝트에서 TMP_Text 컴포넌트를 찾음
        GameEventsManager.instance.npcdialogEvents.onShow += DisplayDialogue;
        
        //dialogueText = dialogueObj.transform.Find("Body").GetComponentInChildren<TMP_Text>();

        // "Name" 이름을 가진 자식 오브젝트에서 TMP_Text 컴포넌트를 찾음
        //npcName = dialogueObj.transform.Find("Name").GetComponentInChildren<TMP_Text>();

        optionParentPos = optionParentsObj.transform; 
        
        options = new List<Button>(); 
    }

    //Np
    /*private void NPCInteraction()
    {
        DisplayDialogue(0);
    }*/

    // Task와 상태를 받아서 해당 상태에 맞는 대화 라인 설정
    public RealDialogueLine[] GetDialogue(Task task, TaskState state)
    {
        switch (state)
        {
            case TaskState.Inactive:
                // Task가 Inactive 상태일 때는 시작 대화 라인
                currentDialogue = task.StartDialogueLines;
                if (currentDialogue == null)
                {
                    state = TaskState.Running;
                    GetDialogue(task, state);
                }
                break;

            case TaskState.Running:
                // Task가 Running 상태일 때는 진행 중 대화 라인
                currentDialogue = task.ProgressDialogueLines;
                if (currentDialogue == null)
                {
                    state = TaskState.WaitingForCompletion;
                    GetDialogue(task, state);
                }
                break;

            case TaskState.WaitingForCompletion:
                // Task가 Complete 상태일 때는 완료 대화 라인
                currentDialogue = task.CompleteDialogueLines;
                break;
        }

        if (currentDialogue == null)
        {
            //return
            Debug.Log("선택된 대화 없어요");
        }

        return currentDialogue;
    }

    // 대화 내용 출력 (예시로 콘솔에 출력) // 버튼 눌리면 이거 한다.
    public void DisplayDialogue() //, string npcName)
    {
        if (optionParentsObj.activeSelf)
        {
            return;
        }
        
        Debug.Log($"대화 인덱스 : {currentDialogueIndex}");

        // 대화창 뜨는 이벤트는 여기서 진행
        //GameEventsManager.instance.npcdialogEvents.ShowRealDialogue(); 
        OnDialogue();

        // 다이알로그 키는 이벤트
        
        string dialogue = currentDialogue[0].dialogue[currentDialogueIndex];
        
        dialogueText.text = dialogue;
        npcName.text = currentDialogue[0].speaker; 

        Debug.Log($"{currentDialogue[0].speaker}: {dialogue}");

        CheckDialogueIndex();
        //CheckDialogueIndex();
    }

    private void CheckDialogueIndex()
    {
        /*if (currentDialogueIndex == 0)
        {
            Debug.Log($"지금 무슨 대화? {task.CodeName}, {task.State}");

            GetDialogue(task, task.State);
        }*/
   
        if (currentDialogueIndex + 1 >= currentDialogue[0].dialogue.Length) // 끝일 떄,
        {
            if (currentDialogue[0].options.Length > 0)
            {
                DisplayOptionDialogue();
                return;
            }
            else
            {
                Debug.Log("함 끝내보까?");
                if (canFinish)
                {
                    Debug.Log("안 끝내??");
                    canFinish = false; 
                    
                    EndDialogue();
                 
                    return;
                    
                }
                canFinish = true; 
            }
        }
        else
        {
            currentDialogueIndex++;
        }
        
    }

    private void OnDialogue()
    {
        dialogueObj.SetActive(true);
    }

    private void OffDialogue()
    {
        dialogueObj.SetActive(false);
    }

    private void OnOptionDialogue()
    {
        optionParentsObj.SetActive(true);
    }

    private void OffOptionDialogue()
    {
        optionParentsObj.SetActive(false);
    }

    public void DisplayOptionDialogue() 
    {
        OnOptionDialogue();
        
        for (int i = 0; i < currentDialogue[0].options.Length; i++)
        {
            string option = currentDialogue[0].options[i].optionText; 
            
            Debug.Log($"{option}"); // 선택지에 따라 만들어 내는 거 성공 
            // currentDialogue = currentDialogue[0].options[0].nextDialogue;
            
            Button optionObj = Instantiate(optionDialogue); // 버튼 생성
            TMP_Text optionText = optionObj.GetComponentInChildren<TMP_Text>();
            
            optionObj.transform.SetParent(optionParentPos, false);
            optionText.text = option; 
            Debug.Log($"버튼 : {i}");
            int optionNum = i; 
            
            optionObj.onClick.AddListener(() => DisplayNextDialogue(optionNum));
            options.Add(optionObj);
        }

        //currentDialogueIndex = 0; 
    }

    public void DisplayNextDialogue(int currentOptionDialogueIndex)
    {
        // 예외처리 할 것
        Debug.Log($"몇 번 버튼 ?: {currentOptionDialogueIndex}");
        currentDialogue = currentDialogue[0].options[currentOptionDialogueIndex].nextDialogue; // 매개변수로 바꿔주기
        // 데화 다시 시작할 수 있게 해줌
        
        foreach (Button option in options)
        {
            Destroy(option);
        }
        
        options.Clear();
        
        OffOptionDialogue();
        currentDialogueIndex = 0; 
        currentDialogueIndex = 0; 
        DisplayDialogue();
    }
    
    private void EndDialogue()
    {
        // 창 끄고
        OffDialogue();
        OffOptionDialogue();
        
        currentDialogueIndex = 0;
        currentOptionDialogueIndex = 0;
        
        //바뀐 상태를 보고
        TaskState prevState = currentDialogue[0].currentState;  // 현재 상태를 저장
        TaskState currentState = currentDialogue[0].changedState; // 대화 끝나고 바뀔 상태
        
        Debug.Log($"대화로 인한 상태 변경 -> {task}: {currentState}");
        // State가 변경되었으므로 onStateChanged 이벤트가 호출되도록 해야 합니다.
        task.OnStateChanged(task, prevState, currentState);  // 이벤트 호출을 대신해 상태 변경 알림
    }
    
    public void SetTask(Task newTask) // Task 가 등록된 순간 이거 할당하고 // 바꾸니 상태를 업데이트
    {
        Debug.Log($"대화주제 : {newTask.CodeName}");
        task = newTask; 
        GetDialogue(task, task.State); // 해당 상태에 맞는 대화 라인 가져오기
    }
    
}

// 대화 상태에 따라 상태륿 변경하는 코드 작성