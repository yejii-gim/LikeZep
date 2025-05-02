using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameStrategy : IPlayerInputStrategy
{
    public void HandleUpdate(PlayerController player)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.ForMiniGameJump();
        }
    }

    public void HandleFixedUpdate(PlayerController player)
    {
        
    }
}
