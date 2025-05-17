using Unity.Mathematics;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] private Transform carryPos;
    [SerializeField] float hitDistance = 5f; 
    private Vector3 inputDir;

    private bool isHitShelf;
    private bool isCarryingObject;
    private Collider playerCollider;
    RaycastHit hit;


    void Start()
    {
        playerCollider = GetComponent<Collider>();
    }

    void Update()
    {
        PlayerMovement();
        PickUpObject();
    }
    

    private void PlayerMovement () {

        float xMov = Input.GetAxisRaw("Horizontal");
        float zMov = Input.GetAxisRaw("Vertical");

        inputDir = new Vector3 (xMov, 0f, zMov);
        

        if (inputDir.magnitude >= 0.1f) {

            Quaternion rotation = Quaternion.LookRotation(inputDir);

            transform.position += inputDir * moveSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10f);
        }
   }

   private void PickUpObject() {

        if (Input.GetKeyDown(KeyCode.E)) {
            isHitShelf = Physics.BoxCast(playerCollider.bounds.center, transform.localScale / 2, transform.forward, out hit, transform.rotation, hitDistance);
            if (isHitShelf && !isCarryingObject)
            {
                isCarryingObject = true;
                Instantiate(hit.collider.GetComponent<Shelf>().shipPart, carryPos);
            }
        }

       
   }
}
