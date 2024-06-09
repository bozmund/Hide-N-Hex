using UnityEngine;
using System.Collections;

public class OpenBookshelf : MonoBehaviour
{
    public RectTransform bookshelfRectTransform; // Reference to the RectTransform of the Bookshelf GameObject
    public float posX1 = 113f; // First X position for the Bookshelf
    public float posX2 = 570f; // Second X position for the Bookshelf
    public float transitionDuration = 0.5f; // Duration of the transition in seconds
    private bool isAtPos1 = true; // Flag to track the current position

    public Transform bookshelfBarrierTransform; // Reference to the Transform of the BookshelfBarrier GameObject
    public float barrierPosX1 = 7.2f; // First X position for the BookshelfBarrier
    public float barrierPosX2 = 12.2f; // Second X position for the BookshelfBarrier
    private bool isBarrierAtPos1 = true; // Flag to track the current position for BookshelfBarrier

    public Transform openBookshelfTransform; // Reference to the Transform of the OpenBookshelf GameObject
    public float openBookshelfPosX1 = 7.2f; // First X position for the OpenBookshelf
    public float openBookshelfPosX2 = 12.2f; // Second X position for the OpenBookshelf
    private bool isOpenBookshelfAtPos1 = true; // Flag to track the current position for OpenBookshelf

    public GameObject leaveCabinObject; // Reference to the LeaveCabin GameObject

    private bool playerInTrigger = false; // Flag to check if player is in the trigger

    void Update()
    {
        // Check if player is in the trigger area and the "F" key is pressed
        if (playerInTrigger && Input.GetKeyDown(KeyCode.F))
        {
            // Toggle between two positions for the Bookshelf
            if (bookshelfRectTransform != null)
            {
                if (isAtPos1)
                {
                    // Move to position 2
                    StartCoroutine(MoveBookshelf(posX2));
                    isAtPos1 = false;
                }
                else
                {
                    // Move to position 1
                    StartCoroutine(MoveBookshelf(posX1));
                    isAtPos1 = true;
                }
            }

            // Toggle between two positions for the BookshelfBarrier
            if (bookshelfBarrierTransform != null)
            {
                if (isBarrierAtPos1)
                {
                    // Move to position 2
                    Vector3 newBarrierPos = bookshelfBarrierTransform.position;
                    newBarrierPos.x = barrierPosX2;
                    bookshelfBarrierTransform.position = newBarrierPos;
                    isBarrierAtPos1 = false;
                }
                else
                {
                    // Move to position 1
                    Vector3 newBarrierPos = bookshelfBarrierTransform.position;
                    newBarrierPos.x = barrierPosX1;
                    bookshelfBarrierTransform.position = newBarrierPos;
                    isBarrierAtPos1 = true;
                }
            }

            // Toggle between two positions for the OpenBookshelf
            if (openBookshelfTransform != null)
            {
                if (isOpenBookshelfAtPos1)
                {
                    // Move to position 2
                    Vector3 newOpenBookshelfPos = openBookshelfTransform.position;
                    newOpenBookshelfPos.x = openBookshelfPosX2;
                    openBookshelfTransform.position = newOpenBookshelfPos;
                    isOpenBookshelfAtPos1 = false;
                }
                else
                {
                    // Move to position 1
                    Vector3 newOpenBookshelfPos = openBookshelfTransform.position;
                    newOpenBookshelfPos.x = openBookshelfPosX1;
                    openBookshelfTransform.position = newOpenBookshelfPos;
                    isOpenBookshelfAtPos1 = true;
                }
            }

            // Toggle state of the LeaveCabin object
            if (leaveCabinObject != null)
            {
                leaveCabinObject.SetActive(!leaveCabinObject.activeSelf);
            }
        }
    }

    IEnumerator MoveBookshelf(float targetPosX)
    {
        float elapsedTime = 0;
        Vector3 startPos = bookshelfRectTransform.anchoredPosition;
        Vector3 targetPos = new Vector3(targetPosX, startPos.y, startPos.z);

        while (elapsedTime < transitionDuration)
        {
            bookshelfRectTransform.anchoredPosition = Vector3.Lerp(startPos, targetPos, (elapsedTime / transitionDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the bookshelf is at the target position
        bookshelfRectTransform.anchoredPosition = targetPos;
    }

    // Detect when player enters the trigger area
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = true;
            Debug.Log("Player entered trigger area");
        }
    }

    // Detect when player exits the trigger area
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false;
            Debug.Log("Player exited trigger area");
        }
    }
}
