using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class CircleFillAmount : MonoBehaviour
{
    public GameObject gameObj; // 이 객체는 Image 컴포넌트를 가지고 있어야 합니다.
    public Transform parents;

    private Image[] playerImages = new Image[8];  
    private float[] playerValues = new float[8]{0.01f, 1f, 1f, 1f, 1f, 1f, 1f, 100f}; // 각 플레이어의 값
    private Color[] PlayerColors = new Color[8]
    {
        new Color(145/255f, 225/255f, 212/255f),
        new Color(203/255f, 183/255f, 169/255f),
        new Color(150/255f, 186/255f, 194/255f),
        //new Color(175/255f, 195/255f, 224/255f),
        new Color(122/255f, 218/255f, 13/255f),
        new Color(191/255f, 214/255f, 0/255f),
        new Color(220/255f, 164/255f, 14/255f),
        new Color(221/255f, 97/255f, 26/255f),
        new Color(255/255f, 255/255f, 255/255f), 
        
    }; // 각 플레이어의 색
    
    private float total = 0f;
    private float[] reviseValue; // 수정된 값
    private float[] currentFillAmounts; // 현재 fillAmount 값 (0 ~ 1 범위)
    private bool isUpdating = false;  // 비동기 업데이트가 진행 중인지 확인하는 변수

    private async void Start()
    {
        // reviseValue 배열을 playerValues 배열의 길이에 맞게 초기화
        reviseValue = new float[playerValues.Length];
        currentFillAmounts = new float[playerValues.Length];

        playerImages = new Image[playerValues.Length];

        // 플레이어 이미지 객체 생성
        for (int i = 0; i < playerValues.Length; i++)
        {
            GameObject newObj = Instantiate(gameObj);
            newObj.transform.SetParent(parents, false);

            // Image 컴포넌트를 제대로 가져오고 있는지 확인
            Image newImage = newObj.GetComponent<Image>(); // 직접 Image 컴포넌트를 찾음
            if (newImage != null)
            {
                playerImages[i] = newImage;
            }
            else
            {
                Debug.LogError("Image 컴포넌트를 찾을 수 없습니다. gameObj의 하위 객체를 확인하세요.");
            }
        }

        // 총합 계산
        for (int i = 0; i < playerValues.Length; i++)
        {
            total += playerValues[i];
        }

        // reviseValue 배열에 각 플레이어의 비율을 저장
        for (int i = 0; i < playerValues.Length; i++)
        {
            reviseValue[i] = playerValues[i] / total;  // 각 플레이어의 비율 계산
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isUpdating)
        {
            // R키가 눌렸을 때 비동기 작업 시작
            ResetFillAmountsAndStartAsync().Forget();
        }
    }

    // fillAmount 값들을 리셋하고 다시 시작하는 비동기 함수
    private async UniTaskVoid ResetFillAmountsAndStartAsync()
    {
        isUpdating = true;

        // fillAmount 값을 모두 0으로 리셋
        for (int i = 0; i < currentFillAmounts.Length; i++)
        {
            currentFillAmounts[i] = 0f;
        }

        // 초기화 후 Lerp 애니메이션 시작
        float[] targetFillAmounts = new float[8];
        targetFillAmounts[0] = reviseValue[0] + reviseValue[1] + reviseValue[2] + reviseValue[3] + reviseValue[4] + reviseValue[5] + reviseValue[6] + reviseValue[7];
        targetFillAmounts[1] = reviseValue[0]+ reviseValue[1] + reviseValue[2] + reviseValue[3] + reviseValue[4] + reviseValue[5] + reviseValue[6];
        targetFillAmounts[2] = reviseValue[0]+ reviseValue[1] +reviseValue[2] + reviseValue[3] + reviseValue[4] + reviseValue[5];
        targetFillAmounts[3] = reviseValue[0]+ reviseValue[1] +reviseValue[2] + reviseValue[3] + reviseValue[4];
        targetFillAmounts[4] = reviseValue[0] + reviseValue[1] + reviseValue[2] + reviseValue[3];
        targetFillAmounts[5] = reviseValue[0] + reviseValue[1] + reviseValue[2];
        targetFillAmounts[6] = reviseValue[0] + reviseValue[1];
        targetFillAmounts[7] = reviseValue[0];

        // 2초 동안 Lerp로 fillAmount 값을 부드럽게 업데이트
        float elapsedTime = 0f;
        float duration = 2f; // 2초 동안

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            for (int i = 0; i < playerImages.Length; i++)
            {
                // Lerp로 fillAmount 값 갱신
                currentFillAmounts[i] = Mathf.Lerp(currentFillAmounts[i], targetFillAmounts[i], elapsedTime / duration);
                playerImages[i].fillAmount = currentFillAmounts[i];
                playerImages[i].color = PlayerColors[i]; // 색상 업데이트
            }

            await UniTask.Yield();  // 한 프레임 대기
        }

        // 목표 값에 정확히 도달하게 보정
        for (int i = 0; i < playerImages.Length; i++)
        {
            playerImages[i].fillAmount = targetFillAmounts[i];
        }

        isUpdating = false;
    }
}