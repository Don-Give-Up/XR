using UnityEngine;

public class FadeGo : MonoBehaviour
{
    public Fade vignetteFade; // VignetteFade 스크립트 참조

    /*private void OnCollisionEnter(Collision collision)
    {
        // 플레이어와 충돌했을 때 페이드 인
        if (collision.gameObject.CompareTag("Fade")) 
        {
            Debug.Log("플레이어 입장");
            vignetteFade.FadeIn();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // 플레이어가 충돌을 벗어났을 때 페이드 아웃
        if (collision.gameObject.CompareTag("Fade")) 
        {
            Debug.Log("플레이어 퇴장");
            vignetteFade.FadeOut();
        }
    }*/
}
