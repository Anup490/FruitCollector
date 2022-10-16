using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class ActorScript : MonoBehaviour
{
    readonly float sensitivity = 20.0f;
    readonly int maxTimeInSecs = 60;

    public bool isDead;

    GameModeScript gameMode;
    Animation walkanim;
    SkinnedMeshRenderer skinnedMeshRenderer;
    Label scoreUI;
    Label timerUI;
    Label healthUI;
    Label gameoverUI;
    Label messageUI;
    bool readyInputs;
    bool readyJump = true;
    int avocadoCount = 0;
    int healthCount = 100;

    // Start is called before the first frame update
    void Start()
    {
        gameMode = GetComponentInParent<GameModeScript>();
        walkanim = GetComponentInChildren<Animation>();
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        InitUI();
        StartCoroutine(DelayAnimStop(0.1f));
    }

    // Update is called once per frame
    void Update()
    {
        if (readyInputs)
        {
            MoveForward(Input.GetAxis("Forward"));
            MoveCameraX(Input.GetAxis("Mouse X"));
            Jump(Input.GetAxis("Jump"));
            UpdateUI();
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

    private void InitUI()
    {
        VisualElement ui = GetComponent<UIDocument>().rootVisualElement;
        if (ui != null)
        {
            scoreUI = ui.Query<Label>("ScoreValue").First();
            timerUI = ui.Query<Label>("TimerValue").First();
            healthUI = ui.Query<Label>("HealthValue").First();
            gameoverUI = ui.Query<Label>("GameOverLabel").First();
            messageUI = ui.Query<Label>("MessageValue").First();
        }
        if (timerUI != null)
            StartCoroutine(UpdateTimer());
        if (gameoverUI != null)
            gameoverUI.visible = false;
        if (messageUI != null)
            messageUI.visible = false;
    }

    private void MoveForward(float axis)
    {
        if (axis > 0.0f)
        {
            Transform t = GetComponent<Transform>();
            Vector3 newPosition = t.position + (t.forward * 0.01f);
            t.SetPositionAndRotation(newPosition, t.rotation);
            PlayWalkAnim(true);
        }
        else
            PlayWalkAnim(false);
    }

    private void MoveCameraX(float axis)
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

    private void UpdateUI()
    {
        if (scoreUI != null)
            scoreUI.text = avocadoCount.ToString();
    }

    private void PlayWalkAnim(bool play)
    {
        if(walkanim != null)
        {
            if (play && readyJump)
                walkanim.Play();
            else
                walkanim.Stop();
        }
    }

    private void DamagePlayer()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        Vector3 forward = rb.transform.forward;
        rb.AddForce(new Vector3(-forward.x * 30000.0f, (forward.y+1.0f) * 15000.0f, -forward.z * 30000.0f));
        if (healthUI != null)
        {
            healthCount -= 20;
            healthUI.text = healthCount.ToString();
        }
        StartCoroutine(ChangeColor());
        if (healthCount <= 0)
        {
            isDead = true;
            if (messageUI != null)
            {
                messageUI.visible = true;
                messageUI.text = "YOU DIED";
            }
            EndGame();
        }
    }

    private IEnumerator DelayAnimStop(float delay)
    {
        yield return new WaitForSeconds(delay);
        readyInputs = true;
    }

    private IEnumerator DisableJumpFor(float delay)
    {
        readyJump = false;
        yield return new WaitForSeconds(delay);
        readyJump = true;
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

    private IEnumerator UpdateTimer()
    {
        for (int i=maxTimeInSecs; i>0; i--)
        {
            timerUI.text = i.ToString();
            yield return new WaitForSeconds(1.0f);
        }
        if (messageUI != null)
        {
            timerUI.text = "0";
            messageUI.visible = true;
            messageUI.text = "TIMER EXPIRED";
        }
        EndGame();
    }

    private void EndGame()
    {
        if (gameMode != null)
            gameMode.PauseGame();
        if (gameoverUI != null)
            gameoverUI.visible = true;
        if (walkanim.isPlaying) 
            walkanim.Stop();
        readyInputs = false;
    }
}
