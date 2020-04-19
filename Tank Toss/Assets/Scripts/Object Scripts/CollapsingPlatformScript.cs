using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollapsingPlatformScript : MonoBehaviour
{
    public float collapseDelay = 1.0f;
    private bool notTriggered = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CollapseSequence()
    {
        gameObject.GetComponentInParent<Rigidbody>().isKinematic = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && notTriggered)
        {
            notTriggered = false;
            Invoke("CollapseSequence", collapseDelay);
        }
    }
}
