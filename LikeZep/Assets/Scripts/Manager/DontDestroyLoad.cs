using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyLoad : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var existing = GameObject.FindWithTag("UI");
        if (existing != null && existing != this.gameObject)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }
}
