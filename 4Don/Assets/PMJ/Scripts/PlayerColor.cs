/*using Fusion;
using UnityEngine;

public class PlayerColor : NetworkBehaviour
{
    public MeshRenderer MeshRenderer;  // 캐릭터의 MeshRenderer (여러 머티리얼을 가진 컴포넌트)

    // 네트워크로 동기화되는 색상 값 (OnChanged로 색상이 변경되면 호출되는 메서드)
    [Networked(OnChanged = nameof(ColorChanged))]
    public Color NetworkedColor { get; set; }

    // 초기 색상 변경
    void Start()
    {
        // 상태 권한이 있을 때만 색상을 초기화
        if (HasStateAuthority)
        {
            // 색상 랜덤으로 설정 (스폰 시 한 번만 호출)
            NetworkedColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);
        }
    }

    // 네트워크에서 색상이 변경되었을 때 호출되는 메서드
    void ColorChanged()
    {
        // 네트워크에서 변경된 색상 값을 MeshRenderer의 material 배열에서 Element2의 색상 변경
        if (MeshRenderer != null && MeshRenderer.materials.Length > 2)
        {
            Material[] materials = MeshRenderer.materials;
            materials[2].color = NetworkedColor; // Element2 (세 번째 머티리얼)의 색상을 변경
            MeshRenderer.materials = materials;  // 변경된 materials 배열을 다시 설정
        }
    }
}*/