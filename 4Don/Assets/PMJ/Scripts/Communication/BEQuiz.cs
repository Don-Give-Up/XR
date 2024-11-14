
using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using Google.Apis.Sheets.v4.Data;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Random = UnityEngine.Random;


public class BEQuiz : MonoBehaviour
{
    public Action QuizTeleport;
    
    public QuizData[] BEQuizdata;


    //public TMP_Text TextquizNum;
    //public TMP_Text Textcategory;
    public TMP_Text Textquiz;
    public TMP_Text TextTitle;
    public TMP_Text Textlevel;
    public TMP_Text resultText;
    public TMP_Text desText;
    public TMP_Text textComponent; // 일정 시간 뒤에 텍스트 띄우는 

    public GameObject sugoimage;
    //public GameObject[] dotory;
    public GameObject test;
    public GameObject desImage;
    
    public Canvas oxCanvas;
    public Button oButton;
    public Button xButton;
    
    public bool onlaborCheak = false;

    public int correntAnswerCount = 0; // 맞힌 정답 갯수
    private List<int> usedQuiz = new List<int>(); // 이미 출제된 문제 기록
    
    private int _Count;
    private int _normalCount;
    private int _hardCount;
    public static bool isFinish = false; 
    

    public float displayTime = 5f; // 해설에 배경 이미지가 표시되는 시간
    public float displayDuration = 2f; // 텍스트가 표시될 시간
    

    public static BEQuiz Instance;

