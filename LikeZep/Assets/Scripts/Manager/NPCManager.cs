using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCManager : BaseManager<NPCManager>
{

    [SerializeField] List<NPCController> npcs = new List<NPCController>();
    private int currentIndex = 0;

    private void Start()
    {
        if(npcs.Count > 0)
        {
            npcs[0].Activate();
        }
    }

    public void NotifyNPCCompleted(NPCController completedNPC)
    {
        if (currentIndex < npcs.Count && npcs[currentIndex] == completedNPC)
        {
            npcs.RemoveAt(currentIndex);
            currentIndex++;
            ActivateCurrentNPC();
        }
    }

    private void ActivateCurrentNPC()
    {
        CleanupInvalidNPCs();

        if (currentIndex < npcs.Count)
        {
            var npc = npcs[currentIndex];
            if (npc != null)
            {
                npc.Activate();
            }
            else
            {
                Debug.LogWarning($"NPC {currentIndex}�� null�Դϴ�. ���� NPC�� �̵�.");
                currentIndex++;
                ActivateCurrentNPC(); // null�̸� ���� NPC�� ��� ȣ��
            }
        }
        else
        {
            Debug.Log("��� NPC �Ϸ�!");
        }
    }
    private void CleanupInvalidNPCs()
    {
        npcs = npcs.FindAll(npc => npc != null);
    }
    // NPC ���
    public void RegisterNPC(NPCController npc)
    {
        if (!npcs.Contains(npc))
        {
            npcs.Add(npc);
        }
    }
}
