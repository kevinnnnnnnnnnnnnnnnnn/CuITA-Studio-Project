using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetSkillsManager : MonoBehaviour
{
    public static GetSkillsManager instance;


    public bool canGetSetGroundSkill;

    public GameObject setGroundSkill;
    
    private void Awake()
    {
        if(instance != null)
            Destroy(instance);
        instance = this;
        
        setGroundSkill.gameObject.SetActive(false);
    }


    private void Update()
    {
        //GetSetGroundSkill();
    }


    public void GetSetGroundSkill()
    {
        if (canGetSetGroundSkill)
        {
            setGroundSkill.SetActive(true);
        }
    }
}
