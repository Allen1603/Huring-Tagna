using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DetectionDialogue : MonoBehaviour
{
    public GameObject dialogueuPanel;
    private bool isPlayerInRange = false;

    private PlayerInputAction uiInputActions;
    private bool inputTrue = false;
    void Start()
    {
        dialogueuPanel.SetActive(false);
    }
    private void Awake()
    {
        uiInputActions = new PlayerInputAction();
        uiInputActions.UI.NPC.performed += ctx => inputTrue = true;
    }
    private void OnEnable() => uiInputActions.Enable();
    private void OnDisable() => uiInputActions.Disable();
    void Update()
    {
        if (inputTrue)
        {
            dialogueuPanel.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            dialogueuPanel.SetActive(false); // Optional: auto-close when leaving
        }
    }
}
