using UnityEngine;
using System.Collections;

public class MainPlayer : MonoBehaviour
{
    public static MainPlayer Instance;

    public NavMeshAgent NavAgent;

    void Awake()
    {
        Instance = this;
        if (null == NavAgent) {
            NavAgent = GetComponent<NavMeshAgent>();
        }
    }

    public void SetPosition(Vector3 pos)
    {
        NavAgent.SetDestination(pos);
    }
}
