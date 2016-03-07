using UnityEngine;
using System.Collections;

public class shooter : MonoBehaviour {
    
    public float gunRangex = 10.0f;
    public float gunRangey = 5.0f;
    public bool gun;
    public bool berserk=false;
    
    // Use this for initialization
    void Start () {
        giveGun();
	}

    // Update is called once per frame
    void Update() {


    }

    void giveGun() {
        float num = Random.value;
        if (num < .3f)
        {
            Debug.Log(num);
            gun = true;
            gameObject.GetComponent<BoxCollider2D>().size = new Vector2( gunRangex * gameObject.GetComponent<BoxCollider2D>().size.x, gunRangey * gameObject.GetComponent<BoxCollider2D>().size.y) ;
        }
        else
        {
            gun = false;
        }
    }

    void OnCollisionEnter2D(Collision2D coll)       //In order for this to work, must turn off isKinematic, must set the mass and gravity to zero, and fix all constraints on the rigidbody
    {
        Debug.Log("I ran into a dude");
        if (berserk && coll.gameObject.tag == "man") //Kill any man i run into while berserk
        {
            Destroy(coll.gameObject);
        }
        if (gun && coll.gameObject.GetComponent<shooter>().berserk)  //if i run into a berserk guy and i have a gun, kill him
        {
            Destroy(coll.gameObject);
        }
            

    }
}