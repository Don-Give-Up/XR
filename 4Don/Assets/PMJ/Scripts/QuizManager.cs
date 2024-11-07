using System;
using System.Collections;
using System.Diagnostics;
using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    public Button readyButton;
    public TMP_Text readyText;
    public NetworkPrefabRef sharedGameDataPrefab;

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

    }
}
