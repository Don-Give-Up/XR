using UnityEngine;

public class Quest : MonoBehaviour
{
  // 선언
  public QuestInfoSO info;

  public QuestState state;
  private int currentQuestStepIndex;

  // 생성
  public Quest(QuestInfoSO questInfo)
  {
    this.info = questInfo;
    this.state = QuestState.REQUIREMENTS_NOT_MET;
    this.currentQuestStepIndex = 0; 
  }
}
