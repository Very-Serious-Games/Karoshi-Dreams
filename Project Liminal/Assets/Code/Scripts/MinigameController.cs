using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class MinigameController : MonoBehaviour
{
    [Header("Cup Settings")]
    public GameObject cupPrefab; // The cup prefab
    public int BaseNumberOfCups = 3; // The base number of cups
    private List<GameObject> cups;

    [Header("Table Settings")]
    public GameObject table; // The table GameObject

    [Header("Scene Settings")]
    public SceneSwitcher sceneSwitcher;

    [Header("Audio Settings")]
    public AudioController audioController;

    private float swappingSpeed; // Swapping speed of the cups
    private bool isSwapping = false;
    private bool isShowing = false;
    public AnimatorController animatorController;

    private bool firstSwap = true;

    void Awake() {
        
        if(GlobalVariables.rounds == 4){
            // Initialize the cups list
        cups = new List<GameObject>();

        // Create the cups
        CreateCups();

        // Get all the cups in the scene
        AssignBallToCup();

        StartCoroutine(showAllCups());
        }
    }

    private void CreateCups()
    {
        // Calculate the total width of all cups and spaces
        float totalWidth = (BaseNumberOfCups + GlobalVariables.rounds - 1) * 0.5f;

        // Calculate the starting X position
        float startX = table.transform.position.x - totalWidth / 2;

        // Create a number of cups based on the current round number
        for (int i = 0; i < BaseNumberOfCups + GlobalVariables.rounds; i++)
        {
            // Instantiate a new cup GameObject
            GameObject cup = Instantiate(cupPrefab);

            // Position the cup at a different X coordinate relative to the table's position
            cup.transform.position = new Vector3(startX + i * 0.5f, cup.transform.position.y, table.transform.position.z);

            // Add the cup to the cups list
            cups.Add(cup);
        }
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
        if (!isSwapping && !isShowing)
        {
            Debug.Log("Cup clicked: " + cup.name);
            
            StartCoroutine(showCupBall(cup, true));
        }
        
    }

    public void OnCupClickedWithBall(GameObject cup)
    {

        // If the player is already swapping cups, return
        if (!isSwapping)
        {
            Debug.Log("You found the ball!");
            // Increment the number of rounds
            GlobalVariables.rounds++;
            Debug.Log("Rounds: " + GlobalVariables.rounds);
            sceneSwitcher.SwitchScene("AwakeScene");
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
        swappingSpeed = UnityEngine.Random.Range(0.5f, 1.0f) * (GlobalVariables.rounds / 2 + 1);

        audioController.PlayAudio("shuffle_cups");

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

    private IEnumerator showCupBall(GameObject cup, bool clicked = false)
    {
        isShowing = true;
        if(cup.tag == "CupWithBall")
        {
            cup.GetComponent<Animator>().Play("vasosup");

            yield return new WaitForSeconds(1);

            cup.GetComponent<Animator>().Play("vasosdw");
            if(clicked)OnCupClickedWithBall(cup);
        }
        else
        {
            cup.GetComponent<Animator>().Play("vasoupnn");
            yield return new WaitForSeconds(1);
            cup.GetComponent<Animator>().Play("vasosdw");

            yield return new WaitForSeconds(0.5f);
            // Start the ShuffleCups coroutine

            foreach (GameObject cup1 in cups)
            {
                if (cup1.tag == "CupWithBall")
                {
                    yield return StartCoroutine(showCupBall(cup1, false));
                    yield return new WaitForSeconds(1);
                    break;
                }
            }
            StartCoroutine(ShuffleCups((int)(10 + GlobalVariables.rounds * 2)));
        }
        isShowing = false;
        yield return null;
    }

    private IEnumerator showAllCups()
    {
        isShowing = true;
        yield return new WaitForSeconds(5);

        foreach (GameObject cup in cups)
        {
           if(cup.tag == "CupWithBall")
            {
                cup.GetComponent<Animator>().Play("vasosup");

                yield return new WaitForSeconds(1);

                cup.GetComponent<Animator>().Play("vasosdw");
            }
            else
            {
                cup.GetComponent<Animator>().Play("vasoupnn");
                yield return new WaitForSeconds(1);
                cup.GetComponent<Animator>().Play("vasosdw");
            }
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(0.5f);
        isShowing = false;
        StartCoroutine(ShuffleCups((int)(10 + GlobalVariables.rounds * 2)));
    }
}