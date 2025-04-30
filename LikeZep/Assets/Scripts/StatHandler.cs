using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatHandler : MonoBehaviour
{
    [Range(1, 10)][SerializeField] private int health = 10;

    public int Health
    {
        get => health;
        set => health = Mathf.Clamp(value, 0, 100);
    }

    [Range(1, 10)][SerializeField] private int speed = 3;

    public int Speed
    {
        get => speed;
        set => speed = Mathf.Clamp(value, 0, 20);
    }
}
