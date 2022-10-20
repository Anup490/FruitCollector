using UnityEditor;
using UnityEngine;

class Prefab
{
    public string displayPath { get; }
    public string actualPath { get; }
    public Vector3 displayDisplacement { get; }
    public Vector3 actualDisplacement { get; }

    public Prefab(string displayPath, string actualPath, Vector3 displayDisplacement, Vector3 actualDisplacement)
    {
        this.displayPath = displayPath;
        this.actualPath = actualPath;
        this.displayDisplacement = displayDisplacement;
        this.actualDisplacement = actualDisplacement;
    }
}

public class MeshLoader
{
    public string selectedPrefabPath = "Assets/SampleScene/Actor/ActorPrefab.prefab";
    public Vector3 selectedPrefabDisplacement = new Vector3(30.0f, -1.0f, 40.0f);

    static MeshLoader loader;

    readonly Prefab[] prefabData = new Prefab[] 
    { 
        new Prefab("Assets/MenuScene/ActorPrefab.prefab", "Assets/SampleScene/Actor/ActorPrefab.prefab", new Vector3(9.7f, 42.125f, 28.25f), new Vector3(30.0f, -1.0f, 40.0f)), 
        new Prefab("Assets/MenuScene/HeroPrefab.prefab", "Assets/SampleScene/Hero/HeroPrefab.prefab", new Vector3(0.0f,1.875f, -5.0f), Vector3.zero) 
    };
    GameObject characterObject;
    GameObject[] prefabs;
   
    public static MeshLoader Get()
    {
        if(loader == null)
            loader = new MeshLoader();
        return loader;
    }

    private MeshLoader() 
    {
        prefabs = new GameObject[prefabData.Length];
        characterObject = GameObject.Find("CharacterCam");
        if (characterObject != null)
        {
            for (int i=0; i < prefabData.Length; i++)
            {
                GameObject prefabContent = PrefabUtility.LoadPrefabContents(prefabData[i].displayPath);
                Transform transform = characterObject.transform;
                Vector3 position = transform.position - prefabData[i].displayDisplacement;
                GameObject prefab = Object.Instantiate(prefabContent, position, transform.rotation, transform);
                prefab.SetActive(false);
                prefabs[i] = prefab;
            }
            prefabs[0].SetActive(true);
        }
    }

    public void OnPrevClick()
    {
        prefabs[0].SetActive(true);
        prefabs[1].SetActive(false);
        selectedPrefabPath = prefabData[0].actualPath;
        selectedPrefabDisplacement = prefabData[0].actualDisplacement;
    }

    public void OnNextClick()
    {
        prefabs[0].SetActive(false);
        prefabs[1].SetActive(true);
        selectedPrefabPath = prefabData[1].actualPath;
        selectedPrefabDisplacement = prefabData[1].actualDisplacement;
    }

}
