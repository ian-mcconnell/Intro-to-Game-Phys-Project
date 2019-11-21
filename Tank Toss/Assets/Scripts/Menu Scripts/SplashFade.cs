using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplashFade : MonoBehaviour
{
    public Image splashPic;
    public string loadLevel;

    IEnumerator Start()
    {
        splashPic.canvasRenderer.SetAlpha(0.0f);

        FadeIn();
        yield return new WaitForSeconds(2.5f);
        FadeOut();
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(loadLevel);
    }

    void FadeIn()
    {
        splashPic.CrossFadeAlpha(1.0f, 1.5f, false);
    }

    void FadeOut()
    {
        splashPic.CrossFadeAlpha(0.0f, 2.5f, false);
    }
}


