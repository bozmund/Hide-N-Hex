using System;
using Scriptable_Objects.WorldTime;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using WorldTime;

namespace Day_Night_Cycle
{
    [RequireComponent(typeof(Light2D))]
    public class WorldLight : MonoBehaviour
    {
        private Light2D _light;

        [SerializeField] private WorldTime.WorldTime _worldTime;
        [SerializeField] private Gradient _gradient;

        public static float _percentOfDay;
        private static TimeSpan _timeSpan;
        private WorldTimeSO _worldTimeSO;

        private void Awake()
        {
            _light = GetComponent<Light2D>();
            _worldTime.WorldTimeChanged += OnWorldTimeChanged;
        }

        private void OnDestroy()
        {
            _worldTime.WorldTimeChanged -= OnWorldTimeChanged;
            _worldTimeSO.TimeSpan += _timeSpan;
        }

        private void OnWorldTimeChanged(object sender, TimeSpan newTime)
        {
            _percentOfDay = PercentOfDay(newTime);
            _light.color = _gradient.Evaluate(_percentOfDay);
        }

        private float PercentOfDay(TimeSpan timeSpan)
        {
            var realTimePassed = timeSpan.TotalMinutes + _worldTimeSO.TimeSpan.TotalMinutes;
            _percentOfDay = (float)(realTimePassed % WorldTimeConstant.MinutesInDay) / WorldTimeConstant.MinutesInDay;
            _timeSpan = timeSpan;
            return _percentOfDay;
        }
    }
}
