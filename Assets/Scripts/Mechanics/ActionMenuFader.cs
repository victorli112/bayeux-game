using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMenuFader : MonoBehaviour
{
    public bool actionMenuActive = true;
    public float fadeDuration;

    public CanvasGroup attackMenuCanvasGroup;
    

    public GameObject Return;
    public GameObject Slash;
    public GameObject Cleave;
    public GameObject AttackButton;
    public GameObject SpecialButton;
    public GameObject Dialogue;

    void Start()
    {
        Return.gameObject.SetActive(false);
        Slash.gameObject.SetActive(false);
        Cleave.gameObject.SetActive(false);
        AttackButton.gameObject.SetActive(true);
        SpecialButton.gameObject.SetActive(true);
        Dialogue.gameObject.SetActive(true);
    }

    public void Fade() {
        // fade in attack menu
        if (actionMenuActive) {
            AttackMenuActive();
            StartCoroutine(DoFade(attackMenuCanvasGroup, attackMenuCanvasGroup.alpha, 1));
        }
        // fade in action menu
        else {
            ActionMenu();
            StartCoroutine(DoFade(attackMenuCanvasGroup, attackMenuCanvasGroup.alpha, 0));
        }
        actionMenuActive = !actionMenuActive;
    }

    public IEnumerator DoFade(CanvasGroup canvGroup, float start, float end) {
        float timeCounter = 0f;

        while (timeCounter < fadeDuration) {
            timeCounter += Time.deltaTime;
            canvGroup.alpha = Mathf.Lerp(start, end, timeCounter / fadeDuration);

            yield return null;
        }
    }

    public void AttackMenuActive() {
            Return.gameObject.SetActive(true);
            Slash.gameObject.SetActive(true);
            Cleave.gameObject.SetActive(true);
            AttackButton.gameObject.SetActive(false);
            SpecialButton.gameObject.SetActive(false);
            Dialogue.gameObject.SetActive(false);
    }

    public void ActionMenu() {
        Return.gameObject.SetActive(false);
        Slash.gameObject.SetActive(false);
        Cleave.gameObject.SetActive(false);
        AttackButton.gameObject.SetActive(true);
        SpecialButton.gameObject.SetActive(true);
        Dialogue.gameObject.SetActive(true);
    }
}
