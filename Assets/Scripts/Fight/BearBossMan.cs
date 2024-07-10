using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class BearBossMan : MonoBehaviour
{
    [Header("References")]
    public Animator animator;
    public Rigidbody2D rb2d;
    public Transform player;
    public HealthValue health, bearHP;
    public LayerMask attackable;
    public BoxCollider2D boxCollider;
    public BearShit poop;

    [Header("Attributes")]
    public float movementSpeed;
    public Vector2 position;
    public float attackCD;
    public bool isAttacking;
    public bool sleep, alert, dead;
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    private bool pickUp;
    public bool confused;

    private void Start()
    {
        pickUp = true;
        bearHP.fillAmount = 1f;
        player = GameObject.Find("Player").transform;
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        sleep = true;
        poop.gameObject.SetActive(false);
    }

    private void Update()
    {
        Animate();
        playerInSightRange = Physics2D.OverlapCircle(transform.position, sightRange, attackable);
        playerInAttackRange = Physics2D.OverlapCircle(transform.position, attackRange, attackable);

        if(!(bearHP.fillAmount == 0))
        {
            if (!playerInSightRange && !playerInAttackRange) return;

            if (playerInSightRange && !playerInAttackRange)
            {
                StartCoroutine(WakeUp());
                Invoke(nameof(ChasePlayer), 1f);           
            }

            if(playerInSightRange && playerInAttackRange)
            {
                if (!isAttacking)
                {
                    animator.SetBool("isAttack", true);
                    isAttacking = true;
                    Invoke(nameof(AttackAnim), 0.5f);
                    Invoke(nameof(ResetAttack), attackCD);
                }
            }

        }
        else
        {
            animator.SetBool("isDead", true);
            rb2d.velocity = Vector2.zero;
            boxCollider.enabled = false;
            if (pickUp)
            {
                pickUp = false;
                poop.gameObject.SetActive(true);
            }
        }
    }

    private IEnumerator WakeUp()
    {
        animator.SetBool("isAlert", true);
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("isAlert", false);
    }

    private void Animate()
    {
        if (!isAttacking)
        {
            if (rb2d.velocity != Vector2.zero)
            {
                animator.SetFloat("Horizontal", rb2d.velocity.x);
                animator.SetFloat("Vertical", rb2d.velocity.y);
            }
            animator.SetFloat("Speed", rb2d.velocity.sqrMagnitude);
        }
    }

    private void ResetAttack()
    {
        isAttacking = false;
        animator.SetBool("isAttack", false);
        health.fillAmount -= 0.4f;
    }

    private void ChasePlayer()
    {
        if (!confused)
        {
            Vector2 targetPos = new Vector2(player.transform.position.x, player.transform.position.y);
            Vector2 direction = (targetPos - rb2d.position).normalized;
            rb2d.velocity = direction * movementSpeed;
        }
        else
        {
            StartCoroutine(dontSpazm());
        }
    }

    private IEnumerator dontSpazm()
    {
        Vector2 targetPos = new Vector2(transform.position.x + UnityEngine.Random.Range(0f, 3f), transform.position.y + UnityEngine.Random.Range(0f, 3));
        Vector2 direction = (targetPos - rb2d.position).normalized;
        rb2d.velocity = direction * movementSpeed;
        yield return new WaitForSeconds(1f);

    }

    private void AttackAnim()
    {
        animator.SetBool("isAttack", false);
    }
}
