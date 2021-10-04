using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float radius;
    public float power;
    public float meche;

    private AudioSource audioData;
    private Rigidbody rb;
    public GameObject shape;
    public GameObject explosion;

    private float timer;
    private bool exploded;

    void Start()
    {
        timer = 0;
        audioData = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        exploded = false;
        explosion.SetActive(false);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > meche && !exploded)
        {
            Boom();
            exploded = true;
        }
    }

    void Boom()
    {

        Destroy(gameObject, 2);

        audioData.Play();
        shape.SetActive(false);
        explosion.SetActive(true);
        rb.isKinematic = true;

        //Physic
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(power, explosionPos, radius, 3.0F);
        }

    }
}