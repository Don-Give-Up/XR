using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class RealDialogueManager : MonoBehaviour
{
    [SerializeField] 
    private GameObject dialogueObj;
    [SerializeField]
    private TMP_Text dialogueText;
    [SerializeField]
    private TMP_Text npcName; 
    
    private RealDialogueLine[] currentDialogue; // 대화 라인 배열
    private Task task; // 현재 할당된 Task
    private int currentDialogueIndex = 0;
    private int currentOptionDialogueIndex = 0; // 일단 이거 쓰는 곳 없음, 어떻게 해야할 지 모르겠음 
    private bool canFinish = false;
    private bool canStartEvent = false;
    private int eventNum = 0;
    private string beforeString = "";
    [SerializeField]
    private GameObject[] eventObj;
    
    [SerializeField] 
    private Button optionDialogue;
    [SerializeField] 
    private GameObject optionParentsObj;
    private Transform optionParentPos;
    private List<Button> options;
    
    private CancellationTokenSource cancellationTokenSource;

    public AudioSource audioSource;
    private void Start()
    {
        GameEventsManager.instance.npcdialogEvents.onShow += DisplayDialogue;

        optionParentPos = optionParentsObj.transform; 
        
        options = new List<Button>();
        //eventObj = new GameObject[]{ };
        foreach (var obj in eventObj)
        {
            obj.SetActive(false);
        }
    }
    
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
        // 여기서 OnShow 관련돤 거 한다음 대본 없음 추가 다른 말 하게 하기
     
        if (optionParentsObj.activeSelf) //여기!
        {
            return;
        }
        
        Debug.Log($"대화 인덱스 : {currentDialogueIndex}");
        
        OnDialogue();

        // 다이알로그 키는 이벤트
        
        string dialogue = currentDialogue[0].dialogue[currentDialogueIndex];
        
        
        string formattedDialogue = FormatDialogue(dialogue);

        
        dialogueText.text = formattedDialogue;
        npcName.text = currentDialogue[0].speaker; 

        Debug.Log($"{currentDialogue[0].speaker}: {formattedDialogue}");

        CheckDialogueIndex();
        CheckEvent(dialogue);
        //PlayDialogue(dialogue);
        //PlayDialogueWithPitchAdjustment(dialogue);
        //CheckDialogueIndex();
    }

    private async UniTaskVoid PlayDialogue(string dialogue)
    {
        if (beforeString == dialogue)
        {
            return;
        }

        beforeString = dialogue;

        // 이전 대사가 실행 중이면 취소 처리
        if (cancellationTokenSource != null)
        {
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
        }

        cancellationTokenSource = new CancellationTokenSource(); // 새로운 토큰 소스 생성
        CancellationToken token = cancellationTokenSource.Token;

        Debug.Log($"어떤말하고 있나? {dialogue}");
        int length = dialogue.Length;
        int num = 0;
        int randomNum = (int)Random.Range(1f, 19f);

        while (num < length)
        {
            num++;

            // 취소 토큰을 체크하여 취소 요청이 있으면 종료
            if (token.IsCancellationRequested)
            {
                Debug.Log("대사 재생이 취소되었습니다.");
                return;
            }

            AudioClip clip = Resources.Load<AudioClip>($"Audio/{randomNum}");
            // 피치 조정
            float randomPitch = Random.Range(3f, 4f);

            // AudioSource에 clip과 pitch 설정
            audioSource.clip = clip;
            audioSource.pitch = randomPitch;  // 피치 조절
            audioSource.Play();
            await UniTask.WaitForSeconds(1 / 10F); 
        }
    }

    private void CheckEvent(string currentText)
    {
        if (currentText.Contains("@")) //currentText.StartsWith("@") // 시작이 저렇게 되는지 확인
        {
            canStartEvent = true; 
            // 포함되어 있다면 다음번 클릭이 들어 왔을 
        }
    }

    public void ReturnDialogue()
    {
        GameEventsManager.instance.npcdialogEvents.ShowRealDialogue();
        Debug.Log($"다이알로그로 돌아와요");
        eventObj[eventNum].SetActive(false);
        canStartEvent = false;
        OnDialogue();
        //CheckDialogueIndex(); // 일단 해보자잉~
        //이벤트 순서가 되었음 , 다음 클릭 때 이벤트 발생하게 해놈
        // 이벤트가 발생함 
        // 이 순서로 돌아옴 
        // 
    }

    private void StartEevnet(int objNum)
    {
        GameEventsManager.instance.npcdialogEvents.offRealDialogue();
        eventObj[objNum].SetActive(true);
        canStartEvent = false; 
        OffDialogue();
        OffOptionDialogue();
    }
    
    // 대화 텍스트를 말 끝나는 기호로 분리하고 줄 바꿈 추가
    private string FormatDialogue(string dialogue)
    {
        dialogue = dialogue.Replace("@", "");
        // 말 끝나는 기호를 기준으로 텍스트를 나눔
        // 정규 표현식을 사용하여 문장 끝을 기준으로 분리하되, 구분자도 포함한다.
        string pattern = @"(?<=[.!?])\s*";  // [] 안에 있는 걸 기준으로 매칭되는 텍스트를 찾음 // 문장 끝에 !, ., ? 가 있을 경우, 그 뒤의 공백을 기준으로 문자열을 나누는 패턴
        string[] sentences = Regex.Split(dialogue, pattern); // Regular Expression
        
        // 각 문장 끝에 줄 바꿈 추가
        List<string> formattedSentences = new List<string>();
        for (int i = 0; i < sentences.Length; i++)
        {
            string sentence = sentences[i].Trim();

            // 마지막 문장이 아니라면 줄 바꿈 추가
            if (i < sentences.Length - 1)
            {
                formattedSentences.Add(sentence + "\n");
            }
            else
            {
                // 마지막 문장은 그대로 추가 (줄 바꿈 없음)
                formattedSentences.Add(sentence);
            }
        }

        PlayDialogue(formattedSentences[0]);
        // 문장들을 하나로 합쳐서 반환
        return string.Join("", formattedSentences);
    }
    
    private void CheckDialogueIndex()
    {
        Debug.Log("대화 인덱스 확인");
        if (canStartEvent)
        {
            eventNum = (int) currentDialogue[0].eventNum.y; 
            Debug.Log($"이벤트 번호: {eventNum}");
            StartEevnet(eventNum);
            return;
        }

        if (currentDialogueIndex + 1 >= currentDialogue[0].dialogue.Length) // 끝일 떄,
        {
            if (currentDialogue[0].options.Length > 0)
            {
                //PlayDialogue(currentDialogue[0].dialogue[currentDialogueIndex]); 
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
            //PlayDialogue(currentDialogue[0].dialogue[currentDialogueIndex]);
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
        // 대화 시작할 떄를 알려줌
        //GameEventsManager.instance.npcdialogEvents.ShowRealDialogue();
        
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
        //PlayDialogue(currentDialogue[0].dialogue[0]);
        
        foreach (Button option in options)
        {
            Destroy(option.gameObject);
        }
        
        
        options.Clear();
        
        OffOptionDialogue();
        currentDialogueIndex = 0; 
        currentDialogueIndex = 0; 
        DisplayDialogue();
    }
    
    private void EndDialogue()
    {
        GameEventsManager.instance.npcdialogEvents.offRealDialogue();
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            task.OnStateChanged(task, TaskState.Inactive, TaskState.Complete);
        }
    }

    public void SetTask(Task newTask) // Task 가 등록된 순간 이거 할당하고 // 바꾸니 상태를 업데이트
    {
        Debug.Log($"대화주제 : {newTask.CodeName}");
        task = newTask; 
        GetDialogue(task, task.State); // 해당 상태에 맞는 대화 라인 가져오기
    }
    
}

// 대화 상태에 따라 상태륿 변경하는 코드 작성