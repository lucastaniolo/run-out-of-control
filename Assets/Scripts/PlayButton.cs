using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    [SerializeField] private int levelIndex;
    
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene(levelIndex));
    } 
}
