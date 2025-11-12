using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Goal : MonoBehaviour
{
    public TextMeshProUGUI pointsDisplay;

    int childCountStart;
    int newChildCount;

    public int points
    {
        get
        {
            return childCountStart - newChildCount;
        }
    }
    void Start()
    {
        childCountStart = transform.childCount;
    }
    void Update()
    {
        newChildCount = transform.childCount;
        CountActiveChildren();
        pointsDisplay.text = points + " / " + childCountStart;

       if (points >= childCountStart)
        {
            LoadNextScene();
        }
    }
    void CountActiveChildren()
    {
        int count = 0;
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeInHierarchy)
                count++;
        }

        newChildCount = count;
    }

    void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
