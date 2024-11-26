using UnityEngine;

public class LoadingPanel : MonoBehaviour
{
    public GameObject loading;
    public CameraToggle cameraToggle;

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
        var players = FindObjectsOfType<MJPlayerMovement>();
        foreach (var player in players)
        {
            if (player.HasStateAuthority) // 로컬 플레이어만 처리
            {
                player.Spuare();
                cameraToggle.PlayerCamera();
                break;
            }
        }
    }


}
