using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelClearedText : MonoBehaviour
{
    void Start()
    {
        var level = SceneManager.GetActiveScene().buildIndex + 1;
        GetComponent<TextMeshProUGUI>().SetText($"Level {level} Cleared!");
    }
}
