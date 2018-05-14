using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

[System.Serializable]
public class ServerResponse{
	public int status;
	public string message;

	public static ServerResponse CreateFromJSON(string jsonString){
		return JsonUtility.FromJson<ServerResponse> (jsonString);
	}
}

public class Login : MonoBehaviour {
	public InputField username;
	public InputField password;
	public Text responseText;
	public Button loginButton;
	public Button signupButton;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void LogIn(){
		StartCoroutine(LoginCoroutine());
	}

	public void SignUp(){
		StartCoroutine(SignUpCoroutine());
	}

	IEnumerator LoginCoroutine() {
		loginButton.enabled = false;
		signupButton.enabled = false;

		WWWForm form = new WWWForm();
		form.AddField("username", username.text);
		form.AddField ("password", password.text);

		UnityWebRequest www = UnityWebRequest.Post("https://aqueous-bayou-19925.herokuapp.com/users/login", form);
		yield return www.SendWebRequest();

		if(www.isNetworkError || www.isHttpError) {
			Debug.Log(www.error);
		}
		else {
			ServerResponse response = ServerResponse.CreateFromJSON (www.downloadHandler.text);
			if (response.status == 200) {
				responseText.color = Color.green;
				responseText.text = "Login success!";
			} else {
				responseText.color = Color.red;
				responseText.text = response.message;
			}
		}

		loginButton.enabled = true;
		signupButton.enabled = true;
	}

	IEnumerator SignUpCoroutine() {
		loginButton.enabled = false;
		signupButton.enabled = false;

		WWWForm form = new WWWForm();
		form.AddField("username", username.text);
		form.AddField ("password", password.text);

		UnityWebRequest www = UnityWebRequest.Post("https://aqueous-bayou-19925.herokuapp.com/users/signup", form);
		yield return www.SendWebRequest();

		if(www.isNetworkError || www.isHttpError) {
			Debug.Log(www.error);
		}
		else {
			ServerResponse response = ServerResponse.CreateFromJSON (www.downloadHandler.text);
			if (response.status == 200) {
				responseText.color = Color.green;
				responseText.text = "SignUp success!";
			} else {
				responseText.color = Color.red;
				responseText.text = response.message;
			}
		}

		loginButton.enabled = true;
		signupButton.enabled = true;
	}
}
