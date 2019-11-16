using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionController : MonoBehaviour
{
    private Scene ThisScene;
    private float animClipLength;

    // Start is called before the first frame update
    void Start()
    {
        animClipLength = GameObject.Find("Title").GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.length;
        ThisScene = SceneManager.GetActiveScene();
        Invoke("LoadNextLevel", animClipLength);
    }

    private void LoadNextLevel()
    {
        if (ThisScene.buildIndex != SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(ThisScene.buildIndex + 1);
        }
        else { SceneManager.LoadScene("MainMenu"); }
    }
}
