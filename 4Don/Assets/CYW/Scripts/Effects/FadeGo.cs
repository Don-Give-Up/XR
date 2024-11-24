using System;
using UnityEngine;


public class FadeGo : MonoBehaviour
{
    public Animator fadeInAnim;
    public Animator fadeOutAnim;

    public void Play()
    {
        Debug.Log("페이드 인 시작");
        fadeInAnim.SetTrigger("FadeIn");
        fadeOutAnim.SetTrigger("FadeOut");
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("플레이어 입장");
            // 페이드 인 시작
            fadeInAnim.SetTrigger("FadeIn");
            fadeOutAnim.SetTrigger("FadeOut");
        }
    }

    /*
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("플레이어 퇴장");
            fadeOutAnim.SetTrigger("FadeOut");
        }
    }*/
}
