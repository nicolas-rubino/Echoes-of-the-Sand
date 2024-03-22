using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass_Bar : MonoBehaviour
{
    public RectTransform compassBar;

    public RectTransform objectiveMarkerTransform;
    public RectTransform northMarkerTransform;
    public RectTransform westMarkerTransform;
    public RectTransform eastMarkerTransform;
    public RectTransform southMarkerTransform;

    public Transform cameraTransform;
    public Transform objectiveTransform;
    public Transform northTransform;
    public Transform eastTransform;
    public Transform westTransform;
    public Transform southTransform;

    // Update is called once per frame
    void Update()
    {
        SetMarkerPosition(objectiveMarkerTransform, objectiveTransform.position);
        SetMarkerPosition(northMarkerTransform, northTransform.position);
        SetMarkerPosition(westMarkerTransform, westTransform.position);
        SetMarkerPosition(eastMarkerTransform, eastTransform.position);
        SetMarkerPosition(southMarkerTransform, southTransform.position);
       // SetMarkerPosition(northMarkerTransform, Vector3.forward * 1000);
        //SetMarkerPosition(southMarkerTransform, Vector3.back * 1000);

    }

    private void SetMarkerPosition(RectTransform marker, Vector3 worldPosition)
    {
        Vector3 directionToTarget = worldPosition - cameraTransform.position;
        float angle = Vector2.SignedAngle(new Vector2(directionToTarget.x, directionToTarget.z),
            new Vector2(cameraTransform.transform.forward.x, cameraTransform.transform.forward.z));
        float compassPositionX = Mathf.Clamp(2 * angle / Camera.main.fieldOfView, -1, 1);

        marker.anchoredPosition = new Vector2(compassBar.rect.width / 2 * compassPositionX, 0);
    
    }
}
