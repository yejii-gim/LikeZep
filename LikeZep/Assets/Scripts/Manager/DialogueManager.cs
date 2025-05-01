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
        dialogueLines.Add(new DialogueLine("Goblin","����, �� ���� ������� ���� �� ��������~!","����! ������ �� �����Ծ�? ���� ��!",false));
        dialogueLines.Add(new DialogueLine("Chort","�ΰ���, ��������. ���� �ϳ��� ����� ���.", "����� ���� �ڴ�, �� �� ���� ���Ѵ�. ���� ������",false));
        dialogueLines.Add(new DialogueLine("Doc","��̷ӱ�. ���迡 �ʿ��� ��Ḧ ���ش��� �� �ְڳ�? ������ �ʿ���","���� ���� �� �ߴٰ�? ������ ������ ��! ������", false));
        dialogueLines.Add(new DialogueLine("Dwarf","������ �� ��ģ�װ� ��Ź �� ���ڲٳ�. ���� �ϳ���!","���� ģ��, ������ ����ΰ�? ����", false));
        dialogueLines.Add(new DialogueLine("Elf","�� ���� ������ ���� ������ ��Ź�ؿ�.","�ڿ��� ��ٷ����� �ʾƿ䡦 ������ ���ѷ��ּ���.", false));
        dialogueLines.Add(new DialogueLine("Pumpkin","Ǫ����~ ���ุ ������ �� �� ������ ���� �� �־�!", "Ǫ����! ������ ������ �� ���ߴ� ���̳�, �ּ���!",false));
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
