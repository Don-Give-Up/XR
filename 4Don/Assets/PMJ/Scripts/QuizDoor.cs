using System;
using UnityEngine;

public class QuizDoor : MonoBehaviour
{
    public BEQuiz a;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("QuizDoor"))
        {
            Debug.Log("노동 문 열어라");

            a.AStart();
        }

    }
}
