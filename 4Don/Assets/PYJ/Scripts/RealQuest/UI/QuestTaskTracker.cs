using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestTaskTracker : MonoBehaviour
{
    public GameObject questPrefab;
    public Transform questParent; // 퀘스트가 표시될 부모 객체
    private Queue<GameObject> quests;
    private Task currentTask;

    private void Awake()
    {
        quests = new Queue<GameObject>();
    }

    // 만약 이미 불렀던 값이면 다시 부르지 말기
    public void OnDisplay(Task task)
    {
        // 현재 등록된 퀘스트를 가지고 온다.
        
        string title = task.DisplayName;
        string body = task.Description; 
        
        Debug.Log($"{title}, {body}");
        
        if (questPrefab == null)
        {
            Debug.LogError("questPrefab is not assigned!");
            return;  // questPrefab이 null이면 아무 작업도 하지 않고 종료
        }
        
        // 인스턴스를 통해 questPrefab 접근
        GameObject questDisplay = Instantiate(questPrefab, questParent);
        quests.Enqueue(questDisplay);
        
        // questDisplay의 텍스트를 업데이트
        TMP_Text titleText = questDisplay.transform.Find("Title").GetComponentInChildren<TMP_Text>();
        TMP_Text bodyText = questDisplay.transform.Find("Body").GetComponentInChildren<TMP_Text>();

        if (titleText != null)
            titleText.text = title;  // 제목 텍스트 설정

        if (bodyText != null)
            bodyText.text = body;  // 본문 텍스트 설정

        // 애니메이션 시작 (아래에서 위로 나타나며)
        StartCoroutine(FadeInAndMoveUp(questDisplay));
    }

    // 퀘스트 UI를 아래에서 위로 점점 나타나게 하는 메서드
    private IEnumerator FadeInAndMoveUp(GameObject questDisplay)
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
        float moveOffset = 100f; 
        Vector3 initialPosition = rectTransform.position;
        Vector3 targetPosition = new Vector3(initialPosition.x, initialPosition.y + moveOffset, initialPosition.z);  // 위로 이동

        // 애니메이션 동안 반복
        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;  // 0에서 1까지 변화
            float alpha = Mathf.Lerp(0f, 1f, t);  // 투명도 (Alpha) 변화
            Vector3 newPosition = Vector3.Lerp(initialPosition, targetPosition, t);  // 위치 변화

            canvasGroup.alpha = alpha;  // 투명도 설정
            rectTransform.position = newPosition;  // 위치 설정

            elapsedTime += Time.deltaTime;  // 시간 경과
            yield return null;  // 매 프레임 대기
        }

        // 애니메이션 종료 후 최종 상태로 설정
        canvasGroup.alpha = 1f;
        rectTransform.position = targetPosition;
        
        // 띠링 효과 잠시 동안 떠 있게 하기
    }

    // 퀘스트 UI를 흐리게 되면서 왼쪽으로 이동하여 삭제하는 메서드
    public void OffDisplay()
    {
        if (quests.Count > 0)
        {
            // 첫 번째 퀘스트 UI 객체를 큐에서 꺼냄
            GameObject questToRemove = quests.Dequeue();

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