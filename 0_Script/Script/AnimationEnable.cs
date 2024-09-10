using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class AnimationEnable : MonoBehaviour
{
    public WaterScaleCube WaterScaleCubeScript;
    public Button Button;
    public GameObject h2o;
    public GameObject h2oNo;
    public GameObject canva1;
    public GameObject canva2;


    private bool a1 = false;
    public LevelEndSequence levelEndSequence;
    // public Stage5_UIManager stage5_UIManager;
    public ScreenShow screenShow;
    // public ELFStatus elfStatus;

    void Start()
    {
        Button.onClick.AddListener(() => OnButtonClicked(h2o, canva1));
        h2o.SetActive(false);
        Button.onClick.AddListener(End);
    }
    
    private void End()
    {
        // screenShow.ShowScreen(1f);
        levelEndSequence.EndLevel(false,true,2f,0f,8f,0f,"1");
        StartCoroutine(HideScreenActive(5f));
        
        // h2oNo.SetActive(false);
        
    }


    IEnumerator HideScreenActive(float delayTime)
    {
        h2o.SetActive(false);
        yield return new WaitForSeconds(delayTime);
        // screenShow.HideScreen();
    }
    
    private void Update()
    {
        if(WaterScaleCubeScript.isMove &&!a1)
        {
            OnButtonClicked(h2oNo , canva2);
            a1 = true;
            End();
        }
   }

    public void OnButtonClicked(GameObject a , GameObject canva)
    {
        a.SetActive(true);
        h2o.SetActive(true);
        //canva.SetActive(true);
    }
}
