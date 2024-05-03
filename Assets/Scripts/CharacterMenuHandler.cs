using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class CharacterMenuHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public PlayerData playerData;

    public Button button;

    public GameObject minimisedContent;
    public GameObject expandedContent;

    public RectTransform expandedRect;

    public Image minimisedImg;
    public Image expandedImg;

    public TextMeshProUGUI minimisedName;
    public TextMeshProUGUI expandedName;
    public TextMeshProUGUI description;

    private Tween tween;

    public List<Image> ratingA;
    public List<Image> ratingB;
    public List<Image> ratingC;
    public List<Image> ratingD;

    void Awake()
    {
        expandedContent.SetActive(false);
        button.onClick.AddListener(() =>
        {
            MenuManager.instance.CharacterSelected(playerData);
        });

        Init();
    }

    public void Init()
    {
        minimisedName.text = playerData.playerName;
        expandedName.text = playerData.playerName;

        minimisedImg.sprite = playerData.character;
        expandedImg.sprite = playerData.character;

        minimisedImg.preserveAspect = true;
        expandedImg.preserveAspect = true;

        description.text = playerData.characterDescription;

        for(int i = 0; i < ratingA.Count; i++)
        {
            if(i <= playerData.damage)
            {
                ratingA[i].color = Color.white;
            }
            else
            {
                ratingA[i].color = Color.black;
            }
        }
        for(int i = 0; i < ratingB.Count; i++)
        {
            if (i <= (playerData.health / 8))
            {
                ratingB[i].color = Color.white;
            }
            else
            {
                ratingB[i].color = Color.black;
            }
        }
        for(int i = 0; i < ratingC.Count; i++)
        {
            if(i <= playerData.speed)
            {
                ratingC[i].color = Color.white;
            }
            else
            {
                ratingC[i].color = Color.black;
            }
        }
        for(int i = 0; i < ratingD.Count; i++)
        {
            if (i <= playerData.rating)
            {
                ratingD[i].color = Color.white;
            }
            else
            {
                ratingD[i].color = Color.black;
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        minimisedContent.SetActive(false);
        expandedContent.SetActive(true);
        if(tween != null) { tween.Kill(); }

        tween = expandedRect.DOSizeDelta(new Vector2(expandedRect.sizeDelta.x, 650), 0.2f);
            //.SetEase(Ease.OutBounce);

        //expandedContent.transform.DOScaleY(2, 0.2f)
          //  .SetEase(Ease.OutBounce);
        //throw new System.NotImplementedException();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (tween != null) { tween.Kill(); }

        tween = expandedRect.DOSizeDelta(new Vector2(expandedRect.sizeDelta.x, expandedRect.sizeDelta.x), 0.1f)
            .OnComplete(() =>
            {
                minimisedContent.SetActive(true);
                expandedContent.SetActive(false);
            });

/*        expandedContent.transform.DOScale(1, 0.2f)
            .SetEase(Ease.InBounce)
            .OnComplete(() =>
            {
                minimsedContent.SetActive(true);
                expandedContent.SetActive(false);
            });*/
        //throw new System.NotImplementedException();
    }
}
