using UnityEngine;
using UnityEngine.UI;

public class GameCompleteView : MonoBehaviour {
    [SerializeField] Text scoreText;

	public void SetScore(float score)
	{
        Debug.Log(score);
        scoreText.text = "I am knocked: " + score.ToString() + " M";
	}
}
