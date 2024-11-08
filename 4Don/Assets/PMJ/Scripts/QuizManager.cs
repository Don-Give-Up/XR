using System;
using System.Collections;
using System.Diagnostics;
using Fusion;
using SD;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    public Button readyButton;
    public TMP_Text readyText;
    public NetworkPrefabRef sharedGameDataPrefab;
    public NetworkPrefabRef playerPrefab;

    private bool _isReady;
    
    public void Ready()
    {
        _isReady = true;
        readyButton.interactable = false;
        SharedGameData.Instance.RpcReady();
        readyText.text = $"{SharedGameData.ReadyCount}";
    }

    public void Start()
    {
        StartCoroutine(process());
    }

    private IEnumerator process()
    {
        // SharedGameData 스폰
        var dataOp = PhoStartGame.Instance.runner.SpawnAsync(sharedGameDataPrefab);
        yield return new WaitUntil(() => dataOp.Status == NetworkSpawnStatus.Spawned);
        dataOp.Object.name = $"{nameof(SharedGameData)}: {dataOp.Object.Id}";

        // 플레이어 스폰
        var op = PhoStartGame.Instance.runner.SpawnAsync(playerPrefab);
        yield return new WaitUntil(() => op.Status == NetworkSpawnStatus.Spawned);
        
        /*_spawnedPlayer = op.Object;
        _spawnedPlayer.name = $"Player: {_spawnedPlayer.Id}";

        var playerController = _spawnedPlayer.GetComponent<PlayerController>();
        playerController.Off();*/
    }
}
