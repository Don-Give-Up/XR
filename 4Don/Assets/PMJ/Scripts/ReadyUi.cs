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
   
    public TMP_Text readyText1;
    public TMP_Text readyText2;
    
    public TMP_Text countdownText;

    public int countdown = 3;


    private TickTimer startTimer;
    private bool _isReady;

    public static ReadyUi Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        StartCoroutine(Process());
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
        countdownText.gameObject.SetActive(false);
        readyButton.gameObject.SetActive(true);
        
        // SharedGameData 스폰
        var dataOp = PhoStartGame.Instance.runner.SpawnAsync(sharedGameDataPrefab);
        yield return new WaitUntil(() => dataOp.Status == NetworkSpawnStatus.Spawned);
        dataOp.Object.name = $"{nameof(SharedGameData)}: {dataOp.Object.Id}";

       
        // 모든 플레이어가 레디할 때까지 대기
        var wfs = new WaitForSeconds(0.5f);
        while (true)
        {
            var totalCount = PhoStartGame.Instance.runner.SessionInfo.PlayerCount;
            var currentCount = SharedGameData.ReadyCount;
            readyText1.text = $"{currentCount}";
            readyText2.text = $"/{totalCount}";

            yield return wfs;

            if (currentCount == totalCount)
                break;
        }

        readyText1.text = $"{SharedGameData.ReadyCount}";
        readyText2.text = $"/{PhoStartGame.Instance.runner.SessionInfo.PlayerCount}";
        yield return wfs;

        image.SetActive(false);
        // 카운트다운 UI 활성화
        readyButton.gameObject.SetActive(false);
        countdownText.gameObject.SetActive(true);
        
        // 게임 시작
        readyBG.SetActive(false);

        // 카운트다운 진행
        startTimer = TickTimer.CreateFromSeconds(PhoStartGame.Instance.runner, countdown + 1.1f);
        for (var i = countdown; i > 0; i--)
        {
            countdownText.text = $"{i}!";
            Debug.Log(countdownText.text);
            yield return new WaitForSeconds(1f);
            countdownText.text = string.Empty;
            yield return new WaitForSeconds(0.1f);
        }
        
        // 네트워크상의 시간이 완료될때까지 대기
        yield return new WaitUntil(() => startTimer.Expired(PhoStartGame.Instance.runner));

        // 게임 종료 대기
        yield return new WaitUntil(() => SharedGameData.GameEndCount == PhoStartGame.Instance.runner.SessionInfo.PlayerCount);
        
        // 게임 종료
        ExitGame();
    }
}
