using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using RPG.Core;
using RPG.Quest;
using RPG.Utility;
using System;

namespace RPG.Character
{
    public class NPCController : MonoBehaviour
    {
        private Canvas canvasCmp;
        public TextAsset inkJSON;
        public QuestItemSO desiredQuestItem;
        public Reward rewardCmp;
        public bool hasQuestItem = false;

        private void Awake()
        {
            canvasCmp = GetComponentInChildren<Canvas>();
            rewardCmp = GetComponent<Reward>();
        }

        private void Start()
        {
            if (PlayerPrefs.HasKey("NPCItems"))
            {
                List<string> npcItems = PlayerPrefsUtility.GetString("NPCItems");
                npcItems.ForEach(CheckNPCQuestItem);
            }
        }

        private void OnTriggerEnter()
        {
            canvasCmp.enabled = true;
        }

        private void OnTriggerExit()
        {
            canvasCmp.enabled = false;
        }

        public void HandleInteract(InputAction.CallbackContext context)
        {
            if (!context.performed || !canvasCmp.enabled) return;

            if (inkJSON == null)
            {
                Debug.LogWarning("NPC is missing an ink file");
            }

            EventManager.RaiseInitiateDialogue(inkJSON, gameObject);
        }

        public bool CheckPlayerForQuestItem()
        {
            if (hasQuestItem) return true;
            Inventory inventoryCmp = GameObject.FindGameObjectWithTag(Constants.PLAYER_TAG).GetComponent<Inventory>();

            hasQuestItem = inventoryCmp.HasItem(desiredQuestItem);

            if (rewardCmp != null && hasQuestItem)
            {
                rewardCmp.SendReward();
            }

            return hasQuestItem;
        }

        private void CheckNPCQuestItem(string itemName)
        {
            if (itemName == desiredQuestItem.itemName)
            {
                hasQuestItem = true;
            }
        }

    }
}

