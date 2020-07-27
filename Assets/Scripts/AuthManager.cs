using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Database;
using Firebase.Unity.Editor;

using Photon.Pun.Demo.Cockpit.Forms;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Runtime.ExceptionServices;

public class AuthManager : MonoBehaviour
{
    class userData {
        public string email;
        public string pw;
        

        public userData(string email, string pw) {
            this.email = email;
            this.pw = pw;
        }
    }

    class userGame {
        public bool myTurn;
        public int single;
        public int seconds;
        public int triple;
        public int fours;
        public int fives;
        public int sixes;
        public int chance;
        public int fullhouse;
        public int fourOfkind;
        public bool smallStraight;
        public bool largeStraight;
        public bool yacht;

        public userGame(bool myTurn, int single, int seconds, int triple, int fours, int fives, int sixes, int chance, int fullhouse, int fourOfkind, bool smallStraight, bool largeStraight, bool yacht) {
            this.myTurn = myTurn;
            this.single = single;
            this.seconds = seconds;
            this.triple = triple;
            this.fours = fours;
            this.fives = fives;
            this.sixes = sixes;
            this.chance = chance;
            this.fullhouse = fullhouse;
            this.fourOfkind = fourOfkind;
            this.smallStraight = smallStraight;
            this.largeStraight = largeStraight;
            this.yacht = yacht;
        }
    
    }


    public bool IsFirebaseReady { get; private set; }
    public bool IsSignInOnProgress { get; private set; }
    public bool IsSignUpOnProgress { get; private set; }

    public InputField emailField;
    public InputField passwordField;
    public Button signInButton;
    public Button singUPButton;

    public static FirebaseDatabase firebaseDatabase;
    public static FirebaseApp firebaseApp;
    public static FirebaseAuth firebaseAuth;
    public static DatabaseReference reference;

    public static FirebaseUser User;

    public void Start()
    {
        DontDestroyOnLoad(this.gameObject);
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
                firebaseDatabase = FirebaseDatabase.DefaultInstance;
                firebaseApp.SetEditorDatabaseUrl("https://yachtdiecbackend.firebaseio.com/");
                reference = firebaseDatabase.RootReference;
                
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
                    GameObject.Find("Canvas").transform.Find("LS").gameObject.SetActive(true);
                    Debug.LogError(task.Exception);
                    
                }
                else if (task.IsCanceled)
                {
                    Debug.LogError("It's canceled");
                }
                else
                {
                    User = task.Result;
                    Debug.Log(User.UserId);
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
                    Debug.Log(task.Result.UserId);
                    writeNewUser(task.Result.UserId, emailField.text, passwordField.text);
                    //wirteNewStat(task.Result.UserId);
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

    private void writeNewUser(string id, string email, string pw)
    {
        userData user = new userData(email, pw);
        string json = JsonUtility.ToJson(user);

        reference.Child("users").Child(id).SetRawJsonValueAsync(json);
    }

    /*private void wirteNewStat(string id) {
        userGame user = new userGame(false, 0, 0, 0, 0, 0, 0, 0, 0, 0, false, false, false);
        string json = JsonUtility.ToJson(user);

        reference.Child("usrGameStat").Child(id).SetRawJsonValueAsync(json);
    }*/


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