using UnityEditor;
using UnityEngine;

public class MeshLoader
{
    public PrefabScriptableObject[] prefabData;
    public string selectedPrefabPath = "Assets/SampleScene/Actor/ActorPrefab.prefab";
    public Vector3 selectedPrefabDisplacement = new Vector3(30.0f, -1.0f, 40.0f);

    static MeshLoader loader;

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
        prefabData = new PrefabScriptableObject[2];
        prefabData[0] = AssetDatabase.LoadAssetAtPath("Assets/MenuScene/ActorPrefabData.asset", typeof(PrefabScriptableObject)) as PrefabScriptableObject;
        prefabData[1] = AssetDatabase.LoadAssetAtPath("Assets/MenuScene/HeroPrefabData.asset", typeof(PrefabScriptableObject)) as PrefabScriptableObject;

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
