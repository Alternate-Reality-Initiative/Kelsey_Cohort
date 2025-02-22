using UnityEngine;

public class UVDebug : MonoBehaviour
{
    void Start()
    {
        Mesh mesh = GetComponent<MeshFilter>().sharedMesh;
        if (mesh.uv.Length > 0)
        {
            Debug.Log("UVs found! First UV: " + mesh.uv[0]);
        }
        else
        {
            Debug.LogError("No UVs found on this mesh!");
        }
    }
}
