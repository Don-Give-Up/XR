using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using TMPro;

public class QuestTaskTracker : MonoBehaviour
{
    public GameObject questPrefab; 
    public GameObject[] changePrefabs; 
    private GameObject detailObj; 
    public Transform questParent; // 퀘스트가 표시될 부모 객체
    private Queue<GameObject> quests;
    private Task currentTask = null;
    private string title;
    private string body; 
    private string[] taskDetails; 

    private void Awake()
    {
        quests = new Queue<GameObject>();
    }

    // 만약 이미 불렀던 값이면 다시 부르지 말기
    public void OnDisplay(Task task)
    {
        if (currentTask == task)
        {
            return;
        }
        // 현재 등록된 퀘스트를 가지고 온다.
        currentTask = task;
        taskDetails = new string[] { }; 
        
        title = task.DisplayName;
        body = task.Description;
        taskDetails = task.Directions;
        
        Debug.Log($"타이틀: {title}, 본문: {body}");
        
        // 일단 
        // 1. 1번재 게임 오브젝트를 생성한다. 
        // 2. 2번째 게임 오브젝트를 생성한다. 
        // 3. 3번쨰 게임 오브젝트를 생성한다. 
        // 4. 4번째 게임 오브젝트는 누르면 내용을 더 보여주고 다시 클릭하면 돌아와야한다. 
        PopUpQuest().Forget(); 
    }

    private void UpdateText(GameObject obj)
    {
        TMP_Text titleText = obj.transform.Find("Title")?.GetComponentInChildren<TMP_Text>();
        TMP_Text bodyText = obj.transform.Find("Body")?.GetComponentInChildren<TMP_Text>();
        TMP_Text detailText = obj.transform.Find("Detail")?.GetComponentInChildren<TMP_Text>();
        Debug.Log($"UpdateText {title}", titleText);

        if (titleText != null)
        {
            titleText.text = title;  // 제목 텍스트 설정
        }
        else
        {
            Debug.Log("텍스트 없됴");
        }


        if (bodyText != null)
        {
            bodyText.text = body;
        }


        string dirs = ""; 
        
        if (detailText != null)
        {
            for (int i = 0; i< taskDetails.Length; i++)
            {
                dirs += $"{i+1}. {taskDetails[i]}\n";
            }
            detailText.text = dirs; 
        }
    }

    private async UniTaskVoid PopUpQuest()
    {
        GameObject questDiaplay = Instantiate(changePrefabs[0]);
        questDiaplay.transform.SetParent(questParent, false);
        RectTransform rectTransform = questDiaplay.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(740F, -350F); // 화면 중앙에 배치
        
        UpdateText(questDiaplay);
        // 소리 효과, 띠링

        //QuestSoundManager questSoundManager = GameObject.Find() 
        
        await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0));

        Destroy(questDiaplay.gameObject);
        StartCoroutine(FadeInAndMove());
    }

    // 퀘스트 UI를 아래에서 위로 점점 나타나게 하는 메서드
    private IEnumerator FadeInAndMove()
    {
        GameObject moveQuest = Instantiate(changePrefabs[1]);
        moveQuest.transform.SetParent(questParent, false);
        RectTransform rectTransform = moveQuest.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(740f, -350f); 
        
        
        CanvasGroup canvasGroup = moveQuest.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            // CanvasGroup이 없다면 새로 추가
            canvasGroup = moveQuest.AddComponent<CanvasGroup>();
        }

        float duration = 0.5f;  // 애니메이션 지속 시간
        float elapsedTime = 0f;
        float moveOffset = 100f; 
        Vector3 initialPosition = rectTransform.position;
        Vector3 targetPosition = new Vector3(questParent.position.x, questParent.position.y, questParent.position.z);  // 위로 이동

        //Vector3 targetPosition = new Vector3(initialPosition.x, initialPosition.y + moveOffset, initialPosition.z);  // 위로 이동

        // 애니메이션 동안 반복
        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration; 
            float alpha = Mathf.Lerp(1f, 0f, t);  
            
            Vector3 newPosition = Vector3.Lerp(initialPosition, targetPosition, t);  // 위치 변화
            
            canvasGroup.alpha = alpha;
            rectTransform.position = newPosition;  // 위치 설정

            elapsedTime += Time.deltaTime;  // 시간 경과
            yield return null;  // 매 프레임 대기
        }

        // 애니메이션 종료 후 최종 상태로 설정
        rectTransform.position = targetPosition;
        
        Destroy(moveQuest.gameObject);

        GameObject questObj = Instantiate(changePrefabs[2]); 
        questObj.transform.SetParent(questParent, false);
        RectTransform rectTransforms = questObj.GetComponent<RectTransform>();
        rectTransforms.anchoredPosition = new Vector2(0f, 0f); 
        quests.Enqueue(questObj);
        UpdateText(questObj);
    }

    // 퀘스트 UI를 흐리게 되면서 왼쪽으로 이동하여 삭제하는 메서드
    public void OffDisplay()
    {
        if (quests.Count > 0)
        {
            // 첫 번째 퀘스트 UI 객체를 큐에서 꺼냄
            GameObject questToRemove = quests.Dequeue();
            // 퀘스트 완료 효과, 띠로링

            // 애니메이션 시작 (흐리게 되면서 왼쪽으로 이동)
            StartCoroutine(FadeOutAndMoveLeft(questToRemove));

            Debug.Log("Quest UI is disappearing...");
        }
        else
        {
            Debug.LogWarning("No quest to remove!");
        }
    }

    // 흐리게 되면서 왼쪽으로 이동하는 애니메이션 코루틴
    private IEnumerator FadeOutAndMoveLeft(GameObject questDisplay)
    {
        CanvasGroup canvasGroup = questDisplay.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            // CanvasGroup이 없다면 새로 추가
            canvasGroup = questDisplay.AddComponent<CanvasGroup>();
        }

        RectTransform rectTransform = questDisplay.GetComponent<RectTransform>();

        float duration = 1.0f;  // 애니메이션 지속 시간
        float elapsedTime = 0f;
        float moveOffset = 200f; 
        Vector3 initialPosition = rectTransform.position;
        Vector3 targetPosition = new Vector3(initialPosition.x - moveOffset, initialPosition.y, initialPosition.z);  // 왼쪽으로 이동

        // 애니메이션 동안 반복
        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;  // 0에서 1까지 변화
            float alpha = Mathf.Lerp(1f, 0f, t);  // 투명도 (Alpha) 변화
            Vector3 newPosition = Vector3.Lerp(initialPosition, targetPosition, t);  // 위치 변화

            canvasGroup.alpha = alpha;  // 투명도 설정
            rectTransform.position = newPosition;  // 위치 설정

            elapsedTime += Time.deltaTime;  // 시간 경과
            yield return null;  // 매 프레임 대기
        }

        // 애니메이션 종료 후 완전히 흐려지고 왼쪽으로 이동했으므로 삭제
        Destroy(questDisplay);
    }
    
    // 퀘스트가 설명이 있으면 눌렀을 떄 늘어나면서 나오던가 
    // 퀘스트가 설명이 있으면 밑에 작게 추가되던가
}