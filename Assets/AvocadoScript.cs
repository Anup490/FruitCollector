using UnityEngine;

public class AvocadoScript : MonoBehaviour
{
    public LayerMask layerMask;
    public bool isBomb;

    readonly string actorName = "Actor";

    bool hasStarted;
    ActorInterface actorInterface;
    Material material;
    Vector3 boxPosition;

    // Start is called before the first frame update
    void Start()
    {
        actorInterface = ActorInterface.Create();
        MeshRenderer mesh = GetComponentInChildren<MeshRenderer>();
        material = mesh.materials[0];
        hasStarted = true;
        boxPosition = transform.position;
        boxPosition.y += 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!actorInterface.pause)
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
            if (actorName.Equals(collider.name))
                material.SetColor("_Color", Color.black);
            else
                material.SetColor("_Color", Color.white);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (hasStarted)
            Gizmos.DrawWireCube(boxPosition, transform.localScale);
    }
}
