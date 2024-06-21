using Player;
using UnityEngine;

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
            
        }

        public void ApplyRecall()
        {
            throw new System.NotImplementedException();
        }

        public void ApplyPurification()
        {
            throw new System.NotImplementedException(); 
        }

        public void ApplyParalyticGas()
        {
            throw new System.NotImplementedException();
        }

        public void ApplyMindVision()
        {
            throw new System.NotImplementedException();
        }

        public void ApplyMight()
        {
            throw new System.NotImplementedException();
        }

        public void ApplyLowerSus()
        {
            throw new System.NotImplementedException();
        }

        public void ApplyLiquidFlame()
        {
            throw new System.NotImplementedException();
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

        public void ApplyHealing()
        {
            throw new System.NotImplementedException();
        }

        public void ApplyConfusion()
        {
            throw new System.NotImplementedException();
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