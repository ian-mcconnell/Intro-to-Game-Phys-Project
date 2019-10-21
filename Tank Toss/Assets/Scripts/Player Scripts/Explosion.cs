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

    void OnTriggerEnter(Collider other)
    {
        Rigidbody targetRigidbody = other.GetComponent<Rigidbody>();

        targetRigidbody.AddExplosionForce(ExplosionForce, transform.position, ExplosionRadius);

        GetComponent<Collider>().enabled = false;

        //ExplosionParticles.transform.parent = null;
        //ExplosionParticles.Play();

        ExplosionAudio.Play();

        //ParticleSystem.MainModule mainModule = ExplosionParticles.main;
        //Destroy(ExplosionParticles.gameObject, ExplosionParticles.main.duration);
        Destroy(gameObject, ExplosionDuration);
    }
}
