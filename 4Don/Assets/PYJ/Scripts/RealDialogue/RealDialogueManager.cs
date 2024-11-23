using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    private TaskGroup taskGroup; // 퀘스트 그룹
    private Task task; // 현재 할당된 Task
    private int currentDialogueIndex = 0;
    private int currentOptionDialogueIndex = 0; // 일단 이거 쓰는 곳 없음, 어떻게 해야할 지 모르겠음 
    private bool canFinish = false;
    private bool canStartEvent = false;
    private int eventNum = 0;
    private GameObject[] eventObj;
    
    // dialogue 
    //option 의 구조 반복을 어떻게 처리할 지 확인해보기

    [SerializeField] 
    private Button optionDialogue;
    [SerializeField] 
    private GameObject optionParentsObj;
    private Transform optionParentPos;
    private List<Button> options;

    public AudioSource audioSource;
    
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
        eventObj = new GameObject[]{ };
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
        
        ExtractChosung(dialogue);
        
        string formattedDialogue = FormatDialogue(dialogue);

        CheckEvent(formattedDialogue);
        
        dialogueText.text = formattedDialogue;
        npcName.text = currentDialogue[0].speaker; 

        Debug.Log($"{currentDialogue[0].speaker}: {formattedDialogue}");

        CheckDialogueIndex();
        //CheckDialogueIndex();
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
        eventObj[eventNum].SetActive(false);
        CheckDialogueIndex(); // 일단 해보자잉~
    }

    private void StartEevnet(int objNum)
    {
        eventObj[objNum].SetActive(true);
        canFinish = false; 
        OffDialogue();
        OffOptionDialogue();
    }

    // 대화 텍스트를 말 끝나는 기호로 분리하고 줄 바꿈 추가
    private string FormatDialogue(string dialogue)
    {
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

        // 문장들을 하나로 합쳐서 반환
        return string.Join("", formattedSentences);
    }
    
    // 문자열에서 한 글자씩 분리해서 초성만 분리해서 뭉쳐서 내보낸다.  
    // 
    
    // 한글 음절을 초성만 분리
    public static char GetChosung(char hangul)
    {
        // 한글 유니코드 범위 확인 (AC00 ~ D7A3)
        if (hangul < 0xAC00 || hangul > 0xD7A3)
            throw new ArgumentException("한글 문자만 입력 가능합니다.");

        // 한글 음절의 유니코드 값을 0xAC00 기준으로 변환
        int code = hangul - 0xAC00;

        // 초성 인덱스 계산
        int chosungIndex = code / (21 * 28); // 초성의 인덱스 (19개 초성)
        
        // 초성의 유니코드 범위: 0x1100 ~ 0x1112
        char chosung = (char)(0x1100 + chosungIndex);

        return chosung;  // 초성만 반환
    }

    // 문자열에서 초성만 추출하는 함수
    public static string ExtractChosung(string input)
    {
        StringBuilder chosung = new StringBuilder(); // string 보다 문자열을 ㅎ율적으로 수정하기 좋음
        foreach (char c in input)
        {
            if (c >= 0xAC00 && c <= 0xD7A3)  // 한글인 경우
            {
                char 초성 = GetChosung(c);  // 초성만 추출
                chosung.Append(초성);  // 초성만 추가
            }
        }
        
        Debug.Log($"초성분리: {chosung}");
        return chosung.ToString();  // 초성만 포함된 문자열 반환
    }
    
    // 초성 음성을 랜덤 피치로 재생
    public void PlaySoundWithRandomPitch(string chosung)
    {
        // 초성에 해당하는 음성 파일 경로
        string filePath = "Assets/Resources/Audio/" + chosung + ".mp3";

        // 음성 파일 불러오기
        AudioClip clip = Resources.Load<AudioClip>(filePath);

        if (clip != null)
        {
            // 랜덤 피치 설정 (0.5 ~ 1.5 사이)
            audioSource.pitch = Random.Range(0.5f, 1.5f);

            // 음성 재생
            audioSource.clip = clip;
            audioSource.Play();
        }
        else
        {
            Debug.LogError($"음성 파일 {chosung}.mp3을 찾을 수 없습니다.");
        }
    }
    public void PlayDialogueWithPitchAdjustment(string dialogue)
    {
        string chosungSequence = ExtractChosung(dialogue); // 초성만 추출

        // 각 초성을 순차적으로 재생
        StartCoroutine(PlayChosungSounds(chosungSequence));
    }

    private IEnumerator PlayChosungSounds(string chosungSequence)
    {
        foreach (char chosung in chosungSequence)
        {
            // 초성에 해당하는 음성 파일 재생
            PlaySoundWithRandomPitch(chosung.ToString());

            // 음성 재생 후 잠시 대기
            yield return new WaitForSeconds(0.5f); // 음성 길이에 맞춰 대기
        }
    }
    

    private void CheckDialogueIndex()
    {
        if (canStartEvent)
        {
            eventNum = currentDialogue[0].options[0].eventObjNum; 
            StartEevnet(eventNum);
            return;
        }

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