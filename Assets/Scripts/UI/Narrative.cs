using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Narrative : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public float fadeDuration;
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

    private string[][] dialogs = new [] {
        new[] {"[H]We don't have to fight, Harold.", "[B]Silence, William!", "[B]Diplomacy was never my strong suit."}, 
        new[] {"[H]Harold, have you no fear for your death?", "[B]Death by the hands of a Norman?", "[B]That I have no fear."},
        new[] {"[H]Peace is always an option, Harold!", "[B]William, where is your honor?", "[B]We are warriors! Fight to the death!"}
    };

    // convert input (contains formatting to identify speaker) to actual lines
    void initLines(string[] dialogLines) {
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

    void showDialogElements() {
        // hide the bottom menu
        AttackButton.gameObject.SetActive(false);
        SpecialButton.gameObject.SetActive(false);
        Dialogue.gameObject.SetActive(false);
        gameObject.SetActive(true);
    }

    // pick random dialog from available set to show
    public void startRandomDialog() {
        int randomIndex = Random.Range(0, dialogs.Length);
        Debug.Log(randomIndex);
        showDialogElements();
        initLines(dialogs[randomIndex]);
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
