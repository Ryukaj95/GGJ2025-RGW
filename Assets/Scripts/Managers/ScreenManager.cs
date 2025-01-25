using UnityEngine;

public class ScreenManager : Singleton<ScreenManager>
{
    private MainMenu mainMenuScreen;
    private StageManager stageManager;

    private bool mainMenuActive => mainMenuScreen.gameObject.activeSelf;

    protected override void Awake() {
        base.Awake();

        mainMenuScreen = GetComponentInChildren<MainMenu>();
        stageManager = GetComponentInChildren<StageManager>();
    }

    private void Start() {
        ShowMainMenu();
    }

    public void StartGame() {
        HideMainMenu();
    }

    private void ShowMainMenu() {
        if (mainMenuActive) return;

        mainMenuScreen.gameObject.SetActive(true);
    }
    
    private void HideMainMenu() {
        if (!mainMenuActive) return;

        mainMenuScreen.gameObject.SetActive(false);
    }
}
