using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFadePeekPreventor : MonoBehaviour
{
    
    [SerializeField] private LayerMask collisionLayer;
    [SerializeField] private float checkDistance;
    [SerializeField] private float obstructionSpeed;

    private Material obstructedViewMaterial;
    private float currentObstructionFactor = 0F;


    private void Awake()
    {
        obstructedViewMaterial = GetComponent<Renderer>().material;
    }
    
    void Update()
    {
        SetViewObstruction(CalculateObstructionFactor());
    }

    private float CalculateObstructionFactor()
    {
        float targetObstructionFactor;
        if (Physics.CheckSphere(transform.position, checkDistance, collisionLayer, QueryTriggerInteraction.Ignore))
        {
            targetObstructionFactor = 1F;
        }
        else
        {
            targetObstructionFactor = 0F;
        }

        return Mathf.MoveTowards(obstructedViewMaterial.GetFloat("_ObstructionFactor"), targetObstructionFactor, Time.deltaTime * obstructionSpeed);
    }

    private void SetViewObstruction(float obstructionFactor)
    {
        if (obstructionFactor >= 1F && IsViewFullyObstructed())
        {
            return;
        }

        if (obstructionFactor <= 0F && IsViewFullyClear())
        {
            return;
        }


        obstructedViewMaterial.SetFloat("_ObstructionFactor", obstructionFactor);
        currentObstructionFactor = obstructionFactor;
    }

    private bool IsViewFullyObstructed()
    {
        return currentObstructionFactor >= 1F;
    }

    private bool IsViewFullyClear()
    {
        return currentObstructionFactor <= 0F;
    }
}
