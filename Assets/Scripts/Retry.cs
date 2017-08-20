using UnityEngine;
using UnityEngine.SceneManagement;

public class Retry : MonoBehaviour {
    public void OnRetryButtonClicked(){
        SceneManager.LoadScene("Main");
    }
}
