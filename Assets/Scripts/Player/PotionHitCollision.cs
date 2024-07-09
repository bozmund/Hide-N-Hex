using System.Collections;
using Player;
using UnityEngine;

namespace PotionSystem
{
    public class PotionHitCollision : MonoBehaviour
    {
        [Header("Potion Attributes")]
        public float explosionRadius = 1f;
        public LayerMask targetLayer;
        private bool hasCollided = false;
        public GameObject explosionEffect;

        public void Init(Vector3 targetPosition)
        {
            StartCoroutine(DetectCollision(targetPosition));
        }

        private IEnumerator DetectCollision(Vector3 targetPosition)
        {
            var playerMovement = PlayerMovement.GetPlayer();
            while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                yield return null;
            }

            if (!hasCollided)
            {
                hasCollided = true;
                Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius, targetLayer);
                foreach (var hitCollider in hitColliders)
                {
                    Target target = hitCollider.GetComponent<Target>();
                    if (target != null)
                    {
                        target.ApplyPotionEffect(playerMovement);
                    }
                }

                if (explosionEffect != null)
                {
                    Instantiate(explosionEffect, transform.position, Quaternion.identity);
                }

                Destroy(gameObject);
            }

            //wait 5 seconds
            yield return new WaitForSeconds(5);
            playerMovement.flamable = false;
            playerMovement.frozen = false;
        }
    }

    public class Target : MonoBehaviour
    {
        public void ApplyPotionEffect(PlayerMovement playerMovement)
        {
            if (gameObject.CompareTag("Player"))
            {
                // Apply potion effect to the player
            }
            else if (gameObject.CompareTag("Enemy"))
            {
                // Apply potion effect to the enemy
            }
            else if (gameObject.CompareTag("NPC"))
            {
                // Apply potion effect to the NPC
            }
            else if (gameObject.CompareTag("Object"))
            {
                // Apply potion effect to the object
            }
            else if (gameObject.CompareTag("Pickup"))
            {
                // Apply potion effect to the pickup
            }
            else if (gameObject.CompareTag("Collectible"))
            {
                // Apply potion effect to the collectible
            }
            else if (gameObject.CompareTag("Consumable"))
            {
                // Apply potion effect to the consumable
            }
        }
    }
}
