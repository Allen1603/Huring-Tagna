using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class DialogueNPC : MonoBehaviour
{
    [Header("Dialogue Settings")]
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed = 0.05f;

    private int index = 0;
    private PlayerInputAction uiInputActions;
    private bool inputTrigger = false;
    private Coroutine typingCoroutine;

    private void Awake()
    {
        uiInputActions = new PlayerInputAction();
        uiInputActions.UI.NextDialogue.performed += ctx => inputTrigger = true;
    }

    private void OnEnable() => uiInputActions.Enable();
    private void OnDisable() => uiInputActions.Disable();

    private void Start()
    {
        textComponent.text = string.Empty;
        StartDialogue();
    }

    private void Update()
    {
        if (inputTrigger)
        {
            inputTrigger = false; // Reset input trigger

            // If current text is fully shown, go to next line
            if (textComponent.text == lines[index])
            {
                ShowNextLine();
            }
            else
            {
                // Fast-forward typing effect
                if (typingCoroutine != null)
                {
                    StopCoroutine(typingCoroutine);
                }
                textComponent.text = lines[index];
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        typingCoroutine = StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        textComponent.text = string.Empty;

        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void ShowNextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
            }
            typingCoroutine = StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        gameObject.SetActive(false);
    }
}
