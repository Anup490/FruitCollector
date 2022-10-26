using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "ScriptableObjects/PrefabScriptableObject", order = 1)]
public class PrefabScriptableObject : ScriptableObject
{
    public string displayPath;
    public string actualPath;
    public Vector3 displayDisplacement;
    public Vector3 actualDisplacement;
}
