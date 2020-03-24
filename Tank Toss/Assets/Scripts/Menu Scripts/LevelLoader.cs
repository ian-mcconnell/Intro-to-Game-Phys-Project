using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public bool isComplete;
    public Animator transition;
    public float time;
    public string nextLevel;
    public Text text;
    void Update()
    {
        if (isComplete)
        {
            LoadNextLevel();
        }
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(time);

        SceneManager.LoadScene(nextLevel);
    }
}
