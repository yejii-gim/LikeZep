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
                Debug.LogWarning($"NPC {currentIndex}가 null입니다. 다음 NPC로 이동.");
                currentIndex++;
                ActivateCurrentNPC(); // null이면 다음 NPC로 재귀 호출
            }
        }
        else
        {
            Debug.Log("모든 NPC 완료!");
        }
    }
    private void CleanupInvalidNPCs()
    {
        npcs = npcs.FindAll(npc => npc != null);
    }
    // NPC 등록
    public void RegisterNPC(NPCController npc)
    {
        if (!npcs.Contains(npc))
        {
            npcs.Add(npc);
        }
    }
}
