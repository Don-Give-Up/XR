using UnityEngine;

public class InteractionEvent : MonoBehaviour
{
    public int lineY; // 원해 x, y값으로 파싱을 진행하였지만 수정됨, x = 1, y는 해당 클래스를 불러와서 범위 지정을 함
    public int s_lineY;
    [SerializeField] DialgoueEvent dialgoue;
    [SerializeField] SelectEvent select;
    
    public Dialgoue[] GetDialgoues()
    {
        dialgoue.dialgoues = DatabaseManager.instance.GetDialgoues(1, lineY); // (int)dialogue.line.x, (int)dialogue.line.y
        return dialgoue.dialgoues;
    }

    public SelectDialogue[] GetSelectes()
    {
        select.Selecter = DatabaseManager.instance.GetSelects(1, s_lineY);
        return select.Selecter;
    }
}

