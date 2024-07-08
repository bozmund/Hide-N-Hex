using UnityEngine;
using Day_Night_Cycle;
using System.Collections;
using System;
using NPC;

public class NightTime : MonoBehaviour
{
    [Header("References")]
    public Rigidbody2D rb2d;
    public NPCFreeRoam randomHodanje;
    public NpcWaypoint setHodanje;
    public GameObject[] npcs;
    public SuspicionValue sus;

    [Header("Variables")]
    [SerializeField] private float waitTime = 1.5f;
    private int chosenNPCIndex;
    private bool npcIsChosen;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        npcs = new GameObject[6];
        npcs[0] = GameObject.Find("FirstNPC");
        npcs[1] = GameObject.Find("SecondNPC");
        npcs[2] = GameObject.Find("ThirdNPC");
        npcs[3] = GameObject.Find("FourthNPC");
        npcs[4] = GameObject.Find("FifthNPC");
        npcs[5] = GameObject.Find("SixthNPC");

        randomHodanje.SetRandomTargetPosition();
        npcIsChosen = false;
    }

    private void Update()
    {
        if (WorldLight.percentOfDay >= 0.2f && WorldLight.percentOfDay <= 0.7f)
        {
            SetAllNPCsActive(true);
            npcIsChosen = false;
            StopCoroutine(randomHodanje.MoveAndWait());
            StartCoroutine(setHodanje.MoveToNextWaypoint());
        }
        else if (!npcIsChosen && (WorldLight.percentOfDay >= 0.2f || WorldLight.percentOfDay <= 0.7f))
        {
            StopCoroutine(setHodanje.MoveToNextWaypoint());
            chosenNPCIndex = UnityEngine.Random.Range(0, npcs.Length);

            SetAllNPCsActive(false);
            npcs[chosenNPCIndex].SetActive(true);
            if (npcs[chosenNPCIndex] == isActiveAndEnabled) StartCoroutine(randomHodanje.MoveAndWait());
            npcIsChosen = true;
        }
    }

    private void SetAllNPCsActive(bool isActive)
    {
        foreach (GameObject npc in npcs)
        {
            if (npc != null)
            {
                npc.SetActive(isActive);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        randomHodanje.isMoving = false;
        setHodanje.isWaiting = true;
        rb2d.velocity = Vector2.zero;
        StartCoroutine(HandleCollision());
    }

    private IEnumerator HandleCollision()
    {

        yield return new WaitForSeconds(waitTime);
        randomHodanje.isMoving = true;
        setHodanje.isWaiting = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (WorldLight.percentOfDay >= 0.2f || WorldLight.percentOfDay <= 0.7f) sus.fillAmount += 0.01f;
    }
}
