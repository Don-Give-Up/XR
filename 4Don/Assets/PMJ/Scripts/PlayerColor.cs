using Fusion;
using UnityEngine;

public class PlayerColor : NetworkBehaviour
{
    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;
    
    [Networked] 
    private Color NetworkedColor { get; set; }

    public override void Spawned()
    {
        Debug.Log($"Player Spawned - HasStateAuthority: {Object.HasStateAuthority}");
        
        if (Object.HasStateAuthority)
        {
            Color randomColor = new Color(
                Random.Range(0f, 1f), 
                Random.Range(0f, 1f), 
                Random.Range(0f, 1f), 
                1f
            );
            NetworkedColor = randomColor;
            Debug.Log($"Set initial color to {randomColor}");
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
            Debug.LogError("SkinnedMeshRenderer is null!");
            return;
        }

        if (skinnedMeshRenderer.materials.Length <= 2)
        {
            Debug.LogError($"Not enough materials! Count: {skinnedMeshRenderer.materials.Length}");
            return;
        }

        Material[] materials = skinnedMeshRenderer.materials;
        materials[2].color = newColor;
        skinnedMeshRenderer.materials = materials;
        
        Debug.Log($"Applied color {newColor} to material");
    }
}