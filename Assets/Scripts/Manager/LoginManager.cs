using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoginManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TMP_InputField usernameField;
    public TMP_InputField passwordField;
    public TextMeshProUGUI errorMessageText;
    public Button loginButton;
    public Button registerButton;

    [Header("Settings")]
    public string nextSceneName = "MainMenu";
    private bool isLoggingIn = false;

    void Start()
    {
        HideError();

        loginButton.onClick.AddListener(OnLoginClicked);
        if (registerButton != null)
            registerButton.onClick.AddListener(OnRegisterClicked);

    }

    public void OnLoginClicked()
    {
        if (isLoggingIn) return;
        isLoggingIn = true;

        string username = usernameField.text.Trim();
        string password = passwordField.text;

        HideError();
        CancelInvoke();

        if (string.IsNullOrEmpty(username))
        {
            ShowError("Inserisci l'username.");
            usernameField.Select();
            return;
        }

        if (string.IsNullOrEmpty(password))
        {
            ShowError("Inserisci la password.");
            passwordField.Select();
            return;
        }

        if (IsValidLogin(username, password))
        {
            Debug.Log($"Login riuscito! Benvenuto {username}.");
            ClearFields();
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            ShowError("Credenziali errate. Riprova.");
            passwordField.text = "";
            passwordField.Select();
        }
    }

    public void OnRegisterClicked()
    {
        ShowError("Funzione registrazione non ancora implementata.");
    }

    bool IsValidLogin(string username, string password)
    {
        return username == "admin" && password == "1234";
    }

    void ShowError(string message)
    {
        errorMessageText.text = message;
        errorMessageText.gameObject.SetActive(true);
        Invoke("HideError", 6f);
    }

    void HideError()
    {
        errorMessageText.gameObject.SetActive(false);
    }

    void ClearFields()
    {
        usernameField.text = "";
        passwordField.text = "";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if (usernameField.isFocused || passwordField.isFocused)
            {
                OnLoginClicked();
            }
        }
    }
}