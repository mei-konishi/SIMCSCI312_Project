using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public GameObject levelButtonContainer;
    public GameObject levelPanelContainer;
    public GameObject levelPanel;
    public string gameSceneName = "DoubleMergedScene";

    private Transform cameraTranform;
    private Transform cameraFinalLookAt;
    private const float CAMERA_ROTATION_SPEED = 3.0f;

    public Button[] levelButtons;

    public void Start()
    {
        cameraTranform = Camera.main.transform;

        // Creates a set of buttons with images and a loadscene function
        
        Sprite[] thumbnails = Resources.LoadAll<Sprite>("Levels");
        foreach(Sprite thumbnail in thumbnails)
        {
            GameObject container = Instantiate(levelPanel) as GameObject;
            container.transform.Find("Image").GetComponent<Image>().sprite = thumbnail;
            //container.GetComponent<Image> ().sprite = thumbnail;
            container.transform.SetParent(levelPanelContainer.transform, false);

            string sceneName = thumbnail.name;
            container.name = sceneName;
            container.transform.Find("StartLevelButton").GetComponent<Button>().onClick.AddListener(() => LoadLevel(gameSceneName));
            container.transform.Find("ClosePanelButton").GetComponent<Button>().onClick.AddListener(() => CloseLevelPanel(sceneName));

            levelButtonContainer.transform.Find(sceneName).GetComponent<Button>().onClick.AddListener(() => OpenLevelPanel(sceneName));
        }
        CheckStagesUnlocked();
    }

    public void Update()
    {
        // Camera rotation speed adjust here
        if (cameraFinalLookAt != null)
        {
            cameraTranform.rotation = Quaternion.Slerp(cameraTranform.rotation, cameraFinalLookAt.rotation, CAMERA_ROTATION_SPEED * Time.deltaTime);
        }
    }

    // For menu rotation
    public void LookAtMenu(Transform menuTransform)
    {
        //Camera.main.transform.LookAt(menuTransform.position);
        cameraFinalLookAt = menuTransform;
    }

    // For displaying the Monster Info
    public void OpenLevelPanel(string sceneName)
    {
        levelPanelContainer.transform.Find(sceneName).gameObject.SetActive(true);
    }

    void CloseLevelPanel(string sceneName)
    {
        levelPanelContainer.transform.Find(sceneName).gameObject.SetActive(false);
    }

    public void LoadLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void SetPlayedLevel(int level)
    {
        PlayerPrefs.SetInt("stageSelected", level);
    }

    public void CheckStagesUnlocked ()
    {
        int levelReached = PlayerPrefs.GetInt("stageUnlocked");
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i+1 > levelReached)
            {
                levelButtons[i].interactable = false;
            }
        }
    }
}
