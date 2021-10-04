using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    private static MusicManager instance;
    public static MusicManager GetInstance() {
        return instance;
    }

    private int level;
    private bool levelUp;
    private Animator animator;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    void Start() {
        animator = GetComponent<Animator>();
        Init();
    }

    public void Init() {
        levelUp = false;
        level = 0;
        animator.SetTrigger("Down");
    }

    void Update() {
        int itemCount = CollectableManager.GetInstance().GetCollectedItemCount();

        if (itemCount > 4 && level == 0) {
            levelUp = true;
        } else if (itemCount > 6 && level == 1) {
            levelUp = true;
        } else if (itemCount > 7 && level == 2) {
            levelUp = true;
        } else if (itemCount > 8 && level == 3) {
            levelUp = true;
        } else if (itemCount > 10 && level == 4) {
            levelUp = true;
        } else if (itemCount > 12 && level == 5) {
            levelUp = true;
        } else if (itemCount > 15 && level == 6) {
            levelUp = true;
        }

        if (levelUp) {
            animator.SetTrigger("Up");
            levelUp = false;
            level++;
        }
    }
}
