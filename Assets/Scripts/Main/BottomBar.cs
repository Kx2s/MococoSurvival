using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottomBar : MonoBehaviour
{
    Vector2 basic;
    Button[] buttons;

    public Transform page;

    private void Awake() {
        basic = transform.GetChild(0).GetComponent<RectTransform>().sizeDelta;

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
            button.gameObject.GetComponent<RectTransform>().sizeDelta = basic;
        }

        buttons[num].interactable = false;
        buttons[num].gameObject.GetComponent<RectTransform>().sizeDelta *= 1.5f;

        //화면 전환 추가 예정
        for (int i=0; i<page.childCount; i++)
            page.GetChild(i).gameObject.SetActive(false);

        page.GetChild(num).gameObject.SetActive(true);
    }
}
