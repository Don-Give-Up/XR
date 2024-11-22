using UnityEngine;

public class BEQuizStart : MonoBehaviour
{
    public BEQuiz quiz;
    void Start()
    {
        Debug.Log("퀴즈 부른다");
        quiz.QuizStart();
        Debug.Log("퀴즈 스타트!");
    }
    
}
