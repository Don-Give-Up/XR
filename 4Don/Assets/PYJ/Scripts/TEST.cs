using UnityEngine;

public class TEST : MonoBehaviour
{
    private Task task;

    public void SetTask(Task a)
    {
        task = a; 
        Debug.Log($"치트퀘스트: {task.CodeName}");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) 
        {
          task.OnStateChanged(task, TaskState.Running, TaskState.Complete);
        }
        
    }
}
