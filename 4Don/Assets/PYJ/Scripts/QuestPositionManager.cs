using System;
using UnityEngine;

public class QuestPositionManager : MonoBehaviour
{
    //내 위치에서 
    //콜라이더에 부딪혔는데 QuestPosition 울 가지고 있엇으면 그만둔다.
    // 방향을 장소로 안내해야 하는 건 task 가 Running 상태일 때 밖에 없다. 

    public static QuestPositionManager instance; 
    
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
    }
    
    // 일단 은행으로 요청들어왔다고 가정 
    
   // public void 
}
