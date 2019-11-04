using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameTimer : MonoBehaviour
{
    [SerializeField]
	private float timeRemaining;

    [SerializeField]
	private Text timerText;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = gameObject.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update () 
	{
        UpdateTime();

        if (timeRemaining <= 0)
        { 
            gameManager.GameOver();
		}
	}

    void UpdateTime()
    {
        timeRemaining = timeRemaining - Time.deltaTime;
        timerText.text = "TIME: " + (int)timeRemaining;
    }

    public void InreaseTime(float amount)
    {
        timeRemaining += amount;
    }
}