    public SeeSawManager seeSawManager;
    
   
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //RoundSystem.Instance.onRoundChange += QuizReset;
            isFinish = true; 
        }
        else
        {
            Debug.Log("BEQuiz Destroy");
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        Debug.Log("OnDestroy");
    }


    /*private void QuizReset(int day)
    {
        //onlaborCheak = false;
        Debug.Log("하루 지났다용"+onlaborCheak);
        // 초기화
       
    }*/

    public void Start()
    {
        
        desImage.SetActive(false);
        
        
        
        if (!onlaborCheak)
        {
            Debug.Log("오늘 노동을 시작.");
            //oxCanvas.gameObject.SetActive(true);
            QuizStart();
            //ShowEasyQuiz();
            

            Debug.Log("퀴즈 시작합니당당구리동동");

            // O 버튼과 X 버튼에 정답 체크 이벤트 연결
            oButton.onClick.RemoveAllListeners(); // 혹시 모를 중복 방지
            xButton.onClick.RemoveAllListeners();
            oButton.onClick.AddListener(() => OnAnswerSelected("O"));
            xButton.onClick.AddListener(() => OnAnswerSelected("X"));
            // 게임을 시작하는 코드들 
        }
        else
        {
            Debug.Log("하루에 노동은 한번만 가능합니다");
        }


    }


    public async void QuizStart() // 퀴즈 먼저 읽어오기
    {
        if (!GoogleSheetManager.Instance.IsLoaded)
            await UniTask.WaitUntil(() => GoogleSheetManager.Instance.IsLoaded);
        
        var urlData = GoogleSheetManager.Instance.UrldataGet("퀴즈데이터");
        
        if (string.IsNullOrEmpty(urlData.Server))
        {
            Debug.LogError("퀴즈데이터 URL의 서버 주소가 비어있습니다.");
            return;
        }



        GetQuizDataFromUrl(urlData.Server).Forget();

    }
    
    private async UniTask GetQuizDataFromUrl(string url)
    {
      
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            //request.SetRequestHeader("Authorization",LoginCommunicator.Value); main
            request.SetRequestHeader("Authorization", "Bearer eyJkYXRlIjoxNzMwNzEyNjA4NTY4LCJ0eXBlIjoiand0IiwiYWxnIjoiSFMyNTYifQ.eyJzdWIiOiJ0b2tlbiA6IDgiLCJtZW1iZXJTY2hvb2wiOiJzY2hvb2wiLCJtZW1iZXJHcmFkZSI6MywibWVtYmVyTmFtZSI6Im5hbWUiLCJtZW1iZXJOaWNrbmFtZSI6Im5pY2tuYW1lIiwiZXhwIjoxNzYyMjQ4NjA4LCJtZW1iZXJSb2xlIjoiU1RVREVOVCIsIm1lbWJlckNsYXNzIjozLCJtZW1iZXJJZCI6OCwibWVtYmVyRW1haWwiOiJlbWFpbCJ9.dxvJMBF88sWHvsPLosKjD4jbgzDPh_-ROUZ7U8vpMW4"); // test
            await request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("퀴즈 데이터를 가져오는 중 오류 발생: " + request.error);
            }
            else
            {
                // JSON 데이터를 받아옴
                string jsonQuizData = request.downloadHandler.text;

                try
                {
                    // JSON 데이터를 QuizData 객체 배열로 변환
                    BEQuizdata = JsonConvert.DeserializeObject<QuizData[]>(jsonQuizData);

                    if (BEQuizdata != null && BEQuizdata.Length > 0)
                    {
                        ShowEasyQuiz();
                        Debug.Log($"총 {BEQuizdata.Length}개의 퀴즈 데이터를 불러왔습니다.");
                    }
                    else
                    {
                        Debug.LogError("퀴즈 데이터를 불러오는 데 실패했습니다.");
                    }
                }
                catch (JsonReaderException ex)
                {
                    Debug.LogError("JSON 파싱 중 오류 발생: " + ex.Message);
                }
            }
        }
    }

    private QuizData GETEasyQuiz() 
    {
        Random.InitState(100);
        _Count = BEQuizdata.Length;
        if (BEQuizdata != null && _Count > 0 && usedQuiz.Count < _Count)
        {
            int QuizRandom;

            do
            {
                QuizRandom = Random.Range(0, _Count);
            } while (usedQuiz.Contains(QuizRandom));

            usedQuiz.Add(QuizRandom);

            return BEQuizdata[QuizRandom];
        }
        else
        {
            return null;
        }
    }

    public void ShowEasyQuiz() // 퀴즈가 보이게 함.
    {
        QuizTeleport?.Invoke();
        QuizData easyQuiz = GETEasyQuiz();


        if (easyQuiz != null)
        {
            Debug.Log(easyQuiz.quizNum);
            Debug.Log(easyQuiz.category);
            Debug.Log(easyQuiz.quiz);
            Debug.Log(easyQuiz.type);
            Debug.Log(easyQuiz.answer);
            Debug.Log(easyQuiz.desc);
            Debug.Log(easyQuiz.level);
            //TextquizNum.text = $"{easyQuiz.quizNum}";
            //Textcategory.text = $"{easyQuiz.category}";
            //Textquiz.text = $"{easyQuiz.quiz}";
            TextTitle.text = $"{easyQuiz.quiz}";
            //Textlevel.text = $"{easyQuiz.level}";

        }
        else
        {
            Debug.Log("더이상 문제 없음");
        }
    }

    // 플레이어 위치가 +면 O 선택, -면 X 선택
    
    
    
    public void OnAnswerSelected(string selectedAnswer)
    {
        Debug.Log("정답이 체크되고 있음"+ selectedAnswer);
        Process(selectedAnswer).Forget();
    }

    private async UniTaskVoid Process(string selectedAnswer)
    {
        TextTitle.text = "";
        
        //현재 어디이썽?
        if (usedQuiz.Count > 0)
        {
            int lastQuestionIndex = usedQuiz[usedQuiz.Count - 1];
            QuizData currentQuiz = BEQuizdata[lastQuestionIndex];

            //정답 체크
            if (currentQuiz.answer == selectedAnswer)
            {
                Debug.Log("정답입니다");
                correntAnswerCount++;
                // 화면에 정답 개수를 표시
                resultText.text = "정답 개수: " + correntAnswerCount.ToString(); // UI 텍스트로 정답 개수를 출력

                // 화면에 정답입니다 텍스트 표시
                await DisplayTextForTime("정답입니다", 1f);
                
                
                

                // 노동 종료할 때 수고 이미지 띄우기
                if (correntAnswerCount >= 3)
                {
                    // 유진이 언니의 월급 관리자 호출
                    // 노동 관리자 호출
                    onlaborCheak = true;
                    Debug.Log("정답을 다 맞혔습니다! 노동을 종료합니다!");
                    ShowDescription(currentQuiz.desc);
                    await UniTask.Delay(100);
                    sugoimage.SetActive(true);

                    await UniTask.Delay(2000);
                    SetActiveFalse();
                    
                    await PhoStartGame.Instance.JoinSquare();
                    
                    return;
                }
                
            }
            else
            {
                Debug.Log("틀렸습니다.");
                await DisplayTextForTime("오답입니다", displayDuration);


            }

            // 문제 사라지고
            // 정답입니다
            ShowDescription(currentQuiz.desc);
            await UniTask.Delay((int)(displayTime * 1000));
            HIdeDescriptionAfterTime(); // 7초 후에 해설을 숨기는 코드

            //다음문제
            //여기다가 플레이어 위치 초기화되는 코드 추가해주기.
            // bool 타입 통해서 다음 문제 나오기 전에 시소 클릭 안 되게 하기
            ShowEasyQuiz();
        }
    }

    
    // 마지막에 수고 이미지도 해설 뜬 이후에 뜨기
    
    private async UniTask DisplayTextForTime(string message, float duration)
    {
        // 텍스트 설정
        textComponent.text = message;

        // duration 동안 기다림
        await UniTask.Delay((int)duration * 1000);

        // 텍스트 숨기기
        textComponent.text = "";

    }

    public void ShowDescription(string currentQuizDesc)
    {
        
        desImage.SetActive(true);
        desText.text = currentQuizDesc;

        // 정답 체크 후 문제 사라지게
        // 정답 체크 후 '정답입니다', '오답입니다' 텍스트 뜨게
        // 해설 숨기고 난 후에 그라운드 체크가 돼야 함
        // 해설 숨기고 난 후에 다음 문제 나오게
       
    }

    private void HIdeDescriptionAfterTime()
    {
        desImage.SetActive(false);
        desText.text = "";
        
        // 해설이 끝난 후 SeeSawManager의 TriggerCollision 메서드 호출
        if (seeSawManager != null)
        {
            seeSawManager.TriggerCollision();
            Debug.Log("SeeSawManager 호출");
        }
        


    }
    
    

    public bool OnLaborCheak()
    {
        return onlaborCheak;
    }

    private void SetActiveFalse()
    {
        sugoimage.SetActive(false);
        oxCanvas.gameObject.SetActive(false);
        
        correntAnswerCount = 0;
        /*foreach (var a in dotory)
        {
            a.SetActive(false); // dotory 초기화
        }*/
        
    }
    
}

// 뽑는 메소드 1
// 출력하는 메소드 1 나눠리ㅏ!!
//o, x 버튼이 나누어져 있는데 매 문제에 들어오는 버튼을 answer에 따라 정답인지 아닌지 판단하기.