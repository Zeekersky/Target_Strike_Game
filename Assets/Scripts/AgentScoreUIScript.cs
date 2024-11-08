using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AgentScoreUIScript : MonoBehaviour
{
    public AgentController agentController;
    public Text agentScoreText;

    // Update is called once per frame
    void Update()
    {
        agentScoreText.text = "Agent: " + agentController.totalReward.ToString("F2");
    }
}
