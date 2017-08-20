using UnityEngine;
using UnityEngine.UI;

public class GameCompleteView : MonoBehaviour {
    [SerializeField] Text scoreText;

	void Start () {
        scoreText.text = "I am knocked: -- M";
	}

	public void SetScore(float score)
	{
        scoreText.text = "I am knocked: " + score.ToString() + " M";
	}
}
