using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonMashing : MonoBehaviour {

      private int mashAmount;

      private int damageDealt;

      // keep track of the previous key, player must alternate between A and D
      private string prevKey = "";

      // how long the user amash for
      public float mashDuration = 7.5f;

      // the final result text with the damage dealth
      public TMPro.TextMeshProUGUI resultText;

      public BossModel boss;

      public void Start() {
        transform.gameObject.SetActive(false);
      }

      public void ButtonMashingEvent() {
        Debug.Log("ButtonMashingEvent");
        transform.gameObject.SetActive(true);
        StartCoroutine(ButtonMashEventHandler());
      }

      public IEnumerator ButtonMashEventHandler() {
        yield return StartCoroutine(InitializeMasher());
        yield return StartCoroutine(StartMasher());
        yield return StartCoroutine(ShowFinalDamage());
      }

      public IEnumerator InitializeMasher() {
        mashAmount = 0;
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
              Debug.Log("press A");
            } else if (isD && prevKey == "A") {
              prevKey = "D";
              mashAmount++;
              Debug.Log("press D");
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
        resultText.text = damageDealt.ToString();
        resultText.gameObject.SetActive(true);
        transform.gameObject.SetActive(false);
        float timeBeforeDisable = 3.0f;
        float timeCounter = 0f;
        while (timeCounter < timeBeforeDisable) {
          timeCounter += Time.deltaTime;
          yield return null;
        }
        resultText.gameObject.SetActive(false);
      }
}