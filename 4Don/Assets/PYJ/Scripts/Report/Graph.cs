using System;
using Cysharp.Threading.Tasks;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class Graph : MonoBehaviour
{
    public GameObject line;
    public GameObject head; 
    public GameObject parents;

    private float[] price = new float[] { 0.7f, 0.5f, 0.9f, 0.1f, 0.3f };

    private float offset = 250 + 50f; // 물체의 절반 + 생성된 물체의 절반
    
    public Transform[] poss = new Transform[5];

    private float deadLine = 2f;
    private void Start()
    {
        Sstart().Forget();
    }

    private async UniTask Sstart()
    {
        await UniTask.Delay(2000); 
        for (int i = 0; i < poss.Length - 1; i++)
        {
            GameObject newLine = Instantiate(line);
            GameObject newHead = Instantiate(head);
            GameObject newTail = Instantiate(head);
            
            newLine.transform.SetParent(parents.transform, false);
            newHead.transform.SetParent(parents.transform,false);
            newTail.transform.SetParent(parents.transform,false);
            
            // 생성 위치
            float x = poss[i].position.x;
            float xx = poss[i + 1].position.x; 
            float y = price[i] * 500 + offset;
            float yy = price[i + 1] * 500 + offset;

            Vector3 currentPos = new Vector3(x, y, 0f);
            Vector3 targetPos = new Vector3(xx, yy, 0f);

            // 새로운 라인의 위치를 currentPos로 설정
            newLine.transform.position = currentPos; // 이거 ㄱㅊ
            newHead.transform.position = currentPos; 
            newTail.transform.position = targetPos; 
            
            // 방향 계산
            Vector3 direction = targetPos - currentPos;  // 두 점의 차이를 구함

            // 방향 벡터 정규화 (길이를 1로 만듦)
            direction.Normalize();
            
            // 크기 계산 (두 점 사이의 거리)
            float distance = Vector3.Distance(currentPos, targetPos);  // 두 점 간의 거리
            
            // 방향과 크기를 사용해서 라인을 회전시키고 크기를 조정
            newLine.transform.localScale = new Vector3(distance, newLine.transform.localScale.y, newLine.transform.localScale.z);

            // 회전 (x축을 기준으로 회전)
            Quaternion rotation = Quaternion.FromToRotation(Vector3.right, direction); // x축을 기준으로 방향 벡터로 회전
            newLine.transform.rotation = rotation; // 회전 적용

        }
        
    }
}
