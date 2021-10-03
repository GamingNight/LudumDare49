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
        Debug.Log("targetDir " + targetDir);
        RectTransform rt = gameObject.GetComponent<RectTransform>();
        Vector2 size = rt.sizeDelta;
        Debug.Log("size " + size);

        bool isSelected = ((Mathf.Abs(targetDir.x) < size.x) && (Mathf.Abs(targetDir.y) < size.y/2));
        if (!lastSelectedState & isSelected) {
            pauseManager.FocusCursorOnvalue(index);
        }
        lastSelectedState = isSelected;
    }
}
