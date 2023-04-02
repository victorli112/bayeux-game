using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovingActionBar : MonoBehaviour
{
    public float fadeDuration;

    // speed the bar will move from left -> right
    public float speed;

    public RectTransform movingActionBar;

    // point to start the bar
    public RectTransform startPoint;
    
    // point the bar ends if no action before
    public RectTransform endPoint;

    public RectTransform sweetPoint;

    public TMPro.TextMeshProUGUI resultText;

    // the boss model to deal damage to
    public BossModel boss;

    // used during damage calculation
    private int bossDamageTaken;

    // set to true on user input before bar reaches MISS, or if no input by the time MISS is triggered
    private bool actionFinished;

    public void MovingActionBarEvent() {
        StartCoroutine(MovingActionBarEventHandler());
    }

    // starts the moving action bar attack event
    public IEnumerator MovingActionBarEventHandler() {
        yield return StartCoroutine(InitBar());
        yield return StartCoroutine(StartBarMove());
        yield return StartCoroutine(DisableBar());
        yield return StartCoroutine(MainActionMenuReturn());
    }

    public IEnumerator InitBar() {
        var canvGroup = GetComponent<CanvasGroup>();

        // some safety checks and "resetters" here
        actionFinished = false;
        movingActionBar.position = startPoint.position;
        resultText.gameObject.SetActive(false);
        bossDamageTaken = 0;
        canvGroup.alpha = 0;

        // here we fade in the action bar from alpha 0 -> 1
        float timeCounter = 0f;
        while (timeCounter < fadeDuration) {
            timeCounter += Time.deltaTime;
            canvGroup.alpha = Mathf.Lerp(canvGroup.alpha, 1, timeCounter / fadeDuration);
            yield return null;
        }
        // TODO: user can't click on any menu options while the action bar event is active
    }

    public IEnumerator StartBarMove() {
        // while no finished action, keep moving the bar and doing distance checks
        while (!actionFinished) {
            // 1. move bar towards the endPoint
            movingActionBar.position = Vector2.MoveTowards(movingActionBar.position, endPoint.position, speed * Time.deltaTime);

            // 2. calculate distances, determine result (miss, sweet spot, normal), calculate damage to give to boss
            // based on distance of moving bar to those points
            float tolerance = 0.5f;
            // case 1: user interacted before bar reached end
            if (Input.GetKeyDown(KeyCode.Space)) {
                actionFinished = true;
                // immediately calculate the current position, will be either a NORMAL or SWEET attack
                if (Vector2.Distance(movingActionBar.position, sweetPoint.position) < tolerance) {
                    resultText.text = "SWEET!";
                    bossDamageTaken = 75;
                }
                else {
                    resultText.text = "NORMAL";
                    bossDamageTaken = 30;
                }
            }
            // case 2: bar reaches end without user input
            else {
                if (Vector2.Distance(movingActionBar.position, endPoint.position) < tolerance) {
                    resultText.text = "MISS";
                    // boss doesn't take damage here
                    bossDamageTaken = 0;
                    actionFinished = true;
                }
            }

            // 3. determine if action finished
            yield return null;
        }

        // here action is finished
        resultText.gameObject.SetActive(true);
    }

    public IEnumerator DisableBar() {
        // after damage calculation and some time for attack animation sequences, disable the bar 
        /*
        var canvGroup = GetComponent<CanvasGroup>();
        // here we fade out the action bar from alpha 1 -> 0
        float timeCounter = 0f;
        while (timeCounter < fadeDuration) {
            timeCounter += Time.deltaTime;
            canvGroup.alpha = Mathf.Lerp(canvGroup.alpha, 0, timeCounter / fadeDuration);
            yield return null;
        }
        */
        yield return null;
    }

    public IEnumerator MainActionMenuReturn() {
        // TOOD: setActive false here or not needed? (maybe not needed as the actionFinished flag false)
        // TODO: re-enable menu options, go back to main action menu
        yield return null;
    }
    /*
    // speed to move from left -> right
    public float speed;

    // point to start the bar
    public RectTransform leftPoint;
    
    // point the bar ends if no action before
    public RectTransform rightPoint;

    public RectTransform sweetPoint;

    public TMPro.TextMeshProUGUI resultText;

    // the boss model to deal damage to
    public BossModel boss;

    // determines if user has done their action before bar reaches end
    private bool userInteracted;

    // used during damage calculation
    private int bossDamageTaken;

    private bool inputDisabled = false;
    private float disableInputDelay = 2.0f;
    private int pointsIndex;

    // Start is called before the first frame update
    void Start()
    {
        // use at start (initialize) or to "reset" the state
        transform.position = leftPoint.position;
        resultText.gameObject.SetActive(false);
        userInteracted = false;
        bossDamageTaken = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (inputDisabled) {
            // only thing to do here is to update the disableInputDelay timer, nothing else
            disableInputDelay -= Time.deltaTime;
        }
        else {
            // determine result (miss, sweet spot, normal) based on distance of moving bar to those points
            float tolerance = 20f;
            if ((Vector2.Distance(transform.position, resultPoints[0].position) < tolerance) || 
                (Vector2.Distance(transform.position, resultPoints[2].position) < tolerance)) {
                resultText.text = "MISS";
                // boss doesn't take damage here
                bossDamageTaken = 0;
            }
            else if (Vector2.Distance(transform.position, resultPoints[1].position) < tolerance) {
                resultText.text = "SWEET";
                bossDamageTaken = 75;
            }
            else {
                resultText.text = "NORMAL";
                bossDamageTaken = 30;
            }

            // move bar to pointsIndex position
            transform.position = Vector2.MoveTowards(transform.position, points[pointsIndex].position, speed * Time.deltaTime);
        }

        // space bar stops the movement of bar to get attack results, pausing the bar and any input for
        // disableInputDelay seconds
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (!inputDisabled) {
                inputDisabled = true;
                resultText.gameObject.SetActive(true);
                // apply damage calculation immediately, and only once per action
                boss.TakeDamage(bossDamageTaken);
            }
            // if input already disabled, pressing space doesn't do anything
        }

        // renable input if disableInputDelay seconds reached
        if (disableInputDelay <= 0.0f) {
            inputDisabled = false;
            // hide resultText until next action
            resultText.gameObject.SetActive(false);
            disableInputDelay = 3.0f;
        }
    
    }
    */
}