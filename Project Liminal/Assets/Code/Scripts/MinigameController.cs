using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameController : MonoBehaviour
{
    [Header("Cup Settings")]
    public List<GameObject> cups;

    private float swappingSpeed; // Swapping speed of the cups
    private bool isSwapping = false;

    void Awake() {
        // Get all the cups in the scene
        AssignBallToCup();
    }

    private void AssignBallToCup()
    {
        // Generate a random index
        int index = UnityEngine.Random.Range(0, cups.Count);

        // Assign the ball to the selected cup
        cups[index].tag = "CupWithBall";
    }

    public void OnCupClicked(GameObject cup)
    {

        // If the player is already swapping cups, return
        if (!isSwapping)
        {
            Debug.Log("Cup clicked: " + cup.name);
            // Start the ShuffleCups coroutine
            StartCoroutine(ShuffleCups(10));
        }
        
    }

    private IEnumerator ShuffleCups(int numberOfSwaps)
    {
        for (int i = 0; i < numberOfSwaps; i++)
        {
            // Generate two different random indices
            int index1 = UnityEngine.Random.Range(0, cups.Count);
            int index2;
            do
            {
                index2 = UnityEngine.Random.Range(0, cups.Count);
            } while (index1 == index2);

            Debug.Log("Swapping cups " + index1 + " and " + index2);

            // Start the SwapCups coroutine and wait for it to finish
            yield return StartCoroutine(SwapCups(cups[index1], cups[index2]));
        }
    }

    private IEnumerator SwapCups(GameObject cup1, GameObject cup2)
    {
        isSwapping = true;

        Vector3 cup1TargetPosition = cup2.transform.position;
        Vector3 cup2TargetPosition = cup1.transform.position;

        float time = 0;
        swappingSpeed = UnityEngine.Random.Range(1, 5);
        while (time < 1)
        {
            time += Time.deltaTime * swappingSpeed;
            Debug.Log("Swapping cups at speed " + swappingSpeed);

            cup1.transform.position = Vector3.Lerp(cup1.transform.position, cup1TargetPosition, time);
            cup2.transform.position = Vector3.Lerp(cup2.transform.position, cup2TargetPosition, time);

            yield return null;
        }

        isSwapping = false;
    }
}