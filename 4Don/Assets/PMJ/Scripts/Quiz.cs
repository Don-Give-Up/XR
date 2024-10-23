using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class Quiz : MonoBehaviour
{
    public QuizlistData quizlistdata;

    public TMP_Text TextquizNum;
    public TMP_Text Textcategory;
    public TMP_Text Textquiz;

    public GameObject sugoimage;
    public GameObject[] dotory;
    
    public Button oButton;
    public Button xButton;

    private int correntAnswerCount = 0; // 맞힌 정답 갯수
    private List<int> usedEasyQuestions = new List<int>(); // 이미 출제된 문제 기록

    private int _easyCount;
    private int _normalCount;
    private int _hardCount;

    private void Start()
    {
        QuizStart();
        ShowEasyQuiz();
        sugoimage.SetActive(false);

        // O 버튼과 X 버튼에 정답 체크 이벤트 연결
        oButton.onClick.AddListener(() => OnAnswerSelected("O"));
        xButton.onClick.AddListener(() => OnAnswerSelected("X"));
    }

    public void QuizStart() // 퀴즈 먼저 읽어오기
    {
        //폴더에서 json파일 찾아
        TextAsset jsonQuizData = Resources.Load<TextAsset>("Quiz");

        if (jsonQuizData != null)
        {
            //Debug.Log(" json 찾았지롱"+ jsonQuizData.text);

            //데이터 읽어오고 객체로 변환
            quizlistdata = JsonConvert.DeserializeObject<QuizlistData>(jsonQuizData.text);

            if (quizlistdata != null)
            {
                Debug.Log("Easy 모드 퀴즈 수: " + quizlistdata.easy.Count);
                Debug.Log("Normal 모드 퀴즈 수: " + quizlistdata.normal.Count);
                Debug.Log("Hard 모드 퀴즈 수: " + quizlistdata.hard.Count);
            }
            else
            {
                Debug.LogError("Quiz 데이터 역직렬화 삐빅");
            }
        }
        else
        {
            Debug.LogError("Quiz.json파일 삐빅");
        }
        //QuizlistData loadedData = JsonConvert.DeserializeObject<QuizlistData>();
    }

    private QuizData GETEasyQuiz() // easy난이도를 선택했을때 나옴, 이건 처음에 1문제만 나오지만 정답이 5개가 될때까지 나오도록 고쳐야함.
    {
        _easyCount = quizlistdata.easy.Count;
        if (quizlistdata != null && _easyCount > 0 && usedEasyQuestions.Count < _easyCount)
        {
            int easyRandom;

            do
            {
                easyRandom = Random.Range(0, _easyCount);
            } while (usedEasyQuestions.Contains(easyRandom));

            usedEasyQuestions.Add(easyRandom);

            return quizlistdata.easy[easyRandom];
        }
        else
        {
            return null;
        }

    }

    public void ShowEasyQuiz() // 퀴즈가 보이게 함.
    {
        QuizData easyQuiz = GETEasyQuiz();

        Debug.Log(easyQuiz.quizNum);
        Debug.Log(easyQuiz.category);
        Debug.Log(easyQuiz.quiz);
        Debug.Log(easyQuiz.type);
        Debug.Log(easyQuiz.answer);
        Debug.Log(easyQuiz.desc);

        if (easyQuiz != null)
        {
            TextquizNum.text = $"{easyQuiz.quizNum}";
            Textcategory.text = $"{easyQuiz.category}";
            Textquiz.text = $"{easyQuiz.quiz}";
        }
        else
        {
            Debug.Log("그만 틀리세용!");
        }
    }

    public void OnAnswerSelected(string selectedAnswer)
    {
        //현재 어디이썽?
        if (usedEasyQuestions.Count > 0)
        {
            int lastQuestionIndex = usedEasyQuestions[usedEasyQuestions.Count - 1];
            QuizData currentQuiz = quizlistdata.easy[lastQuestionIndex];

            //정답 체크
            if (currentQuiz.answer == selectedAnswer)
            {
                Debug.Log("정답입니다");
                correntAnswerCount++;
                

                if (correntAnswerCount >= 5)
                {
                    Debug.Log("정답을 다 맞췄습니다! 노동을 종료합니다!");
                    sugoimage.SetActive(true);
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
}

// 뽑는 메소드 1
// 출력하는 메소드 1 나눠리ㅏ!!
//o, x 버튼이 나누어져 있는데 매 문제에 들어오는 버튼을 answer에 따라 정답인지 아닌지 판단하기.