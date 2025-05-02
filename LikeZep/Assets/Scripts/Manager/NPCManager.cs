using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCManager : BaseManager<NPCManager>
{
    public static NPCManager Instance;

    [SerializeField] List<NPCController> npcs = new List<NPCController>();
    private int currentIndex = 0;
    private void Awake()
    {
        if (Instance != this && Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
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

    // NPC ���
    public void RegisterNPC(NPCController npc)
    {
        npcs.Add(npc);
    }
}
