using System;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject[] languageButtonArray;
    public string currentLanguage = "English";

    public GameObject cellMovementSpeedSlider;
    public GameObject cellRotationSpeedSlider;
    public GameObject cellWanderSpeedSlider;

    public GameObject cellSpawner;
    public GameObject cellSpawnSpeedSlider;
    public GameObject cellSpawnQuantitySlider;
    public GameObject foodSpawner;
    public GameObject foodSpawnSpeedSlider;
    public GameObject foodSpawnQuantitySlider;

    public LanguageSystem languageSystem;

    public void SetImageColor(GameObject gameObject, Color color)
    {
        gameObject.GetComponent<Image>().color = color;
    }

    public void SetListImageColor(GameObject[] gameObjects, Color color)
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            GameObject gameObject = gameObjects[i];
            SetImageColor(gameObject, color);
        }
    }

    public void BindSlider(GameObject slider, Action<float> onValueChanged, string format)
    {
        slider.GetComponent<Slider>().onValueChanged.AddListener((float value) =>
        {
            onValueChanged(value);
            slider.transform.Find("Label").GetComponent<Text>().text = value.ToString(format) + "x";
        });
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (GameObject languageButton in languageButtonArray)
        {
            GameObject captured = languageButton;
            captured.GetComponent<Button>().onClick.AddListener(() =>
            {
                SetListImageColor(languageButtonArray, new Color32(120, 120, 120, 255));
                SetImageColor(captured, Color.darkGreen);
                currentLanguage = captured.name;
                languageSystem.LanguageUpdate(currentLanguage);
            });
        }

        BindSlider(cellMovementSpeedSlider, v => GameManager.Instance.cellMovementSpeed = v * 0.6f, "F2");

        BindSlider(cellRotationSpeedSlider, v => GameManager.Instance.cellRotationSpeed = v * 100, "F2");

        BindSlider(cellWanderSpeedSlider, v => GameManager.Instance.cellWanderSpeed = v, "F2");

        BindSlider(cellSpawnSpeedSlider, v => cellSpawner.GetComponent<ObjectSpawner>().WaitingTimeMultiplier = v, "F2");

        BindSlider(cellSpawnQuantitySlider, v => cellSpawner.GetComponent<ObjectSpawner>().MaxSpawnQuantity = (int)v, "F0");

        BindSlider(foodSpawnSpeedSlider, v => foodSpawner.GetComponent<ObjectSpawner>().WaitingTimeMultiplier = v, "F2");

        BindSlider(foodSpawnQuantitySlider, v => foodSpawner.GetComponent<ObjectSpawner>().MaxSpawnQuantity = (int)v, "F0");
    }
}
