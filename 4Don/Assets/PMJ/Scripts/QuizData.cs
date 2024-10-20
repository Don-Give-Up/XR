using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuizData
{
    public int quizNum;
    public string catagory;
    public string quiz;
    public string type;
    public string answer;
    public string desc;
}

public class QuizlistData
{
    public List<QuizData> easy;
    public List<QuizData> normal;
    public List<QuizData> hard;
}

//이지 하드 노말 이 들어가 있는 리스트 만들기 그 안에 구조체 다시 만들어야함!
