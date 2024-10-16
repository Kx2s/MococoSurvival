using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottomBar : MonoBehaviour
{
    Vector2 size = new Vector2(25, 25);
    Button[] buttons;

    private void Awake() {
        buttons = gameObject.GetComponentsInChildren<Button>();
        for(int i=0; i<buttons.Length; i++){
            int idx = i;
            buttons[i].onClick.AddListener(() => Click(idx));
        }
    }

    //시작시 캐릭터창으로
    private void Start() {
        Click(2);
    }

    public void Click(int num){ 
        Debug.Log("하단바 버튼 클릭됨");
        //버튼 크기 조정
        foreach(Button button in buttons){
            button.interactable = true;
            button.gameObject.GetComponent<RectTransform>().sizeDelta = size;
        }

        buttons[num].interactable = false;
        buttons[num].gameObject.GetComponent<RectTransform>().sizeDelta *= 1.5f;

        //화면 전환 추가 예정
    }
}
