using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovingActionBar : MonoBehaviour
{
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
        /*
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
        */
    }
}