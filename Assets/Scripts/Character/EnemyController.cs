using System;
using System.Collections.Generic;
using UnityEngine;
using RPG.Utility;
using RPG.Core;

namespace RPG.Character
{
    public class EnemyController : MonoBehaviour
    {
        [NonSerialized] public float distanceFromPlayer;
        [NonSerialized] public Vector3 originalPosition;
        [NonSerialized] public Movement movementCmp;
        [NonSerialized] public GameObject player;
        [NonSerialized] public Patrol patrolCmp;
        [NonSerialized] public Combat combatCmp;
        [NonSerialized] public bool hasUIOpenend = false;
        public string enemyID = "";
        private Health healthCmp;
        public CharacterStatsSO stats;
        public float chaseRange = 2.5f;
        public float attackRange = 0.75f;


        private AIBaseState currentState;
        public AIReturnState returnState = new AIReturnState();
        public AIChaseState chaseState = new AIChaseState();
        public AIAttackState attackState = new AIAttackState();
        public AIPatrolState patrolState = new AIPatrolState();
        public AIDefeatedState defeatedState = new AIDefeatedState();

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        private void Awake()
        {
            if (stats == null)
            {
                Debug.LogWarning($"{name} does not have stats SO");
            }

            if (enemyID.Length == 0)
            {
                Debug.LogWarning($"{name} does not have an ID");
            }

            currentState = returnState;

            player = GameObject.FindWithTag(Constants.PLAYER_TAG);

            movementCmp = GetComponent<Movement>();
            patrolCmp = GetComponent<Patrol>();
            healthCmp = GetComponent<Health>();
            combatCmp = GetComponent<Combat>();

            originalPosition = transform.position;
        }

        /// <summary>
        /// Start is called on the frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>
        private void Start()
        {
            currentState.EnterState(this);
            healthCmp.healthPoints = stats.health;
            combatCmp.damage = stats.damage;

            if (healthCmp.sliderCmp != null)
            {
                healthCmp.sliderCmp.maxValue = stats.health;
                healthCmp.sliderCmp.value = stats.health;
            }

            List<string> enemiesDefeated = PlayerPrefsUtility.GetString("EnemiesDefeated");

            enemiesDefeated.ForEach((ID) =>
            {
                if (ID == enemyID)
                {
                    Destroy(gameObject);
                }
            });
        }

        private void OnEnable()
        {
            healthCmp.OnStartDefeated += HandleStartDefeated;
            EventManager.OnToggleUI += HandleToggleUI;
        }

        private void OnDisable()
        {
            healthCmp.OnStartDefeated -= HandleStartDefeated;
            EventManager.OnToggleUI -= HandleToggleUI;
        }

        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        private void Update()
        {
            CalculateDistanceFromPlayer();
            currentState.UpdateState(this);
        }

        public void SwitchState(AIBaseState newState)
        {
            currentState = newState;
            currentState.EnterState(this);
        }

        private void CalculateDistanceFromPlayer()
        {
            if (player == null) return;

            Vector3 enemyPosition = transform.position;
            Vector3 playerPosition = player.transform.position;

            distanceFromPlayer = Vector3.Distance(enemyPosition, playerPosition);
        }

        private void HandleStartDefeated()
        {
            SwitchState(defeatedState);
            currentState.EnterState(this);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseRange);
        }

        private void HandleToggleUI(bool isOpened)
        {
            hasUIOpenend = isOpened;
        }
    }
}

