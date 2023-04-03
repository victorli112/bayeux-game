using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMenuFader : MonoBehaviour
{
    public bool actionMenuActive = true;
    public float fadeDuration;

    public CanvasGroup attackMenuCanvasGroup;
    

    public void Fade() {
        // fade in attack menu
        if (actionMenuActive) {
            StartCoroutine(DoFade(attackMenuCanvasGroup, attackMenuCanvasGroup.alpha, 1));
        }
        // fade in action menu
        else {
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
}
