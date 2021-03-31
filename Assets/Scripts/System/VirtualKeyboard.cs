using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class VirtualKeyboard : MonoBehaviour
{
    public List<GameObject> numKeys = new List<GameObject>();

    public GameObject backspace;
    public GameObject submit;
    public GameObject leave;

    public string str;
    public Text textArea;

    private void OnEnable()
    {
        foreach (GameObject obj in numKeys)
        {
            GameHandler.Singleton.BindEvent(
                obj,
                EventTriggerType.PointerClick,
                delegate
                {
                    JacDev.Audio.AudioHandler.Singleton.PlaySound(JacDev.Audio.AudioHandler.Singleton.soundList.typing);
                    TypeIn(obj.GetComponentInChildren<Text>().text);
                }
            );
        }

        GameHandler.Singleton.BindEvent(
                backspace,
                EventTriggerType.PointerClick,
                delegate
                {
                    JacDev.Audio.AudioHandler.Singleton.PlaySound(JacDev.Audio.AudioHandler.Singleton.soundList.typing);
                    Backspace();
                }
            );

        GameHandler.Singleton.BindEvent(
                submit,
                EventTriggerType.PointerEnter,
                delegate
                {
                    JacDev.Audio.AudioHandler.Singleton.PlaySound(JacDev.Audio.AudioHandler.Singleton.soundList.select);
                }
            );

        GameHandler.Singleton.BindEvent(
                submit,
                EventTriggerType.PointerClick,
                delegate
                {
                    JacDev.Audio.AudioHandler.Singleton.PlaySound(JacDev.Audio.AudioHandler.Singleton.soundList.hover);

                    GameHandler.Singleton.SetPlayerName(str);

                    // if (GameHandler.Singleton.LoadPlayerData(str) != null)
                    // {
                    //     GameHandler.Singleton.SetPlayerData(GameHandler.Singleton.LoadPlayerData(str));
                    //     print("load " + str);
                    // }
                    // else
                    // {
                    //     GameHandler.Singleton.SetPlayerName(str);
                    //     GameHandler.Singleton.SavePlayerData();
                    //     print("create "+ str);
                    // }
                    GetComponent<UIQuickSetting>().TurnOff();
                    GameHandler.Singleton.StageFinish();
                }
            );

        GameHandler.Singleton.BindEvent(
                leave,
                EventTriggerType.PointerClick,
                delegate
                {
                    Application.Quit();
                }
            );
    }


    void TypeIn(string s)
    {
        str += s;
        textArea.text = str;
    }

    void Backspace()
    {
        str = str.Remove(str.Length - 1, 1);
        textArea.text = str;
    }
}
