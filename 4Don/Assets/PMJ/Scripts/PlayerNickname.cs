using System;
using Fusion;
using TMPro;
using UnityEngine;

public class PlayerNickname : NetworkBehaviour
{
    [SerializeField] private TextMeshProUGUI nicknameText; // 닉네임 표시할 UI Text
    [SerializeField] private Transform followTarget; // 캐릭터의 위치를 따라갈 대상

    // 네트워크 변수로 Nickname을 정의
    [Networked] 
    public NetworkString<_32> Nickname { get; set; }

    private Camera mainCamera;

    private void Awake()
    {
        // 시작할 때 한 번만 메인 카메라 찾기
        mainCamera = Camera.main;
    }

    private void Start()
    {
        if (nicknameText != null && mainCamera != null)
        {
            nicknameText.transform.rotation = Quaternion.LookRotation(
                nicknameText.transform.position - mainCamera.transform.position);
        }
        // 씬이 로드된 후 NickNameManager에서 닉네임 가져오기
        if (NickNameManager.Instance != null)
        {
            nicknameText.text = NickNameManager.Instance.GetNickName();
        }
    }

    public override void Spawned()
    {
        RpcHoya();
    }


    public override void FixedUpdateNetwork()
    {
        if (followTarget != null && nicknameText != null && mainCamera != null)
        {
            

            // UI가 항상 카메라를 향하도록
            nicknameText.transform.rotation = Quaternion.LookRotation(
                nicknameText.transform.position - mainCamera.transform.position);
        }
    }

    [Rpc(RpcSources.All,RpcTargets.All)]
    private void RpcHoya()
    {
        Debug.Log("이름 위치");
        // 캐릭터 위치 + 오프셋에 닉네임 UI 위치시키기
        Vector3 targetPos = followTarget.position + Vector3.up * 1.9f;
        nicknameText.transform.position = targetPos;
    }
    private void LateUpdate()
    {
        if (mainCamera != null && nicknameText != null)
        {
            // 빌보드 효과: UI가 항상 카메라를 향하도록
            nicknameText.transform.forward = mainCamera.transform.forward;
            
            // UI 크기를 거리에 따라 조절 (선택사항)
            float distance = Vector3.Distance(mainCamera.transform.position, transform.position);
            float scale = Mathf.Max(1, distance * 0.1f);
            nicknameText.transform.localScale = Vector3.one * scale;
        }
    }

    public void UpdateMainCamera()
    {
        mainCamera = Camera.main;
    }

    // 다른 플레이어와 닉네임이 겹치지 않도록 처리 (선택사항)
    private void HandleNicknameOverlap()
    {
        if (nicknameText != null)
        {
            // 레이캐스트로 다른 닉네임 UI와 겹치는지 확인
            RaycastHit[] hits = Physics.RaycastAll(
                Camera.main.transform.position,
                nicknameText.transform.position - Camera.main.transform.position
            );

            // 겹치는 경우 약간 위로 조정
            foreach (RaycastHit hit in hits)
            {
                if (hit.transform != transform && hit.transform.GetComponent<PlayerNickname>() != null)
                {
                    nicknameText.transform.position += Vector3.up * 0.5f;
                }
            }
        }
    }
}
