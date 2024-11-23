using Fusion;
using UnityEngine;

public class PlayerColor : NetworkBehaviour
{
    public SkinnedMeshRenderer SkinnedMeshRenderer;  // 캐릭터의 SkinnedMeshRenderer (여러 머티리얼을 가진 컴포넌트)

    // 네트워크로 동기화되는 색상 값
    [Networked]
    public Color NetworkedColor { get; set; }

    // 네트워크 상태가 변경되었을 때 호출되는 메서드
    public override void FixedUpdateNetwork()
    {
        // 색상이 변경되었을 때 실행되는 메서드
        // NetworkedColor가 변경될 때마다 호출됩니다.
        if (SkinnedMeshRenderer != null && SkinnedMeshRenderer.materials.Length > 2)
        {
            Material[] materials = SkinnedMeshRenderer.materials;
            materials[2].color = NetworkedColor;  // Element2 (세 번째 머티리얼)의 색상을 변경
            SkinnedMeshRenderer.materials = materials;  // 변경된 materials 배열을 다시 설정
            Debug.Log("색 바뀜");
        }
    }

    // 초기 색상 설정 (스폰 시 한 번만 호출)
    void Start()
    {
        if (HasStateAuthority)  // 상태 권한이 있는 경우에만 색상 설정
        {
            // 색상 랜덤으로 설정 (스폰 시 한 번만 호출)
            NetworkedColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);
            Debug.Log("스폰 + 색 바뀜");
        }
    }

    // 상태 권한이 있을 때 색상을 변경하는 메서드
    public void SetColorOnSpawn()
    {
        if (HasStateAuthority)
        {
            // 랜덤 색상으로 설정
            NetworkedColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);
            Debug.Log("색상 변경됨");
        }
    }
}