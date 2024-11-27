using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class QuestPositionManager : MonoBehaviour
{
    //내 위치에서 
    //콜라이더에 부딪혔는데 QuestPosition 울 가지고 있엇으면 그만둔다.
    // 방향을 장소로 안내해야 하는 건 task 가 Running 상태일 때 밖에 없다. 

    public static QuestPositionManager instance;

    private string[] name = new string[3] {"Quiz", "Bank", "Store"};
    private GameObject me; // 이거 어떻게 해야지? 
    private Transform mePosition; 
    public GameObject[] door = new GameObject[1]; // 여기에 그냥 순서대로 넣어 놓기 
    public GameObject directionObj;

    private List<GameObject> allDots = new List<GameObject>(); 
    public bool isTouched = false; 
    
    
    private void Start()
    {
        if (instance == null)
        {
            instance = this; 
        }
        else
        {
            Destroy(this);
        }

        me = GameObject.FindGameObjectWithTag("Player");
        
        if (me == null)
        {
            Debug.Log("플레이어 못 찾음");
            return; 
        }

        mePosition = me.transform;

        Position(0);
    }
    
    // 퀘스트가 러닝 상태가 되면 요청 
    // 그동안에는 특정 콜라이더와 부딪힐 떄 까지 계속 반복
    // 
    // 일단 은행으로 요청들어왔다고 가정 

    // 요청들어왔다고 가정(완료)
    
    // y축 방향으로 회전
    // 두 물체의 위치가 그리는 선
    // 시작 위치는 나보다 앞 쪽으로 z축에 1, y축에 2 만큼 offset 추가
    public async UniTaskVoid Position(int num)
    {
        if (num == 0)
        {
            return;
        }

        int numNum = num - 1; 

        while (!isTouched)
        {
            Transform doorPosition = door[numNum].transform;
            // 내가 있는 위치 (mePosition)에서 doorPosition으로 향하는 방향 벡터 계산
            Vector3 direction = doorPosition.position - mePosition.position;
            // 방향 벡터의 정보 로그로 출력 (디버그용)
            Debug.Log($"방향: {direction.x}, {direction.y}, {direction.z}");
            // 방향 벡터의 길이를 구합니다 (단위 벡터로 만들기 위함)
            float distance = direction.magnitude;
            // 방향 벡터를 정규화하여 방향만 얻음
            Vector3 unitDirection = direction.normalized;
            // 일정한 간격을 설정합니다 (예: 1 유닛 간격)
            float interval = 2f;
            // 표시할 객체의 개수 계산 (간격에 맞게)
            int objectCount = Mathf.FloorToInt(distance / interval);
            // 새로운 객체를 간격에 맞게 생성하고 배치
            for (int i = 0; i < objectCount; i++)
            {
                // 새 객체를 생성
                GameObject newObj = Instantiate(directionObj);

                // 객체의 위치는 mePosition + (간격 * i * unitDirection)
                Vector3 position = mePosition.position + unitDirection * (i * interval);

                // 새 객체를 해당 위치에 배치
                newObj.transform.position = position;

                allDots.Add(newObj);
                // 해당 객체가 방향을 향하도록 회전 (단방향으로 맞추기 위해 LookRotation 사용)
                //newObj.transform.rotation = Quaternion.LookRotation(unitDirection);
            }

            await UniTask.Delay(1000); 
        }
        // 문 객체의 위치 (door[0] 위치)
        
        // 2f 정도에 한번씩 초기화 되게 하기 

        // 터치 상태 초기화
        isTouched = false;

        /*for (int i = 0; i < allDots.Count; i++)
        {
            Destroy(allDots[i]);
        }*/
        
        allDots.Clear();
    }
}
