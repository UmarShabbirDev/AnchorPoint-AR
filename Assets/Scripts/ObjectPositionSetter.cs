using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class ObjectPositionSetter : MonoBehaviour
{
    public ARRaycastManager raycastManager;
    public ScanController scanController;

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Vector2 touchPosition = Input.GetTouch(0).position;

            if (raycastManager.Raycast(touchPosition, hits, TrackableType.FeaturePoint | TrackableType.PlaneWithinPolygon))
            {
                var hitPose = hits[0].pose;
                scanController.objectPosition = hitPose.position;
                Debug.Log("Object position set at: " + hitPose.position);
            }
        }
    }
}
