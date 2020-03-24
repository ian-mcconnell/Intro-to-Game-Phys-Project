using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    public LevelLoader LL;

    public void ChangeText(string t)
    {
        LL.text.text = t;
    }
    public void Select(string name)
    {
        LL.nextLevel = name;
        LL.isComplete = true;
    }
}
