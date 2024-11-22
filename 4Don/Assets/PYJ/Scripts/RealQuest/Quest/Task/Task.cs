using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum TaskState
{
    Inactive,
    Running,
    Complete
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
    
    public event StateChangedHandler onStateChanged;
    public event SuccessChangedHandler onSuccessChanged;

    public int CurrentSuccess // 성공 횟수에 대한 부분
    {
        get => currentSuccess;
        set
        {
            int prevSuccess = currentSuccess;
            currentSuccess = Mathf.Clamp(value, 0, needSuccessToComplete);
            if (currentSuccess != prevSuccess) // 성공횟수 상태가 변했을 때, 
            {
                Debug.Log($" 완료codeName: {codeName}, Category: {category}, Target: {targets}");
                State = currentSuccess == needSuccessToComplete ? TaskState.Complete : TaskState.Running; 
                //realDialogueManager.SetTask(this); // 상태 바뀔 떄도 시작
                onSuccessChanged?.Invoke(this, currentSuccess, prevSuccess); // 성공 횟수가 변할 떄 마다 호출
            }
        }
    }
    public Category Category => category;
    public string CodeName => codeName;
    public string DisplayName => displayName;
    public string Description => description;
    public int NeedSuccessToComplete => needSuccessToComplete;
    public TaskState State // 상태가 변경될 때
    {
        get => state;
        set
        {
            var prevState = state;
            state = value;
            onStateChanged?.Invoke(this, state, prevState);
            
            // 상태가 변경될 때마다 RealDialogueManager에 반영
            if (realDialogueManager != null)
            {
                realDialogueManager.SetTask(this);
            }
        }
    }
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
        State = TaskState.Inactive; // 일단 등록되면 Inactive 상태가 맞는 거 같음
        //State = TaskState.Running; // 이 상태로라면 Inactive 한 상태가 없음! + 다이알로그 시스템이랑 결합하여 수정할 것 , 
        Debug.Log($"starttaskName: {codeName}"); // 이때이미 시작되어 있네
        
        /*
        QuestTaskTracker questTaskTracker = new GameObject("QuestTaskTracker").AddComponent<QuestTaskTracker>(); // 각 업무마다 작업이 등록
        questTaskTracker.OnDisplay(this,displayName, description);
        */
        
        if (initialSuccessValue)
            CurrentSuccess = initialSuccessValue.GetValue(this);
        
        questTaskTracker = FindObjectOfType<QuestTaskTracker>();  
        if (questTaskTracker != null)
        {
            questTaskTracker.OnDisplay(this);  // task를 RealDialogueManager에 설정
        }
        else
        {
            Debug.LogError("RealDialogueManager not found in the scene!");
        }
        
        realDialogueManager = FindObjectOfType<RealDialogueManager>();  // RealDialogueManager 인스턴스를 찾아서
        if (realDialogueManager != null)
        {
            realDialogueManager.SetTask(this);  // task를 RealDialogueManager에 설정
        }
        else
        {
            Debug.LogError("RealDialogueManager not found in the scene!");
        }
    }

    public void End() // 자연스럽게 마무리됨 
    {
        questTaskTracker.OffDisplay();
        onStateChanged = null;
        onSuccessChanged = null;
    }

    public void ReceiveReport(int successCount) //보고를 받는 부분
    {
        Debug.Log("지금 보고 잘 이루어지고 있나?");
        CurrentSuccess = action.Run(this, CurrentSuccess, successCount);
    }

    public void Complete() // task 즉시 완료
    {
        CurrentSuccess = needSuccessToComplete;
    }

    public bool IsTarget(string category, object target)
        => Category == category &&
        targets.Any(x => x.IsEqual(target)) &&
        (!IsComplete || (IsComplete && canReceiveReportsDuringCompletion));

    public bool ContainsTarget(object target) => targets.Any(x => x.IsEqual(target));
}

