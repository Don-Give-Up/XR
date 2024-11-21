using UnityEngine;

public class TEST : MonoBehaviour
{
    public QuestReporter QuestReporter; 
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 다이알로그 시작한 상황
            QuestReporter.Report();
        }
        
    }
}
