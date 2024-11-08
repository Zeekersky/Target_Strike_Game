using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnvBehavior : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    public TargetController target;
    [SerializeField] List<TargetController> targets = new List<TargetController>();
    [SerializeField] Transform trainingGround;

    public void spawnAgent()
    {

        playerController.transform.localPosition = new Vector3(0f, 10f, 0f);
    }

    public void spawnTarget()
    {
        if (targets.Count > 0)
        {
            foreach (TargetController t in targets)
            {
                Destroy(t.gameObject);
            }
            targets.Clear();
        }
        TargetController new_target = Instantiate(target, trainingGround);
        // new_target.transform.localPosition = new Vector3(Random.Range(-173f, 173f), 10f, Random.Range(-173f, 173f));
        new_target.transform.localPosition = new Vector3(Random.Range(-150f, 150f), 10f, Random.Range(-150f, 150f));
        targets.Add(new_target);
    }
}
