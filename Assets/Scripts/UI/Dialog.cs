using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public float fadeDuration;
    public string[] dialogLines;
    public float textSpeed;
    
    private int linesIndex;

    public CanvasGroup attackMenuCanvasGroup;

    public GameObject AttackButton;
    public GameObject SpecialButton;
    public GameObject Dialogue;

    public Image bossHeadImage;
    public Image heroHeadImage;

    private string[] lines;
    // contains true if current line index is hero talking, false if boss talking
    private bool[] activeSpeakerArray;

    void initLines() {
        activeSpeakerArray = new bool[dialogLines.Length];
        lines = new string[dialogLines.Length];
        // extract lines
        for (int i = 0; i < dialogLines.Length; i++) {
            // extract H or B from [H]/[B]
            activeSpeakerArray[i] = dialogLines[i][1].Equals('H');
            // strip [H] or [B] from the input lines
            lines[i] = dialogLines[i].Substring(3);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        AttackButton.gameObject.SetActive(false);
        SpecialButton.gameObject.SetActive(false);
        Dialogue.gameObject.SetActive(false);
        textComponent.text = string.Empty;
        initLines();
        StartDialog();
    }

    void setHeadActive(bool heroActive) {
        // show hero head
        if (heroActive) {
            bossHeadImage.gameObject.SetActive(false);
            heroHeadImage.gameObject.SetActive(true);
        }
        // show boss head
        else {
            heroHeadImage.gameObject.SetActive(false);
            bossHeadImage.gameObject.SetActive(true);
        }
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
        // activeSpeakerArray contains info on who is speaking this line
        setHeadActive(activeSpeakerArray[linesIndex]);
        // type character 1 by 1
        string currentLine = lines[linesIndex];
        foreach (char c in currentLine.ToCharArray()) {
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
