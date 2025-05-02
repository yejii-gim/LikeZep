using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    public string npcName; // NPC이름
    public string firstMeeting; // 첫번째 만날때
    public string isQuesting; // 퀘스트 진행중일때

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
