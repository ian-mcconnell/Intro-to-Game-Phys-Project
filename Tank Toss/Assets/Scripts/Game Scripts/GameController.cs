using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int ammoInit = 4;
    public int ammoCurrent;
    public bool ammoWarningActive = false;
    public Text AmmoWarningText;

    public Text ParText;

    public bool levelComplete = false;

    // Start is called before the first frame update
    void Start()
    {
        ammoCurrent = ammoInit;
        ParText.text = "Ammo: " + ammoCurrent.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        InputHandler();
        CheckVictory();
        AmmoTracker();

        ParText.text = "Ammo: " + ammoCurrent.ToString();
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
            ammoWarningActive = true;
            Invoke("DisplayAmmoWarning", 2f);
        }
    }

    void DisplayAmmoWarning()
    {
        AmmoWarningText.text = "Press 'r' to restart";
    }
}
