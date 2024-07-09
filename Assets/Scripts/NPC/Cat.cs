using Day_Night_Cycle;
using Items;
using System;
using UnityEngine;
using static PlantListForCat;

namespace CatSystem
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class Cat : MonoBehaviour
    {
        public MainInventory MainInventoryData;
        public InventoryUIManager inventoryUIManager;
        public CatCalled catExists;

        public bool hasItem = true;
        private bool isInTrigger = false;
        public bool exists => catExists.exists;


        private Animator animator;
        private SpriteRenderer prikaz;
        private BoxCollider2D kolizija1;
        private BoxCollider2D kolizija2;

        void Start()
        {
            animator = GetComponent<Animator>();
            prikaz = GetComponent<SpriteRenderer>();
            kolizija1 = GetComponent<BoxCollider2D>();
            kolizija2 = transform.Find("IngredientCollider").GetComponent<BoxCollider2D>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            isInTrigger = true;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            isInTrigger = false;
        }

        private void GetIngredient()
        {
            PlantsForCat randomPlant = GetRandomPlant();
            Debug.Log("Random plant selected: " + randomPlant);
            hasItem = false;
            animator.SetBool("hasItem", false);
            var potionCount = MainInventoryData.GetSlotAndCountForItem(randomPlant.ToString(), out var itemNumber);
            potionCount += 1;
            MainInventoryData.UpdateMainInventory(itemNumber, randomPlant.ToString(), potionCount);
            inventoryUIManager.LoadInventorySprites();
        }

        void Update()
        {

            if (exists)
            {
                if (WorldLight.percentOfDay >= 0.2f && WorldLight.percentOfDay <= 0.8f)
                {
                    ShowCat();
                    if (hasItem)
                    {
                        animator.SetBool("hasItem", true);
                    }
                    if (Input.GetKeyUp(KeyCode.F) && hasItem && isInTrigger)
                    {
                        GetIngredient();
                    }
                }
                else
                {
                    HideCat();
                    hasItem = true;
                    animator.SetBool("hasItem", false);
                }
            }
            else HideCat();
        }

        private void HideCat()
        {
            prikaz.enabled = false;
            kolizija1.enabled = false;
            kolizija2.enabled = false;
        }

        private void ShowCat()
        {
            prikaz.enabled = true;
            kolizija1.enabled = true;
            kolizija2.enabled = true;
        }

        public static PlantsForCat GetRandomPlant()
        {
            Array plants = Enum.GetValues(typeof(PlantsForCat));
            System.Random random = new System.Random();
            PlantsForCat randomPlant = (PlantsForCat)plants.GetValue(random.Next(plants.Length));
            return randomPlant;
        }
    }
}
