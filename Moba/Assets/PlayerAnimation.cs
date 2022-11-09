using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAnimation : MonoBehaviour
{
    NavMeshAgent _agent;
    [SerializeField] private Animator _anim;

    float motionSmoothTime = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        _agent = gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float speed = _agent.velocity.magnitude / _agent.speed;
        _anim.SetFloat("Speed", speed, motionSmoothTime, Time.deltaTime);
    }
}
