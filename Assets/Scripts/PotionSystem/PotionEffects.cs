using System.Collections;
using Bars;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

namespace PotionSystem
{
    public class PotionEffects : MonoBehaviour
    {
        private readonly PlayerMovement _player;

        public PotionEffects(PlayerMovement player)
        {
            if (!player) Debug.Log("player not exist");
            _player = player;
        }

        public void ApplyWeakness()
        {
            _player.AddEffect(new Effect(
                "Weakness",
                10f,
                player => player.strength = 0,
                player => player.strength = 1
            )); 
        }

        public void ApplyUsefulness()
        {
            throw new System.NotImplementedException();
        }

        public void ApplySwiftness()
        {
            _player.AddEffect(new Effect(
                "SpeedBoost",
                10f,
                player => player.movementSpeed *= 2,
                player => player.movementSpeed /= 2
            ));
            Debug.Log("Speed potion consumed. Player is now faster!");
        }

        public void ApplyStrength()
        {
            _player.strength = 1;
        }

        public static void ApplyRecall()
        {
            SceneManager.LoadScene("Cabin");
        }

        public void ApplyPurification()
        {
            _player.ActiveEffects.Clear();
        }

        public void ApplyParalyticGas()
        {
            _player.AddEffect(new Effect(
                "ParalyticGas",
                5f,
                player => player.movementSpeed = 0,
                player => player.movementSpeed = 2.5f
            ));
        }

        public void ApplyMindVision()
        {
        }

        public void ApplyMight()
        {
            _player.strength = 2;
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public static void ApplyLowerSus()
        {
            GameObject.Find("SuspicionBar").GetComponent<SuspicionBar>().DecreaseSuspicion();
        }
        
        // ReSharper disable Unity.PerformanceAnalysis
        public static void ApplyMoreSus(bool isCrafting)
        {
            var suspicionBar = GameObject.Find("SuspicionBar").GetComponent<SuspicionBar>();
            suspicionBar.IncreaseSuspicion();
            switch (suspicionBar.fillAmountData.fillAmount)
            {
                case >= 1f when isCrafting:
                    //explode
                    break;
                case >= 1f:
                    //game over
                    break;
            }
        }


        // ReSharper disable Unity.PerformanceAnalysis
        public static void ApplyLiquidFlame()
        {
            GameObject.Find("HealthBar").GetComponent<HealthBar>().DecreaseHealth(3f);
        }

        public void ApplyLevitation()
        {
            var top = GameObject.Find("Top");
            var playerGoesBehind = GameObject.Find("PlayerGoesBehind");

            top.GetComponent<TilemapCollider2D>().enabled = false;
            playerGoesBehind.GetComponent<TilemapRenderer>().sortingOrder = 1;

            StartCoroutine(RevertLevitation(top, playerGoesBehind));
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private static IEnumerator RevertLevitation(GameObject top, GameObject playerGoesBehind)
        {
            yield return new WaitForSeconds(60);

            top.GetComponent<TilemapCollider2D>().enabled = true;
            playerGoesBehind.GetComponent<TilemapRenderer>().sortingOrder = 2;
        }

        public void ApplyInvisibility()
        {
            var color = GameObject.Find("Player").GetComponent<SpriteRenderer>().color;
            color.a = 0.5f;
            GameObject.Find("Player").GetComponent<SpriteRenderer>().color = color;
            
            StartCoroutine(RevertInvisibility(color));
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private IEnumerator RevertInvisibility(Color color)
        {
            yield return new WaitForSeconds(2);
            color.a = 1f;
            GameObject.Find("Player").GetComponent<SpriteRenderer>().color = color;
        }

        public void ApplyHolyGrail()
        {
            throw new System.NotImplementedException();
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public static void ApplyHealing()
        {
            var healthBar = GameObject.Find("HealthBar").GetComponent<HealthBar>();
            healthBar.IncreaseHealth(3f);
        }

        public void ApplyConfusion()
        {
            //invert player controls
            
        }

        public void ApplyClothing()
        {
            throw new System.NotImplementedException();
        }

        public void ApplyCatcalling()
        {
            //the player will summon a cat
        }
    }
}