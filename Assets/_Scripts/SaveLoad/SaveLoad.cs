using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoad : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            SaveLoadManager.instance.Save();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            SaveLoadManager.instance.Load();
        }
    }
}
