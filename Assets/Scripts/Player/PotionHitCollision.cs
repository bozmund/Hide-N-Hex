using System.Collections;
using System.Collections.Generic;
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
                        target.ApplyPotionEffect();
                    }
                }

                if (explosionEffect != null)
                {
                    Instantiate(explosionEffect, transform.position, Quaternion.identity);
                }

                Destroy(gameObject);
            }
        }
    }

    public class Target : MonoBehaviour
    {
        public void ApplyPotionEffect()
        {
            // Placeholder for applying the potion effect to the target
            Debug.Log("Potion effect applied to " + gameObject.name);
        }
    }
}
