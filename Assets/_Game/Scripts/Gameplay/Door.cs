using UnityEngine;

[ExecuteAlways]
public class Door : MonoBehaviour
{
    [SerializeField] private Sprite closedSprite;
    [SerializeField] private Sprite openSprite;
    [SerializeField] private Vector2 topLeft;
    [SerializeField] private Vector2 topRight;
    [SerializeField] private Vector2 bottomLeft;
    [SerializeField] private Vector2 bottomRight;
    [SerializeField] private AudioSource doorAudio;
    private Mesh mesh;
    private Sprite current;

    void OnEnable()
    {
        if (current == null)
        {
            current = closedSprite;
        }

        UpdateMesh();
        UpdateTexture();
    }

    void OnValidate()
    {
        UpdateMesh();
    }

    public void Open()
    {
        current = openSprite;
        UpdateTexture();

        if (Application.isPlaying && doorAudio != null)
        {
            doorAudio.Play();
        }
    }

    public void Close()
    {
        current = closedSprite;
        UpdateTexture();
    }

    void UpdateMesh()
    {
        if (current == null)
        {
            current = closedSprite;
        }

        if (current == null)
        {
            return;
        }

        MeshFilter meshFilter = GetComponent<MeshFilter>();
        if (meshFilter == null)
        {
            return;
        }

        if (mesh == null)
        {
            mesh = new Mesh();
            mesh.name = "Door";
            meshFilter.sharedMesh = mesh;
        }

        float w = current.rect.width / current.pixelsPerUnit;
        float h = current.rect.height / current.pixelsPerUnit;
        float x = w / 2f;
        float y = h / 2f;

        Vector3[] vertices = new Vector3[4];
        vertices[0] = new Vector3(-x + bottomLeft.x, -y + bottomLeft.y, 0f);
        vertices[1] = new Vector3(x + bottomRight.x, -y + bottomRight.y, 0f);
        vertices[2] = new Vector3(x + topRight.x, y + topRight.y, 0f);
        vertices[3] = new Vector3(-x + topLeft.x, y + topLeft.y, 0f);
        mesh.vertices = vertices;

        mesh.uv = new Vector2[4] { new Vector2(0f, 0f), new Vector2(1f, 0f), new Vector2(1f, 1f), new Vector2(0f, 1f) };
        mesh.triangles = new int[6] { 0, 2, 1, 0, 3, 2 };
        mesh.normals = new Vector3[4] { Vector3.back, Vector3.back, Vector3.back, Vector3.back };
        mesh.tangents = new Vector4[4] { new Vector4(1f, 0f, 0f, 1f), new Vector4(1f, 0f, 0f, 1f), new Vector4(1f, 0f, 0f, 1f), new Vector4(1f, 0f, 0f, 1f) };
        mesh.RecalculateBounds();
    }

    void UpdateTexture()
    {
        if (current == null)
        {
            return;
        }

        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer == null)
        {
            return;
        }

        if (Application.isPlaying)
        {
            meshRenderer.material.mainTexture = current.texture;
        }
        else
        {
            meshRenderer.sharedMaterial.mainTexture = current.texture;
        }
    }
}
