/*using UnityEngine;

public class Seesaw : MonoBehaviour
{
    public Rigidbody leftSide;  // 시소 왼쪽 끝
    public Rigidbody rightSide; // 시소 오른쪽 끝
    public float tiltStrength = 10f; // 시소의 기울임 강도

    private HingeJoint hingeJoint;

    void Start()
    {
        // 시소의 중간에 Hinge Joint를 추가
        hingeJoint = gameObject.AddComponent<HingeJoint>();

        // Hinge Joint 설정
        hingeJoint.anchor = Vector3.zero; // 회전 중심
        hingeJoint.axis = Vector3.forward; // 회전 축 (Z 축 기준)
        hingeJoint.useMotor = true;  // 모터를 사용하여 회전 제어
        hingeJoint.motor = new JointMotor
        {
            targetVelocity = 0,  // 초기 속도
            force = 100f  // 모터의 힘
        };
        hingeJoint.useSpring = true;  // 스프링을 사용하여 회전 강도 설정
        hingeJoint.spring = new JointSpring
        {
            spring = tiltStrength, // 강도 설정
            damper = 5f,  // 감쇠 설정
            targetPosition = 0  // 목표 위치 (중립 위치)
        };
    }

    void Update()
    {
        // 왼쪽과 오른쪽에 있는 물체에 힘을 추가하여 시소가 자연스럽게 기울도록 할 수 있음
        if (leftSide != null && rightSide != null)
        {
            // 예: 캐릭터가 왼쪽 끝에 있을 때 시소가 기울어짐
            if (leftSide.velocity.magnitude > 0.1f)
            {
                hingeJoint.motor.targetVelocity = -tiltStrength;  // 왼쪽으로 기울이기
            }
            // 예: 캐릭터가 오른쪽 끝에 있을 때 시소가 반대로 기울어짐
            else if (rightSide.velocity.magnitude > 0.1f)
            {
                hingeJoint.motor.targetVelocity = tiltStrength;  // 오른쪽으로 기울이기
            }
        }
    }
}*/