using Bars;
using Day_Night_Cycle;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PotionSystem
{
    public class PotionEffects
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
                player => player._movementSpeed *= 2,
                player => player._movementSpeed /= 2
            ));
            Debug.Log("Speed potion consumed. Player is now faster!");
        }

        public void ApplyStrength()
        {
            _player.strength = 1;
        }

        public void ApplyRecall()
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
                player => player._movementSpeed = 0,
                player => player._movementSpeed = 2.5f
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
        public void ApplyLowerSus()
        {
            SuspicionBar suspicionBar = GameObject.Find("SuspicionBar").GetComponent<SuspicionBar>();
            suspicionBar.DecreaseSuspicion();
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public void ApplyLiquidFlame()
        {
            var healthBar = GameObject.Find("HealthBar").GetComponent<HealthBar>();
            healthBar.DecreaseHealth(3f);
        }

        public void ApplyLevitation()
        {
            throw new System.NotImplementedException();
        }

        public void ApplyInvisibility()
        {
            throw new System.NotImplementedException();
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
            throw new System.NotImplementedException();
        }
    }
}