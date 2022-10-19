using UnityEngine;

public class ButtonClicks : MonoBehaviour
{
    MeshLoader loader;
    public void OnPrevClick()
    {
        loader.OnPrevClick();
    }

    public void OnNextClick()
    {
        loader.OnNextClick();
    }

    private void Start()
    {
        loader = MeshLoader.Get();
    }
}
