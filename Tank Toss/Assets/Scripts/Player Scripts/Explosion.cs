using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float ExplosionForce = 2000f;
    public float ExplosionRadius = 5f;
    public float ExplosionDuration = .4f;

    public AudioClip ExplosionClip;
    public AudioClip GlassShatteringClip;

    public AudioSource ExplosionAudio;
    //public ParticleSystem ExplosionParticles;

    void OnTriggerEnter(Collider other)
    {
        
        Rigidbody targetRigidbody = other.GetComponent<Rigidbody>();

        targetRigidbody.AddExplosionForce(ExplosionForce, transform.position, 0.0f, 0.0f);

        if (other.gameObject.tag == "Breakable")
        {
            Destroy(other.gameObject);
            ExplosionAudio.clip = GlassShatteringClip;
            ExplosionAudio.Play();
        }
    }

    void Start()
    {
        ExplosionAudio.clip = ExplosionClip;
        ExplosionAudio.Play();
        Destroy(gameObject, ExplosionDuration);
        StartCoroutine(DisableCollider());
    }

    IEnumerator DisableCollider()
    {
        yield return 1;

        GetComponent<Collider>().enabled = false;
    }
}
