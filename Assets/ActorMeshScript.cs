using UnityEngine;

public class ActorMeshScript : MonoBehaviour
{
    ActorScript actor;
    bool notRotated = true;

    // Start is called before the first frame update
    void Start()
    {
        actor = GetComponentInParent<ActorScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(actor != null && actor.isDead && notRotated)
        {
            Transform transform = GetComponent<Transform>();
            transform.Rotate(0, 0, 90);
            notRotated = false;
        }
    }
}
