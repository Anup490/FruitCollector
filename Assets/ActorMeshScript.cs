using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorMeshScript : MonoBehaviour
{
    ActorInterface actorInterface;
    bool notRotated = true;

    // Start is called before the first frame update
    void Start()
    {
        actorInterface = ActorInterface.Create();
    }

    // Update is called once per frame
    void Update()
    {
        if(actorInterface.dead && notRotated)
        {
            Transform transform = GetComponent<Transform>();
            transform.Rotate(0, 0, 90);
            notRotated = false;
        }
    }
}
