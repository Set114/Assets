using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class AnimationController : MonoBehaviour
{
    private Animator animator;
    public Button Button;
    public Button Button2;   
    public Button Button3;   
    public Button Button4;   
    public GameObject canva1;
    public GameObject canva2;  
    public GameObject canva3;  
    public GameObject canva4;  
    public GameObject ELF1_3_1;  
    public TMP_Text levelIndex;
    public Stage5_UIManager stage5_UIManager;
    //public ScreenShow screenShow;
    public ELFStatus elfStatus;
    public GlucoseScaleCube glucoseScaleCube1;
    public GlucoseScaleCube glucoseScaleCube2;
    //public DisplayText displayText;
    
    public LevelEndSequence levelEndSequence;

    void Start()
    {
        animator = GetComponent<Animator>();
        Button.onClick.AddListener(OnButtonClicked);
        Button2.onClick.AddListener(OnButtonClicked2);
        Button3.onClick.AddListener(OnButtonClicked2);
        Button4.onClick.AddListener(OnButtonClicked2);
    }                   

    public void OnButtonClicked()
    {
        StartCoroutine(ShowElfAndPlayAnimation());
    }

    private IEnumerator ShowElfAndPlayAnimation()
    {
        ELF1_3_1.SetActive(true);
        //screenShow.ShowScreen();
        yield return new WaitForSeconds(2f);
        animator.SetBool("isClick", true);
        yield return new WaitForSeconds(7f);
        ELF1_3_1.SetActive(false);
        //screenShow.HideScreen();
        yield return new WaitForSeconds(3f);
        //displayText.OnButtonClicked(); 
        //有更新
    }    
    public void OnButtonClicked2()
    {
        StartCoroutine(AnimationMiddle2Routine());
    }

    private IEnumerator AnimationMiddle2Routine()
    {
        // yield return ShowELFWithDelay(2f);
        //screenShow.ShowScreen();
        // yield return new WaitForSeconds(2f);
        Debug.Log("start");
        ResumeAnimation();
        if (levelIndex.text == "5-5")
        {
            levelEndSequence.EndLevel(true,true,2f,0f,8f,0f,"1");
        }else{
            levelEndSequence.EndLevel(false,true,2f,0f,8f,0f,"1");
        }
        
        yield return new WaitForSeconds(1f);
        // StartCoroutine(DelayedLevelChange());
        //canva2.SetActive(true);
    }


    public void OnAnimationMiddle1()
    {
        animator.speed = 0f;
        glucoseScaleCube1.OnButtonClicked();
        glucoseScaleCube2.OnButtonClicked();
    }
    public void OnAnimationMiddle2()
    {       
        animator.speed = 0f;//動畫暫停
        Debug.Log("OnAnimationMiddle2");
    }
    public void OnAnimationMiddle3()
    {
        animator.speed = 0f;
        Debug.Log("OnAnimationMiddle3");
    }

    public void OnAnimationEnd()
    {
        animator.StopPlayback();
        Debug.Log("OnAnimationEnd");
    }

    public void ResumeAnimation()
    {
        animator.speed = 1f;
        Debug.Log("ResumeAnimation");
    }

    // 定义一个协程来延迟显示 ELF
    IEnumerator ShowELFWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        elfStatus.ShowELF();
    }

    IEnumerator DelayedLevelChange()
    {
        yield return new WaitForSeconds(6f);
        elfStatus.HideELF();
        //screenShow.HideScreen();
        StartCoroutine(ShowNextUIAfterDelay(1f));
    }

    // 定义一个协程来延迟显示 UI
    IEnumerator ShowNextUIAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (levelIndex.text == "5-5")
        {
            stage5_UIManager.ShowEndUI();
        }else{
            stage5_UIManager.ShowNextUI();
        }
        
    }
}
