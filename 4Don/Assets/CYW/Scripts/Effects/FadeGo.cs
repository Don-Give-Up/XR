using System;
using Fusion;
using UnityEngine;


public class FadeGo : MonoBehaviour
{
    public Animator fadeInAnim;
    public Animator fadeOutAnim;

    private bool _interact = true; // >> 플레이어가 닿았을때 true, 

    public void Play()
    {
        Debug.Log("페이드 인 시작");
        fadeInAnim.SetTrigger("FadeIn");
        fadeOutAnim.SetTrigger("FadeOut");
    }


    private void OnTriggerEnter(Collider other)
    {

        // 자기한테만 페이드인 페이드아웃 적용되게
        Debug.Log("닿음");
        var ntObject = other.GetComponent<NetworkObject>();
        Debug.Log($"ntObject " + ntObject);
        if (other.CompareTag("Player") && _interact)
        {
            if (ntObject.InputAuthority == PhoStartGame.Instance.runner.LocalPlayer)
            {
                // 페이드 인 시작
                fadeInAnim.SetTrigger("FadeIn");
                fadeOutAnim.SetTrigger("FadeOut");
                Debug.Log("노");
            }
        }

    }

}
