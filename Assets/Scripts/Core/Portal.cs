using UnityEngine;
using RPG.Utility;

namespace RPG.Core
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] private int nextSceneIndex;
        private Collider colliderCmp;
        public Transform spawnPoint;
        private void Awake()
        {
            colliderCmp = GetComponent<Collider>();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(Constants.PLAYER_TAG)) return;

            colliderCmp.enabled = false;

            EventManager.RaisePortalEnter(other, nextSceneIndex);
            
            StartCoroutine(SceneTransition.Initiate(nextSceneIndex));
        }
    }
}
