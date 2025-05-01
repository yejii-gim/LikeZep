using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : BaseManager<DialogueManager>
{
    public static DialogueManager Instance;
    public List<DialogueLine> dialogueLines = new List<DialogueLine>();
    private float typingspeed = 0.1f;
    private Coroutine typingCoroutine;
    private void Awake()
    {
        Instance = this;
    }

    public void Init()
    {
        dialogueLines.Add(new DialogueLine("Goblin","헤헤, 이 몸을 도우려면 물약 좀 가져오라구~!","끼익! 아직도 안 가져왔어? 물약 얼른!",false));
        dialogueLines.Add(new DialogueLine("Chort","인간아, 협상하자. 물약 하나로 목숨을 사라.", "약속을 어기는 자는, 두 번 살지 못한다. 물약 가져와",false));
        dialogueLines.Add(new DialogueLine("Doc","흥미롭군. 실험에 필요한 재료를 구해다줄 수 있겠나? 물약이 필요해","아직 실험 못 했다고? 과학을 모독하지 마! 물약줘", false));
        dialogueLines.Add(new DialogueLine("Dwarf","으음… 이 노친네가 부탁 좀 하자꾸나. 물약 하나만!","젊은 친구, 아직도 빈손인가? 물약", false));
        dialogueLines.Add(new DialogueLine("Elf","이 숲의 생명을 위한 물약을 부탁해요.","자연은 기다려주지 않아요… 물약을 서둘러주세요.", false));
        dialogueLines.Add(new DialogueLine("Pumpkin","푸흐흐~ 물약만 있으면 날 더 무섭게 만들 수 있어!", "푸하하! 아직도 물약을 못 구했단 말이냐, 애송이!",false));
    }
    private IEnumerator TypingText(GameObject dialoguePanel,TextMeshProUGUI messageText,string message)
    {
        messageText.text = "";

        for(int i = 0; i<message.Length; i++)
        {
            messageText.text += message[i];
            LayoutRebuilder.ForceRebuildLayoutImmediate(dialoguePanel.GetComponent<RectTransform>());
            yield return new WaitForSeconds(typingspeed);

        }
    }

    public void ShowDialogue(GameObject dialoguePanel, TextMeshProUGUI messageText, string message)
    {
        dialoguePanel.SetActive(true);

        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        typingCoroutine = StartCoroutine(TypingText(dialoguePanel, messageText, message));
    }

    public void HideDialogue(GameObject dialoguePanel)
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }

        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }
    }
}
