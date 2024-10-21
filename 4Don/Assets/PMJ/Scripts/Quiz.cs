using System;
using Newtonsoft.Json;
using UnityEngine;
using Random = UnityEngine.Random;


public class Quiz : MonoBehaviour
{
    public QuizlistData quizlistdata;

    private int _easyCount;
    private int _normalCount;
    private int _hardCoont;

    private void Start()
    {
        QuizStart();   
        EasyQuiz(); 
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

    private void EasyQuiz()
    {
        if (quizlistdata != null)
        {
            var easy = quizlistdata.easy;
            _easyCount = easy.Count;

            int easyRandom = Random.Range(0, _easyCount);
            
            Debug.Log(easy[easyRandom].quiz);

            //int a = quizlistdata.easy[easyRandom];
        }
    }
}
