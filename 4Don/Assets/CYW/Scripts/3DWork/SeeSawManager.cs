using UnityEngine;
using UnityEngine.UI;

public class SeeSawManager : MonoBehaviour
{
    // 정답 선택 상태를 추적하는 변수
    private string selectedAnswer = ""; // "O" 또는 "X"로 설정

// O, X 버튼 클릭을 처리할 Button 컴포넌트
    public Button buttonO;
    public Button buttonX;

// 이미지 게임 오브젝트와 두 개의 버튼 (확인, 다시)
    public GameObject imageObject; // 충돌 후 나타날 이미지
    public Button confirmButton; // 이미지에서 나타날 확인 버튼
    public Button retryButton; // 이미지에서 나타날 다시 버튼

    private bool isImageVisible = false; // 이미지가 보이는지 여부

    private void Start()
    {
        // 처음에 이미지는 비활성화
        imageObject.SetActive(false);

        // 버튼 이벤트 초기화
        confirmButton.onClick.AddListener(OnConfirmClicked);
        retryButton.onClick.AddListener(OnRetryClicked);
        retryButton.gameObject.SetActive(false); // 처음에는 다시하기 버튼 비활성화
    }

// 충돌 시 처리되는 함수
    private void OnCollisionEnter(Collision collision)
    {
        // 충돌한 객체의 태그가 "GroundO"일 경우
        if (collision.gameObject.CompareTag("GroundO"))
        {
            Debug.Log("GroundO 지면입니다");
            selectedAnswer = "O"; // 선택된 답을 "O"로 설정
            ShowImageAndWaitForConfirmation(); // 이미지와 확인 버튼 표시
        }
        // 충돌한 객체의 태그가 "GroundX"일 경우
        else if (collision.gameObject.CompareTag("GroundX"))
        {
            Debug.Log("GroundX 지면입니다");
            selectedAnswer = "X"; // 선택된 답을 "X"로 설정
            ShowImageAndWaitForConfirmation(); // 이미지와 확인 버튼 표시
        }
    }

// 이미지 오브젝트를 표시하고, 확인을 기다리는 함수
    private void ShowImageAndWaitForConfirmation()
    {
        // 이미지 오브젝트를 활성화
        imageObject.SetActive(true);

        // 다시하기 버튼 활성화
        retryButton.gameObject.SetActive(true);
    }

// 확인 버튼 클릭 시 처리 (이미지 숨기고, 버튼 클릭 트리거 실행)
    private void OnConfirmClicked()
    {
        // 디버그 로그 추가
        Debug.Log("확인 버튼이 클릭되었습니다.");

        // 이미지 오브젝트 비활성화
        imageObject.SetActive(false);

        // 선택된 답에 해당하는 버튼 클릭 트리거 실행
        if (selectedAnswer == "O")
        {
            Debug.Log("O 버튼을 클릭합니다.");
            buttonO.onClick.Invoke(); // O 버튼 클릭 트리거
        }
        else if (selectedAnswer == "X")
        {
            Debug.Log("X 버튼을 클릭합니다.");
            buttonX.onClick.Invoke(); // X 버튼 클릭 트리거
        }

        // 다시하기 버튼 비활성화
        retryButton.gameObject.SetActive(false);
    }

// 다시하기 버튼 클릭 시 처리 (이미지 숨기고 초기 상태로 돌아가기)
    private void OnRetryClicked()
    {
        // 디버그 로그 추가
        Debug.Log("다시하기 버튼이 클릭되었습니다.");

        // 이미지 오브젝트 비활성화
        imageObject.SetActive(false);

        // 다시하기 버튼 비활성화
        retryButton.gameObject.SetActive(false);
    }
}
