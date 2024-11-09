using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    public GameObject[] images; // 0번부터 5번까지 이미지 게임 오브젝트 배열
    private int currentIndex = 0; // 현재 활성화된 이미지 인덱스
    
    private int scaleIncrement = 1; // 스케일 증가량 (1씩 증가)
    private int maxScale = 3; // 최대 스케일 (3배)
    private bool waitingToReset = false; // 3초 대기 중인지 여부

    void Start()
    {
        SetImageVisibility(); // 초기 상태: 0번만 활성화
    }

    void Update()
    {
        if (waitingToReset)
        {
            // 3초 대기 후 0번 이미지로 돌아가도록 처리
            return; // 대기 중일 때는 다른 처리를 하지 않음
        }

        if (Input.GetKeyDown(KeyCode.E)) // E키를 누르면
        {
            if (currentIndex < images.Length - 1) // 5번 이미지까지 가기 전에
            {
                currentIndex++;
                SetImageVisibility();
            }
            else if (currentIndex == images.Length - 1) // 5번 이미지까지 갔다면
            {
                // 이미지들을 다시 초기화하고 대상 오브젝트의 스케일을 증가시킴
                ResetImages();
                StartCoroutine(WaitAndReset()); // 3초 기다리고 0번 이미지로 돌아감
            }
        }

        // 5번 이미지에 도달했을 때 자동으로 3초 대기
        if (currentIndex == images.Length - 1 && !waitingToReset)
        {
            StartCoroutine(WaitAndReset()); // 3초 대기 후 0번 이미지로 돌아감
        }
    }

// 이미지의 활성화 상태를 설정하는 함수
    void SetImageVisibility()
    {
        for (int i = 0; i < images.Length; i++)
        {
            images[i].SetActive(i == currentIndex); // 현재 인덱스만 활성화
        }
    }

// 5번 이미지까지 모두 활성화된 후 이미지들을 끄고 0번만 켜는 함수
    void ResetImages()
    {
        foreach (GameObject image in images)
        {
            image.SetActive(false); // 모든 이미지 비활성화
        }

        images[0].SetActive(true); // 0번만 다시 활성화
    }
    

// 3초 기다린 후 0번 이미지로 돌아가는 코루틴
    IEnumerator WaitAndReset()
    {
        waitingToReset = true; // 3초 대기 중
        yield return new WaitForSeconds(1f); // 3초 대기
        currentIndex = 0; // 0번 이미지로 돌아감
        SetImageVisibility(); // 0번 이미지 활성화
        waitingToReset = false; // 대기 끝
    }
}
