using Fusion;
using UnityEngine;

public class SharedGameData : NetworkBehaviour
{
    public static SharedGameData Instance;

    public  int readyCount { get; private set; }
    private int maxPlayers = 4;

    private bool isReady = false;
    public static int GameEndCount { get; private set; }

    public override void Spawned()
    {
        if (!HasStateAuthority)
            return;
        Instance = this;

        readyCount = 0;
        GameEndCount = 0;
    }

    public void OnReadyButtonPressed()
    {
        if (isReady)
            return;
        if (Runner.IsServer)
        {
            readyCount++;
            CheckAllPlayerReady();
        }
        else
        {
            RPC_IncreaseReadyCount();
        }
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_IncreaseReadyCount()
    {
        readyCount++;
        CheckAllPlayerReady();
    }

    private void CheckAllPlayerReady()
    {
        if (readyCount >= maxPlayers)
        {
            if (Runner.IsServer)
            {
                // 씬 불러주기
            }
        }
    }
    /*
    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void RpcReady(RpcInfo info = default)
    {
        ReadyCount++;
        Debug.Log($"ReadyCount Changed : {ReadyCount}");
    }
    
    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void RpcGameEnd(RpcInfo info = default)
    {
        GameEndCount++;
        Debug.Log($"GameEndCount Changed : {GameEndCount}");
    }
    */

}