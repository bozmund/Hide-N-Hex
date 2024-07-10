using System;
using System.Collections;
using System.Drawing;
using Items;
using Player;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PotionSystem
{
    public class PotionHitCollision : MonoBehaviour
    {
        public MainInventory MainInventoryData;
        public InventoryUIManager inventoryUIManager;

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
        public NPCAttributes NPCAttributes;

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
                    deletePotionFromInventory(_potionInHand.potionName);
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
                NPCAttributes NPCAttributes = gameObject.GetComponent<NPCAttributes>();
                if (_potionInHand.potionName == "HealingPotion")
                {
                    NPCAttributes.health.fillAmount += 0.3f;
                    Invoke(nameof(DeletePotion), 31f);
                }
                if (_potionInHand.potionName == "InvisibilityPotion")
                {
                    var color = NPCAttributes.spriteRenderer.color;
                    color.a = 0.5f;
                    NPCAttributes.spriteRenderer.color = color;
                    StartCoroutine(NPCVisibility());
                    Invoke(nameof(DeletePotion), 31f);
                }
                if (_potionInHand.potionName == "LiquidFlamePotion")
                {
                    NPCAttributes.health.fillAmount -= 0.3f;
                    Invoke(nameof(DeletePotion), 31f);
                }
                if (_potionInHand.potionName == "ParalyticGasPotion")
                {
                    NPCAttributes.movementSpeed = 0f;
                    StartCoroutine(NPCLetMeIn());
                    Invoke(nameof(DeletePotion), 31f);
                }
                if (_potionInHand.potionName == "SwiftnessPotion")
                {
                    NPCAttributes.movementSpeed *= 2f;
                    StartCoroutine(NPCFastAFBoi());
                    Invoke(nameof(DeletePotion), 31f);
                }
                if (_potionInHand.potionName == "ToxicGasPotion")
                {
                    dead = true;
                    gameObject.SetActive(false);
                    Invoke(nameof(aliveIt), 5f);
                    deletePotionFromInventory("ToxicGasPotion");
                }
            }

            else if (gameObject.CompareTag("DisappearObject"))
            {
                if (_potionInHand.potionName == "FrostPotion")
                {
                    PlayerPrefs.SetInt("canColectFire", 1);
                    StartCoroutine(TimerColectFire());
                    Invoke(nameof(DeletePotion), 31f);
                    deletePotionFromInventory("FrostPotion");
                }

                if (_potionInHand.potionName == "LiquidFlamePotion")
                {
                    PlayerPrefs.SetInt("canColectFrozen", 1);
                    StartCoroutine(TimerColectFrozen());
                    Invoke(nameof(DeletePotion), 31f);
                    deletePotionFromInventory("LiquidFlamePotion");
                }

                if (_potionInHand.potionName == "UsefulnessPotion")
                {
                    PlayerPrefs.SetInt("canMultiply", 1);
                    StartCoroutine(TimerMultiply());
                    Invoke(nameof(DeletePotion), 21f);
                    deletePotionFromInventory("UsefulnessPotion");
                }
            }

            else if (gameObject.CompareTag("House"))
            {
                if (_potionInHand.potionName == "RepairPotion")
                {
                    deletePotionFromInventory("RepairPotion");
                    SceneManager.LoadScene("WinRepair");
                }
            }

            else if (gameObject.CompareTag("BossMan"))
            {
                if (_potionInHand.potionName == "ConfusionPotion")
                {
                    bossMan.confused = true;
                    StartCoroutine(Confusion());
                    deletePotionFromInventory("ConfusionPotion");
                }
                if (_potionInHand.potionName == "HealingPotion")
                {
                    bossHP.fillAmount += 0.3f;
                    deletePotionFromInventory("HealingPotion");
                }
                if (_potionInHand.potionName == "InvisibilityPotion")
                {
                    var color = bossMan.GetComponent<SpriteRenderer>().color;
                    color.a = 0.5f;
                    GameObject.Find("BearBossMan").GetComponent<SpriteRenderer>().color = color;
                    StartCoroutine(Visibility());
                    deletePotionFromInventory("InvisibilityPotion");
                }
                if (_potionInHand.potionName == "LiquidFlamePotion")
                {
                    bossHP.fillAmount -= 0.3f;
                    deletePotionFromInventory("LiquidFlamePotion");
                }
                if (_potionInHand.potionName == "ParalyticGasPotion")
                {
                    bossMan.movementSpeed = 0f;
                    StartCoroutine(LetMeIn());
                    deletePotionFromInventory("ParalyticGasPotion");
                }
                if (_potionInHand.potionName == "SwiftnessPotion")
                {
                    bossMan.movementSpeed *= 2f;
                    StartCoroutine(FastAFBoi());
                    deletePotionFromInventory("SwiftnessPotion");
                }
                if (_potionInHand.potionName == "ToxicGasPotion")
                {
                    StartCoroutine(Poison());
                    deletePotionFromInventory("ToxicGasPotion");
                }
                if (_potionInHand.potionName == "PurificationPotion")
                {
                    BearShit shit = Instantiate(bearShit, bearShit.transform.position, Quaternion.identity);
                    shit.GetComponent<BearShit>();
                    shit.gameObject.SetActive(true);
                    shit.transform.localScale= new Vector3(5f, 5f, 0f);
                    deletePotionFromInventory("PurificationPotion");
                }
            }
        }

        private IEnumerator NPCVisibility()
        {
            yield return new WaitForSeconds(5f);
            var color = NPCAttributes.spriteRenderer.color;
            color.a = 1f;
            NPCAttributes.spriteRenderer.color = color;
        }

        private IEnumerator NPCPoison()
        {
            NPCAttributes.health.fillAmount -= 0.1f;
            yield return new WaitForSeconds(1f);

            NPCAttributes.health.fillAmount -= 0.1f;
            yield return new WaitForSeconds(1f);

            NPCAttributes.health.fillAmount -= 0.1f;
            yield return new WaitForSeconds(1f);
        }

        private IEnumerator NPCFastAFBoi()
        {
            yield return new WaitForSeconds(5f);
            NPCAttributes.movementSpeed /= 1f;
        }

        private IEnumerator NPCLetMeIn()
        {
            yield return new WaitForSeconds(5f);
            NPCAttributes.movementSpeed = 1f;
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
            yield return new WaitForSeconds(5f);
            var color = bossMan.GetComponent<SpriteRenderer>().color;
            color.a = 1f;
            GameObject.Find("Player").GetComponent<SpriteRenderer>().color = color;          
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

        void deletePotionFromInventory(string potionName)
        {
            var potionCount = MainInventoryData.GetSlotAndCountForItem(potionName, out var itemNumber);
            potionCount -= 1;

            if (potionCount == 0)
            {
                _potionInHand.potionName = null;
                MainInventoryData.UpdateMainInventory(itemNumber, "", potionCount);
                inventoryUIManager.ClearUIElement(itemNumber);
            }

            else
            {
                MainInventoryData.UpdateMainInventory(itemNumber, potionName, potionCount);
            }

            inventoryUIManager.LoadInventorySprites();

        }
    }
}
