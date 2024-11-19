using UnityEngine;
using System.Linq;

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
    private string description;
    
    [Header("Action")]
    [SerializeField]
    private TaskAction action;
    
    [Header("Target")]
    [SerializeField]
    private TaskTarget[] targets;
    
    [Header("Setting")]
    [SerializeField]
    private InitialSuccessValue initialSuccessValue; // 나중에 테스크의 start 함수에서 사용
    private int needSuccessToComplete;
    private TaskState state;
    private int currentSuccess;
    [SerializeField]
    private bool canReceiveReportsDuringCompletion; // 완료해도 계속 보고를 받을 것이냐
    
    public event StateChangedHandler onStateChanged;
    public event SuccessChangedHandler onSuccessChanged;
    
    public int CurrentSuccess
    {
        get => currentSuccess;
        set
        {
            int prevSuccess = currentSuccess;
            currentSuccess = Mathf.Clamp(value, 0, needSuccessToComplete);
            if (currentSuccess != prevSuccess)
            {
                State = currentSuccess == needSuccessToComplete ? TaskState.Complete : TaskState.Running;
                onSuccessChanged?.Invoke(this, currentSuccess, prevSuccess);
            }
        }
    }
    public Category Category => category;

    public string CodeName => codeName;
    
    public string Description => description;
    
    public int NeedSuccessToComplete => needSuccessToComplete;
    
    public TaskState State
    {
        get => state;
        set
        {
            var prevState = state;
            state = value;
            onStateChanged?.Invoke(this, state, prevState);
        }
    }
    
    public bool IsComplete => State == TaskState.Complete;
    
    public Quest Owner { get; private set; }
    
    public void Setup(Quest owner) // Awake 역할
    {
        Owner = owner;
    }
    
    public void Start()
    {
        State = TaskState.Running;
        if (initialSuccessValue)
            CurrentSuccess = initialSuccessValue.GetValue(this);
    }

    public void End()
    {
        onStateChanged = null;
        onSuccessChanged = null;
    }
    
    // 외부에서 currentSuccess 값을 변경할 수 있는 매소드 
    public void ReceiveReport(int successCount) // swith 문으로 작동 로직을 구분하지 않고 모듈화 한 것 = TaskAction
    {
        CurrentSuccess = action.Run(this, CurrentSuccess, successCount); // Run 은 로직을 실행한 결과값을 반환 // 누가 했는지 task 를 알려주는 게 편하다. 
        //CurrentSuccess = successCount;
    }

    public void Complete()
    {
        CurrentSuccess = needSuccessToComplete;
    }
    
    //Task가 성공 횟수를 보고 받을 대상인지 확인하는 함수
    public bool IsTarget(string category, object target)
        => Category == category &&
           targets.Any(x => x.IsEqual(target)) &&
           (!IsComplete || (IsComplete && canReceiveReportsDuringCompletion));
}
