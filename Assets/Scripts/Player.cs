using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private LayerMask brickLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float speed = 5;
    [SerializeField] private Transform cube;
    [SerializeField] private Swipe swipeControl;

    private bool isMove = true;
    public bool isPause = false;
    // Start is called before the first frame update
    void Start()
    {
        cube.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
    }
    
    // Update is called once per frame
    void Update()
    {
        // Di chuyển
        Move();
        if (isMove == true || isPause)
        {
            return;
        }
        // Điều chỉnh hướng của Cube để bắn raycast
        if (Input.GetKey(KeyCode.LeftArrow) || swipeControl.SwipeLeft)
        {
            cube.rotation = Quaternion.Euler(new Vector3(0, -90, 0));
        }

        if (Input.GetKey(KeyCode.RightArrow) || swipeControl.SwipeRight)
        {
            cube.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
        }

        if (Input.GetKey(KeyCode.UpArrow) || swipeControl.SwipeForward)
        {
            cube.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }

        if (Input.GetKey(KeyCode.DownArrow) || swipeControl.SwipeBackward)
        {
            cube.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
    }
    public void Move()
    {
        // Bắn raycast theo hướng OZ của Cube layer là wallLayer trả về hit
        RaycastHit hit;
        Physics.Raycast(cube.position, cube.TransformDirection(Vector3.forward), out hit, 100f, wallLayer);
        if (Physics.Raycast(cube.position, cube.TransformDirection(Vector3.forward), out hit, 100f, wallLayer))
        {
            Debug.DrawRay(cube.position, cube.TransformDirection(Vector3.forward) * hit.distance, Color.red);
            //Debug.Log("Did Hit");
            // Vị trí di chuyển đến
            Vector3 target = transform.position + cube.TransformDirection(Vector3.forward) * (hit.distance - 0.5f);
            isMove = true;
            // Di chuyển
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            if (transform.position == target) isMove = false;
        }
        else
        {
            Debug.DrawRay(cube.position, cube.TransformDirection(Vector3.forward) * 1000, Color.white);
            //Debug.Log("Did not Hit");
        }
    }
}
