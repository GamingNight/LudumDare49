using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableManager : MonoBehaviour
{
    private static CollectableManager instance;

    private AudioSource biteSFX;

    public static CollectableManager GetInstance() {

        return instance;
    }

    public GameObject collectablePrefab;
    public float popupMaxRadius;
    public float heightBetweenItems;
    
    private int nbItemCollected;
    private float initHeight;
    private float heightOffset;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    private void Start() {
        nbItemCollected = 0;
        heightOffset = 1;
        biteSFX = GetComponent<AudioSource>();
        Init();
    }

    public void Init() {
        nbItemCollected = 0;
        initHeight = heightOffset - (levelManager.GetInstance().nextLevelOffset * levelManager.GetInstance().GetDeathCount());
        InstantiateNewCollectable();
    }

    public void CollectItem(GameObject collectable) {
        nbItemCollected++;
        Destroy(collectable);
        InstantiateNewCollectable();
        biteSFX.Play();
    }
    // Idée pour avoir une difficulté qui ne dépende pas du tirage aléatoire de radius. Calculer une variable level de la même façon que la hauteur:
    // Level = initLevel + (levelBetweenItems*nbItemCollected), tirer au hasard radius et calculer la hauteur du spawn avec height=level-radius/K1 avec K1
    // un paramètre ajustable.
    // Comme ça, pour un niveau atteint, vous avez une hauteur de spawn qui dépend linéairement du rayon où ça spawn. Autrement dit: plus c'est loin du
    // centre plus c'est dur donc plus la hauteur est basse pour compenser.
    private void InstantiateNewCollectable() {

        GameObject item = Instantiate<GameObject>(collectablePrefab, transform);
        float radius = Random.Range(0f, popupMaxRadius);
        if(nbItemCollected == 0) {
            radius = 0;
        }
        float angle = Random.Range(0f, 2 * Mathf.PI);
        item.transform.position = new Vector3(radius * Mathf.Cos(angle), initHeight + (heightBetweenItems * nbItemCollected), radius * Mathf.Sin(angle));
    }

    public int GetCollectedItemCount() {

        return nbItemCollected;
    }
}
