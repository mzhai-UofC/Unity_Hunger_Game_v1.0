using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;
    [SerializeField] Light sunLight;


    public float dayTime;
    public float dayToNightTime;
    public float nightTime;
    public float nightToDayTime;

    private float lightValue = 1;
    private int dayNum=0;

    [SerializeField] Image timeStateImg;
    [SerializeField] Text dayNumText;
    [SerializeField] Sprite[] dayStateSprites;

    

    private bool isDay = true;

    public bool IsDay
    {
        get => isDay;
        set
        {
            isDay = value;
            if (isDay)
            {
                dayNum++;
                dayNumText.text = "Day " + dayNum;
                timeStateImg.sprite = dayStateSprites[0];
   
            }
            else
            {
                timeStateImg.sprite = dayStateSprites[1];
            }
        }
    }

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        IsDay = true;
        StartCoroutine(UpdateTime());
    }

    private IEnumerator UpdateTime()
    {
        while (true)
        {
            yield return null;
            if (IsDay)
            {
                lightValue -= 1 / dayToNightTime * Time.deltaTime;
                SetLightValue(lightValue);
                if (lightValue <= 0)
                {
                    IsDay = false;
                    yield return new WaitForSeconds(nightTime);//wait for night pass
                }
            }
            //night
            else
            {
                lightValue += 1 / nightToDayTime * Time.deltaTime;
                SetLightValue(lightValue);
                if (lightValue >= 1)
                {
                    IsDay = true;
                    yield return new WaitForSeconds(dayTime);//wait for day pass
                }
            }
        }
    }
    
   private void SetLightValue(float value)
    {
        RenderSettings.ambientIntensity = value;
        sunLight.intensity = value;
    }
}
