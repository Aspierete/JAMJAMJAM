using Unity.Mathematics;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    private Vector3 inputDir;
    // TO:DO
    // check player input 
    // move object by player input direction 

    void Update()
    {
        PlayerMovement();
    }


    private void PlayerMovement () {

        float xMov = Input.GetAxisRaw("Horizontal");
        float zMov = Input.GetAxisRaw("Vertical");

        inputDir = new Vector3 (xMov, 0f, zMov);

        Quaternion rotation = Quaternion.LookRotation(inputDir);

        if (inputDir.magnitude >= 0.1f) {
            transform.position += inputDir * moveSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10f);
        }
   }
}
