using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionDialogue : MonoBehaviour
{
    public GameObject dialogueuPanel;
    private bool isPlayerInRange = false;

    void Start()
    {
        dialogueuPanel.SetActive(false);
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F))
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
