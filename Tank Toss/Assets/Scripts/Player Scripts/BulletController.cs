using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public Transform explosionPrefab;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Bouncer")
        {
            ContactPoint contact = collision.contacts[0];
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 pos = contact.point;
            Instantiate(explosionPrefab, pos + new Vector3 (0f,-.01f,0f), rot);
            Destroy(gameObject);
        }
    }
}
