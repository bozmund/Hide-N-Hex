using System.Collections;
using System.Collections.Generic;
using Bars;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using CatSystem;
using Scriptable_Objects;

namespace PotionSystem
{
    public class PotionEffects : MonoBehaviour
    {
        private PlayerMovement _player;
        public CatCalled catExists;

        private void Awake()
        {
            // If the player is not assigned in the inspector, try to find it.
            if (_player) return;
            _player = GameObject.Find("Player").GetComponent<PlayerMovement>();
            if (!_player)
            {
                Debug.LogError("PlayerMovement component not found on 'Player' GameObject.");
            }
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
                player => player.movementSpeed = 3f
            ));
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
                    SceneManager.LoadScene("GameOverHealth");
                    break;
                case >= 1f:
                    SceneManager.LoadScene("GameOverSuspicion");
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
            _player.flying = true;
            var top = GameObject.Find("Top");
            var playerGoesBehind = GameObject.Find("PlayerGoesBehind");

            top.GetComponent<TilemapCollider2D>().enabled = false;
            playerGoesBehind.GetComponent<TilemapRenderer>().sortingOrder = 1;

            StartCoroutine(RevertLevitation(top, playerGoesBehind));
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private IEnumerator RevertLevitation(GameObject top, GameObject playerGoesBehind)
        {
            yield return new WaitForSeconds(60);

            top.GetComponent<TilemapCollider2D>().enabled = true;
            playerGoesBehind.GetComponent<TilemapRenderer>().sortingOrder = 2;
            _player.flying = false;
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
            SceneManager.LoadScene("WinScreen");
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public static void ApplyHealing()
        {
            var healthBar = GameObject.Find("HealthBar").GetComponent<HealthBar>();
            healthBar.IncreaseHealth(3f);
        }

        public void ApplyConfusion()
        {
            _player._invertControls = true;
            StartCoroutine(RevertConfusion());
        }

        private IEnumerator RevertConfusion()
        {
            yield return new WaitForSeconds(10);
            _player._invertControls = false;
        }

        public void ApplyCatcalling(CatCalled cat)
        {
            cat.exists = true;
        }
    }
}