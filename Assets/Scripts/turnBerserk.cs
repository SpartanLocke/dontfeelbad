using UnityEngine;
using System.Collections;

public class turnBerserk : MonoBehaviour
{

    public int radius = 25;
    public GameObject BrokenHeart;

    public bool activate(Vector3 point)
    {
        Debug.Log("activate called, point x: " + point.x + ", y: " + point.y);
        GameObject[] men = GameObject.FindGameObjectsWithTag("man");
        foreach (GameObject man in men)
        {
            if (man != null)
            {
                float distanceSqr = (point - man.transform.position).sqrMagnitude;
                if (distanceSqr < radius)
                {
                    //man.GetComponent<shooter>().berserk = true;
                    Instantiate(BrokenHeart, point, Quaternion.identity);
                    
                    return true;
                }
            }
        }
        return false;
    }
}
