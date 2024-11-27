using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ReadyUi : MonoBehaviour
{
    public NetworkPrefabRef sharedGameDataPrefab;
    
    public GameObject readyBG;
    public Button readyButton;
    public GameObject image;
    public Canvas readyCanvas;
   
    public TMP_Text readyText1;
    public TMP_Text readyText2;
    
    
    private bool _isReady;

    public static ReadyUi Instance;

    private void Awake()
    {
        readyText1.text = "";
        readyText2.text = "";
        Instance = this;
    }

    public void Start()
    {
        StartCoroutine(Process());
        readyCanvas.gameObject.SetActive(false);
    }

    public void ReadyPanel()
    {
        readyCanvas.gameObject.SetActive(true);
    }

    public void Ready()
    {
        _isReady = true;
        readyButton.interactable = false;
        readyButton.gameObject.SetActive(false);
        SharedGameData.Instance.RpcReady();
    }

    public void GameEnd()
    {
        SharedGameData.Instance.RpcGameEnd();
    }

    public void ExitGame()
    {
        
        PhoStartGame.Instance.runner.Shutdown();
        
        //SceneManager.LoadScene("Intro");
    }


    private IEnumerator Process()
    {
        // 초기화
        _isReady = false;
        
        // SharedGameData 스폰
        var dataOp = PhoStartGame.Instance.runner.SpawnAsync(sharedGameDataPrefab);
        yield return new WaitUntil(() => dataOp.Status == NetworkSpawnStatus.Spawned);
        dataOp.Object.name = $"{nameof(SharedGameData)}: {dataOp.Object.Id}";

       
        // 모든 플레이어가 레디할 때까지 대기
        var wfs = new WaitForSeconds(0.5f);
        readyButton.gameObject.SetActive(true);
        while (true)
        {
            //var totalCount = PhoStartGame.Instance.runner.SessionInfo.MaxPlayers;
            var totalCount = 2;
            var currentCount = SharedGameData.ReadyCount;
            readyText1.text = $"{currentCount}";
            readyText2.text = $"/{totalCount}";

            yield return wfs;

            if (currentCount == totalCount)
                break;
        }

        readyText1.text = $"{SharedGameData.ReadyCount}";
        //readyText2.text = $"/{PhoStartGame.Instance.runner.SessionInfo.MaxPlayers}";
        readyText2.text = "/2";
        yield return wfs;

        image.SetActive(false);
        // 카운트다운 UI 활성화
        readyButton.gameObject.SetActive(false);
        
        // 게임 시작
        readyBG.SetActive(false);
        

        // 게임 종료 대기
        yield return new WaitUntil(() => SharedGameData.GameEndCount == PhoStartGame.Instance.runner.SessionInfo.PlayerCount);
        
        // 게임 종료
        ExitGame();
    }
}
