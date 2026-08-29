using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalArea : MonoBehaviour
{
    [SerializeField]
    private string nextSceneName;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
