using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float ExplosionForce = 3000f;
    public float ExplosionRadius = 5f;
    public float ExplosionDuration = .4f;

    public AudioSource ExplosionAudio;
    //public ParticleSystem ExplosionParticles;

    void OnTriggerRemain(Collider other)
    {
        if (other.gameObject.tag == "Player") { Debug.Log("Explosion detected player"); }
        Rigidbody targetRigidbody = other.GetComponent<Rigidbody>();

        targetRigidbody.AddExplosionForce(ExplosionForce, transform.position, ExplosionRadius);
    }

    void Start()
    {
        ExplosionAudio.Play();
        Destroy(gameObject, ExplosionDuration);
        StartCoroutine(DisableCollider());
    }

    IEnumerator  DisableCollider()
    {
        yield return 0;

        GetComponent<Collider>().enabled = false;
    }
}
