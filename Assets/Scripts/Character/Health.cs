using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using RPG.Utility;
using RPG.Core;
using UnityEngine.Events;

namespace RPG.Character
{
    public class Health : MonoBehaviour
    {
        public event UnityAction OnStartDefeated = () => { };
        [NonSerialized] public float healthPoints = 0f;
        [NonSerialized] public int potionCount = 1;
        [NonSerialized] public Slider sliderCmp;
        [SerializeField] private float healAmount = 15f;

        private Animator animatorCmp;
        private bool isDefeated = false;
        private BubbleEvent bubbleEventCmp;

        private void Awake()
        {
            animatorCmp = GetComponentInChildren<Animator>();
            bubbleEventCmp = GetComponentInChildren<BubbleEvent>();
            sliderCmp = GetComponentInChildren<Slider>();
        }


        private void OnEnable()
        {
            bubbleEventCmp.OnBubbleCompleteDefeat += HandleBubbleCompleteDefeat;
        }

        private void OnDisable()
        {
            bubbleEventCmp.OnBubbleCompleteDefeat -= HandleBubbleCompleteDefeat;
        }

        private void Start()
        {
            if (CompareTag(Constants.PLAYER_TAG))
            {
                EventManager.RaiseChangePlayerPotions(potionCount);
            }
        }

        public void TakeDamage(float damageAmount)
        {
            healthPoints = MathF.Max(healthPoints - damageAmount, 0);

            if (CompareTag(Constants.PLAYER_TAG))
            {
                EventManager.RaiseChangePlayerhealth(healthPoints);
            }

            if (sliderCmp != null)
            {
                sliderCmp.value = healthPoints;
            }

            if (healthPoints == 0)
            {
                Defeated();
            }
        }

        public void Defeated()
        {
            if (isDefeated) return;

            if (CompareTag(Constants.ENEMY_TAG))
            {
                OnStartDefeated.Invoke();
            }

            isDefeated = true;
            animatorCmp.SetTrigger(Constants.DEFEATED_ANIMATOR_PARAM);
        }

        private void HandleBubbleCompleteDefeat()
        {
            if (CompareTag(Constants.PLAYER_TAG))
            {
                EventManager.RaiseGameOver();
            }

            if (CompareTag(Constants.BOSS_TAG))
            {
                EventManager.RaiseVictory();
            }
            Destroy(gameObject);
        }

        public void HandleHeal(InputAction.CallbackContext context)
        {
            if (!context.performed || potionCount == 0) return;

            potionCount--;
            healthPoints += healAmount;

            EventManager.RaiseChangePlayerhealth(healthPoints);
            EventManager.RaiseChangePlayerPotions(potionCount);
        }
    }
}