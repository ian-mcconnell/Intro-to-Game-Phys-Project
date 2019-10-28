using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
    public GameController gameController;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GetComponent<Collider>().enabled = false;
            audioSource.Play();

            StartCoroutine(VictoryPause());
        }
    }

    IEnumerator VictoryPause()
    {
        yield return 96;

        gameController.levelComplete = true;
    }
}
