using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ActorScript : MonoBehaviour
{
    readonly float sensitivity = 20.0f;
    readonly int maxTimeInSecs = 60;

    GameModeScript gameMode;
    ActorMeshScript mesh;
    Image healthBar;

    bool readyInputs = true;
    bool readyJump = true;
    int avocadoCount;
    int healthCount = 100;

    // Start is called before the first frame update
    void Start()
    {
        gameMode = GetComponentInParent<GameModeScript>();
        mesh = GetComponentInChildren<ActorMeshScript>();
        healthBar = GetComponentInChildren<Image>();
        StartCoroutine(UpdateTimer());
    }

    // Update is called once per frame
    void Update()
    {
        if (readyInputs)
        {
            MoveForward(Input.GetAxis("Forward"));
            MoveCameraYaw(Input.GetAxis("Mouse X"));
            Jump(Input.GetAxis("Jump"));
            gameMode.UpdateScore(avocadoCount);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject collidedObject = collision.gameObject;
        AvocadoScript avscript = collidedObject.GetComponent<AvocadoScript>();
        if (avscript != null)
        {
            if (avscript.isBomb)
                DamagePlayer();
            else
                avocadoCount++;
        }
    }

    private void MoveForward(float axis)
    {
        if (axis > 0.0f)
        {
            Transform t = GetComponent<Transform>();
            Vector3 newPosition = t.position + (t.forward * 0.01f);
            t.SetPositionAndRotation(newPosition, t.rotation);
            mesh.PlayWalkAnim(readyJump && true);
        }
        else
            mesh.PlayWalkAnim(false);
    }

    private void MoveCameraYaw(float axis)
    {
        Transform t = GetComponent<Transform>();
        t.Rotate(new Vector3(0.0f, axis * sensitivity, 0.0f));
        t.SetPositionAndRotation(t.position, t.rotation);
    }

    private void Jump(float axis)
    {
        if (axis > 0.0f && readyJump)
        {
            StartCoroutine(DisableJumpFor(1.0f));
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.AddForce(new Vector3(0.0f, axis * 11000.0f, 0.0f));
        }
    }

    private void DamagePlayer()
    {
        PushBack();
        healthCount -= 20;
        UpdateHealth();
        mesh.ShowDamage();
        if (healthCount <= 0)
        {
            mesh.OnDead();
            EndGame("YOU DIED");
        }
    }

    private IEnumerator DisableJumpFor(float delay)
    {
        readyJump = false;
        yield return new WaitForSeconds(delay);
        readyJump = true;
    }

    private IEnumerator UpdateTimer()
    {
        gameMode.UpdateTimer(maxTimeInSecs);
        for (int i=maxTimeInSecs; i>=0; i--)
        {
            yield return new WaitForSeconds(1.0f);
            gameMode.UpdateTimer(i);
        }
        EndGame("TIMER EXPIRED");
    }

    private void PushBack()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        Vector3 forward = rb.transform.forward;
        rb.AddForce(new Vector3(-forward.x * 30000.0f, (forward.y + 1.0f) * 15000.0f, -forward.z * 30000.0f));
    }

    private void UpdateHealth()
    {
        if (healthBar != null)
        {
            RectTransform rectTransform = healthBar.rectTransform;
            float width = (healthCount * 2.0f) / 100.0f;
            rectTransform.sizeDelta = new Vector2(width, 0.2f);
            if (healthCount < 50) healthBar.color = Color.red;
        }
    }

    private void EndGame(string message)
    {
        readyInputs = false;
        gameMode.StopGame(message);
        mesh.PlayWalkAnim(false);
    }
}