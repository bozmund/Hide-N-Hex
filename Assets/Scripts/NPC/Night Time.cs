using UnityEngine;
using Day_Night_Cycle;
using System.Collections;
using System;
using NPC;
using PotionSystem;

public class NightTime : MonoBehaviour
{
    [Header("References")]
    public Rigidbody2D rb2d;
    public NPCFreeRoam randomHodanje;
    public NpcWaypoint setHodanje;
    public GameObject[] npcs;
    public SuspicionValue sus;
    public PotionHitCollision potionHit;

    [Header("Variables")]
    [SerializeField] private float waitTime = 1.5f;
    private int chosenNPCIndex;
    private bool npcIsChosen;
    [SerializeField] private float detectionRadius = 5f;
    [SerializeField] private LayerMask playerLayer;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        potionHit = GetComponent<PotionHitCollision>();
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
            if(potionHit.dead) SetAllNPCsActive(true);
            npcIsChosen = false;
            StopCoroutine(randomHodanje.MoveAndWait());
            StartCoroutine(setHodanje.MoveToNextWaypoint());
        }
        else if (!npcIsChosen && (WorldLight.percentOfDay < 0.2f || WorldLight.percentOfDay > 0.7f))
        {
            StopCoroutine(setHodanje.MoveToNextWaypoint());
            chosenNPCIndex = UnityEngine.Random.Range(0, npcs.Length);

            SetAllNPCsActive(false);
            npcs[chosenNPCIndex].SetActive(true);
            if (npcs[chosenNPCIndex] == isActiveAndEnabled) StartCoroutine(randomHodanje.MoveAndWait());
            npcIsChosen = true;
        }

        if (WorldLight.percentOfDay < 0.2f || WorldLight.percentOfDay > 0.7f)
        {
            Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, detectionRadius, playerLayer);
            if (playerCollider != null && playerCollider.CompareTag("Player"))
            {
                sus.fillAmount += 0.05f * Time.deltaTime;
            }
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
}
