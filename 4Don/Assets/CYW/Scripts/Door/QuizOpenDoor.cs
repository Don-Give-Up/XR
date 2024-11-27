using UnityEngine;

public class QuizOpenDoor : MonoBehaviour
{
    public Animator leftDoorAnim;
    public Animator rightDoorAnim;

    public AudioSource audioSource; // 문 여는 소리
    private PhoStartGame a;
    
   
    

    private void OnTriggerEnter(Collider other)
    {
      
        
        // 충돌한 물체의 태그가 "Door"라면
        if (other.CompareTag("Player"))
        {
            Debug.Log("문과 충돌");
            // 두 문을 동시에 열게 트리거 실행
            leftDoorAnim.SetTrigger("Open");
            rightDoorAnim.SetTrigger("Open");
            audioSource.Play();
        }
    }
    
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("퀴즈 출발~!~!");
            //a.JoinQuiz();
           
        }
    }
    
}
