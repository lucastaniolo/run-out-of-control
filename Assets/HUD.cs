using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] private GameObject winHud;
    [SerializeField] private GameObject dieHud;
    
    void Start()
    {
        Player.Win += OnWin;
        Player.Died += OnDie;
    }
    
    private void OnDestroy()
    {
        Player.Win -= OnWin;
        Player.Died -= OnDie;
    }

    private void OnDie()
    {
        dieHud.SetActive(true);
    }

    private void OnWin()
    {
        winHud.SetActive(true);
    }
}
