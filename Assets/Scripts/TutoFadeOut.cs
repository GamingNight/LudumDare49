using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutoFadeOut : MonoBehaviour
{
    public Image tuto;
    public float fadeOutDuration = 5;

    private float timer;

    void Start() {
        timer = 0;
    }

    // Update is called once per frame
    void Update() {
        timer += Time.deltaTime;
        float alpha = Mathf.Lerp(1f, 0f, timer / fadeOutDuration);
        tuto.color = new Color(tuto.color.r, tuto.color.g, tuto.color.b, alpha);
        if (timer >= fadeOutDuration) {
            Destroy(gameObject);
        }
    }
}
