using UnityEngine.SceneManagement;

public class ScenesManager : Singleton<ScenesManager>
{
    protected override void Awake() {
        base.Awake();
    }

    private void Start() {
        ShowMainMenu();
    }

    public void StartGame() {
        SceneManager.LoadScene("Game");
    }

    private void ShowMainMenu() {
        if (SceneManager.GetActiveScene().name == "MainMenu") return;

        SceneManager.LoadScene("MainMenu");
    }
}
