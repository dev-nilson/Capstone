using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameUtilities;

public class ScreenShake : MonoBehaviour
{
    private float shakeTimeRemaining, shakePower, shakeFadeTime;

    public static ScreenShake instance;

    Vector3 startingPosition;

    private void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        //possible if statement for if correct trigger was activated
        //if (IsGameOver())
        //{
        //    StartShake(.5f, 1f);
        //}
    }

    private void LateUpdate()
    {
        if (shakeTimeRemaining > 0)
        {
            shakeTimeRemaining -= Time.deltaTime;

            float xAmount = Random.Range(0f, 1f) * shakePower;
            float yAmount = Random.Range(0f, 1f) * shakePower;

            if (transform.position.x - startingPosition.x > 0f)
                xAmount = -xAmount;
            if (transform.position.y - startingPosition.y > 0f)
                yAmount = -yAmount;

            transform.position += new Vector3(xAmount, yAmount, 0f);

            shakePower = Mathf.MoveTowards(shakePower, 0f, shakeFadeTime * Time.deltaTime);
        }
    }

    public void StartShake(float length, float power)
    {
        startingPosition = transform.position;

        shakeTimeRemaining = length;
        shakePower = power;

        shakeFadeTime = power / length;
    }
}
