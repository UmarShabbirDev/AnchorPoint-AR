using UnityEngine;

public class MeshInspector : MonoBehaviour
{
    private Vector2 previousTouchPos1;
    private Vector2 previousTouchPos2;
    private float rotationSpeed = 0.2f;
    private float zoomSpeed = 0.01f;
    private float minScale = 0.1f;
    private float maxScale = 2f;

    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                float rotationY = touch.deltaPosition.x * rotationSpeed;
                float rotationX = -touch.deltaPosition.y * rotationSpeed;
                transform.Rotate(rotationX, rotationY, 0, Space.World);
            }
        }
        else if (Input.touchCount == 2)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            if (touch2.phase == TouchPhase.Began)
            {
                previousTouchPos1 = touch1.position;
                previousTouchPos2 = touch2.position;
            }
            else if (touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved)
            {
                float prevDistance = Vector2.Distance(previousTouchPos1, previousTouchPos2);
                float currDistance = Vector2.Distance(touch1.position, touch2.position);
                float deltaDistance = currDistance - prevDistance;

                float scaleFactor = 1 + deltaDistance * zoomSpeed;
                Vector3 newScale = transform.localScale * scaleFactor;

                // Clamp scale
                newScale = Vector3.Max(newScale, Vector3.one * minScale);
                newScale = Vector3.Min(newScale, Vector3.one * maxScale);

                transform.localScale = newScale;

                previousTouchPos1 = touch1.position;
                previousTouchPos2 = touch2.position;
            }
        }
    }
}
