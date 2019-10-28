using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int ammoInit = 4;
    public int ammoCurrent;
    public bool ammoWarningActive = false;

    public bool levelComplete = false;

    // Start is called before the first frame update
    void Start()
    {
        ammoCurrent = ammoInit;
    }

    // Update is called once per frame
    void Update()
    {
        InputHandler();
        CheckVictory();
        AmmoTracker();
    }

    void InputHandler()
    {
        if (Input.GetButton("Restart"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void CheckVictory()
    {
        if (levelComplete)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    void AmmoTracker()
    {
        if (ammoCurrent == 0 && !ammoWarningActive)
        {
            StartCoroutine(DisplayAmmoWarning());
        }
    }

    IEnumerator DisplayAmmoWarning()
    {
        yield return 120;


    }
}
