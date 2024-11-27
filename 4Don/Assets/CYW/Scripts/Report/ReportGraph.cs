using System;
using UnityEngine;

public class ReportGraph : MonoBehaviour
{
    // R 누르면 켜지게
    public GameObject graph;
    public GameObject text;
   //public GameObject textR;

    void Start()
    {
        // 게임 시작 시 graph 오브젝트를 비활성화 상태로 설정
        if (graph != null)
        {
            text.SetActive(false); // 시작 시 graph를 비활성화
            graph.SetActive(false);
            //textR.SetActive(false);
        }
    }

    void Update()
    {
        // R 키를 눌렀을 때
        if (Input.GetKeyDown(KeyCode.R))
        {
            // graph 오브젝트를 켬
            if (graph != null)
            {
                text.SetActive(true); // graph를 활성화
                graph.SetActive(true);
                //textR.SetActive(false);
            }
        }
    }
}
