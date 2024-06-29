using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;
using WorldTime;

namespace Day_Night_Cycle
{
    [RequireComponent(typeof(Light2D))]
    public class WorldLight : MonoBehaviour
    {
        private Light2D _light;

        [SerializeField] private WorldTime.WorldTime _worldTime;
        [SerializeField] private Gradient _gradient;

        public static float percentOfDay;

        private void Awake()
        {
            _light = GetComponent<Light2D>();
            _worldTime.WorldTimeChanged += OnWorldTimeChanged;
            percentOfDay = 0f;
        }

        private void OnDestroy()
        {
            _worldTime.WorldTimeChanged -= OnWorldTimeChanged;
        }

        private void OnWorldTimeChanged(object sender, TimeSpan newTime)
        {
            percentOfDay = PercentOfDay(newTime);
            _light.color = _gradient.Evaluate(percentOfDay);
        }

        private float PercentOfDay(TimeSpan timeSpan)
        {
            percentOfDay = (float)(timeSpan.TotalMinutes % WorldTimeConstant.MinutesInDay) / WorldTimeConstant.MinutesInDay;
            return percentOfDay;
        }
    }
}
