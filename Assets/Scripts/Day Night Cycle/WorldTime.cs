using System;
using System.Collections;
using UnityEngine;

namespace WorldTime
{
    public class WorldTime : MonoBehaviour
    {
        public event EventHandler<TimeSpan> WorldTimeChanged;
        [SerializeField] private float _dayLength;
        public TimeSpan _currentTime;
        private float _minuteLength => _dayLength / WorldTimeConstant.MinutesInDay;

        private void Start()
        {
            StartCoroutine(UpdateTime());
        }

        private IEnumerator UpdateTime()
        {
            while (true)
            {
                yield return new WaitForSeconds(_minuteLength * WorldTimeConstant.SecondInDay);
                AddMinute();
            }
        }

        private void AddMinute()
        {
            _currentTime = _currentTime.Add(TimeSpan.FromMinutes(1));
            if (_currentTime.TotalMinutes >= WorldTimeConstant.MinutesInDay)
            {
                _currentTime = TimeSpan.Zero;
            }
            WorldTimeChanged?.Invoke(this, _currentTime);
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }
    }
}