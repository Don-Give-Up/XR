using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks.Triggers;
using Google.Apis.Sheets.v4.Data;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Random = UnityEngine.Random;


public class BEQuiz : MonoBehaviour
{
    public QuizData[] BEQuizdata;

    public TMP_Text TextquizNum;
    public TMP_Text Textcategory;
    public TMP_Text Textquiz;
    public TMP_Text Textlevel;

    public GameObject sugoimage;
    public GameObject[] dotory;
    public GameObject test;
    
    public Canvas oxCanvas;
    
    public Button oButton;
    public Button xButton;
    
    public bool onlaborCheak = false;

    private int correntAnswerCount = 0; // 맞힌 정답 갯수
    private List<int> usedQuiz = new List<int>(); // 이미 출제된 문제 기록
    
    private int _Count;
    private int _normalCount;
    private int _hardCount;

    public static BEQuiz Instance;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            RoundSystem.Instance.onDayChanged += QuizReset;
            
        }
        else
        {
            
            Destroy(gameObject);
        }
    }

    private void QuizReset(int day)
    {
        //onlaborCheak = false;
        Debug.Log("하루 지났다용"+onlaborCheak);
        // 초기화
       
    }

    public async void AStart()
    {
        if (!onlaborCheak)
        {
            
            Debug.Log("오늘 노동을 시작.");
            oxCanvas.gameObject.SetActive(true);
            QuizStart();
            //ShowEasyQuiz();
            sugoimage.SetActive(false);
            //일단 도토리 다 꺼
            foreach (var a in dotory)
            {
                a.SetActive(false);
            }

            Debug.Log("퀴즈 시작합니당당구리동동");

            // O 버튼과 X 버튼에 정답 체크 이벤트 연결
            oButton.onClick.AddListener(() => OnAnswerSelected("O"));
            xButton.onClick.AddListener(() => OnAnswerSelected("X"));
            // 게임을 시작하는 코드들 
        }
        else
        {
            Debug.Log("하루에 노동은 한번만 가능합니다");
        }


    }


    public void QuizStart() // 퀴즈 먼저 읽어오기
    {
       
        var urlData = GoogleSheetManager.Instance.UrldataGet("퀴즈데이터");
        
        if (string.IsNullOrEmpty(urlData.Server))
        {
            Debug.LogError("퀴즈데이터 URL의 서버 주소가 비어있습니다.");
            return;
        }

        StartCoroutine(GetQuizDataFromUrl(urlData.Server));
     
    }
    
    private IEnumerator GetQuizDataFromUrl(string url)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

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
            TextquizNum.text = $"{easyQuiz.quizNum}";
            Textcategory.text = $"{easyQuiz.category}";
            Textquiz.text = $"{easyQuiz.quiz}";
            //Textlevel.text = $"{easyQuiz.level}";

        }
        else
        {
            Debug.Log("어디상 문제 없음");
        }
    }

    public void OnAnswerSelected(string selectedAnswer)
    {
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
                
                //라이프 만들어짐
                if (correntAnswerCount <= dotory.Length)
                {
                    dotory[correntAnswerCount - 1].SetActive(true);
                }
                

                if (correntAnswerCount >= 5)
                {
                    // 유진이 언니의 월급 관리자 호출
                    // 노동 관리자 호출
                    onlaborCheak = true;
                    Debug.Log("정답을 다 맞췄습니다! 노동을 종료합니다!");
                    sugoimage.SetActive(true);

                    correntAnswerCount = 0;
                    
                    Invoke("SetActiveFalse", 3f);
                    
                    
                    return;
                }

            }
            else
            {
                Debug.Log("틀렸습니다.");
            }

            //다음문제
            ShowEasyQuiz();
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
    }
    
}

// 뽑는 메소드 1
// 출력하는 메소드 1 나눠리ㅏ!!
//o, x 버튼이 나누어져 있는데 매 문제에 들어오는 버튼을 answer에 따라 정답인지 아닌지 판단하기.