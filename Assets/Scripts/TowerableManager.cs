using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerableManager : MonoBehaviour
{
    private static TowerableManager instance;

    public static TowerableManager GetInstance() {
        return instance;
    }

    [System.Serializable]
    public struct TowerableStruct
    {
        public GameObject prefab;
        public GameObject ghost;
        public int howManyItemsToUnlock;
    }

    public TowerableStruct[] towerables;
    public int howManyItemsToUnlockSound;
    public AudioClip[] clickSounds;

    private List<TowerableStruct> unlockedTowerables;
    private int currentPrefabIndex;
    private AudioSource audioSource;
    private AudioClip initSound;
    private float initPitch;
    private float initVolume;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    void Start() {

        audioSource = GetComponent<AudioSource>();
        initSound = audioSource.clip;
        initPitch = audioSource.pitch;
        initVolume = audioSource.volume;
        Init();
    }

    public void Init() {

        GenerateNewPrefab();
    }
    private List<TowerableStruct> GetUnlockedPrefabs() {

        List<TowerableStruct> unlockedList = new List<TowerableStruct>();

        foreach (TowerableStruct t in towerables) {
            if (t.howManyItemsToUnlock <= CollectableManager.GetInstance().GetCollectedItemCount()) {
                unlockedList.Add(t);
            }
        }
        return unlockedList;
    }

    public void GenerateNewPrefab() {
        unlockedTowerables = GetUnlockedPrefabs();
        currentPrefabIndex = Random.Range(0, unlockedTowerables.Count);
    }

    public GameObject GetCurrentPrefab() {

        return unlockedTowerables[currentPrefabIndex].prefab;
    }

    public GameObject GetCurrentGhost() {

        return unlockedTowerables[currentPrefabIndex].ghost;
    }

    public void PlaySound() {

        if (howManyItemsToUnlockSound > CollectableManager.GetInstance().GetCollectedItemCount()) {
            audioSource.clip = initSound;
            audioSource.pitch = initPitch + (0.2f * Random.Range(-1f, 1f));
            audioSource.volume = initVolume + (0.1f * Random.Range(-1f, 1f));
            audioSource.Play();
        } else {
            audioSource.clip = clickSounds[Random.Range(0, clickSounds.Length)];
            audioSource.pitch = initPitch;
            audioSource.volume = initVolume;
            audioSource.Play();
        }
    }
}
