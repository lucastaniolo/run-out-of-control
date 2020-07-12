using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputPanelManager : MonoBehaviour
{
    [SerializeField] private InputButton inputButtonPrefab;
    [SerializeField] private RectTransform container;
    [SerializeField] private int maxButtons = 9;

    private readonly List<InputButton> buttons = new List<InputButton>();

    private void Awake()
    {
        InputButton.InputUsed += RemoveButton;
        InputPickUp.InputPickUpEvent += PickUp;
    }

    private void OnDestroy()
    {
        InputButton.InputUsed -= RemoveButton;
        InputPickUp.InputPickUpEvent -= PickUp;
    }

    // CHEATS
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Q))
        //     AddButton(InputType.Jump);
        // if (Input.GetKeyDown(KeyCode.W))
        //     AddButton(InputType.Grow);
        // if (Input.GetKeyDown(KeyCode.E))
        //     AddButton(InputType.Shrink);
        // if (Input.GetKeyDown(KeyCode.R))
        //     AddButton(InputType.Shoot);

        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void AddButton(InputType inputType)
    {
        var button = Instantiate(inputButtonPrefab, container);
        button.SetType(inputType);
        buttons.Add(button);

        if (buttons.Count <= maxButtons) return;

        RemoveButton(buttons[0]);
    }
    
    private void RemoveButton(InputButton inputButton)
    {
        buttons.Remove(inputButton);
        Destroy(inputButton.gameObject);
    }
    
    private void PickUp(InputPickUp inputPickUp)
    {
        AddButton(inputPickUp.InputType);
    }
}
