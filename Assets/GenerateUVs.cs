using UnityEngine;

public class GenerateUVs : MonoBehaviour
{
    void Start()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        if (meshFilter != null && meshFilter.mesh != null)
        {
            Mesh mesh = meshFilter.mesh;
            
            if (mesh.uv.Length == 0)
            {
                Debug.LogWarning("Mesh has no UVs. Generating basic UVs.");
                Vector2[] newUVs = new Vector2[mesh.vertices.Length];

                for (int i = 0; i < newUVs.Length; i++)
                {
                    newUVs[i] = new Vector2(mesh.vertices[i].x, mesh.vertices[i].z); // Simple planar mapping
                }

                mesh.uv = newUVs;
            }
        }
    }
}
