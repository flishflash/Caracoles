
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ItemButton : MonoBehaviour
{
    public GameObject item;
    public fatherRoom room;

    ButtonsBehaviour buttonsBehaviour;
    private void Start()
    {
        if (item != null) GetComponent<Image>().sprite = item.GetComponent<OnGui>().sprite;

        buttonsBehaviour = GameObject.Find("ButtonManager").GetComponent<ButtonsBehaviour>();

        Button button = GetComponent<Button>();

        if (SceneManager.GetActiveScene().name == "BuildRoomScene")
        {
            button.onClick.AddListener(() => buttonsBehaviour.SetSelectedItem(this));
        }
        else
        {
            button.onClick.AddListener(() => buttonsBehaviour.SetSelectedRoom(this));
        }
    }

    public void SetSprite(Sprite image)
    {
        GetComponent<Image>().sprite = image;
    }
}
