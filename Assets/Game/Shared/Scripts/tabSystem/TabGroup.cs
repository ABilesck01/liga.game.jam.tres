using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabGroup : MonoBehaviour
{
    [SerializeField] private List<TabButton> buttons = new List<TabButton>();
    [SerializeField] private Sprite tabIdle;
    [SerializeField] private Sprite tabHover;
    [SerializeField] private Sprite tabSelected;
    [Space]
    [SerializeField] private List<GameObject> objectsToSwap = new List<GameObject>();
    public TabButton selectedTab;

    public void Subscribe(TabButton button)
    {
        if(buttons == null) buttons = new List<TabButton>();

        buttons.Add(button);
    }

    public void OnTabEnter(TabButton button)
    {
        ResetTabs();
        if (tabSelected == null || button != tabSelected)
        {
            button.background.sprite = tabHover;
        }
    }

    public void OnTabExit(TabButton button)
    {
        ResetTabs();
    }

    public void OnTabSelected(TabButton button)
    {
        selectedTab = button;
        ResetTabs();
        button.background.sprite = tabSelected;
        int index = button.transform.GetSiblingIndex();
        for (int i = 0; i < objectsToSwap.Count; i++)
        {
            objectsToSwap[i].SetActive(i == index);
        }
    }

    public void ResetTabs()
    {
        foreach (TabButton tab in buttons)
        {
            if(tabSelected != null && tab == tabSelected)
            {
                continue;
            }

            tab.background.sprite = tabIdle;
        }
    }

}
