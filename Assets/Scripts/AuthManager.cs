using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using Photon.Pun.Demo.Cockpit.Forms;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AuthManager : MonoBehaviour
{
    public bool IsFirebaseReady { get; private set; }
    public bool IsSignInOnProgress { get; private set; }
    public bool IsSignUpOnProgress { get; private set; }

    public InputField emailField;
    public InputField passwordField;
    public Button signInButton;
    public Button singUPButton;

    public static FirebaseApp firebaseApp;
    public static FirebaseAuth firebaseAuth;

    public static FirebaseUser User;

    public void Start()
    {
        signInButton.interactable = false;

        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            var result = task.Result;

            if (result != DependencyStatus.Available)
            {
                Debug.LogError(result.ToString());
                IsFirebaseReady = false;
            }
            else
            {
                IsFirebaseReady = true;

                firebaseApp = FirebaseApp.DefaultInstance;
                firebaseAuth = FirebaseAuth.DefaultInstance;
            }

            signInButton.interactable = IsFirebaseReady;
        });
    }

    public void SignIn()
    {
        if (emailField.text.Length == 0 || passwordField.text.Length == 0) return;
        if (!IsFirebaseReady || IsSignInOnProgress || IsSignUpOnProgress ||User != null ) return;

        IsSignInOnProgress = true;
        signInButton.interactable = false;

        firebaseAuth.SignInWithEmailAndPasswordAsync(emailField.text, passwordField.text).ContinueWithOnMainThread(
            task =>
            {
                Debug.Log($"Sign in status : {task.Status}");

                IsSignInOnProgress = false;
                signInButton.interactable = true;

                if (task.IsFaulted)
                {
                    Debug.LogError(task.Exception);
                }
                else if (task.IsCanceled)
                {
                    Debug.LogError("It's canceled");
                }
                else
                {
                    User = task.Result;
                    SceneManager.LoadScene("Lobby");
                }
            });
    }

    public void SingUp() {
        if (emailField.text.Length == 0 || passwordField.text.Length == 0) return;
        if (!IsFirebaseReady || IsSignInOnProgress || IsSignUpOnProgress || User != null) return;

        firebaseAuth.CreateUserWithEmailAndPasswordAsync(emailField.text, passwordField.text).ContinueWithOnMainThread(
            task =>
            {
                if (task.IsCanceled)
                {
                    Debug.LogError("Create Cancled");
                }
                else if (task.IsFaulted)
                {
                    Debug.LogError(task.Exception);
                }
                else {
                    GameObject.Find("Canvas").transform.Find("SignUpComp").gameObject.SetActive(true);
                }

            });
    
    }

    public void ActiveLoginUI() {
        GameObject.Find("Canvas").transform.Find("LS").gameObject.SetActive(true);
    }

    public void UnactiveLoginUI() { 
        GameObject.Find("Canvas").transform.Find("LS").gameObject.SetActive(false); 
    }


    //나중에 구글플레이 연동시 사용할 코드

    /* public void googleSignIn() {
         if (!IsFirebaseReady || IsSignInOnProgress || User != null) return;

         IsSignInOnProgress = true;
         signInButton.interactable = false;

         Firebase.Auth.Credential credential = Firebase.Auth.GoogleAuthProvider.GetCredential(googleIdToken, googleAccessToken);
         { }
     }

     */
}