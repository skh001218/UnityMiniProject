using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DebugUi : MonoBehaviour
{
    public TMP_InputField goldText;
    public TMP_InputField diaText;
    public Toggle timeToggle;
    
    public GameManager gameManager;
    public ExpendMgr expandMgr;
    private bool createNow = false;

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

        if (gold == "")
            goldAmount = 0;
        if (dia == "")
            diaAmount = 0;

        gameManager.totalGold = goldAmount;
        gameManager.goldText.text = goldAmount.ToString();

        gameManager.totalDiamond = diaAmount;
        gameManager.DiaText.text = diaAmount.ToString();

        expandMgr.noTime = createNow;
    }

    public void OpenUi()
    {
        gameObject.SetActive(true);
        goldText.text = "";
        diaText.text = "";
        timeToggle.isOn = expandMgr.noTime;
    }

    public void NoTime()
    {
        if(timeToggle.isOn)
            createNow = true;
        else
            createNow = false;
    }
}
