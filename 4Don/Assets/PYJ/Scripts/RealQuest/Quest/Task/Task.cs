using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum TaskState
{
    Inactive,
    Running,
    WaitingForCompletion, // 일반적으로 행동 끝냈을 때 이 상태가 되어 있게 
    Complete, // npc 에게 말 걸어야만 이렇게 되게 하기
}

[CreateAssetMenu(menuName = "Quest/Task/Task", fileName = "Task_")]
public class Task : ScriptableObject
{
    #region Events
    public delegate void StateChangedHandler(Task task, TaskState currentState, TaskState prevState);
    public delegate void SuccessChangedHandler(Task task, int currentSuccess, int prevSuccess);
    #endregion

    [SerializeField]
    private Category category;

    [Header("Text")]
    [SerializeField]
    private string codeName;
    [SerializeField] 
    private string displayName;
    [SerializeField]
    private string description;

    [Header("Detail")]
    [SerializeField] 
    private string[] directions; 

    [Header("Action")]
    [SerializeField]
    private TaskAction action;

    [Header("Target")]
    [SerializeField]
    private TaskTarget[] targets;
    
    [Header("Dialogue")] 
    [SerializeField]
    private RealDialogueLine[] beforDialogueLines; 
    [SerializeField]
    private RealDialogueLine[] IngDialogueLines; 
    [SerializeField]
    private RealDialogueLine[] AfterDialogueLines; 

    [Header("Setting")]
    [SerializeField]
    private InitialSuccessValue initialSuccessValue;
    [SerializeField]
    private int needSuccessToComplete;
    [SerializeField]
    private bool canReceiveReportsDuringCompletion;

    private TaskState state;
    private int currentSuccess;

    private RealDialogueManager realDialogueManager;
    private QuestTaskTracker questTaskTracker;
    private TEST test; 
    public event StateChangedHandler onStateChanged;
    public event SuccessChangedHandler onSuccessChanged;

    private QuestReporter questReporter; 

    public int CurrentSuccess // 성공 횟수에 대한 부분 // Setter는 값을 설정하는 매서드, 속성에 값이 할당될 때 set 매서드가 자동으로 할당됨 
    {
        get => currentSuccess;
        set
        {
            Debug.Log("숫자 변경");
            int prevSuccess = currentSuccess;
            if (IsComplete) //만약에 다이알로그에 갔다와서 성공 상태로 바뀌었다면!
            {
                currentSuccess = Mathf.Clamp(value, 0, needSuccessToComplete);
                Debug.Log($" 완료codeName: {codeName}"); // 지금 이 코드가 안 불림 
                Debug.Log($"성공횟수 확인 : {currentSuccess}");
                State = currentSuccess == needSuccessToComplete ? TaskState.Complete : TaskState.Running;  // 
                onSuccessChanged?.Invoke(this, currentSuccess, prevSuccess); // 성공 횟수가 변할 떄 마다 호출
                QuestSystem.Instance.ReceiveReport(this.Category, this.targets[0], this.CurrentSuccess);

            }
            else
            {
                currentSuccess = Mathf.Clamp(value, 0, needSuccessToComplete);
                Debug.Log($"성공횟수 확인 : {currentSuccess}");
                if (currentSuccess != prevSuccess) // 성공횟수 상태가 변했을 때, 
                {
                    //그냥 이거 하고 나서 해야할 듯?? 
                    State = currentSuccess == needSuccessToComplete
                        ? TaskState.WaitingForCompletion
                        : TaskState.Running; // 
                    //realDialogueManager.SetTask(this); // 상태 바뀔 떄도 시작
                    onSuccessChanged?.Invoke(this, currentSuccess, prevSuccess); // 성공 횟수가 변할 떄 마다 호출
                    
                }
            }
            
            // 여기 setTask 부르면 안 됨
        }
    }
    public Category Category => category;
    public string CodeName => codeName;
    public string DisplayName => displayName;
    public string Description => description;
    public int NeedSuccessToComplete => needSuccessToComplete;

    public string[] Directions => directions; 
    public TaskState State // 상태가 변경될 때, 자동으로 할당
    {
        get => state;
        set
        {
            Debug.Log($"상태변경: {State}");
            var prevState = state; // 이 전 상태 
            state = value; // 새로 할당된 값 반환
            onStateChanged?.Invoke(this, state, prevState);

            /*
            if (State == TaskState.Complete)
            {
                Debug.Log($"완료상태: {this.CodeName}");
                onSuccessChanged?.Invoke(this, this.needSuccessToComplete, this.needSuccessToComplete);
            }
            */
            
            // 상태가 변경될 때마다 RealDialogueManager에 반영
            if (realDialogueManager != null)
            {
                realDialogueManager.SetTask(this);
            }
        }
    }

