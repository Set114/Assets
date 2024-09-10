using System.Collections;
using UnityEngine;

public class LevelEndSequence : MonoBehaviour
{
    public CameraController cameraController; // 相機控制器
    //public ScreenShow screenShow; // 屏幕顯示控制
    public SwitchUI switchUI; // 關卡 UI 管理器
    //public Lvl1tutorialGM lvl1tutorialGM; // 關卡教學管理
    public CheckImage checkImage; // 圖像檢查
    public ELFStatus elfStatus; // ELF 狀態控制

    [Header("時間設定")]
    private bool haveAni = false; // 判斷是否需要動畫
    private float showELFDelay = 2f; // 顯示 ELF 的延遲時間
    private float cameraZoomDelay = 0f; // 縮放鏡頭前的延遲時間
    private float levelChangeDelay = 5f; // 關卡變更的延遲時間
    private float nextUIShowDelay = 1f; // 顯示下一個 UI 的延遲時間
    private string answer; 
    //[Header("levelCount")]
    private int levelCount = 1; 
    [Header("END")]
    private bool showEndUI = false; 
    [Header("EndUI")]
    [SerializeField] GameObject learnEndUI;
    [SerializeField] GameObject testEndUI;

    [Header("UI")]
    [SerializeField] GameObject learnUI;
    [Header("TeachEndUI")]
    [SerializeField] GameObject testUI;
    private int chapterMode = 0;

    public LearnDataManager learnDataManager;
    public TestDataManager testDataManager;
    public PlaySpeechAudio playSpeechAudio;
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.Instance;
    }

    public void EndLevel(bool showEndUIBool,bool haveAniBool,float showELFDelayTime,float cameraZoomDelayTime,float levelChangeDelayTime,float nextUIShowDelayTime,string answerData)
    {
        showEndUI = showEndUIBool;
        haveAni = haveAniBool;
        showELFDelay = showELFDelayTime;
        cameraZoomDelay = cameraZoomDelayTime;
        levelChangeDelay = levelChangeDelayTime;
        nextUIShowDelay = nextUIShowDelayTime;
        levelCount = switchUI.GetLevelCount();
        answer = answerData;
        playSpeechAudio.SetCurrentLevel(levelCount);
        StartCoroutine(ShowELFAndThenZoomIn());
        // Debug.Log(levelCount +　"endstart");
    }

    // 先顯示 ELF，再進行鏡頭縮放
    IEnumerator ShowELFAndThenZoomIn()
    {
        if (haveAni == true){
            //Debug.Log(levelCount +　"_true");
            yield return ShowELFWithDelay(showELFDelay); // 等待指定時間後顯示 ELF
            yield return new WaitForSeconds(cameraZoomDelay); // 等待縮放鏡頭的延遲時間
            //cameraController.ZoomIn(); // 縮放鏡頭            
        }
        
        Debug.Log("WAIT");
        StartCoroutine(DelayedLevelChange()); // 開始延遲關卡變更
    }

    // 延遲顯示 ELF
    IEnumerator ShowELFWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay); 
        elfStatus.ShowELF(); // 顯示 ELF
    }

    // 延遲關卡變更
    IEnumerator DelayedLevelChange()
    {
        yield return new WaitForSeconds(levelChangeDelay);
        elfStatus.HideELF(); // 隱藏 ELF
        chapterMode = gameManager.GetChapterMode();

        if (chapterMode == 0)
        {
            learnDataManager.EndLevelWithCallback(answer, () => StartCoroutine(ShowNextUIAfterDelay(nextUIShowDelay)));
        }
        else if (chapterMode == 1)
        {
            testDataManager.EndLevelWithCallback(() => StartCoroutine(ShowNextUIAfterDelay(nextUIShowDelay)));
        }
        //使用回調函數在關卡結束後執行顯示下一個 UI 的操作
        
        //StartCoroutine(ShowNextUIAfterDelay(nextUIShowDelay));
    }

    // 延遲顯示下一個 UI
    IEnumerator ShowNextUIAfterDelay(float delay)
    {
        Debug.Log("in the end");
        chapterMode = gameManager.GetChapterMode();
        yield return new WaitForSeconds(delay); // 等待指定時間
        switchUI.CompletedState(levelCount);
        checkImage.SwitchImage(levelCount); // 切換圖像
        
        if(showEndUI == false)
        {
            switchUI.ShowNextUI(); // 顯示下一個 UI
            Debug.Log("SHOW NEXT UI");
        }
        else 
        {
            if(chapterMode == 0)
            {
                learnUI.SetActive(true);
                learnEndUI.SetActive(true);
            }
            else
            {
                testUI.SetActive(true);
                testEndUI.SetActive(true);
            }
        }
    }




}