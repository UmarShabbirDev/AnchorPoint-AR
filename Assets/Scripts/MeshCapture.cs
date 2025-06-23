using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class MeshCapture : MonoBehaviour
{
    public ARMeshManager meshManager;

    // Call this method from your UI button to save the current scanned mesh data
    public void SaveCurrentMeshes()
    {
        if (meshManager == null)
        {
            Debug.LogWarning("MeshManager reference not set.");
            return;
        }

        List<Vector3> allVertices = new List<Vector3>();
        List<int> allTriangles = new List<int>();
        List<Vector3> allNormals = new List<Vector3>();

        int vertexOffset = 0;

        foreach (var meshFilter in meshManager.meshes)
        {
            Mesh mesh = meshFilter.mesh;

            allVertices.AddRange(mesh.vertices);
            allNormals.AddRange(mesh.normals);

            // Adjust triangle indices to the combined vertex list
            foreach (int index in mesh.triangles)
            {
                allTriangles.Add(index + vertexOffset);
            }

            vertexOffset += mesh.vertexCount;
        }

        MeshData combinedMeshData = new MeshData
        {
            vertices = allVertices,
            triangles = allTriangles,
            normals = allNormals
        };

        string json = JsonUtility.ToJson(combinedMeshData, true);

        string path = Path.Combine(Application.persistentDataPath, "SavedMeshData.json");
        File.WriteAllText(path, json);

        Debug.Log($"Mesh data saved manually to: {path}");
    }
}

[System.Serializable]
public class MeshData
{
    public List<Vector3> vertices;
    public List<int> triangles;
    public List<Vector3> normals;
}
