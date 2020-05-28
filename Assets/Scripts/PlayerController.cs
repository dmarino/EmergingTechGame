using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{    
    [SerializeField]
    private float _moveOffset = 10f;

    [SerializeField]
    private float _radiusOfInteraction = 10f;


    public LayerMask chestLayer;

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


    public void Interact(string word)
    {

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _radiusOfInteraction, chestLayer);

        for(int i=0;i<hitColliders.Length;i++){

            if(hitColliders[i].tag.Equals("chest")){
               hitColliders[i].gameObject.GetComponent<ChestController>().tryOpen(word);
            }
        }

    }


}
