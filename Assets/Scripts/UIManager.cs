using UnityEngine;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("UI Screens")]
    public GameObject loginUI;
    public GameObject registerUI;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        // Always start on Login screen
        LoginScreen();
    }

    // Show Login UI (Back / After register)
    public void LoginScreen()
    {
        loginUI.SetActive(true);
        registerUI.SetActive(false);
    }

    // Show Register UI (Register button)
    public void RegisterScreen()
    {
        loginUI.SetActive(false);
        registerUI.SetActive(true);
    }
}
