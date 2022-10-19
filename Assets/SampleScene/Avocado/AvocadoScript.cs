using UnityEngine;

public class AvocadoScript : MonoBehaviour
{
    public LayerMask layerMask;
    public bool isBomb;

    readonly string actorName = "Actor";
    readonly string heroName = "Hero";

    GameModeScript gameMode;
    Material material;
    Vector3 boxPosition;

    // Start is called before the first frame update
    void Start()
    {
        gameMode = GetComponentInParent<GameModeScript>();
        MeshRenderer mesh = GetComponentInChildren<MeshRenderer>();
        material = mesh.materials[0];
        boxPosition = transform.position;
        boxPosition.y += 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameMode != null && !gameMode.isPaused)
        {
            Transform transform = GetComponent<Transform>();
            transform.Rotate(0, 1, 0);
        }
    }

    private void FixedUpdate()
    {
        if (isBomb)
            HandleOverlap();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }

    private void HandleOverlap()
    {
        Collider[] colliders = Physics.OverlapBox(boxPosition, transform.localScale, Quaternion.identity, layerMask);
        if (colliders.Length == 0)
            material.SetColor("_Color", Color.white);
        foreach (Collider collider in colliders)
        {
            material.SetColor("_Color", (actorName.Equals(collider.name) || heroName.Equals(collider.name)) ? Color.black : Color.white);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxPosition, transform.localScale);
    }
}
