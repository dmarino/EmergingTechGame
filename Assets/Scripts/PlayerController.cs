using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{    
    [SerializeField]
    private float _moveOffset = 5f;

    Rigidbody rb;

    private void Start()
    {
          rb = gameObject.GetComponent<Rigidbody>();
    }
    private void Update(){

        float xVel = _moveOffset * Input.GetAxis("Horizontal");
        float zVel = _moveOffset * Input.GetAxis("Vertical");

        rb.velocity = new Vector3(xVel,0, zVel);
    }


    public void Action()
    {
        Debug.Log("i listen!");
    }


}
