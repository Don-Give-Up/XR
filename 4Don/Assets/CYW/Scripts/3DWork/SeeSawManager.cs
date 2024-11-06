using UnityEngine;

public class SeeSawManager : MonoBehaviour
{
    // 시소 위의 플레이어가 많아질수록 많이 기우는데 기우는 건 X의 로테이션 값임
    // x의 로테이션은 -12와 12사이에서만 기울여야 함
    // O 와 X에 시소가 닿는 걸 인식하고 상태를 체크해야 함
    
    
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

}
