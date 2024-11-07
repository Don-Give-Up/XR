using UnityEngine;
using UnityEngine.UI;

public class SeeSawManager : MonoBehaviour
{
    // 시소 위의 플레이어가 많아질수록 많이 기우는데 기우는 건 X의 로테이션 값임
    // x의 로테이션은 -12와 12사이에서만 기울여야 함
    // O 와 X에 시소가 닿는 걸 인식하고 상태를 체크해야 함
    
    // 정답 입력 들어왔을 때 퀴즈 해설 띄우기
    // 채팅창, 문제 띄우는 UI,방 모양으로, 카메라 수정 (3인칭으로)
    // 앞벽에 문제를 띄워야 되는데...
    
    // 틀린 답 고른놈들 다 불구덩이 or 날리기
    // 얘네한테 기회를 다시 줄지 말지 고민 중 -> 기회 안 주면? 서바이벌 기회 주면? 하트 3개 해서 살 수 있게
    // 그래서 결국 제일 많이 맞히는 사람이 제일 돈 많이 버는 거임 => 능력에 따른 임금의 차등 분배?
    // 단원 학습목표에 나옴

    public Button buttonO;
    public Button buttonX;
    
    /*
    public Transform leftSide;  // 왼쪽 지지대
    public Transform rightSide; // 오른쪽 지지대
    private int playerCount;     // 현재 플레이어 수
    private const float maxTilt = 12f; // 최대 기울기

    void Update()
    {
        // 플레이어 수에 따라 기울기 계산
        float tilt = Mathf.Clamp((playerCount * 2) - 12, -maxTilt, maxTilt); 
        transform.localEulerAngles = new Vector3(0, 0, tilt);
    }

    // 플레이어가 시소에 올라갈 때 호출
    public void PlayerEntered()
    {
        playerCount++;
    }

    // 플레이어가 시소에서 내릴 때 호출
    public void PlayerExited()
    {
        playerCount = Mathf.Max(0, playerCount - 1); // 음수가 되지 않도록
    }
    */

    
    
    // 1. O나 X에 닿는 거 판단
    // 2. 그게 정답인지 아닌지를 판단
    // 3. 판단 후 해설 띄우기
    
    
    // O X 저걸 불러오기 그래서 버튼이 실행되게 하게
    
    
    // 충돌이 일어났을 때 호출되는 코드
    private void OnCollisionEnter(Collision collision)
    {
        // 충돌한 객체의 태그가 "GroundO"일 경우
        if (collision.gameObject.CompareTag("GroundO"))
        {
            Debug.Log("GroundO 지면입니다");
            // OnAnswerSelected("O");
            buttonO.onClick.Invoke();  // O 버튼 클릭 트리거
        }
        // 충돌한 객체의 태그가 "GroundX"일 경우
        else if (collision.gameObject.CompareTag("GroundX"))
        {
            Debug.Log("GroundX 지면입니다");
            // OnAnswerSelected("X");
            buttonX.onClick.Invoke();  // X 버튼 클릭 트리거
        }
    }
    
}
