using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICollectedItems : MonoBehaviour
{
    public Text[] texts;
    public GameObject tuyere;
    private int currentDeathCount;
    private void Start() {
        currentDeathCount = levelManager.GetInstance().GetDeathCount();
        GetComponent<Canvas>().worldCamera = Camera.main;
    }

    void Update() {

        transform.RotateAround(tuyere.transform.position, Vector3.up, 10f * Time.deltaTime);
        if (currentDeathCount == levelManager.GetInstance().GetDeathCount())
            foreach (Text text in texts) {
                text.text = "x " + CollectableManager.GetInstance().GetCollectedItemCount();
            }
    }
}
