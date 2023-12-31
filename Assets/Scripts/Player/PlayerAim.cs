using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 mousePosition;
    private GameObject aim;
    void Start()
    {
        aim = GameObject.FindWithTag("Aim");
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        moveArrow();

    }
    void moveArrow()
    {
      
        Vector2 intersection = getCircleIntersection(transform.position.x, transform.position.y, mousePosition.x, mousePosition.y);
   
        aim.transform.position = new Vector3(intersection.x, intersection.y, 0);
        Debug.Log(aim.transform.position.y);



        float angle;
        float slope = (transform.position.x - mousePosition.x) / (transform.position.y - mousePosition.y);
        if (transform.position.y - mousePosition.y > 0)
        {
            angle = -Mathf.Rad2Deg * (Mathf.Atan(slope)) + 180; aim.transform.rotation = Quaternion.Euler(0, 0, angle);

            return;
        }
        angle = -Mathf.Rad2Deg * (Mathf.Atan(slope));
        aim.transform.rotation = Quaternion.Euler(0, 0, angle);




    }


    public Vector2 getCircleIntersection(float xM, float yM, float x, float y)
    {
        // Circle's center and radius
        float R = 1.2f;

        // External point

        // Calculate direction (dx, dy)
        float dx = x - xM;
        float dy = y - yM;

        // Calculate distance and unit vector
        float distance = (float)Mathf.Sqrt(dx * dx + dy * dy);
        float unit_dx = dx / distance;
        float unit_dy = dy / distance;

        // Calculate intersection point
        float intersect_x = xM + R * unit_dx;
        float intersect_y = yM + R * unit_dy;
        Debug.Log(intersect_y);
        return new Vector2 (intersect_x, intersect_y);

    }
}
