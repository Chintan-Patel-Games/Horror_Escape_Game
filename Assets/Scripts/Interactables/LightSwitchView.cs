using System.Collections.Generic;
using UnityEngine;

public class LightSwitchView : MonoBehaviour, IInteractable
{
    [SerializeField] private List<Light> lightsources = new List<Light>();
    private SwitchState currentState;

    public delegate void LightSwitchDelegate();  // Signture for the delegate
    public static LightSwitchDelegate lightSwitch; // Delegate instance

    private void OnEnable() => lightSwitch += OnLightSwitchToggled; // Subscribe to the delegate

    private void Start() => currentState = SwitchState.Off;

    public void Interact() => lightSwitch.Invoke();

    private void OnLightSwitchToggled()
    {
        toggleLights();
        GameService.Instance.GetInstructionView().HideInstruction();
        GameService.Instance.GetSoundView().PlaySoundEffects(SoundType.SwitchSound);
    }

    private void toggleLights()
    {
        bool lights = false;

        switch (currentState)
        {
            case SwitchState.On:
                currentState = SwitchState.Off;
                lights = false;
                break;
            case SwitchState.Off:
                currentState = SwitchState.On;
                lights = true;
                break;
            case SwitchState.Unresponsive:
                break;
        }

        foreach (Light lightSource in lightsources)
            lightSource.enabled = lights;
    }
}
