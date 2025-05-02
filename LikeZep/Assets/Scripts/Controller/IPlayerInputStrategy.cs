using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerInputStrategy
{
    void HandleUpdate(PlayerController player);
    void HandleFixedUpdate(PlayerController player);
}
