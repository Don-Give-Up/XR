using System;
using UnityEngine;
using UnityEngine.UI;

public class CircleFillAmount : MonoBehaviour
{
    public GameObject gameObj; // 이 객체는 Image 컴포넌트를 가지고 있어야 합니다.
    public Transform parents;

    private Image[] playerImages = new Image[4];  
    private float[] playerValues = new float[]{4f, 3f, 2f, 1f}; // 각 플레이어의 값
    private Color[] PlayerColors = new Color[]{Color.red, Color.blue, Color.green, Color.yellow}; // 각 플레이어의 색
    private float total = 0f;
    private float[] reviseValue; // 수정된 값
    private float fillAmount0= 0f; 
    private float fillAmount1 = 0f; 
    private float fillAmount2 = 0f; 
    private float fillAmount3 = 0f; 
    
    private float[] fillAmount = new float[4];

    private void Start()
    {
        // reviseValue 배열을 playerValues 배열의 길이에 맞게 초기화
        reviseValue = new float[playerValues.Length];

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
        // 각 플레이어에 대해 fillAmount와 색상 설정
        /*
        int j = 0; 
        for (int i = 0; i < playerValues.Length; i++)
        {
            if (j != i)
            {
                j = i; 
                
                fillAmount = Mathf.Lerp(fillAmount, reviseValue[i], Time.deltaTime * 2f);
                playerImages[i].color = PlayerColors[i];
                playerImages[i].fillAmount = fillAmount;
            }
        }
        */
        
        fillAmount0 = Mathf.Lerp(fillAmount0, reviseValue[0] + reviseValue[1] + reviseValue[2]+ reviseValue[3], 2f * Time.deltaTime);
        playerImages[0].color = PlayerColors[0];
        playerImages[0].fillAmount = fillAmount0;

        fillAmount1 = Mathf.Lerp(fillAmount1, reviseValue[1] + reviseValue[2]+ reviseValue[3], 2f * Time.deltaTime);
        playerImages[1].color = PlayerColors[1];
        playerImages[1].fillAmount = fillAmount1;
        
        fillAmount2 = Mathf.Lerp(fillAmount2, reviseValue[2]+ reviseValue[3], 2f * Time.deltaTime);
        playerImages[2].color = PlayerColors[2];
        playerImages[2].fillAmount = fillAmount2;

        fillAmount3 = Mathf.Lerp(fillAmount3, reviseValue[3], 2f * Time.deltaTime);
        playerImages[3].color = PlayerColors[3];
        playerImages[3].fillAmount = fillAmount3;
        
    }
}