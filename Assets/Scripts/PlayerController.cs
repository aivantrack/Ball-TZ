using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public UnityEvent spawnCoin; 
    public UnityEvent plusTime; 

    private NavMeshAgent navMeshAgent;

    [SerializeField] private float speed;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = speed;
    }

    private void Update()
    {
      /*  if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("ClickableObject"))
                {
                    MoveToPosition(hit.point);
                }
            }
        }*/
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("ClickableObject"))
                {
                    MoveToPosition(hit.point);
                }
            }
        }
    }

    private void MoveToPosition(Vector3 position)
    {
        navMeshAgent.SetDestination(position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coin") 
        {
            Destroy(other.gameObject);
            GameManager.Instance.CreateEffect(other.gameObject.transform.position, 3f);

            //GameManager.Instance.SpanwCoin();

            spawnCoin.Invoke();
            plusTime.Invoke();
        }
    }
}