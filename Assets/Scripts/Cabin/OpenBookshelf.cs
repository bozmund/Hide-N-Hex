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
    public GameObject objectToToggle;

    void Start()
    {
        // Load the saved positions when the game starts
        float savedBookshelfPosX = PlayerPrefs.GetFloat("BookshelfPosX", posX1); // Default to posX1 if no saved position
        bookshelfRectTransform.anchoredPosition = new Vector2(savedBookshelfPosX, bookshelfRectTransform.anchoredPosition.y);
        isAtPos1 = (savedBookshelfPosX == posX1);

        float savedBarrierPosX = PlayerPrefs.GetFloat("BarrierPosX", barrierPosX1); // Default to barrierPosX1 if no saved position
        bookshelfBarrierTransform.position = new Vector3(savedBarrierPosX, bookshelfBarrierTransform.position.y, bookshelfBarrierTransform.position.z);
        isBarrierAtPos1 = (savedBarrierPosX == barrierPosX1);

        float savedOpenBookshelfPosX = PlayerPrefs.GetFloat("OpenBookshelfPosX", openBookshelfPosX1); // Default to openBookshelfPosX1 if no saved position
        openBookshelfTransform.position = new Vector3(savedOpenBookshelfPosX, openBookshelfTransform.position.y, openBookshelfTransform.position.z);
        isOpenBookshelfAtPos1 = (savedOpenBookshelfPosX == openBookshelfPosX1);

        // Load the saved active state of the leaveCabinObject
        bool isLeaveCabinActive = PlayerPrefs.GetInt("LeaveCabinActive", 0) == 1; // Default to inactive if no saved state
        leaveCabinObject.SetActive(isLeaveCabinActive);
    }

    void Update()
    {
        // Check if player is in the trigger area and the "F" key is pressed
        if (playerInTrigger && Input.GetKeyDown(KeyCode.F))
        {
            GameObject minorSounds = GameObject.Find("MinorSounds");

            if (minorSounds != null)
            {
                // Find the "BookshelfSound" GameObject inside "MinorSounds"
                GameObject BookshelfSound = minorSounds.transform.Find("BookshelfSound").gameObject;

                if (BookshelfSound != null)
                {
                    // Get the AudioSource component
                    AudioSource audioSource = BookshelfSound.GetComponent<AudioSource>();

                    if (audioSource != null)
                    {
                        // Play the audio
                        audioSource.Play();
                    }
                    else
                    {
                        Debug.LogWarning("DoorSound GameObject does not have an AudioSource component.");
                    }
                }
                else
                {
                    Debug.LogWarning("Could not find GameObject named 'DoorSound' inside 'MinorSounds'.");
                }
            }
            else
            {
                Debug.LogWarning("Could not find GameObject named 'MinorSounds'.");
            }

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
                // Save the new position of the BookshelfBarrier
                PlayerPrefs.SetFloat("BarrierPosX", bookshelfBarrierTransform.position.x);
                PlayerPrefs.Save();
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
                // Save the new position of the OpenBookshelf
                PlayerPrefs.SetFloat("OpenBookshelfPosX", openBookshelfTransform.position.x);
                PlayerPrefs.Save();
            }

            // Toggle state of the LeaveCabin object
            if (leaveCabinObject != null)
            {
                bool newState = !leaveCabinObject.activeSelf;
                leaveCabinObject.SetActive(newState);

                // Save the new active state of the leaveCabinObject
                PlayerPrefs.SetInt("LeaveCabinActive", newState ? 1 : 0);
                PlayerPrefs.Save();
            }
        }
    }

    IEnumerator MoveBookshelf(float targetPosX)
    {
        float elapsedTime = 0;
        Vector2 startPos = bookshelfRectTransform.anchoredPosition;
        Vector2 targetPos = new Vector2(targetPosX, startPos.y);

        while (elapsedTime < transitionDuration)
        {
            bookshelfRectTransform.anchoredPosition = Vector2.Lerp(startPos, targetPos, (elapsedTime / transitionDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the bookshelf is at the target position
        bookshelfRectTransform.anchoredPosition = targetPos;

        // Save the new position of the bookshelf
        PlayerPrefs.SetFloat("BookshelfPosX", targetPosX);
        PlayerPrefs.Save();
    }

    // Detect when player enters the trigger area
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = true;
            //Debug.Log("Player entered trigger area");
            if (objectToToggle != null)
            {
                objectToToggle.SetActive(true); // Set the object to active when the player is in range
            }
        }
    }

    // Detect when player exits the trigger area
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false;
            //Debug.Log("Player exited trigger area");
            if (objectToToggle != null)
            {
                objectToToggle.SetActive(false); // Set the object to inactive when the player is not in range
            }
        }
    }
}
