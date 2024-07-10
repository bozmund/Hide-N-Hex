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
            if (_player.strength != 2)
            {
                _player.strength = 1;
            }
        }

        public static void ApplyRecall()
        {
            SceneManager.LoadScene("Cabin");
        }

        public void ApplyPurification()
        {
            _player.ActiveEffects.Clear();
            RevertLevitation(0);
            RevertConfusion(0);
            RevertInvisibility(0);
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

            if (top != null)
            {
                // Disable TilemapCollider2D if it exists on 'top'
                if (top.TryGetComponent<TilemapCollider2D>(out var tilemapCollider))
                {
                    tilemapCollider.enabled = false;
                }
                else
                {
                    Debug.LogWarning("'top' GameObject does not have a TilemapCollider2D component.");
                }
            }
            else
            {
                Debug.LogWarning("'top' GameObject reference is not set.");
            }

            if (playerGoesBehind != null)
            {
                // Set sortingOrder to 1 if TilemapRenderer exists on 'playerGoesBehind'
                if (playerGoesBehind.TryGetComponent<TilemapRenderer>(out var tilemapRenderer))
                {
                    tilemapRenderer.sortingOrder = 1;
                }
                else
                {
                    Debug.LogWarning("'playerGoesBehind' GameObject does not have a TilemapRenderer component.");
                }
            }
            else
            {
                Debug.LogWarning("'playerGoesBehind' GameObject reference is not set.");
            }

            StartCoroutine(RevertLevitation());
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private IEnumerator RevertLevitation(int seconds = 60)
        {
            var playerGoesBehind = GameObject.Find("PlayerGoesBehind");
            var top = GameObject.Find("Top");
            yield return new WaitForSeconds(60);

            top.GetComponent<TilemapCollider2D>().enabled = true;
            playerGoesBehind.GetComponent<TilemapRenderer>().sortingOrder = 2;
            _player.flying = false;
        }

        public void ApplyInvisibility()
        {
            var find = GameObject.Find("Player");
            var color = find.GetComponent<SpriteRenderer>().color;
            color.a = 0.5f;
            find.GetComponent<SpriteRenderer>().color = color;
            var playerMovement = find.GetComponent<PlayerMovement>();
            playerMovement.invisible = true;
            StartCoroutine(RevertInvisibility());
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private IEnumerator RevertInvisibility(int seconds = 10)
        {
            var find = GameObject.Find("Player");
            var playerMovement = find.GetComponent<PlayerMovement>();
            var color = find.GetComponent<SpriteRenderer>().color;
            yield return new WaitForSeconds(seconds);
            color.a = 1f;
            GameObject.Find("Player").GetComponent<SpriteRenderer>().color = color;
            playerMovement.invisible = false;
        }

        public void ApplyHolyGrail()
        {
            SceneManager.LoadScene("WinScreen");
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public static void ApplyHealing()
        {
            var healthBar = GameObject.Find("HealthBar").GetComponent<HealthBar>();
            healthBar.IncreaseHealth(0.3f);
        }

        public void ApplyConfusion()
        {
            _player._invertControls = true;
            StartCoroutine(RevertConfusion());
        }

        private IEnumerator RevertConfusion(int seconds = 10)
        {
            yield return new WaitForSeconds(seconds);
            _player._invertControls = false;
        }

        public void ApplyCatcalling(CatCalled cat)
        {
            cat.exists = true;
        }
    }
}