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
        // 이 부분은 다른 메소드로 옮기는 게 나을 듯
        /*if (currentDialogueIndex >= currentDialogue[0].dialogue.Length)
        {
            if (currentDialogue[0].options.Length > 0)
            {
                // 선택지 뜸니당. 
                DisplayOptionDialogue();
                return;
            }
            else
            {
                currentDialogueIndex = 0; // 초기화
                // 끝까지 다 했으면 상태변화나 Quest 부르는 코드가 여기 있어도 될 듯
                // 여기서 보고를 하면 될 것 같음
                EndDialogue();
                Debug.Log("끝까지 했다옹쓰"); // 다이알로그 끄는 이벤트 발생
                return;
            }

        }*/
      
        if (optionParentsObj.activeSelf)
        {
            return;
        }
        
        CheckDialogueIndex();
        

        // 대화창 뜨는 이벤트는 여기서 진행
        //GameEventsManager.instance.npcdialogEvents.ShowRealDialogue(); 
        OnDialogue();

        // 다이알로그 키는 이벤트
        
        string dialogue = currentDialogue[0].dialogue[currentDialogueIndex];
        
        dialogueText.text = dialogue;
        npcName.text = currentDialogue[0].speaker; 

        Debug.Log($"{currentDialogue[0].speaker}: {dialogue}");

        //CheckDialogueIndex();
    }

    private void CheckDialogueIndex()
    {
        if (currentDialogueIndex + 1 == currentDialogue[0].dialogue.Length) // 끝일 떄,
        {
            if (currentDialogue[0].options.Length > 0)
            {
                DisplayOptionDialogue();
                return;
            }
            else
            {
                EndDialogue();
                return;
            }
        }
        else
        {
            DisplayOptionDialogue();
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
        currentDialogueIndex = 0; // 데화 다시 시작할 수 있게 해줌
        
        foreach (Button option in options)
        {
            Destroy(option);
        }
        
        options.Clear();
        
        OffOptionDialogue();
        
        DisplayDialogue();
    }
    
// 0번 키를 누르면 대화 출력
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0)) // 다이알로그 대화와 연결
        {
            Debug.Log("입력들어왕");
            if (task != null)  // task가 null이 아닌 경우에만 실행
            {
                //string npcName = "NPC";  // 예시로 NPC 이름 설정, 실제로는 동적으로 할당할 수 있음
                /*Debug.Log("대화할라고");*/
                DisplayDialogue(); //, npcName);  // 현재 Task의 상태에 맞는 대화 출력
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha1)) //선택지 대화와 연결
        {
            Debug.Log("입력들어왕");
            if (task != null)  // task가 null이 아닌 경우에만 실행
            {
                //string npcName = "NPC";  // 예시로 NPC 이름 설정, 실제로는 동적으로 할당할 수 있음
                /*Debug.Log("대화할라고");*/
                //DisplayNextDialogue(); //, npcName);  // 현재 Task의 상태에 맞는 대화 출력
            }
        }
    }

    private void EndDialogue()
    {
        // 창 끄고
        OffDialogue();
        OffOptionDialogue();
        
        TaskState prevState = currentDialogue[0].currentState;  // 현재 상태를 저장
        TaskState currentState = currentDialogue[0].changedState;
    
        Debug.Log($"대화로 인한 상태 변경 -> {currentState}");
        // State가 변경되었으므로 onStateChanged 이벤트가 호출되도록 해야 합니다.
        if (prevState != currentState)  // 상태가 실제로 변경되었을 경우
        {
            task.OnStateChanged(task, prevState, currentState);  // 이벤트 호출을 대신해 상태 변경 알림
        }
    }
    
    public void SetTask(Task newTask) // Task 가 등록된 순간 이거 할당하고, 상태 바뀔 때마다 부름
    {
        task = newTask;
        TaskState state = task.State;
        Debug.Log($"대화 내용: {task}, {state}");
        currentDialogueIndex = 0;
        currentOptionDialogueIndex = 0;
        GetDialogue(task, state); // 해당 상태에 맞는 대화 라인 가져오기
        OffDialogue();
        OffOptionDialogue();
        Debug.Log($"할당: {task.CodeName}"); // 계속 한 대화만 들림

    }

    /*// 퀘스트 그룹과 Task를 설정 (이 부분은 예시로 보여주기 위한 코드)
    public void SetTask(TaskGroup taskGroup, Task task)
    {
        this.taskGroup = taskGroup;
        this.task = task;
    }*/
}

// 대화 상태에 따라 상태륿 변경하는 코드 작성