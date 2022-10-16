using UnityEngine;

public class GameModeScript : MonoBehaviour
{
    [SerializeField]
    GameObject prefab;

    public bool isPaused;

    public void PauseGame()
    {
        Cursor.visible = true;
        isPaused = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        SpawnAvocados(20);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnAvocados(int count)
    {
        Transform transform = GetComponent<Transform>();
        Vector3 position = transform.position + new Vector3(0.0f, -6.0f, 0.0f);
        float originalX = position.x;
        for(int i=0; i<count; i++)
        {
            AvocadoScript script = prefab.GetComponentInChildren<AvocadoScript>();
            if (script != null)
                script.isBomb = MakeBomb();
            if (i % 5 == 0 && i > 0)
            {
                position.z += 5.0f;
                position.x = originalX;
            }
            Instantiate(prefab, position, transform.rotation, transform);
            position.x += 5.0f;
        }
    }

    private bool MakeBomb()
    {
        return Random.Range(0,2) < 1;
    }
}
