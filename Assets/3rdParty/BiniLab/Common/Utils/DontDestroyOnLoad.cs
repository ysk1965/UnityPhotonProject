using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : GameObjectSingleton<DontDestroyOnLoad>
{
    protected override void Awake()
    {
        if (Instance != null)
        {
            GameObject.DestroyImmediate(this.gameObject);
            return;
        }
        base.Awake();
    }

    protected void Start()
    {
        GameObject.DontDestroyOnLoad(this.gameObject);
    }
}
