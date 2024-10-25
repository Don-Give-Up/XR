using System;
using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhoStartGame : MonoBehaviour
{
    //방에서 광장으로 가고 싶어
    /// 방 >> 광장 씬 이동
    /// 세션입장.
    public NetworkRunner _runner;

    public NetworkRunner runnerPrefab;
    public GameObject playerPrefab;
    public static PhoStartGame Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 변경되어도 유지
        }
        else
        {
            Destroy(gameObject); // 중복된 인스턴스가 있으면 파괴
        }
    }

    private void Start()
    {
        //_runner = Instantiate(runnerPrefab);
    }

    private bool test = true;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)&&test)
        {
            //Shutdown();
            test = false;
            _runner.Spawn(playerPrefab,new Vector3(226.1f, 47f, 364.8f), Quaternion.identity);
            test = true;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            BackRoom();
        }
    }

    public async void JoinSquare()
    {
        var sceneInfo = new NetworkSceneInfo();
        sceneInfo.AddSceneRef(SceneRef.FromIndex(1));
        
        var arg = new StartGameArgs
        {
            GameMode = GameMode.Shared,
            SessionName = "광장",
            Scene = sceneInfo
        };
        await _runner.StartGame(arg); // await는 뒤에 있는 거를 기다림
    }

    private async void Shutdown()
    {
        await _runner.Shutdown();
    }

    private void BackRoom()
    {
        SceneManager.LoadScene(0);
    }
}
