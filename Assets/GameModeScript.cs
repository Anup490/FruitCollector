using UnityEngine;

public class GameModeScript : MonoBehaviour
{
    [SerializeField]
    GameObject prefab;

    public bool isPaused;

    public void PauseGame()
    {
        UnityEngine.Cursor.visible = true;
        isPaused = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Cursor.visible = false;
        SpawnAvocados(20);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnAvocados(int count)
    {
        Transform t = GetComponent<Transform>();
        Vector3 position = t.position + new Vector3(0.0f, -6.0f, 0.0f);
        float newx = position.x;
        float newz = position.z;
        for(int i=0; i<count; i++)
        {
            AvocadoScript script = prefab.GetComponentInChildren<AvocadoScript>();
            if (script != null)
                script.isBomb = MakeBomb();
            GameObject avocado = Instantiate(prefab, t.position, t.rotation) as GameObject;
            avocado.transform.parent = t.transform;
            Transform avocadoTransform = avocado.GetComponentInChildren<Transform>();
            if (i % 5 == 0 && i > 0)
            {
                newz += 5.0f;
                newx = position.x;
                avocadoTransform.SetPositionAndRotation(new Vector3(newx, position.y, newz), t.rotation);
                newx += 5.0f;
            }
            else
            {
                avocadoTransform.SetPositionAndRotation(new Vector3(newx, position.y, newz), t.rotation);
                newx += 5.0f;
            }
        }
    }

    private bool MakeBomb()
    {
        return Random.Range(0,2) < 1;
    }
}
