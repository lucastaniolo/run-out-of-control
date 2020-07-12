using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextLevelButton : MonoBehaviour
{
    void Start()
    {
        var buildIndex = SceneManager.GetActiveScene().buildIndex;
        
        if (buildIndex == 5)
        {
            gameObject.SetActive(false);
            return;
        }
        
        GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene(buildIndex + 1));
    } 
}
