using System;
using UnityEngine;
using UnityEngine.InputSystem;
using RPG.Utility;

namespace RPG.Character
{
    public class Combat : MonoBehaviour
    {
        [NonSerialized] public float damage = 0f;
        [NonSerialized] public bool isAttacking = false;
        private BubbleEvent bubbleEventCmp;
        private Animator animatorCmp;
        private void Awake()
        {
            animatorCmp = GetComponentInChildren<Animator>();
            bubbleEventCmp = GetComponentInChildren<BubbleEvent>();
        }

        private void OnEnable()
        {
            bubbleEventCmp.OnBubbleStartAttack += HandleBubbleStartAttack;
            bubbleEventCmp.OnBubbleCompleteAttack += HandleBubbleCompleteAttack;
            bubbleEventCmp.OnBubbleHit += HandleBubbleOnHit;
        }

        private void OnDisable()
        {
            bubbleEventCmp.OnBubbleStartAttack -= HandleBubbleStartAttack;
            bubbleEventCmp.OnBubbleCompleteAttack -= HandleBubbleCompleteAttack;
            bubbleEventCmp.OnBubbleHit -= HandleBubbleOnHit;
        }

        public void HandleAttack(InputAction.CallbackContext context)
        {
            if (!context.performed) return;

            StartAttack();
        }

        public void StartAttack()
        {
            if (isAttacking) return;

            animatorCmp.SetFloat(Constants.SPEED_ANIMATOR_PARAM, 0);
            animatorCmp.SetTrigger(Constants.ATTACK_ANIMATOR_PARAM);
        }

        private void HandleBubbleStartAttack()
        {
            isAttacking = true;
        }

        private void HandleBubbleCompleteAttack()
        {
            isAttacking = false;
        }

        private void HandleBubbleOnHit()
        {
            RaycastHit[] targets = Physics.BoxCastAll(
                transform.position + transform.forward,
                transform.localScale / 2,
                transform.forward,
                transform.rotation,
                1f
            );

            foreach (RaycastHit target in targets)
            {
                if (CompareTag(target.transform.tag)) continue;

                Health healthCmp = target.transform.gameObject.GetComponent<Health>();

                if (healthCmp == null) continue;

                healthCmp.TakeDamage(damage);
            }
        }

        public void CancelAttack()
        {
            animatorCmp.ResetTrigger(Constants.ATTACK_ANIMATOR_PARAM);
        }
    }
}
