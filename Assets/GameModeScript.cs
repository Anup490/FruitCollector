using UnityEngine;

public class GameModeScript : MonoBehaviour
{
    [SerializeField]
    GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
        SpawnAvocados(20);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnAvocados(int count)
    {
        Transform t = GetComponent<Transform>();
        Vector3 ogpos = t.position;
        for(int i=1; i<=count; i++)
        {
            AvocadoScript script = prefab.GetComponentInChildren<AvocadoScript>();
            if (script != null)
                script.isBomb = MakeBomb();
            Instantiate(prefab, t.position, t.rotation);
            if (i % 5 == 0)
               t.SetPositionAndRotation(new Vector3(ogpos.x, t.position.y, t.position.z + 5), t.rotation);
            else
               t.SetPositionAndRotation(new Vector3(t.position.x + 5, t.position.y, t.position.z), t.rotation);
        }
    }

    private bool MakeBomb()
    {
        return Random.Range(0,2) < 1;
    }
}
