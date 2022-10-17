using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuHUD : MonoBehaviour
{
    VisualElement menu;
    ListView listView;
    List<string> menuList;

    // Start is called before the first frame update
    void Start()
    {
        menu = GetComponent<UIDocument>().rootVisualElement;
        listView = menu.Q<ListView>("ListView");
        menuList = new List<string>();
        menuList.Add("PLAY");
        menuList.Add("EXIT");
        RenderListView();
    }

    void RenderListView()
    {
        listView.itemsSource = menuList;
        listView.makeItem = OnMakeItem;
        listView.bindItem = OnBindItem;
        listView.fixedItemHeight = 25;
        listView.onSelectionChange += OnSelectionChange;
    }

    VisualElement OnMakeItem()
    {
        VisualElement visualElement = new VisualElement();
        Label label = new Label();
        IStyle lStyle = label.style;
        lStyle.paddingTop = -50.0f;
        lStyle.color = Color.black;
        lStyle.fontSize = 15;
        visualElement.Add(label);
        IStyle vStyle = visualElement.style;
        vStyle.backgroundColor = Color.white;
        visualElement.RegisterCallback<MouseEnterEvent>( 
            e => 
            {
                lStyle.color = Color.green;
                lStyle.fontSize = 20;
            }       
        );
        visualElement.RegisterCallback<MouseLeaveEvent>(
            e =>
            {
                lStyle.color = Color.black;
                lStyle.fontSize = 15;
            }
        );
        return visualElement;
    }

    void OnBindItem(VisualElement v, int i)
    {
        Label label = v.Query<Label>().AtIndex(0);
        label.text = menuList[i];
    }

    void OnSelectionChange(IEnumerable<object> items)
    {
        foreach (object item in items)
        {
            string s = item as string;
            if (s != null)
                Debug.Log("Clicked item is :: " + s);
            if (s.Equals("EXIT"))
                Application.Quit();
        }
    }
}
