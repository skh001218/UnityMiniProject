using TMPro;
using UnityEngine;

public class DebugUi : MonoBehaviour
{
    public TMP_InputField goldText;
    public TMP_InputField diaText;
    
    public GameManager gameManager;

    public void OnClickX()
    {
        gameObject.SetActive(false);
    }

    public void OnClickAccept()
    {
        gameObject.SetActive(false);

        var gold = goldText.text;
        int goldAmount = 0; 
        int.TryParse(gold, out goldAmount);

        var dia = diaText.text;
        int diaAmount = 0;
        int.TryParse(dia, out diaAmount);


        gameManager.totalGold = goldAmount;
        gameManager.goldText.text = gold;

        gameManager.totalDiamond = diaAmount;
        gameManager.DiaText.text = dia;

    }

    public void OpenUi()
    {
        gameObject.SetActive(true);
    }
}
