using System.Collections;
using UnityEngine;

public class ActorMeshScript : MonoBehaviour
{
    Animation walkanim;
    SkinnedMeshRenderer skinnedMeshRenderer;
    bool enableAnimControl = false;

    public void OnDead()
    {
        Transform transform = GetComponent<Transform>();
        transform.Rotate(0, 0, 90);
    }
    public void PlayWalkAnim(bool play)
    {
        if (walkanim != null && enableAnimControl)
        {
            if (play)
                walkanim.Play();
            else if (walkanim.isPlaying)
                walkanim.Stop();
        }   
    }

    public void ShowDamage()
    {
        StartCoroutine(ChangeColor());
    }

    // Start is called before the first frame update
    void Start()
    {
        walkanim = GetComponent<Animation>();
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        StartCoroutine(DelayAnimControl(0.1f));
    }

    private IEnumerator ChangeColor()
    {
        if (skinnedMeshRenderer != null)
        {
            Material material = skinnedMeshRenderer.materials[0];
            material.SetColor("_Color", Color.red);
            yield return new WaitForSeconds(0.25f);
            material.SetColor("_Color", Color.white);
        }
    }

    private IEnumerator DelayAnimControl(float delay)
    {
        yield return new WaitForSeconds(delay);
        enableAnimControl = true;
    }
}
