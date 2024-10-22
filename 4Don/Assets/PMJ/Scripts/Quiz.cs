using System;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;


public class Quiz : MonoBehaviour
{
    public QuizlistData quizlistdata;
    
    public TMP_Text TextquizNum;
    public TMP_Text Textcategory;
    public TMP_Text Textquiz;
    

    private int _easyCount;
    private int _normalCount;
    private int _hardCoont;

    private void Start()
    {
        QuizStart();
        ShowEasyQuiz();
    }

    public void QuizStart()
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
    
    private QuizData GETEasyQuiz()
    {
        if (quizlistdata != null)
        {
            
            _easyCount = quizlistdata.easy.Count;

            int easyRandom = Random.Range(0, _easyCount);

            return quizlistdata.easy[easyRandom];
        }
        else
        {
            return null;
        }

    }

    public void ShowEasyQuiz()
    {
        QuizData easyQuiz = GETEasyQuiz();
        Debug.Log(easyQuiz.quizNum);
        Debug.Log(easyQuiz.category);
        Debug.Log(easyQuiz.quiz);
        Debug.Log(easyQuiz.type);
        Debug.Log(easyQuiz.answer);
        Debug.Log(easyQuiz.desc);

        TextquizNum.text = $"{easyQuiz.quizNum}";
        Textcategory.text = $"{easyQuiz.category}";
        Textquiz.text = $"{easyQuiz.quiz}";
    }
    
    
}

// 뽑는 메소드 1
// 출력하는 메소드 1 나눠리ㅏ!!
