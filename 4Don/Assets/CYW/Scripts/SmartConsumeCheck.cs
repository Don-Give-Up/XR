using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SmartConsumeCheck : MonoBehaviour
{
    public Toggle toggle1; 
    public Toggle toggle2; 
    public Toggle toggle3; 
    public Toggle toggle4;
    public Toggle toggle5; 
    public Toggle toggle6; 
    public Toggle toggle7; 
    public Toggle toggle8;
    public Toggle toggle9; 
    public Toggle toggle10;

    public TMP_Text messageText; // 메시지를 표시할 텍스트

    private void Start()
    {
        // 초기 상태에서 메시지 텍스트를 비활성화
        messageText.gameObject.SetActive(false);

        // 토글 클릭 이벤트 등록
        toggle1.onValueChanged.AddListener((isOn) => DisplayMessage(toggle1, isOn));
        toggle2.onValueChanged.AddListener((isOn) => DisplayMessage(toggle2, isOn));
        toggle3.onValueChanged.AddListener((isOn) => DisplayMessage(toggle3, isOn));
        toggle4.onValueChanged.AddListener((isOn) => DisplayMessage(toggle4, isOn));
        toggle5.onValueChanged.AddListener((isOn) => DisplayMessage(toggle5, isOn));
        toggle6.onValueChanged.AddListener((isOn) => DisplayMessage(toggle6, isOn));
        toggle7.onValueChanged.AddListener((isOn) => DisplayMessage(toggle7, isOn));
        toggle8.onValueChanged.AddListener((isOn) => DisplayMessage(toggle8, isOn));
        toggle9.onValueChanged.AddListener((isOn) => DisplayMessage(toggle9, isOn));
        toggle10.onValueChanged.AddListener((isOn) => DisplayMessage(toggle10, isOn));
    }

    private void DisplayMessage(Toggle clickedToggle, bool isOn)
    {
        if (isOn)
        {
            // 메시지 텍스트를 활성화하고 내용 설정
            messageText.gameObject.SetActive(true);
            messageText.text = "계획적 소비입니다.";
            
            // 코루틴 호출
            StartCoroutine(HideMessageAfterDelay(2f));
        }
    }

    private IEnumerator HideMessageAfterDelay(float delay)
    {
        // 주어진 시간만큼 대기
        yield return new WaitForSeconds(delay);
        
        // 메시지 텍스트 비활성화
        messageText.gameObject.SetActive(false);
    }
    
}
