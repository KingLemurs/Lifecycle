using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitBehavior : MonoBehaviour
{
    public string nextScene;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hello");
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Exit();
        }
    }

    private void Exit()
    {
        SceneManager.LoadScene(nextScene);
    }
}
