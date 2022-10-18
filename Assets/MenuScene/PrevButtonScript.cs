using UnityEngine;

public class PrevButtonScript : MonoBehaviour
{
    // Start is called before the first frame update

    MeshRenderer mesh;
    Material material;

    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        material = mesh.materials[0];
        material.SetColor("_Color", Color.green);
    }
}
