using System;
using System.Collections;
using System.Drawing;
using Player;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PotionSystem
{
    public class PotionHitCollision : MonoBehaviour
    {
        [Header("Potion Attributes")]
        public float explosionRadius;
        public LayerMask targetLayer;
        private bool hasCollided = false;

        [Header("Potion References")]
        public GameObject explosionEffect;
        public ParticleSystem particle;
        public PotionInHand _potionInHand;

        [Header("Bear References")]
        //referenca na medvjeda gameobject
        public BearBossMan bossMan;
        //referenca na medvjeda scriptable object
        public HealthValue bossHP;
        public BearShit bearShit;

        public bool dead;

        private void Start()
        {
            if (GameObject.Find("BearBossMan")) bossMan.GetComponent<BearBossMan>();
            else return;
            if (GameObject.Find("BearShit")) return;
        }
        //gameObject koji se pogada mora imat hittable layer i pripadajuci tag da se applya potion effect
        //Invoke metoda iz nekog razloga ne radi kako treba, koristi korutina i ienumerator

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
                    var particleMain = particle.main;
                    particleMain.playOnAwake = true;
                    var particles = Instantiate(explosionEffect, transform.position, Quaternion.identity);
                    Destroy(particles, 0.5f);
                }

                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                if (hitColliders.Length == 0)
                {
                    DeletePotion();
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
                if (_potionInHand.potionName == "FrostPotion")
                {
                    PlayerPrefs.SetInt("canColectFire", 1);
                    StartCoroutine(TimerColectFire());
                    Invoke(nameof(DeletePotion), 31f);
                }

                if (_potionInHand.potionName == "LiquidFlamePotion")
                {
                    PlayerPrefs.SetInt("canColectFrozen", 1);
                    StartCoroutine(TimerColectFrozen());
                    Invoke(nameof(DeletePotion), 31f);
                }

                if (_potionInHand.potionName == "UsefulnessPotion")
                {
                    PlayerPrefs.SetInt("canMultiply", 1);
                    StartCoroutine(TimerMultiply());
                    Invoke(nameof(DeletePotion), 21f);
                }
            }

            else if (gameObject.CompareTag("House"))
            {
                if (_potionInHand.potionName == "RepairPotion")
                {
                    SceneManager.LoadScene("WinScreen");
                }
            }

            else if (gameObject.CompareTag("BossMan"))
            {
                if (_potionInHand.potionName == "ConfusionPotion")
                {
                    bossMan.confused = true;
                    StartCoroutine(Confusion());
                }
                if (_potionInHand.potionName == "HealingPotion")
                {
                    bossHP.fillAmount += 0.3f;
                }
                if (_potionInHand.potionName == "InvisibilityPotion")
                {
                    var color = bossMan.GetComponent<SpriteRenderer>().color;
                    color.a = 0.5f;
                    GameObject.Find("BearBossMan").GetComponent<SpriteRenderer>().color = color;
                    StartCoroutine(Visibility());
                }
                if (_potionInHand.potionName == "LiquidFlamePotion")
                {
                    bossHP.fillAmount -= 0.3f;
                }
                if (_potionInHand.potionName == "ParalyticGasPotion")
                {
                    bossMan.movementSpeed = 0f;
                    StartCoroutine(LetMeIn());
                }
                if (_potionInHand.potionName == "SwiftnessPotion")
                {
                    bossMan.movementSpeed *= 2f;
                    StartCoroutine(FastAFBoi());
                }
                if (_potionInHand.potionName == "ToxicGasPotion")
                {
                    StartCoroutine(Poison());
                }
                if (_potionInHand.potionName == "PurificationPotion")
                {
                    BearShit shit = Instantiate(bearShit, bearShit.transform.position, Quaternion.identity);
                    shit.GetComponent<BearShit>();
                    shit.gameObject.SetActive(true);
                    shit.transform.localScale= new Vector3(5f, 5f, 0f);
                }
            }
        }

        private IEnumerator Poison()
        {
            bossHP.fillAmount -= 0.1f;
            yield return new WaitForSeconds(1f);

            bossHP.fillAmount -= 0.1f;
            yield return new WaitForSeconds(1f);

            bossHP.fillAmount -= 0.1f;
            yield return new WaitForSeconds(1f);
        }

        private IEnumerator FastAFBoi()
        {
            yield return new WaitForSeconds(3f);
            bossMan.movementSpeed /= 2f;
        }

        private IEnumerator LetMeIn()
        {     
            yield return new WaitForSeconds(5f);
            bossMan.movementSpeed = 1f;
        }

        private IEnumerator Visibility()
        {
            var color = bossMan.GetComponent<SpriteRenderer>().color;
            color.a = 1f;
            GameObject.Find("Player").GetComponent<SpriteRenderer>().color = color;
            yield return new WaitForSeconds(5f);
        }

        private IEnumerator Confusion()
        {
            yield return new WaitForSeconds(5f);
            bossMan.confused = false;
        }

        private IEnumerator TimerColectFrozen()
        {
            yield return new WaitForSeconds(30f);
            PlayerPrefs.SetInt("canColectFrozen", 0);
        }

        private IEnumerator TimerColectFire()
        {
            yield return new WaitForSeconds(30f);
            PlayerPrefs.SetInt("canColectFire", 0);
        }

        private IEnumerator TimerMultiply()
        {
            yield return new WaitForSeconds(20f);
            PlayerPrefs.SetInt("canMultiply", 0);
        }

        void aliveIt()
        {
            dead = false;
        }

        public void DeletePotion()
        {
            Destroy(gameObject);
        }
    }
}
