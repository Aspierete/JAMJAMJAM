using Unity.Mathematics;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject carryingShipPart;
    [SerializeField] float moveSpeed;
    [SerializeField] private Transform carryPos;
    [SerializeField] float hitDistance = 5f;
    [SerializeField] LayerMask shelfLayer;
    [SerializeField] LayerMask customerLayer;
    [SerializeField] LayerMask trashLayer;

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
        DeliverShipPart();
        ThrowTrash();
    }


    private void PlayerMovement()
    {

        float xMov = Input.GetAxisRaw("Horizontal");
        float zMov = Input.GetAxisRaw("Vertical");

        inputDir = new Vector3(xMov, 0f, zMov);


        if (inputDir.magnitude >= 0.1f)
        {
            Quaternion rotation = Quaternion.LookRotation(inputDir);

            transform.position += inputDir * moveSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10f);
        }
    }

    private void PickUpObject()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isHitShelf = Physics.BoxCast(playerCollider.bounds.center, transform.localScale / 2, transform.forward, out hit, transform.rotation, hitDistance, shelfLayer);
            if (isHitShelf && !isCarryingObject)
            {
                isCarryingObject = true;
                carryingShipPart = Instantiate(hit.collider.GetComponent<Shelf>().shipPart, carryPos);
            }
        }
    }

    private void DeliverShipPart()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            bool isHitCustomer = Physics.BoxCast(playerCollider.bounds.center, transform.localScale / 2, transform.forward, out hit, transform.rotation, hitDistance, customerLayer);
            if (isHitCustomer && isCarryingObject)
            {
                hit.collider.GetComponent<Customer>().CheckDelivery(carryingShipPart.GetComponent<ShipPart>());
                Destroy(carryingShipPart);
                isCarryingObject = false;
            }
        }
    } 
    private void ThrowTrash() {
        if (Input.GetKeyDown(KeyCode.E))
        {
            bool isHitTrash = Physics.BoxCast(playerCollider.bounds.center, transform.localScale / 2, transform.forward, out hit, transform.rotation, hitDistance, trashLayer);
            if (isHitTrash && isCarryingObject)
            {
                print("throw trash");
                Destroy(carryingShipPart);
                isCarryingObject = false;
            }
        }
   } 
}
