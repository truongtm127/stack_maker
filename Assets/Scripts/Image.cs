using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Image : MonoBehaviour
{
    [SerializeField] private Player player;

    public float delay = 1.5f; // thời gian chờ trước khi chuyển sang scene tiếp theo
    private int currentSceneIndex = 0;
    private bool isPaused = false;
    public void RestartScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
    public void Paused()
    {
        // Đảo ngược giá trị của biến isPaused
        isPaused = !isPaused;

        // Bật hoặc tắt hành động của game tùy thuộc vào giá trị của biến isPaused
        if (isPaused)
        {
            Time.timeScale = 0; // Tạm dừng game
            player.isPause= true;
        }
        else
        {
            Time.timeScale = 1; // Tiếp tục game
            player.isPause = false;
        }
    }
    public void LoadNextScene()
    {
        // Tăng chỉ số scene và chuyển sang scene tiếp theo trong thứ tự
        currentSceneIndex++;
        if (currentSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(currentSceneIndex);
        }
        else
        {
            Debug.LogWarning("Không có scene tiếp theo để chuyển đến.");
        }
    }
    public void NextScene()
    {
        // Lấy chỉ số của scene hiện tại
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        // Chờ một thời gian rồi chuyển sang scene tiếp theo
        Invoke("LoadNextScene", delay);

    }
}
