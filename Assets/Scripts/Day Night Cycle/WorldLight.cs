using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace WorldTime
{
    [RequireComponent(typeof(Light2D))]
    public class WorldLight : MonoBehaviour
    {
        private Light2D _light;

        [SerializeField] private WorldTime _worldTime;
        [SerializeField] private Gradient _gradient;

        private float _percentOfDay;

        private void Awake()
        {
            _light = GetComponent<Light2D>();
            _worldTime.WorldTimeChanged += OnWorldTimeChanged;
            _percentOfDay = 0f;
        }

        private void OnDestroy()
        {
            _worldTime.WorldTimeChanged -= OnWorldTimeChanged;
        }

        private void OnWorldTimeChanged(object sender, TimeSpan newTime)
        {
            _percentOfDay = PercentOfDay(newTime);
            _light.color = _gradient.Evaluate(_percentOfDay);
        }

        private float PercentOfDay(TimeSpan timeSpan)
        {
            return (float)(timeSpan.TotalMinutes % WorldTimeConstant.MinutesInDay) / WorldTimeConstant.MinutesInDay;
        }
    }
}
