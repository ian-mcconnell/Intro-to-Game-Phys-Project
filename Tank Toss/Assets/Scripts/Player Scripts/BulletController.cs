using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public Transform explosionPrefab;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Bouncer" && other.gameObject.tag != "Explosion")
        {
            Vector3 pos = gameObject.transform.position;
            Instantiate(explosionPrefab, pos, new Quaternion(0f,0f,0f,0f));
            Destroy(transform.parent.gameObject);
            Destroy(gameObject);
        }
    }
}
