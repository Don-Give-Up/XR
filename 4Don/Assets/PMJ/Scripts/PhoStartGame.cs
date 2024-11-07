using System;
using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class PhoStartGame : MonoBehaviour
{
    public static PhoStartGame Instance { get; private set; }
    
    //방에서 광장으로 가고 싶어
    /// 방 >> 광장 씬 이동
    /// 세션입장.
    public NetworkRunner runner;

    public NetworkRunner runnerPrefab;
    public GameObject playerPrefab;
    
    private bool test = true;
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
        InstantiateRunner();
    }

    private void InstantiateRunner()
    {
        runner = Instantiate(runnerPrefab);
        runner.AddCallbacks(new RunnerController());
    }
   
    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.E)&&test)
        {
            test = false;
            runner.Spawn(playerPrefab,new Vector3(226.1f, 47f, 364.8f), Quaternion.identity);
            test = true;
        }*/

        if (Input.GetKeyDown(KeyCode.F)) // 광장
        {
            InstantiateRunner();
            JoinSquare();
        }
        
        if (Input.GetKeyDown(KeyCode.K)) // 퀴즈
        {
            Shutdown();
            InstantiateRunner();
            JoinQuiz();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Shutdown();
            BackRoom();
        }
    }

    public async void JoinSquare()
    {
        /*var sceneInfo = new NetworkSceneInfo();
        //sceneInfo.AddSceneRef(SceneRef.FromIndex(1));
        sceneInfo.AddSceneRef(SceneRef.FromPath("PMJ/Scenes/Login"));*/
        
        var arg = new StartGameArgs
        {
            GameMode = GameMode.Shared,
            SessionName = "광장",
            Scene = SceneRef.FromIndex(SceneUtility.GetBuildIndexByScenePath("TestMap"))
                
        };
        await runner.StartGame(arg); // await는 뒤에 있는 거를 기다림
    }

    public async void JoinQuiz()
    {
        
        var arg = new StartGameArgs()
        {
            GameMode = GameMode.Shared,
            SessionName = "노동",
            Scene = SceneRef.FromIndex(SceneUtility.GetBuildIndexByScenePath("Photon"))
        };
        await runner.StartGame(arg);
    }

    private async void Shutdown()
    {
        await runner.Shutdown();
    }

    private void BackRoom()
    {
        SceneManager.LoadScene("Room");
    }
}
