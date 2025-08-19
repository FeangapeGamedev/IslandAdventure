using UnityEngine;
using RPG.Character;

namespace RPG.Quest
{
    [CreateAssetMenu(fileName = "Reward", menuName = "RPG/Reward", order = 2)]
    public class RewardSO : ScriptableObject
    {
        public float bonusHealth = 0f;
        public float bonusDamage = 0f;
        public int bonusPotions = 0;
        public bool forceWeaponsSwap = false;
        public Weapons weapon = Weapons.Sword;

    }
}
