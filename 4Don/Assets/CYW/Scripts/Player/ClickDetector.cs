using System;
using UnityEngine;

public class ClickDetector : MonoBehaviour
{

    private HungryBar _hungryBar;


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // 카메라의 위치에서 마우스 클릭 위치로 Ray 생성
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Raycast를 통해 물체를 감지
            if (Physics.Raycast(ray, out hit))
            {
                // 클릭된 물체의 태그 확인
                int breadLevel = 0; // 레벨을 저장할 변수

                if (hit.collider.CompareTag("Bread1"))
                {
                    breadLevel = 1;
                }
                else if (hit.collider.CompareTag("Bread5"))
                {
                    breadLevel = 5;
                }
                else if (hit.collider.CompareTag("Bread10"))
                {
                    breadLevel = 10;
                }

                // 유효한 레벨일 경우 EatBread 호출
                if (breadLevel > 0)
                {
                    Debug.Log("Clicked on: " + hit.collider.gameObject.name);
                    _hungryBar.EatBread(breadLevel);
                }
            }
        }
    }

}

