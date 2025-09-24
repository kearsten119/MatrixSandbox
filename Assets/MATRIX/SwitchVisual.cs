using UnityEngine;
using UnityEngine.InputSystem;
//using UnityEngine.XR.Interaction.Toolkit.Inputs.Readers;

public class SwitchVisual : MonoBehaviour
{
    public InputAction thumbstickPressAction; // Assign this in the Inspector
    //[SerializeField] XRInputValueReader<bool> ScaleToggle;

    
    public void Start()
    {
       
    }

    void OnEnable()
    {
        thumbstickPressAction.Enable();
        thumbstickPressAction.started += OnThumbstickPressStarted;
        thumbstickPressAction.canceled += OnThumbstickPressCanceled;
    }

    void OnDisable()
    {
        thumbstickPressAction.started -= OnThumbstickPressStarted;
        thumbstickPressAction.canceled -= OnThumbstickPressCanceled;
        thumbstickPressAction.Disable();
    }

    private void OnThumbstickPressStarted(InputAction.CallbackContext context)
    {
        Debug.Log("Thumbstick Pressed!");
    }

    private void OnThumbstickPressCanceled(InputAction.CallbackContext context)
    {
        Debug.Log("Thumbstick Released!");
    }
}
