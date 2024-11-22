using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class FadeGo : MonoBehaviour
{
    public PostProcessVolume volume;  // PostProcessing Volume
    public Vignette vignette;         // Vignette 효과

    // 페이드 속도
    public float fadeSpeed = 1f;
    private bool isFadingIn = false;
    private bool isFadingOut = false;

    private void Start()
    {
        // PostProcessVolume을 찾아 Vignette 컴포넌트 초기화
        volume = GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out vignette);
    }

    private void Update()
    {
        // 페이드 인 상태일 때
        if (isFadingIn && vignette.intensity.value < 0.5f)  // 적당한 intensity 값으로 수정
        {
            vignette.intensity.value += fadeSpeed * Time.deltaTime;
        }
        // 페이드 아웃 상태일 때
        else if (isFadingOut && vignette.intensity.value > 0f)
        {
            vignette.intensity.value -= fadeSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("플레이어 입장");
            // 페이드 인 시작
            isFadingIn = true;
            isFadingOut = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("플레이어 퇴장");
            // 페이드 아웃 시작
            isFadingIn = false;
            isFadingOut = true;
        }
    }
}
