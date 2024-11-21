using UnityEngine;

public struct DialogueLine
{
    public string speaker;
    public string dialogue;

    public DialogueLine(string speaker, string dialogue)
    {
        this.speaker = speaker;
        this.dialogue = dialogue;
    }
}

public class RealDialogue
{
    public QuestState state;
    public DialogueLine[] dialogueLines; 

}
