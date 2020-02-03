using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class MagicOrchestraBuilderManager : MonoBehaviour
{
	// Singleton of the MagicOrchestraBuilderManager class
	public static MagicOrchestraBuilderManager singleton = null;

    // Cameras
	public GameObject frontalCamera;
	public GameObject zenithCamera;
	public GameObject controllerCamera;


	/* <summary>
     * The function is called when the component is instantiated
     * </summary>
     */
	void Awake()
	{
		//Code to manage the singleton uniqueness
		if ((singleton != null) && (singleton != this))
		{
			Destroy(gameObject);
			return;
		}
		singleton = this;

		DontDestroyOnLoad(this.gameObject);
	}

	// Start is called before the first frame update
	void Start()
    {
        this.ActivateAllCameras();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) == true)
            this.ChangeDisplay();
        if (Input.GetKeyDown(KeyCode.Escape) == true)
            this.QuitGame();
    }

    public void ActivateAllCameras()
    {
        this.CameraActivator(this.frontalCamera);
        this.CameraActivator(this.zenithCamera);
        this.CameraActivator(this.controllerCamera);
    }

    private void CameraActivator(GameObject gameObject)
    {
        gameObject.gameObject.SetActive(true);
        gameObject.GetComponent<Camera>().enabled = true;
    }

    private void ChangeDisplay()
    {
        int hint;
        hint = this.controllerCamera.GetComponent<Camera>().targetDisplay;        
        this.controllerCamera.GetComponent<Camera>().targetDisplay = this.frontalCamera.GetComponent<Camera>().targetDisplay;
        this.frontalCamera.GetComponent<Camera>().targetDisplay = hint;
    }

    private void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
