using Fusion;
using UnityEngine;

public class PlayerColor : NetworkBehaviour
{
    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;
    
    [Networked] 
    private Color NetworkedColor { get; set; }

    private const string ColorKey = "PlayerColor"; // 색상 저장 키

    public override void Spawned()
    {
        Debug.Log($"Player Spawned - HasStateAuthority: {Object.HasStateAuthority}");
        
        if (Object.HasStateAuthority)
        {
            // 씬 전환 후에도 색상 유지
            string savedColor = PlayerPrefs.GetString(ColorKey, string.Empty);
            
            if (string.IsNullOrEmpty(savedColor))
            {
                // 색상이 저장되지 않은 경우, 새로운 랜덤 색상 생성
                Color randomColor = new Color(
                    Random.Range(0f, 1f), 
                    Random.Range(0f, 1f), 
                    Random.Range(0f, 1f), 
                    1f
                );
                NetworkedColor = randomColor;
                Debug.Log($"Set initial color to {randomColor}");
                
                // 색상 저장
                PlayerPrefs.SetString(ColorKey, ColorUtility.ToHtmlStringRGBA(randomColor));
                PlayerPrefs.Save();
            }
            else
            {
                // 저장된 색상을 불러와서 적용
                if (ColorUtility.TryParseHtmlString("#" + savedColor, out Color loadedColor))
                {
                    NetworkedColor = loadedColor;
                    Debug.Log($"Loaded saved color: {loadedColor}");
                }
            }
        }
    }

    public override void Render()
    {
        ApplyColor(NetworkedColor);
    }

    private void ApplyColor(Color newColor)
    {
        if (skinnedMeshRenderer == null)
        {
            Debug.LogError("SkinnedMeshRenderer가 null입니다!");
            return;
        }

        // 머티리얼이 최소 1개 이상 있는지 확인
        if (skinnedMeshRenderer.materials.Length <= 0)
        {
            Debug.LogError($"머티리얼이 없습니다! Count: {skinnedMeshRenderer.materials.Length}");
            return;
        }

        // 첫 번째 머티리얼의 색상 변경
        Material[] materials = skinnedMeshRenderer.materials;
        materials[0].color = newColor; // 첫 번째 머티리얼의 색상 변경
        skinnedMeshRenderer.materials = materials;

        Debug.Log($"첫 번째 머티리얼에 색상 {newColor} 적용 완료");
    }
}
