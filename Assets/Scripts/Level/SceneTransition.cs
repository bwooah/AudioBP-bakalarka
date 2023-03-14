using UnityEngine;
using UnityEngine.SceneManagement;

namespace Level
{
    public class SceneTransition:MonoBehaviour
    {
        public GameObject player;
        public GameObject walkingTo;
        [SerializeField] private string nextSceneName;
        [SerializeField] private string thisSceneName;

        private void Update()
        {
            // Check if the task is completed
            if (Vector3.Distance(player.transform.position, walkingTo.transform.position)<1)
            {
                // Load the next scene
                SceneManager.UnloadSceneAsync(thisSceneName);
                SceneManager.LoadScene(nextSceneName);
            }
        }
    }

}