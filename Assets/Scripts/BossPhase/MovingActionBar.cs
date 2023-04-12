using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovingActionBar : MonoBehaviour
{

    public float fadeDuration;

    public AudioSource SweetSpot;
    public AudioSource Miss;
    public AudioSource Normal;

    // speed the bar will move from left -> right
    public float speed;

    // is this a cleave?
    public bool isCleave;

    // moving bar for VISUAL
    public RectTransform movingActionBar;

    // (invisible) moving bar for ACTUAL RESULTS CALCULATION
    public RectTransform movingActionBarInternal;

    // point to start the bar
    public RectTransform startPoint;
    
    // point the bar ends for VISUAL
    public RectTransform endPoint;

    // (invisible) actual end where an attack MISSES if no input
    public RectTransform endPointInternal;

    public RectTransform sweetPoint;

    public TMPro.TextMeshProUGUI resultText;

    public TMPro.TextMeshProUGUI damageText;

    // the boss model to deal damage to
    public BossModel boss;

    // the player to trigger animation
    public Animator player;

    // set the other buttons to inactive when the attack is going on
    public CanvasGroup attackMenu;

    // used during damage calculation
    private int bossDamageTaken;

    // set to true on user input before bar reaches MISS, or if no input by the time MISS is triggered
    private bool actionFinished;

    private bool visualMovingActionBarEnd;

    private bool isActionMenuActive() {
        var canvGroup = GetComponent<CanvasGroup>();
        // action menu active if this canvas group's (attack menu canvas group) alpha is 0
        return Mathf.Approximately(canvGroup.alpha, 0f);
    }

    public void MovingActionBarEvent() {
        Debug.Log(isActionMenuActive());
        /*
        if (!isActionMenuActive()) {
            transform.gameObject.SetActive(true);
            StartCoroutine(MovingActionBarEventHandler());
        }
        else {
            // Coroutine for special attack here
        }
        */
        transform.gameObject.SetActive(true);
        StartCoroutine(MovingActionBarEventHandler());
    }

    // starts the moving action bar attack event
    public IEnumerator MovingActionBarEventHandler() {
        resultText.gameObject.SetActive(false);
        damageText.gameObject.SetActive(false);
        yield return StartCoroutine(InitBar());
        if (isCleave) {
            yield return StartCoroutine(StartBarMove(0.8f));
            yield return StartCoroutine(BetweenBars());
            yield return StartCoroutine(StartBarMove(1.2f));
            yield return StartCoroutine(BetweenBars());
            yield return StartCoroutine(StartBarMove(1.1f));
        } else {
            yield return StartCoroutine(StartBarMove(1.0f));
        }
        yield return StartCoroutine(EndEvent());
    }

    public IEnumerator BetweenBars() {
        // reset for next run
        actionFinished = false;
        visualMovingActionBarEnd = false;

        float timeBeforeDisable = 2.0f;
        float timeCounter = 0f;
        while (timeCounter < timeBeforeDisable) {
            timeCounter += Time.deltaTime;
            yield return null;
        }
        damageText.text = "";
    }

    public IEnumerator InitBar() {
        // set the buttons to inactive
        attackMenu.gameObject.SetActive(false);

        var canvGroup = GetComponent<CanvasGroup>();

        resultText.text = "";
        damageText.text = "";

        // some safety checks and "resetters" here
        actionFinished = false;
        movingActionBar.position = startPoint.position;
        movingActionBarInternal.position = startPoint.position;
        visualMovingActionBarEnd = false;
        bossDamageTaken = 0;
        canvGroup.alpha = 0;

        // here we fade in the action bar from alpha 0 -> 1
        float timeCounter = 0f;
        while (timeCounter < fadeDuration) {
            timeCounter += Time.deltaTime;
            canvGroup.alpha = Mathf.Lerp(canvGroup.alpha, 1, timeCounter / fadeDuration);
            yield return null;
        }
        // TODO: disable attack menu options
    }

    public IEnumerator StartBarMove(float speedAdjustment) {
        movingActionBar.position = startPoint.position;
        movingActionBarInternal.position = startPoint.position;

        // while no finished action, keep moving the bar and doing distance checks
        while (!actionFinished) {
            // 1. move bar towards the endPoint
            if (!visualMovingActionBarEnd) {
                movingActionBar.position = Vector2.MoveTowards(movingActionBar.position, endPoint.position, speedAdjustment * speed * Time.deltaTime);
            }
            movingActionBarInternal.position = Vector2.MoveTowards(movingActionBarInternal.position, endPointInternal.position, speedAdjustment * speed * Time.deltaTime);

            // 2. calculate distances, determine result (miss, sweet spot, normal), calculate damage to give to boss
            // based on distance of moving bar to those points
            float tolerance = 40f;
            // case 1: user interacted before bar reached end
            if (Input.GetKeyDown(KeyCode.Space)) {
                actionFinished = true;
                // immediately calculate the current position, will be either a NORMAL or SWEET attack
                if (Vector2.Distance(movingActionBarInternal.position, sweetPoint.position) < tolerance) {
                    resultText.text = "SWEET!";
                    damageText.text = "75";
                    bossDamageTaken += 75;
                    player.SetTrigger("Attack");
                    SweetSpot.Play();
                }
                else {
                    resultText.text = "NORMAL";
                    damageText.text = "30";
                    bossDamageTaken += 30;
                    player.SetTrigger("Attack");
                    Normal.Play();
                }
            }
            // case 2: bar reaches end without user input
            else {
                if (Vector2.Distance(movingActionBar.position, endPointInternal.position) < tolerance) {
                    visualMovingActionBarEnd = true;
                }
                if (Vector2.Distance(movingActionBarInternal.position, endPointInternal.position) < tolerance) {
                    resultText.text = "MISS";
                    damageText.text = "0";
                    // boss doesn't take damage here
                    bossDamageTaken += 0;
                    actionFinished = true;
                    Miss.Play();
                }
            }

            // 3. determine if action finished
            yield return null;
        }

        // here action is finished
        resultText.gameObject.SetActive(true);
        damageText.gameObject.SetActive(true);
    }

    public IEnumerator EndEvent() {
        // after damage calculation and some time for attack animation sequences, disable the bar 
        boss.TakeDamage(bossDamageTaken);
        float timeBeforeDisable = 3.0f;
        float timeCounter = 0f;
        while (timeCounter < timeBeforeDisable) {
            timeCounter += Time.deltaTime;
            yield return null;
        }
        // set the buttons to active
        attackMenu.gameObject.SetActive(true);
        // here timeBeforeDisable finish, remove the bar from the screen
        transform.gameObject.SetActive(false);
        // TODO: re-enable menu options, go back to main action menu

    }

}