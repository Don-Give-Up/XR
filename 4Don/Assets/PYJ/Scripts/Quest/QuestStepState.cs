using UnityEngine;

public class QuestStepState
{

    public QuestState state;
    public string status;

    public QuestStepState(QuestState state, string status)
    {
        this.state = state;
        this.status = status; 
    }

    public QuestStepState()
    {
        this.state = QuestState.REQUIREMENTS_NOT_MET;
        this.status = "";
    }
}
