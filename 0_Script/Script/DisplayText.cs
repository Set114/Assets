using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class DisplayText : MonoBehaviour
{
    public GlucoseScaleCube glucoseScaleCubeScript;
    public TMP_Text text;
    public Text temperature_text;
    public string displayText1 = "0%";
    public string displayText2 = "0%";
    public string displayText3 = "0%";
    public string displayText4 = "0%";
    public string displayText5 = "0%";
    public Vector3 scale = new Vector3(0, 0.1f, 0);

    private bool a1 = false;
    // private AnimationController animationController;


    void Start()
    {
        if (text != null)
        {
            text.text = displayText1;
        }
        //Button.onClick.AddListener(OnButtonClicked);

        // animationController = FindObjectOfType<AnimationController>();
        // if (animationController == null)
        // {
        //     Debug.LogError("AnimationController not found in the scene.");
        // }
    }

    private void Update()
    {
        if (glucoseScaleCubeScript.isParticleTriggered && !a1)
        {
            StartCoroutine(AnimateText(displayText2));
            a1 = true;
        }
    }

    public void ignition()
    {
        StartCoroutine(AnimateText(displayText3));
        StartCoroutine(AnimateTemperature(50));
    }

    public void flow()
    {
        StartCoroutine(AnimateText(displayText4));
        
        StartCoroutine(DelayedAction());
    }

    private IEnumerator DelayedAction()
    {
        yield return new WaitForSeconds(7.5f);
        StartCoroutine(AnimateText(displayText5));
        glucoseScaleCubeScript.SendMessage("UpdateScaleFactor", new GlucoseScaleCube.ScaleFactorParameters(scale, false));
    }

    private IEnumerator AnimateText(string targetText)
    {
        float currentValue = float.Parse(text.text.Replace("%", ""));
        float targetValue = float.Parse(targetText.Replace("%", ""));
        float duration = 4.0f; //  ʵe    ɶ  ]   ^
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float newValue = Mathf.Lerp(currentValue, targetValue, elapsed / duration);
            text.text = Mathf.RoundToInt(newValue).ToString() + "%";
            yield return null;
        }

        text.text = targetValue.ToString() + "%"; //  T O ̲׭ȥ  T ] m
    }

    private IEnumerator AnimateTemperature(float targetTemperature)
    {
        float currentTemperature = float.Parse(temperature_text.text.Replace("°C", ""));
        float duration = 4.0f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float newTemperature = Mathf.Lerp(currentTemperature, targetTemperature, elapsed / duration);
            temperature_text.text = Mathf.RoundToInt(newTemperature).ToString() + "°C";
            yield return null;
        }

        temperature_text.text = targetTemperature.ToString() + "°C";
    }
}
