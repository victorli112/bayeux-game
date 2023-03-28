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

    private bool interacted = false; 

    // Start is called before the first frame update
    void Start()
    {
        transform.position = points[startingPoint].position;
    }

    // Update is called once per frame
    void Update()
    {
        // check current distance of bar to the next point to determine direction
        // if bar basically overlaps an endpoint
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
        }
        else if (Vector2.Distance(transform.position, resultPoints[1].position) < tolerance) {
            resultText.text = "SWEET";
            if (interacted) {
                boss.TakeDamage(75);
                interacted = false;
            }
        }
        else {
            resultText.text = "NORMAL";
            if (interacted) {
                boss.TakeDamage(30);
                interacted = false;
            }
        }

        // move bar to pointsIndex position
        if (!interacted) {
            transform.position = Vector2.MoveTowards(transform.position, points[pointsIndex].position, speed * Time.deltaTime);
        }

        // space bar stops the movement of bar to get results
        if (Input.GetKeyDown(KeyCode.Space)) {
            interacted = !interacted;
        }
    }
}

