using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReadyUI : MonoBehaviour
{
    public Button readyButton; // 레디 버튼
    public TMP_Text readyStatusText; // 상태 표시 텍스트

    private SharedGameData sharedGameData;

    private void Start()
    {
        sharedGameData = SharedGameData.Instance; // Singleton을 통해 인스턴스를 참조

        if (sharedGameData == null)
        {
            Debug.LogError("SharedGameData instance is not found!");
            return;
        }

        // 버튼 클릭 이벤트 등록
        readyButton.onClick.AddListener(() =>
        {
            sharedGameData.OnReadyButtonPressed();
            readyButton.interactable = false; // 버튼 비활성화 (이미 준비됨)
        });
    }

    private void Update()
    {
        if (sharedGameData != null)
        {
            // 준비 상태 텍스트 업데이트
            readyStatusText.text = $"{sharedGameData.readyCount} / 4 Players Ready";
        }
    }
}