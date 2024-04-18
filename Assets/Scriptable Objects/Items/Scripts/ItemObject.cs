using UnityEngine;

namespace Scriptable_Objects.Items.Scripts
{
    public abstract class ItemObject : ScriptableObject
    {
        public GameObject prefab;
        public ItemType itemType;
    }
}