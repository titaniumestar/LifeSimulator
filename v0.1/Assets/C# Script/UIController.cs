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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < languageButtonArray.Length; i++)
        {
            GameObject languageButton = languageButtonArray[i];
            languageButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                SetListImageColor(languageButtonArray, new Color32(120, 120, 120, 255));
                SetImageColor(languageButton, Color.darkGreen);
                currentLanguage = languageButton.name;
                languageSystem.LanguageUpdate(currentLanguage);
            });
        }

        cellMovementSpeedSlider.GetComponent<Slider>().onValueChanged.AddListener((float value) =>
        {
            GameManager.Instance.cellMovementSpeed = value * 0.6f;
            cellMovementSpeedSlider.transform.Find("Label").GetComponent<Text>().text = value.ToString("F2") + "x";
        });

        cellRotationSpeedSlider.GetComponent<Slider>().onValueChanged.AddListener((float value) =>
        {
            GameManager.Instance.cellRotationSpeed = value * 100;
            cellRotationSpeedSlider.transform.Find("Label").GetComponent<Text>().text = value.ToString("F2") + "x";
        });

        cellWanderSpeedSlider.GetComponent<Slider>().onValueChanged.AddListener((float value) =>
        {
            GameManager.Instance.cellWanderSpeed = value;
            cellWanderSpeedSlider.transform.Find("Label").GetComponent<Text>().text = value.ToString("F2") + "x";
        });

        cellSpawnSpeedSlider.GetComponent<Slider>().onValueChanged.AddListener((float value) =>
        {
            cellSpawner.GetComponent<ObjectSpawner>().WaitingTimeMultiplier = value;
            cellSpawnSpeedSlider.transform.Find("Label").GetComponent<Text>().text = value.ToString("F2") + "x";
        });

        cellSpawnQuantitySlider.GetComponent<Slider>().onValueChanged.AddListener((float value) =>
        {
            cellSpawner.GetComponent<ObjectSpawner>().MaxSpawnQuantity = (int)value;
            cellSpawnQuantitySlider.transform.Find("Label").GetComponent<Text>().text = value.ToString() + "x";
        });

        foodSpawnSpeedSlider.GetComponent<Slider>().onValueChanged.AddListener((float value) =>
        {
            foodSpawner.GetComponent<ObjectSpawner>().WaitingTimeMultiplier = value;
            foodSpawnSpeedSlider.transform.Find("Label").GetComponent<Text>().text = value.ToString("F2") + "x";
        });

        foodSpawnQuantitySlider.GetComponent<Slider>().onValueChanged.AddListener((float value) =>
        {
            foodSpawner.GetComponent<ObjectSpawner>().MaxSpawnQuantity = (int)value;
            foodSpawnQuantitySlider.transform.Find("Label").GetComponent<Text>().text = value.ToString() + "x";
        });
    }
}
