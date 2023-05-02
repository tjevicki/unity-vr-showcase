using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceBasedPeekPreventor : MonoBehaviour
{
    
    [SerializeField] private LayerMask collisionLayer;
    [SerializeField] private float checkDistance;

    private Material obstructedViewMaterial;
    private float currentObstructionFactor = 0F;
    private float headSize = 0.037F;


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
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, checkDistance, collisionLayer, QueryTriggerInteraction.Ignore);

        if (hitColliders.Length > 0)
        {
            float hitDistance = checkDistance;
            foreach (var hitCollider in hitColliders)
            {
                float distanceToContact = Vector3.Distance(hitCollider.ClosestPoint(transform.position), transform.position);

                if (distanceToContact < hitDistance)
                {
                    hitDistance = distanceToContact;
                }
            }

            float alpha = 1 - (hitDistance - headSize) / (checkDistance - headSize);
            return alpha;
        }
        else
        {
            return 0F;
        }
    }

    private void SetViewObstruction(float obstructionFactor)
    {
        obstructedViewMaterial.SetFloat("_ObstructionFactor", obstructionFactor);
        currentObstructionFactor = obstructionFactor;
    }
}
