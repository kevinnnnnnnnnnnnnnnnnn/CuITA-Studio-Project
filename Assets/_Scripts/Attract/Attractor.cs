using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    public float gravity = -10;
    public LayerMask attractableLayer;

    [SerializeField]
    private float effectionRadius = 10;
    public List<Collider2D> attractedObjects = new List<Collider2D>();
    [HideInInspector]
    public Transform planetTrans;

    private void Awake()
    {
        planetTrans = GetComponent<Transform>();
    }


    private void Update()
    {
        SetAttractedObjects();
    }

    private void FixedUpdate()
    {
        AttractObject();
    }


    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, effectionRadius);
    }



    public void SetAttractedObjects()
    {
        attractedObjects = Physics2D.OverlapCircleAll(transform.position, effectionRadius, attractableLayer).ToList();
    }


    public void AttractObject()
    {
        for(int i=0;i<attractedObjects.Count;i++)
        {
            attractedObjects[i].GetComponent<Attractable>().Attract(this);
        }
    }

}