using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMouseOver : MonoBehaviour
{
    public int index;

    private PauseManager pauseManager = null;
    private bool lastSelectedState = false;
    // Start is called before the first frame update
    public void Start()
    {
        pauseManager = FindObjectOfType<PauseManager>();
    }

    public void Update() {
        Vector3 cursorPos = Input.mousePosition;
        Vector3 objPos = gameObject.transform.position;

        Vector3 targetDir = cursorPos - objPos;
        RectTransform rt = gameObject.GetComponent<RectTransform>();
        Vector2 size = rt.sizeDelta;

        bool isSelected = (Mathf.Abs(targetDir.y) < size.y/2);
        if (isSelected) {
            if (!lastSelectedState)
            {
                pauseManager.SetSelection(index);
            }
            
            bool leftClick = Input.GetMouseButtonDown(0);
            if (leftClick)
            {
                pauseManager.ExecuteSelection();
            }
        }

        lastSelectedState = isSelected;
    }
}