    public bool IsComplatable => State == TaskState.WaitingForCompletion; 
    public bool IsComplete => State == TaskState.Complete;
    public Quest Owner { get; private set; }
    public RealDialogueLine[] StartDialogueLines => beforDialogueLines; 
    public RealDialogueLine[] ProgressDialogueLines => IngDialogueLines; 
    public RealDialogueLine[] CompleteDialogueLines => AfterDialogueLines; 

    public void Setup(Quest owner)
    {
        Owner = owner;
    }

    public void Start() // Task 가 시작 될 때 // 이 때 표시되게 하면 될 듯 , 
    {
        test = FindObjectOfType<TEST>();
        
        if (test != null)
        {
            test.SetTask(this);
        }

        State = TaskState.Inactive; // 일단 등록되면 Inactive 상태가 맞는 거 같음
        //State = TaskState.Running; // 이 상태로라면 Inactive 한 상태가 없음! + 다이알로그 시스템이랑 결합하여 수정할 것 , 
        Debug.Log($"starttaskName: {codeName}"); // 이때이미 시작되어 있네
        
        Active();

        /*if (this.CodeName == Owner.TaskGroups[0].Tasks[0].codeName)
        {
            //첫 퀘스트라면 
            Debug.Log($"firstQuest : {this.CodeName}");
            State = TaskState.Running;
            Active();
        }*/
        
        if (initialSuccessValue)
            CurrentSuccess = initialSuccessValue.GetValue(this);
        
        realDialogueManager = FindObjectOfType<RealDialogueManager>();  // RealDialogueManager 인스턴스를 찾아서
       
        if (realDialogueManager != null)
        {
            realDialogueManager.SetTask(this);  // task를 RealDialogueManager에 설정
        }
       
    }

    public void Active()
    {
        // 활성화되면 이거 실시. 
        State = TaskState.Running;
        
        questTaskTracker = FindObjectOfType<QuestTaskTracker>();  
        // 두번 이루어지면 안 반복되게 하기
        if (questTaskTracker != null)
        {
            questTaskTracker.OnDisplay(this);  // task를 RealDialogueManager에 설정
        }
        
        if (realDialogueManager != null)
        {
            realDialogueManager.SetTask(this);  // task를 RealDialogueManager에 설정
        }
    }

    public void End() // 자연스럽게 마무리됨 
    {
        questTaskTracker.OffDisplay();
        if (realDialogueManager != null)
        {
            realDialogueManager.SetTask(this);  // task를 RealDialogueManager에 설정
        }
        onStateChanged = null;
        onSuccessChanged = null;
    }
    
    public void OnStateChanged(Task task,TaskState prevState, TaskState newState) // 다이알로그가 부르는 곳
    {
        // 이벤트를 호출
        /*
        Debug.Log("상태 변경 호출 전");
        Debug.Log($"{task}, {newState}, {prevState}");
        Debug.Log("상태 변경 호출 후");
        */

        switch (newState)
        {
            case TaskState.Running:
                Debug.Log("활성화");
                onStateChanged?.Invoke(this, newState, prevState);
                Active();
                break;
            case TaskState.Complete:
                Debug.Log("끝내야함!!");
                this.state = TaskState.Complete; 
                onStateChanged?.Invoke(this, newState, prevState);
                onSuccessChanged?.Invoke(this, CurrentSuccess++, CurrentSuccess);
                //CurrentSuccess++; 
                break;
        }
    }

    public void ReceiveReport(int successCount) //보고를 받는 부분 // Task그룹에서 보고 받음
    {
        //Debug.Log("지금 보고 잘 이루어지고 있나?");
        CurrentSuccess = action.Run(this, CurrentSuccess, successCount);
    }

    public void Complete() // task 즉시 완료
    {
        //CurrentSuccess = needSuccessToComplete
        this.State = TaskState.Complete; 
        //onStateChanged?.Invoke(this, newState, prevState);
    }

    public bool IsTarget(string category, object target)
        => Category == category &&
        targets.Any(x => x.IsEqual(target)) &&
        (!IsComplete || (IsComplete && canReceiveReportsDuringCompletion));

    public bool ContainsTarget(object target) => targets.Any(x => x.IsEqual(target));
}

