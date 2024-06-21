using System;

namespace Player
{
    public class Effect
    {
        public string name;
        public float duration;
        public Action<PlayerMovement> Apply;
        public Action<PlayerMovement> End;

        public Effect(string name, float duration, Action<PlayerMovement> apply, Action<PlayerMovement> end)
        {
            this.name = name;
            this.duration = duration;
            this.Apply = apply;
            this.End = end;
        }
    }
}