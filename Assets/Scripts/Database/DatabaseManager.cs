using UnityEngine;
using Firebase;
using Firebase.Database;

public class DatabaseManager : MonoBehaviour
{
    private string userID;
    private DatabaseReference dbReference;
    private int counter = 0;
    private FirebaseApp app;
    
    // Start is called before the first frame update
    void Start()
    {
        
        //checks if google play services are up to date
        // FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
        //       var dependencyStatus = task.Result;
        //       if (dependencyStatus == DependencyStatus.Available) {
        //         
        //            app = FirebaseApp.DefaultInstance;
        //
        //         // Set a flag here to indicate whether Firebase is ready to use by your app.
        //       } else {
        //         Debug.LogError(System.String.Format(
        //           "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
        //         // Firebase Unity SDK is not safe to use here.
        //       }
        // });
        userID = SystemInfo.deviceUniqueIdentifier;
        
        createUser("mobiluppload111");
    }
    
    void Update(){
        // check for tap on mobile
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began){
            // get the direction the player is looking
            string name = "mobUp" + counter;
            createUser(name);
        }
    }
    // Update is called once per frame
    public void createUser(string name)
    {
        User newUser = new User(name);
        string json = JsonUtility.ToJson(newUser);
        FirebaseDatabase.DefaultInstance.RootReference.Child("users").Child(userID).SetRawJsonValueAsync(json);
        Debug.Log("?");
    }
}
