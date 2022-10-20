using UnityEngine;

public class SceneInterface
{
    private static SceneInterface sceneInterface;

    public string characterPath{ set; get; }
    public Vector3 displacement{ set; get; }

    public static SceneInterface Get()
    {
        if (sceneInterface == null)
            sceneInterface = new SceneInterface();
        return sceneInterface;
    }

    private SceneInterface() 
    {
        characterPath = "Assets/SampleScene/Hero/HeroPrefab.prefab";
        displacement = Vector3.zero;
    }
}
