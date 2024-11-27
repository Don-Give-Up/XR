/*
using System;
using Fusion;
using TMPro;
using UnityEngine;

public class PlayerNickname : NetworkBehaviour
{
    [SerializeField] private Canvas nicknameCanvas;
    [SerializeField] private TextMeshProUGUI nicknameText; // 닉네임 표시할 UI Text
    [SerializeField] private Transform followTarget; // 캐릭터의 위치를 따라갈 대상

    private Transform CamTR => Camera.main.transform;
    
    private void Start()
    {
        if (nicknameText != null && Camera.main != null)
        {
            nicknameText.transform.rotation = Quaternion.LookRotation(nicknameText.transform.position - CamTR.position);
        }
        
        // 씬이 로드된 후 NickNameManager에서 닉네임 가져오기
        if (NickNameManager.Instance != null)
        {
            if (!HasStateAuthority)
                return;
            RpcNickname();
        }
    }
    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void RpcNickname()
    {
        nicknameText.text = NickNameManager.Instance.GetNickName();
        
    }
 
    public override void Spawned()
    {
        RpcHoya();
    }

    public override void FixedUpdateNetwork()
    {
        if (followTarget != null && nicknameText != null && Camera.main != null)
        {
            var rot = Quaternion.LookRotation(nicknameCanvas.transform.position - CamTR.position);
            nicknameCanvas.transform.rotation = rot;
        }
    }

    [Rpc(RpcSources.All,RpcTargets.All)]
    private void RpcHoya()
    {
        // 캐릭터 위치 + 오프셋에 닉네임 UI 위치시키기
        Vector3 targetPos = followTarget.position + Vector3.up * 1.7f;
        nicknameCanvas.transform.position = targetPos;
    }
    private void LateUpdate()
    {
        if (Camera.main != null && nicknameText != null)
        {
            // 빌보드 효과: UI가 항상 카메라를 향하도록
            nicknameCanvas.transform.forward = CamTR.forward;
            // UI 크기를 거리에 따라 조절 (선택사항)
            // float distance = Vector3.Distance(mainCamera.transform.position, transform.position);
            // float scale = Mathf.Max(1, distance * 0.1f);
            // nicknameText.transform.localScale = Vector3.one * scale;
            // nicknameBox.transform.localScale = Vector3.one * scale;
        }
    }
}
*/
