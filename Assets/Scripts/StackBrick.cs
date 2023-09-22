using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackBrick : MonoBehaviour
{
    [SerializeField] private GameObject brick;
    [SerializeField] private Transform people;
    [SerializeField] private GameObject canvasFinish;
    [SerializeField] private Player player;

    Stack<GameObject> stackBrick = new Stack<GameObject>();
    private int countBrick = 0;
    Vector3 brick_y = new Vector3(0, 0.25f, 0);
    private Transform transformPeople;
    private void Start()
    {
        transformPeople = people.transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Va chạm với Brick
        if (other.gameObject.tag == "Brick")
        {
            // Thêm gạch
            AddBrick(other);
        }
        // Va chạm với Bridge
        if (other.gameObject.tag == "Bridge")
        {
            // Xóa gạch
            RemoveBrick(other);
        }
        if (other.gameObject.tag == "CubeFinish")
        {
            // Xóa gạch
            clearBrick();
            TurnOnFinishLevel();
        }
    }

    private void AddBrick(Collider other)
    {
        other.transform.SetParent(transform.parent);// Viên gạch thành con Player
        other.transform.position = transform.position + brick_y * countBrick; // Đặt vị trí viên gạch
        other.transform.rotation = Quaternion.Euler(new Vector3(-90, 90, 90));
        people.transform.position = people.transform.position + brick_y; // Đặt vị trí people
        countBrick++;
        other.GetComponent<BoxCollider>().enabled = false; // Tắt component box collider
        stackBrick.Push(other.gameObject); // Thêm gạch vào stack
    }
    private void RemoveBrick(Collider other)
    {
        // Debug.Log("hit");
        Destroy(stackBrick.Pop()); // Xóa gạch từ stack
        // Tạo gạch trên cầu
        Instantiate(brick, new Vector3(other.transform.position.x, 2.55f, other.transform.position.z), Quaternion.Euler(new Vector3(-90, 0, 0)));
        people.transform.position -= brick_y; // Đặt vị trí people
        countBrick--;
        Destroy(other.gameObject); // Xóa tấm vàng trên cầu
    }
    private void clearBrick()
    {
        while(countBrick > 1)
        {
            Destroy(stackBrick.Pop()); // Xóa gạch từ stack
            people.transform.position -= brick_y; // Đặt vị trí people
            countBrick--;
        }
    }
    public void TurnOnFinishLevel()
    {
        canvasFinish.SetActive(true);
        player.isPause = true;
    }

    private void OnTriggerExit(Collider other)
    {

    }
}
