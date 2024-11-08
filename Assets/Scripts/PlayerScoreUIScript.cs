using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScoreUIScript : MonoBehaviour
{
    public PlayerController playerController;
    public Text playerScoreText;

    // Update is called once per frame
    void Update()
    {
        playerScoreText.text = "Player: " + playerController.totalRewardPlayer.ToString("F2");
    }
}
