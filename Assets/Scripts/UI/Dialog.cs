using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public float fadeDuration;
    public string[] lines;
    public float textSpeed;
    
    private int linesIndex;

    public CanvasGroup attackMenuCanvasGroup;

    public GameObject AttackButton;
    public GameObject SpecialButton;
    public GameObject Dialogue;

    // Start is called before the first frame update
    void Start()
    {
        AttackButton.gameObject.SetActive(false);
        SpecialButton.gameObject.SetActive(false);
        Dialogue.gameObject.SetActive(false);

        textComponent.text = string.Empty;
        
        StartDialog();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            if (textComponent.text == lines[linesIndex]) {
                NextLine();
            }
            else {
                StopAllCoroutines();
                textComponent.text = lines[linesIndex];
            }
        }
    }

    void StartDialog() {
        linesIndex = -1;
        NextLine();
    }

    IEnumerator TypeLine() {
        // type character 1 by 1
        foreach (char c in lines[linesIndex].ToCharArray()) {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    // show the next line in the array
    void NextLine() {
        if (linesIndex < lines.Length - 1) {
            linesIndex++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        // reach last line, any action after -> remove the dialog
        else {
            AttackButton.gameObject.SetActive(true);
            SpecialButton.gameObject.SetActive(true);
            Dialogue.gameObject.SetActive(true);
            gameObject.SetActive(false);
            
        }
    }

}