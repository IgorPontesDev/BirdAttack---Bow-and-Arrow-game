using UnityEngine;
using System.Collections;

// this class steers the arrow and its behaviour


public class RotacaoDaFlecha : MonoBehaviour
{


    // References to GameObjects gset in the inspector
    public GameObject arrowHead;
    public GameObject bow;

    // Use this for initialization
    void Start()
    {
      
    }



    // Update is called once per frame
    void Update()
    {
        //this part of update is only executed, if a rigidbody is present
        // the rigidbody is added when the arrow is shot (released from the bowstring)
        if (transform.GetComponent<Rigidbody2D>() != null)
        {
            // do we fly actually?
            if (GetComponent<Rigidbody2D>().velocity != Vector2.zero)
            {
                // get the actual velocity
                Vector3 vel = GetComponent<Rigidbody2D>().velocity;
                // calc the rotation from x and y velocity via a simple atan2
                float angleZ = Mathf.Atan2(vel.y, vel.x) * Mathf.Rad2Deg;
                float angleY = Mathf.Atan2(vel.z, vel.x) * Mathf.Rad2Deg;
                // rotate the arrow according to the trajectory
                transform.eulerAngles = new Vector3(0, -angleY, angleZ);
            }
        }
    }


    //
    // void OnCollisionEnter(Collision other)
    //
    // other: the other object the arrow collided with
    //

    //
    // public void setBow
    //
    // set a reference to the main game object 

    public void setBow(GameObject _bow)
    {
        bow = _bow;
    }
}
