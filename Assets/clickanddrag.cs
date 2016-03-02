using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(SphereCollider))]

public class clickanddrag : MonoBehaviour
{
    public float speed = 1.0f;
    public float scale;
	public GameObject landMine;
    public HeartAttack ha;
    private Vector3 originalSize;
    private Vector3 originalPos;
    public Vector3 point;
    private bool moved = false;

    void Start()
    {
        originalPos = gameObject.transform.position;
    }

    void OnMouseDown()
    {
        if (!moved)
        {
            UnityEngine.Cursor.visible = false;
            originalSize = gameObject.transform.localScale;
            gameObject.transform.localScale = originalSize * scale;
        }

    }
    void OnMouseDrag()
    {
        if (!moved)
        {
            point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            point.z = gameObject.transform.position.z;
            gameObject.transform.position = point;
            
        }

    }

    void OnMouseUp()
    {
        moved = true;
        UnityEngine.Cursor.visible = true;
        if (gameObject.tag != "mine")
        {
            if (gameObject.tag == "heartattack") //TODO: move this logic outside
            {
                bool killed = ha.activate(point);
                if (killed) {
                    Destroy(gameObject);
                } else
                {
                    //MOVE BACK TO ORIGINAL SPOT
                    moved = false;
                    gameObject.transform.localScale = originalSize;
                    gameObject.transform.position = originalPos;
                }
            }
        }
        else{
			Destroy (gameObject);
			Instantiate(landMine, point, Quaternion.identity);
            //transform.position = point;
        }
    }
}