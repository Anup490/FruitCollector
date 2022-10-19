using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class GameModeScript : MonoBehaviour
{
    [SerializeField] GameObject prefabAvocado;

    public bool isPaused;
    HUD hud;

    public void UpdateScore(int score)
    {
        hud.UpdateScore(score);
    }

    public void UpdateTimer(int timer)
    {
        hud.UpdateTimer(timer);
    }

    public void UpdateHealth(int health)
    {
        hud.UpdateHealth(health);
    }

    public void StopGame(string message)
    {
        hud.ShowGameOver(message);
        UnityEngine.Cursor.visible = true;
        isPaused = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Cursor.visible = false;
        SpawnPlayer();
        SpawnAvocados(20);
        hud = new HUD(GetComponent<UIDocument>().rootVisualElement);
    }

    private void SpawnPlayer()
    {
        GameObject prefabPlayer = PrefabUtility.LoadPrefabContents(SceneInterface.Get().characterPath);
        Transform transform = GetComponent<Transform>();
        Vector3 position = transform.position + SceneInterface.Get().displacement;
        Instantiate(prefabPlayer, position, transform.rotation, transform);
    }

    private void SpawnAvocados(int count)
    {
        Transform transform = GetComponent<Transform>();
        Vector3 position = transform.position + new Vector3(0.0f, -6.0f, 0.0f);
        float originalX = position.x;
        for(int i=0; i<count; i++)
        {
            AvocadoScript script = prefabAvocado.GetComponentInChildren<AvocadoScript>();
            if (script != null)
                script.isBomb = MakeBomb();
            if (i % 5 == 0 && i > 0)
            {
                position.z += 5.0f;
                position.x = originalX;
            }
            Instantiate(prefabAvocado, position, transform.rotation, transform);
            position.x += 5.0f;
        }
    }

    private bool MakeBomb()
    {
        return Random.Range(0,2) < 1;
    }
}
