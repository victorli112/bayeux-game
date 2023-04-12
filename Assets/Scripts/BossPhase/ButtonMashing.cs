using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonMashing : MonoBehaviour {

      private int mashAmount;

      private int damageDealt;

      public AudioSource arrow;

      // keep track of the previous key, player must alternate between A and D
      private string prevKey = "";

      // how long the user amash for
      public float mashDuration = 7.5f;

      // the final result text with the damage dealth
      public TMPro.TextMeshProUGUI resultText;

      public BossModel boss;

      public Animator aKey;
       
      public Animator dKey;

      public CanvasGroup attackMenu;
      
      public void ButtonMashingEvent() {
        Debug.Log("ButtonMashingEvent");
        transform.gameObject.SetActive(true);
        resultText.gameObject.SetActive(false);
        aKey.gameObject.SetActive(true);
        dKey.gameObject.SetActive(true);
        StartCoroutine(ButtonMashEventHandler());
      }

      public IEnumerator ButtonMashEventHandler() {
        yield return StartCoroutine(InitializeMasher());
        yield return StartCoroutine(StartMasher());
        yield return StartCoroutine(ShowFinalDamage());
      }

      public IEnumerator InitializeMasher() {
        mashAmount = 0;
        attackMenu.gameObject.SetActive(false);
        yield return null;
      }

      public IEnumerator StartMasher() {
        float timeCounter = 0f;
        while (timeCounter < mashDuration) {
          timeCounter += Time.deltaTime;
          bool isA = Input.GetKeyDown(KeyCode.A);
          bool isD =  Input.GetKeyDown(KeyCode.D);

          // terrible logic for alternating A/D
          if (isA || isD)
          {
            if (isA && prevKey == "D") {
              prevKey = "A";
              mashAmount++;
            } else if (isD && prevKey == "A") {
              prevKey = "D";
              mashAmount++;
            } else if (prevKey == "") {
              prevKey = isA ? "A" : "D";
              mashAmount++;
            }
          }
          yield return null;
        }
        
        // damage calculation step
        damageDealt = mashAmount * 2;
        boss.TakeDamage(damageDealt);
        Debug.Log(damageDealt.ToString() + " dealt, mash amount: " + mashAmount.ToString());
      }

      public IEnumerator ShowFinalDamage() {
        Debug.Log("before");
        aKey.gameObject.SetActive(false);
        dKey.gameObject.SetActive(false);
        arrow.Play();
        var canvGroup = GetComponent<CanvasGroup>();
        resultText.text = damageDealt.ToString();
        resultText.gameObject.SetActive(true);
        attackMenu.gameObject.SetActive(true);
        float timeCounter = 0f;
        float timeBeforeDisable = 3.0f;
        while (timeCounter <= 1f) {
          timeCounter += Time.deltaTime / timeBeforeDisable;
          yield return null;
        }
        resultText.gameObject.SetActive(false);
        transform.gameObject.SetActive(false);
      }   
}