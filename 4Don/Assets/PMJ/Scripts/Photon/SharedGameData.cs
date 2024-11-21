using Fusion;
using UnityEngine;

public class SharedGameData : NetworkBehaviour
{
    public static SharedGameData Instance;

    public static int ReadyCount { get; private set; }
    public static int GameEndCount { get; private set; }

    public override void Spawned()
    {
        
        if (!HasStateAuthority)
            return;
        Instance = this;

        ReadyCount = 0;
        GameEndCount = 0;
    }
    
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

}