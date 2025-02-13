using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUi : MonoBehaviour
{
    public TextMeshProUGUI text;
    public GameObject textList;

    public Button nextBtn;
    public Button prevBtn;

    private List<TextMeshProUGUI> texts = new List<TextMeshProUGUI>();

    public int curPageIndex = 0;

    private void Start()
    {
        texts = textList.transform.GetComponentsInChildren<TextMeshProUGUI>(includeInactive: true).ToList();
        SetPage();
    }

    private void Update()
    {
        
    }

    public void OnClickX()
    {
        gameObject.SetActive(false);
    }

    public void OpenUi()
    {
        gameObject.SetActive(true);
    }

    public void NextBtn()
    {
        curPageIndex++;
        SetPage();
    }

    public void PrevBtn()
    {
        curPageIndex--;
        SetPage();
    }

    public void SetPage()
    {
        foreach (TextMeshProUGUI text in texts)
        {
            text.gameObject.SetActive(false);
        }
        texts[curPageIndex].gameObject.SetActive(true);

        prevBtn.gameObject.SetActive(!(curPageIndex <= 0));

        nextBtn.gameObject.SetActive(!(curPageIndex + 1 >= texts.Count));
    }

}
