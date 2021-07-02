using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text SpeedButtonText;

    public void OnClickSpeedButton()
    {
        switch (Time.timeScale)
        {
            case 0:
                {
                    Time.timeScale = 1;
                    SpeedButtonText.text = "X 1";
                }
                break;

            case 1:
                {
                    Time.timeScale = 2;
                    SpeedButtonText.text = "X 2";
                }
                break;

            case 2:
                {
                    Time.timeScale = 4;
                    SpeedButtonText.text = "X 4";
                }
                break;

            case 4:
                {
                    Time.timeScale = 0;
                    SpeedButtonText.text = "||";
                }
                break;
        }

    }

}
