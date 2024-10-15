using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetStepTargetManager : MonoBehaviour
{
    public static SetStepTargetManager instance;

    public float stepPlayerTargetX;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
        }
        else
        {
            instance = this;
        }
    }
}
