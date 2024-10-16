using DataTable;
using UnityEngine;
using UnityEngine.UI;

public class StageInfo : MonoBehaviour
{
    Text desc;
    Button btn;
    GameObject Rock;

    public Stage info;

    private void Awake() {
        btn = GetComponentInChildren<Button>(true);
        Rock = transform.GetChild(0).GetChild(1).gameObject;
        desc = Rock.GetComponentInChildren<Text>(true);
    }

    private void Start() {
        bool isRock = false;
        string text = "";
        GetComponentInChildren<Text>().text = info.name;

        if (info.tema != 0){
        if (PlayerPrefs.GetInt((info.tema-1).ToString()) == 0){
            text += "<color=#FF302D>";
            isRock = true;
        }
        else
            text += "<color=#40FF0A>";
        text += (info.tema-1).ToString() + "</color> 클리어\n";
        }

        if (PlayerPrefs.GetInt("nowStage") < info.index){
            text += "<color=#FF302D>";
            isRock = true;
        }
        else 
            text += "<color=#40FF0A>";
        text+= "이전 단계</color> 클리어";
        desc.text = text;


        Rock.SetActive(isRock);
        if (isRock)
            btn.interactable = false;
        else 
            btn.interactable = true;
    }
}
