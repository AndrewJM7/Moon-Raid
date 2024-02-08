using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Alien : MonoBehaviour
{
    private StateMachine stateMachine;
    private NavMeshAgent navMeshAgent;
    public Animator animator;
    public Camera playerCamera;
    public Canvas canvas;

    // Getter for agent
    public NavMeshAgent Agent { get => navMeshAgent; }

    // For debugging
    [SerializeField]
    private string currentState;
    public Path path;
    private GameObject player;
    public float sightDistance = 20f;
    public float FOV = 85f;
    public float eyeHeight;

    // Start is called before the first frame update
    void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        stateMachine.Initialise();
        player = GameObject.FindGameObjectWithTag("Player");

        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }

        if (canvas == null)
        {
            canvas = GetComponentInChildren<Canvas>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        AvoidOtherAliens();

        PlayerInSight();
        currentState = stateMachine.activeState.ToString();

        // Check if the agent is currently moving
        bool isMoving = navMeshAgent.velocity.magnitude > 0.1f;

        // Update the "IsMoving" parameter in the Animator
        animator.SetBool("IsMoving", isMoving);

        if (canvas != null)
        {
            canvas.transform.LookAt(
                canvas.transform.position + playerCamera.transform.rotation * Vector3.forward,
                playerCamera.transform.rotation * Vector3.up
            );

            // Reverse the rotation to make the canvas face away from the camera
            canvas.transform.Rotate(0, 180, 0);
        }
    }

    public bool PlayerInSight()
    {
        if (player != null)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < sightDistance) 
            {
                Vector3 targetDirection = player.transform.position - transform.position - (Vector3.up * eyeHeight);
                float playerAngle = Vector3.Angle(targetDirection, transform.forward); // Angle to player from alien

                if (playerAngle >= -FOV && playerAngle <= FOV)
                {
                    Ray ray = new Ray(transform.position + (Vector3.up * eyeHeight), targetDirection);
                    RaycastHit hit = new RaycastHit();

                    if (Physics.Raycast(ray, out hit, sightDistance)) 
                    {
                        if (hit.transform.gameObject == player)
                        {
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }

void AvoidOtherAliens()
{
    float avoidanceRadius = 0.1f; 

    Collider[] hitColliders = Physics.OverlapSphere(transform.position, avoidanceRadius);

    foreach (Collider col in hitColliders)
    {
        if (col.CompareTag("Enemy") && col.gameObject != gameObject)
        {
            Vector3 avoidanceDirection = transform.position - col.transform.position;
            navMeshAgent.velocity += avoidanceDirection.normalized;
        }
    }
}
}

