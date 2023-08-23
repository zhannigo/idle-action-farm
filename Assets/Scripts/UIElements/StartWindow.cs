using UnityEngine;
using UnityEngine.UI;

public class StartWindow : MonoBehaviour
{
    [SerializeField] private GameObject GameHUD;
    [SerializeField] private Button _button;

    void Start()
    {
        Time.timeScale = 0.0f;
        GameHUD.SetActive(false);
        _button.onClick.AddListener(PlayGame);
    }

    private void PlayGame()
    {
        Time.timeScale = 1.0f;
        GameHUD.SetActive(true);
        gameObject.SetActive(false);
    }
}
