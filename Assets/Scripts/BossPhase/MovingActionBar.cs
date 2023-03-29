using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovingActionBar : MonoBehaviour
{
    // speed to move left <-> right
    public float speed;
    // starting index for bar
    public int startingPoint;
    // list of transform points
    public RectTransform[] points;

    // list of points to detect result (left/right miss, sweet spot, everything else is normal)
    // index 0 = left miss
    // index 1 = sweet spot
    // index 2 = right miss
    public RectTransform[] resultPoints;

    public Text resultText;

    // the boss model to deal damage
    public BossModel boss;

    private int pointsIndex;

    // how long from when user declares attack (pauses bar) to when bar becomes available again in seconds
    private bool inputDisabled = false;
    private float disableInputDelay = 2.0f;
    private int bossDamageTaken = 0;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = points[startingPoint].position;
        resultText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (inputDisabled) {
            // only thing to do here is to update the disableInputDelay timer, nothing else
            disableInputDelay -= Time.deltaTime;
        }
        else {
            // movement of bar left <-> right by checking if bar overlaps an endpoint
            if (Vector2.Distance(transform.position, points[pointsIndex].position) < 0.02f) {
                pointsIndex++;
                if (pointsIndex == points.Length) {
                    pointsIndex = 0;
                }
            }

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
            disableInputDelay = 3.0f;
            Start();
        }
    }
}