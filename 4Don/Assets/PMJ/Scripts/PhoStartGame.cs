using System;
using System.Collections;
using System.Linq;
using Cysharp.Threading.Tasks;
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
    public NetworkPrefabRef sharedGameDataPrefab;
    public NetworkPrefabRef playerPrefab;
    //public NetworkPrefabRef JJANGPrefab;

    public GameObject loadingPanel;
    
    private bool test = true;
    
    public GameObject loadingObject; // Loading 오브젝트
    private LoginCommunicator loginCommunicator; // LoginCommunicator 스크립트

    public AudioSource audioSourceL; // 로딩씬
    public AudioSource audioSourceQ; // 퀴즈씬
    public AudioSource audioSourceD; // 광장씬
    

    
    
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
        
        // LoginCommunicator 스크립트 가져오기
        loginCommunicator = FindObjectOfType<LoginCommunicator>();
        
    }

    
    // 여기서 러너 생성
    /*private void Start()
    {
        if (runner == null)
        {
            InstantiateRunner();
        }
    }*/

    public void InstantiateRunner()
    {
        runner = Instantiate(runnerPrefab);
        runner.AddCallbacks(new RunnerController());
    }
   
    /*private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)&&test)
        {
            test = false;
            runner.Spawn(playerPrefab,new Vector3(226.1f, 47f, 364.8f), Quaternion.identity);
            test = true;
        }

        if (Input.GetKeyDown(KeyCode.F)) // 광장
        {
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
    }*/

    private async UniTask ResetRunner()
    {
        if (runner != null)
        {
            await runner.Shutdown();
            Debug.Log("RunnerShutdown");
        }
        
        InstantiateRunner();
        Debug.Log("리셋 되었습니다.");
    }

    public async UniTask JoinSquare()
    {
        try 
        {
            // 기존 연결 정리를 확실히
            if (runner != null && runner.IsRunning) 
            {
                await runner.Shutdown();
            }
        
            await ResetRunner();

            loadingPanel.SetActive(true);
        
            loginCommunicator.StopAudio();
            audioSourceQ.Stop();
            audioSourceL.Play();

            var sceneInfo = new NetworkSceneInfo();
            sceneInfo.AddSceneRef(SceneRef.FromIndex(2));

            var arg = new StartGameArgs
            {
                GameMode = GameMode.Shared,
                SessionName = GameDataManager.Instance.gameName,
                PlayerCount = 10,
                Scene = sceneInfo
            };
            
            await UniTask.Delay(2000);
            Debug.Log($"들어간 방 이름{GameDataManager.Instance.gameName}");
            
        
            // 연결 시도 전 상태 체크
            if (!runner.IsRunning)
            {
                var result = await runner.StartGame(arg);
            
                // 결과 확인
                if (result.Ok)
                {
                    Debug.Log("광장 접속됨");
                    
                }
                else
                {
                    Debug.LogError($"Failed to start game: {result.ErrorMessage}");
                    return;
                }
            }

            
            await UniTask.Delay(4000);
            loadingPanel.SetActive(false);

            audioSourceL.Stop();
            audioSourceD.Play();
        }
        catch (Exception e)
        {
            Debug.LogError($"Error joining square: {e.Message}");
            loadingPanel.SetActive(false);
        }
    }
    
    

    public async void JoinQuiz()
    {
        await ResetRunner();
        
       //loadingPanel.SetActive(true);
        {
            // 광장씬 노래 종료
            
            audioSourceD.Stop();
           // audioSourceL.Play();
            var arg = new StartGameArgs()
            {
                GameMode = GameMode.Shared,
                SessionName = "노동",
                PlayerCount = 2,
                Scene = SceneRef.FromIndex(SceneUtility.GetBuildIndexByScenePath("3DWork 1"))
            };
            await runner.StartGame(arg);
        }
        Debug.Log("퀴즈 접속됨");
        //await UniTask.Delay(2000);
        //loadingPanel.SetActive(false);
        // 로딩씬 노래 종료
        //audioSourceL.Stop();
        // 퀴즈씬 노래 시작
        audioSourceQ.Play();
        
    }

    public async UniTask Shutdown()
    {
        await runner.Shutdown();
        runner = null;
    }

    private void BackRoom()
    {
        SceneManager.LoadScene("Room");
    }

    private IEnumerator Process()
    {
        // SharedGameData 스폰
        var dataOp = PhoStartGame.Instance.runner.SpawnAsync(sharedGameDataPrefab);
        yield return new WaitUntil(() => dataOp.Status == NetworkSpawnStatus.Spawned);
        dataOp.Object.name = $"{nameof(SharedGameData)}: {dataOp.Object.Id}";

        // 플레이어 스폰
        var op = PhoStartGame.Instance.runner.SpawnAsync(playerPrefab);
        yield return new WaitUntil(() => op.Status == NetworkSpawnStatus.Spawned);
    }
}
