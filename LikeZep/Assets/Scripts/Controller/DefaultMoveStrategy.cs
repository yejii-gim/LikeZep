using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class DefaultMoveStrategy : IPlayerInputStrategy
{
    public void HandleUpdate(PlayerController player)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.StartJump();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            player.TryShoot(); 
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            player.Interact(); 
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            UIManager.Instance.ToggleRiding();
        }
    }

    public void HandleFixedUpdate(PlayerController player)
    {
        player.Move(); 
    }
}
