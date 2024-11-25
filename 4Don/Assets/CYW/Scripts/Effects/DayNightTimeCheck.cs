using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DayNightTimeCheck : MonoBehaviour
{
    [Header("스카이 박스3개, Light 넣기")]
    [SerializeField] Material skybox1;
    [SerializeField] Material skybox2;
    [SerializeField] Material skybox3;
    [SerializeField] GameObject directionalLight;   // 아침엔 True
    int hours;  //시간

    float timeInCurrentCycle;  // 5분 주기 내의 현재 시간 (0-300초)
    float cycleDuration = 300f; // 5분 (300초)

    // Start is called before the first frame update
    void Start()
    {
        hours = DateTime.Now.Hour;
        timeInCurrentCycle = (DateTime.Now.Minute * 60) + DateTime.Now.Second; // 현재 초 단위 시간 계산
        StartCoroutine("HourCheck");
    }

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
        if (timeInCurrentCycle < (cycleDuration / 3)) // 0 ~ 100초: skybox1
        {
            directionalLight.SetActive(true);
            RenderSettings.skybox = skybox1;
        }
        else if (timeInCurrentCycle < (cycleDuration * 2 / 3)) // 100 ~ 200초: skybox2
        {
            directionalLight.SetActive(false);
            RenderSettings.skybox = skybox2;
        }
        else // 200 ~ 300초: skybox3
        {
            directionalLight.SetActive(false);
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
}
