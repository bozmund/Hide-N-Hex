using System.Collections;
using Player;
using UnityEngine;

namespace PotionSystem
{
    public class PotionHitCollision : MonoBehaviour
    {
        [Header("Potion Attributes")]
        public float explosionRadius;
        public LayerMask targetLayer;
        private bool hasCollided = false;
        public GameObject explosionEffect;
        public ParticleSystem particle;
        public PotionInHand _potionInHand;

        public bool dead;

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

                    if (hitCollider.gameObject != null)
                    {
                        ApplyPotionEffect(hitCollider.gameObject);
                    }
                }

                if (explosionEffect != null)
                {
                    particle.playOnAwake = true;
                    GameObject particles = Instantiate(explosionEffect, transform.position, Quaternion.identity);
                    Destroy(particles, 0.5f);
                }

                Destroy(gameObject);
            }

            //wait 5 seconds
            yield return new WaitForSeconds(5);
            playerMovement.flamable = false;
            playerMovement.frozen = false;
        }

        public void ApplyPotionEffect(GameObject gameObject)
        {
            if (gameObject.CompareTag("NPC"))
            {
                if (_potionInHand.potionName == "ToxicGasPotion")
                {
                    dead = true;
                    gameObject.SetActive(false);
                    Invoke(nameof(aliveIt), 5f);
                }
            }
        }

        void aliveIt()
        {
            dead = false;
        }
    }
}
