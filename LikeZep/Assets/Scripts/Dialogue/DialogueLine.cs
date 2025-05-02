using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    public string npcName; // NPC�̸�
    public string firstMeeting; // ù��° ������
    public string isQuesting; // ����Ʈ �������϶�

    public bool isComplete = false;
    public bool isQuest = false;

    public DialogueLine(string npcName, string firstMeeting, string isQuesting, bool isQuest, bool isComplete = false   )
    {
        this.npcName = npcName;
        this.firstMeeting = firstMeeting;
        this.isQuesting = isQuesting;
        this.isQuest = isQuest;
        this.isComplete = isComplete;
    }
}
