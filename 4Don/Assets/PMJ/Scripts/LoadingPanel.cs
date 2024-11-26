using UnityEngine;

public class LoadingPanel : MonoBehaviour
{
    public GameObject loading;
    
    public void OnLoading()
    {
        loading.SetActive(true);
    }

    public void EndLoading()
    {
        loading.SetActive(false);
    }

    public void GotoSpaure()
    {
        var player = FindObjectOfType<MJPlayerMovement>();
        if (player != null )
        {
            player.Teleport();
            FirstPersonCamera.Instance.SwitchToFixedCamera();  // 모든 플레이어의 카메라를 고정 카메라로 전환
            
        }
    }
}
