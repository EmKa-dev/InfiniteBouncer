using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Sprites")]
    public Sprite CleanSprite;
    public Sprite PUJUMP_Sprite;
    public Sprite PUSAFE_Sprite;
    public Sprite Life_Sprite;

    [Header("UI Components")]
    public Image UI_ActivePowerUp;
    public Image[] UI_Lives;
    public int UI_LivesIndex = 0;
    public Canvas GameOverPanel;

    public static Action<TypeOfPowerUp> UpdateUI_PowerUp;
    public static Action UpdateUI_ClearPowerUp;
    public static Action ShowGameOverScreen;

	void Start()
    {
        PlayerController.ChangeLifeAction += UpdateLivesOnUI;
        UpdateUI_PowerUp += ChangePowerUpSpriteOnUI;
        UpdateUI_ClearPowerUp += ClearPowerUpSpriteOnUI;
        ShowGameOverScreen += ActivateGameOverScreen;
        GameOverPanel.gameObject.SetActive(false);
	}


    private void ChangePowerUpSpriteOnUI(TypeOfPowerUp pu)
    {
        switch (pu)
        {
            case TypeOfPowerUp.Jump:
                UI_ActivePowerUp.sprite = PUJUMP_Sprite;
                break;
            case TypeOfPowerUp.Safe:
                UI_ActivePowerUp.sprite = PUSAFE_Sprite;
                break;
            default:
                break;
        }
    }

    private void ClearPowerUpSpriteOnUI()
    {
        UI_ActivePowerUp.sprite = CleanSprite;     
    }

    private void UpdateLivesOnUI(int amount)
    {
        switch (amount)
        {
            case 1:
                //add one life
                UI_Lives[UI_LivesIndex].sprite = Life_Sprite;
                if (UI_LivesIndex < 2)
                {
                    UI_LivesIndex++;
                }
                break;
            case -1:
                //Remove one life
                if (UI_LivesIndex > 0)
                {
                    UI_LivesIndex--;
                }
                UI_Lives[UI_LivesIndex].sprite = CleanSprite;
                break;
            default:
                break;
        }
    }

    private void ActivateGameOverScreen()
    {
        GameOverPanel.gameObject.SetActive(true);
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().enabled = false;
    }

    public void RestartButton_Click()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitButton_Click()
    {
        Application.Quit();
    }

    private void OnDestroy()
    {
        PlayerController.ChangeLifeAction -= UpdateLivesOnUI;
        UpdateUI_PowerUp -= ChangePowerUpSpriteOnUI;
        UpdateUI_ClearPowerUp -= ClearPowerUpSpriteOnUI;
        ShowGameOverScreen -= ActivateGameOverScreen;
    }
}
