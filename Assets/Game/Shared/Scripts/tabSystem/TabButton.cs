using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private TabGroup group;
    
    public Image background;

    public void OnPointerClick(PointerEventData eventData)
    {
        group.OnTabSelected(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        group.OnTabEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        group.OnTabExit(this);
    }

    private void Awake()
    {
        group = GetComponentInParent<TabGroup>();
        background = GetComponent<Image>();
    }

    private void Start()
    {
        group.Subscribe(this);
    }
}
