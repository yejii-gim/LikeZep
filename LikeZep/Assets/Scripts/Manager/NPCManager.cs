using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : BaseManager<NPCManager>
{
    public static NPCManager Instance;

    [SerializeField] List<NPCController> npcs = new List<NPCController>();
    private int currentIndex = 0;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (npcs.Count > 0)
        {
            npcs[0].Activate();
        }
    }

    // ���� NPC�� �Ϸ�Ǿ��� �� ���� NPC�� Ȱ��ȭ
    public void NotifyNPCCompleted(NPCController completedNPC)
    {
        if (npcs.Contains(completedNPC))
        {
            npcs.Remove(completedNPC);
        }

        if (npcs.Count > 0)
        {
            npcs[0].Activate();
        }
    }

    public void RegisterNPC(NPCController npc)
    {
        npcs.Add(npc);
    }
}
