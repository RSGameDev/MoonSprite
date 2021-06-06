using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowTracker : MonoBehaviour
{
    [Header("Script Properties")]
    public LayerMask groundLayer;    
    public GameObject shadowObject;

    [Header("Shadow Properties")]
    public float minScale = 0.1f;
    public float maxScale = 1f;
    public float maxDistance = 10f;

    [Header("Scale Properties (0 for default)")]
    public float xRatio;
    public float yRatio;
    public float zRatio;

    [Header("Debug Values")]
    public float scaleFactor;
    public float distance;
    public Vector3 impactPoint; 

    // Update is called once per frame
    void Update()
    {
        GetRayData();
        UpdateShadow();
    }

    private void GetRayData()
    {
        //Get distance to ground
        RaycastHit2D distanceRay = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.down, maxDistance, groundLayer);

        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y, transform.position.z), Vector3.down * maxDistance, Color.black);

        distance = distanceRay.distance;
        impactPoint = distanceRay.point;
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

       

        shadowObject.transform.localScale = CalculateNewScale();

    }

    private Vector3 CalculateNewScale()
    {
         Vector3 currentScale;

        scaleFactor = distance / maxDistance;

        scaleFactor = Mathf.Lerp(maxScale, minScale, scaleFactor);

        currentScale = shadowObject.transform.localScale;

        currentScale.x = scaleFactor * xRatio;
        currentScale.y = scaleFactor * yRatio;
        currentScale.z = scaleFactor * zRatio;

        if (xRatio == 0) currentScale.x = shadowObject.transform.localScale.x;
        if (yRatio == 0) currentScale.y = shadowObject.transform.localScale.y;
        if (zRatio == 0) currentScale.z = shadowObject.transform.localScale.z;

        return currentScale;
    }
}
