using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(SphereCollider))]

public class clickanddrag : MonoBehaviour
{
    public float speed = 1.0f;
    public float scale;
	public GameObject landMine;
    private Vector3 originalSize;
    public Vector3 point;
    private bool moved = false;
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
			var mousePos = Input.mousePosition;
            Destroy(gameObject);
            //put your conditions to trigger your event here
			Instantiate(landMine, mousePos, Quaternion.identity);
        }
        else{
            
            transform.position = point;
        }
    }
}