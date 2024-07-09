using System;
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

                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                if (hitColliders.Length == 0)
                {
                    Invoke(nameof(DeletePotions), 5f);
                }
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

            else if (gameObject.CompareTag("DisappearObject"))
            {
                PlayerPrefs.SetInt("canColect", 1);
                StartCoroutine(TimerColect());
                Invoke(nameof(DeletePotions), 5f);
            }

            else
            {
                Invoke(nameof(DeletePotions), 6f);
            }
        }

        private IEnumerator TimerColect()
        {
            yield return new WaitForSeconds(4f);
            PlayerPrefs.SetInt("canColect", 0);
            Destroy(gameObject);
        }

        void aliveIt()
        {
            dead = false;
        }

        public void DeletePotions()
        {
            // Find all game objects in the scene
            GameObject[] allObjects = FindObjectsOfType<GameObject>();

            int count = 0;

            // Loop through each game object to check its name
            foreach (GameObject obj in allObjects)
            {
                if (obj.name == "Potion(Clone)")
                {
                    Destroy(obj);
                    count++;
                }
            }

            // Optional: log the number of potions destroyed for debugging
            Debug.Log(count + " potions destroyed.");
        }
    }
}
