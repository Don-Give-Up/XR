using System;
using UnityEngine;

public class QuizError : MonoBehaviour
{
    public GameObject quiz;
    
    private void Start()
    {
       DontDestroyOnLoad(quiz);
    }
}
