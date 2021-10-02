using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectableObject
: MonoBehaviour
{
    private Renderer rend;
    private Color32 objColor;

    public void Start()
    {
        rend = GetComponent<Renderer>();
    }

    public void OnMouseEnter()
    {
        objColor = rend.material.color;
        rend.material.color -= new Color(0.5F, 0, 0);
    }

    public void OnMouseExit()
    {
        rend.material.color = objColor;
    }

}
