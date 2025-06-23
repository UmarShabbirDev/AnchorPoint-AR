using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ScanController : MonoBehaviour
{
    public ARMeshManager meshManager;
    public GameObject objectMeshGroup;

    private bool isScanning = false;
    public Vector3 objectPosition;
    public float scanRadius = 0.5f;

    public GameObject startButton;
    public GameObject stopButton;

    void Start()
    {
        startButton.SetActive(true);
        stopButton.SetActive(false);
        isScanning = false;
        meshManager.enabled = true; // keep enabled always
    }

    public void StartScan()
    {
        isScanning = true;
        startButton.SetActive(false);
        stopButton.SetActive(true);
        Debug.Log("Scanning started.");
    }

    public void StopScan()
    {
        isScanning = false;
        startButton.SetActive(true);
        stopButton.SetActive(false);
        Debug.Log("Scanning stopped.");

        // Detach and keep mesh chunks visible
        KeepMeshesAlive();

        ShowScannedObjectMesh();
    }

    void KeepMeshesAlive()
    {
        foreach (var mf in meshManager.meshes)
        {
            mf.transform.SetParent(objectMeshGroup.transform);
        }
    }

    public void ShowScannedObjectMesh()
    {
        foreach (var mf in objectMeshGroup.GetComponentsInChildren<MeshFilter>())
        {
            if (Vector3.Distance(mf.transform.position, objectPosition) <= scanRadius)
                mf.gameObject.SetActive(true);
            else
                mf.gameObject.SetActive(false);
        }
    }
}
