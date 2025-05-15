using UnityEngine;
using UnityEngine.InputSystem;

public class DetectionDialogue : MonoBehaviour
{
    public GameObject dialoguePanel;
    public GameObject pressButtonPanel;
    private bool isPlayerInRange = false;

    private PlayerInputAction uiInputActions;
    private bool inputTriggered = false;

    private void Awake()
    {
        uiInputActions = new PlayerInputAction();
        uiInputActions.UI.TalkNPC.performed += ctx => inputTriggered = true;
    }

    private void OnEnable() => uiInputActions.Enable();
    private void OnDisable() => uiInputActions.Disable();

    private void Start()
    {
        dialoguePanel.SetActive(false);
    }

    private void Update()
    {
        if (inputTriggered && isPlayerInRange)
        {
            dialoguePanel.SetActive(true);
            inputTriggered = false;
            pressButtonPanel.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            pressButtonPanel.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            dialoguePanel.SetActive(false);
            pressButtonPanel.SetActive(false);
        }
    }
}
