using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowTracker : MonoBehaviour
{
    [Header("Script Properties")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private GameObject shadowObject;

    [Header("Shadow Properties")]
    [SerializeField] private float minScale = 0.1f;
    [SerializeField] private float maxScale = 1f;
    [SerializeField] private float maxDistance = 10f;

    [Header("Scale Properties (0 for default)")]
    [SerializeField] private float xRatio;
    [SerializeField] private float yRatio;
    [SerializeField] private float zRatio;

    [Header("Debug Values")]
    [SerializeField] private float scaleFactor;
    [SerializeField] private float distance;
    [SerializeField] private Vector3 impactPoint; 

    [Header("Trace Debug")]
    [SerializeField] private bool showTrace = true;
    [SerializeField] private Color traceColour = Color.black;

    // Update is called once per frame
    void Update()
    {
        GetRayData();
        UpdateShadow();
    }

    private void GetRayData()
    {
        //Get distance to ground
        RaycastHit2D distanceRayLeft = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.down, maxDistance, groundLayer);
        if (showTrace)
        {
            Debug.DrawRay(new Vector3(transform.position.x, transform.position.y, transform.position.z), Vector3.down * distance, traceColour);
        }        

        distance = distanceRayLeft.distance;
        impactPoint = distanceRayLeft.point;
    }
    private void UpdateShadow()
    {
        shadowObject.transform.position = impactPoint;

        if(distance > maxDistance)
        {
            shadowObject.SetActive(false);
            return;
        }

        if (!shadowObject.activeSelf) shadowObject.SetActive(true);

       

        shadowObject.transform.localScale = CalculateNewScale(shadowObject);

    }

    private Vector3 CalculateNewScale(GameObject shadowObject)
    {
         Vector3 currentScale;

        scaleFactor = distance / maxDistance;

        scaleFactor = Mathf.Lerp(maxScale, minScale, scaleFactor);

        currentScale = shadowObject.transform.localScale;

        currentScale.x = scaleFactor * xRatio;
        currentScale.y = scaleFactor * yRatio;
        currentScale.z = scaleFactor * zRatio;

        if (xRatio == 0) currentScale.x = shadowObject.transform.localScale.x ;
        if (yRatio == 0) currentScale.y = shadowObject.transform.localScale.y;
        if (zRatio == 0) currentScale.z = shadowObject.transform.localScale.z;

        return currentScale;
    }
}
