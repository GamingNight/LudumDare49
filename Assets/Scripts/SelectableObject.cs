using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectableObject
: MonoBehaviour
{
    private Renderer rend;
    private Color32 objColor;
    private bool isSelectable;
    private bool isSelected;

    public void Start()
    {
        rend = GetComponent<Renderer>();
        isSelected = false;
        isSelectable = false;
    }

    public void OnMouseEnter()
    {
        isSelectable = true;
        objColor = rend.material.color;
        rend.material.color -= new Color(0.5F, 0, 0);
    }

    public void OnMouseExit()
    {
        isSelectable = false;
        rend.material.color = objColor;
    }

}
