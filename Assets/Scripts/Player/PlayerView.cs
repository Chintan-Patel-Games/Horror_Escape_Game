using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerView : MonoBehaviour
{
    private Rigidbody playerRigidbody;
    private PlayerController playerController;

    private void Start() => playerRigidbody = GetComponent<Rigidbody>();

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        playerController.Move(playerRigidbody, transform);
        playerController.Jump(playerRigidbody, transform);
        playerController.Interact();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IInteractable>() != null)
        {
            GameService.Instance.GetInstructionView().ShowInstruction(InstructionType.Interact);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        IInteractable interactable;
        if (other.TryGetComponent(out interactable) && playerController.IsInteracted)
        {
            playerController.IsInteracted = false;
            interactable.Interact();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<IInteractable>() != null)
        {
            GameService.Instance.GetInstructionView().HideInstruction();
        }
    }
    public void SetController(PlayerController _playerController) => playerController = _playerController;

}