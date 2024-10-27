using UnityEngine;

public class QuizDoor : MonoBehaviour
{
    public Quiz a;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("QuizDoor"))
        {
            Debug.Log("노동 문 열어라");
           
            a.QuizStart();
            a.ShowEasyQuiz();
            a.oxCanvas.gameObject.SetActive(true);
        }
    }
}
