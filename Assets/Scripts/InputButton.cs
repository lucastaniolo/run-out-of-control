using System;
using UnityEngine;
using UnityEngine.UI;

public enum InputType
{
    Jump,
    Grow,
    Shrink,
    Shoot
}

public class InputButton : MonoBehaviour
{
    public InputType InputType { get; private set; }

    public static event Action<InputButton> InputUsed;

    [SerializeField] private Button button;
    [SerializeField] private Image background;
    
    private void Awake()
    {
        button.onClick.AddListener(OnClick);
    }

    public void SetType(InputType inputType)
    {
        InputType = inputType;
        UpdateView();
    }

    private void OnClick()
    {
        InputUsed?.Invoke(this);
        Destroy(gameObject);
    }

    private void UpdateView()
    {
        background.sprite = SpriteManager.Instance.GetSprite(InputType);
    }
}
