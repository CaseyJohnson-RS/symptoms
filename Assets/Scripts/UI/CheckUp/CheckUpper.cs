using UnityEngine;              // ПОЛНЫЙ КАЛОКОД
using UnityEngine.Events;

public class CheckUpper : MonoBehaviour
{
    public SpriteData spriteData;
    public LogsCollector logCollector;
    // public NPCData tmpData;
    // public Sprite tmpSprite;

    void Start()
    {
        // OnCheckUp(tmpData, "eyes");
    }

    public void OnCheckUp(NPCData data, string name)
    {        
        switch (name)
        {
            case "temperature":
                ShowTemperature(data.Temperature);
                break;
            case "heartRate":
                ShowHeartRate(data.HeartRate);
                break;
            case "arms":
                ShowArms(data.SymptomArms);
                break;
            case "armpits":
                ShowArmpits(data.SymptomArmpits);
                break;
            case "eyes":
                ShowEyes(data.SymptomEyes);
                break;
            case "mouths":
                ShowMouths(data.SymptomMouth);
                break;
        }
    }

    // Internal API

    public UnityEvent<float> onShowTemperature;
    private void ShowTemperature(float temperature)
    {
        logCollector.AddTemperature(temperature);
        onShowTemperature.Invoke(temperature);
    }

    public UnityEvent<float> onShowHeartRate;
    private void ShowHeartRate(float heartRate)
    {
        logCollector.AddHeartRate(heartRate);
        onShowHeartRate.Invoke(heartRate);
    }

    public UnityEvent<Sprite> onShowArms;
    public void ShowArms(bool ill)
    {
        Sprite sprite = spriteData.GetSprite("arms", ill);
        logCollector.AddImage(sprite);
        onShowArms.Invoke(sprite);
    }

    public UnityEvent<Sprite> onShowArmpits;
    public void ShowArmpits(bool ill)
    {
        Sprite sprite = spriteData.GetSprite("armpits", ill);
        logCollector.AddImage(sprite);
        onShowArmpits.Invoke(sprite);
    }

    public UnityEvent<Sprite> onShowEyes;
    public void ShowEyes(bool ill)
    {
        Sprite sprite = spriteData.GetSprite("eyes", ill);
        logCollector.AddImage(sprite);
        onShowEyes.Invoke(sprite);
    }

    public UnityEvent<Sprite> onShowMouths;
    public void ShowMouths(bool ill)
    {
        Sprite sprite = spriteData.GetSprite("mouths", ill);
        logCollector.AddImage(sprite);
        onShowMouths.Invoke(sprite);
    }


}
