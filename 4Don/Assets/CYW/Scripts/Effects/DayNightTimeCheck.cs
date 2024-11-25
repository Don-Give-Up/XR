using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DayNightTimeCheck : MonoBehaviour
{
    [Header("스카이 박스3개, Light 넣기")]
    [SerializeField] Material skybox1;  // 아침
    [SerializeField] Material skybox2;  // 낮
    [SerializeField] Material skybox3;  // 밤
    [SerializeField] GameObject directionalLight; // 아침에만 활성화

    int hours;  // 시간
    float timeInCurrentCycle;  // 5분 주기 내의 현재 시간 (0-300초)
    float cycleDuration = 300f; // 5분 (300초)

    // Start is called before the first frame update
    void Start()
    {
        hours = DateTime.Now.Hour;
        timeInCurrentCycle = 0f;  // 초기화 시 항상 0으로 시작
        StartCoroutine("HourCheck");
        StartGameAtMorning();  // 아침으로 시작하도록 강제
    }

    // 매 프레임마다 시간 체크
    IEnumerator HourCheck()
    {
        while (true)
        {
            // 현재 시간 계산
            timeInCurrentCycle += Time.deltaTime;

            // 5분이 지났으면 다시 초기화
            if (timeInCurrentCycle >= cycleDuration)
            {
                timeInCurrentCycle = 0f; // 5분 주기를 다시 시작
            }

            Check_Environment();
            yield return null; // 매 프레임마다 체크
        }
    }

    // 아침, 새벽 해지기 전, 밤
    void Check_Environment()
    {
        // 18~4시까지 가로등 ON
        if (hours >= 18 || hours <= 4)
        {
            ToggleLamp(true);  // Lamp 태그를 가진 객체를 켬
        }
        else
        {
            ToggleLamp(false); // Lamp 태그를 가진 객체를 끔
        }

        // 5분 안에서 1분 40초씩 3등분하여 스카이박스를 바꿈
        if (timeInCurrentCycle < (cycleDuration / 3)) // 0 ~ 100초: skybox1 (아침)
        {
            directionalLight.SetActive(true); // 아침에만 활성화
            RenderSettings.skybox = skybox1;
        }
        else if (timeInCurrentCycle < (cycleDuration * 2 / 3)) // 100 ~ 200초: skybox2 (낮)
        {
            directionalLight.SetActive(false); // 낮에선 비활성화
            RenderSettings.skybox = skybox2;
        }
        else // 200 ~ 300초: skybox3 (밤)
        {
            directionalLight.SetActive(false); // 밤에선 비활성화
            RenderSettings.skybox = skybox3;
        }
    }

    // 태그가 "Lamp"인 모든 GameObject의 활성화 상태를 변경하는 함수
    void ToggleLamp(bool isActive)
    {
        GameObject[] lamps = GameObject.FindGameObjectsWithTag("Lamp");
        foreach (GameObject lamp in lamps)
        {
            lamp.SetActive(isActive);
        }
    }

    // 게임 시작 시 아침 환경을 강제로 설정
    void StartGameAtMorning()
    {
        // 아침 환경 강제 설정
        hours = 6;  // 아침 6시로 강제 설정 (예: 6시부터 시작)
        timeInCurrentCycle = 0f;  // 5분 주기의 시작을 0초로 설정

        // 아침 Skybox와 조명 설정
        RenderSettings.skybox = skybox1;
        directionalLight.SetActive(true);  // 아침에는 해가 떠 있으므로 조명 활성화
        ToggleLamp(false); // 아침에는 가로등 비활성화
    }
}
