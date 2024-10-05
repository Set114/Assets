using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;


public class GlucoseScaleCube : MonoBehaviour
{
    public ScaleCubeCalculate scaleCubeScript;
    public GameObject hint1;
    public GameObject hint2;
    public Button Button;
    public TextMeshProUGUI parameterDisplayText;
    public Vector3 scale1 = new Vector3(0, 0.1f, 0);
    public Vector3 scale2 = new Vector3(0, 0.1f, 0);
    public int levelindex;

    public SwitchUI switchUI;
    
    

    private bool isMove = false;
    public bool a1 = false;
    public bool isParticleTriggered = false;

    void OnEnable()
    {
        levelindex = switchUI.GetLevelCount();
        Debug.Log("levelindex = " + levelindex);
        if (levelindex == 2)
        {
            Button.onClick.AddListener(OnButtonClicked);
        }
    }

    private void Update()
    {
        if (levelindex == 2)
        {
            if (isParticleTriggered && !isMove)
            {
                UpdateScaleFactor(new ScaleFactorParameters(scale1,true));
                isMove = true;
                Debug.Log("Update is true");
            }
        }
    }

    [System.Serializable]
    public class ScaleFactorParameters
    {
        public Vector3 scale;
        public bool showHint;

        public ScaleFactorParameters(Vector3 scale, bool showHint = false)
        {
            this.scale = scale;
            this.showHint = showHint;
        }
    }


    public void UpdateScaleFactor(ScaleFactorParameters parameters)
    {
        Vector3 direction = Vector3.up;
        scaleCubeScript.ScaleInOneDirection(parameters.scale, direction, parameterDisplayText);
        

         Debug.Log("A2 = " + a1);

        if (parameters.showHint)
        {
            if (a1)
            {
                Debug.Log("A2 is true");
                StartCoroutine(ActivateHintWithDelay(4.5f, hint2));
            }
            else
            {
                Debug.Log("A1 is true");
                StartCoroutine(ActivateHintWithDelay(4.5f, hint1));
            }
        }
    }

   

    private IEnumerator ActivateHintWithDelay(float delay, GameObject hint)
    {
        Debug.Log("hint is true");
        yield return new WaitForSeconds(delay);
        hint.SetActive(true);
        a1 = true;
    }

    public void OnButtonClicked()
    {
        UpdateScaleFactor(new ScaleFactorParameters(scale2,true));
    }
}