using UnityEngine;
using UnityEngine.Accessibility;

public class NPCAttributes : MonoBehaviour 
{
    [Header("Attributes/References")]
    public float movementSpeed = 1f;
    public bool dead;
    public HealthValue health;
    public SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        health.fillAmount = 1f;
    }

    private void Update()
    {
        if(health.fillAmount < 0.1f)
        {
            dead = true;
            gameObject.SetActive(false);
            Invoke(nameof(Respawn), 60f);
        }
    }

    public void Respawn()
    {
        dead = false;
        gameObject.SetActive(true);
    }
}
