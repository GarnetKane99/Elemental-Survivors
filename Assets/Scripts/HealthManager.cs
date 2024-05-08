using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class HealthManager : MonoBehaviour
{
    public Canvas canvas;
    public GameObject decreaseHealthPopup;
    public TextMeshProUGUI popuptext;
    public Slider scrollBar;
    public float maxHealth = default;

    public void Init(float val)
    {
        //scrollBar.size = val;
        scrollBar.maxValue = val;
        scrollBar.value = val;
        maxHealth = val;
    }

    public void Damaged(float dmg)
    {
        GameObject hPopup = Instantiate(decreaseHealthPopup, scrollBar.transform.position, Quaternion.identity, canvas.transform);
        popuptext = hPopup.GetComponent<TextMeshProUGUI>();

        if(popuptext != null)
        {
            popuptext.text = "-" + dmg;
        }

        hPopup.GetComponent<RectTransform>().DOAnchorPosY(2, 0.3f)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                Destroy(hPopup);
            });

        scrollBar.value -= dmg;//(dmg/10) * maxHealth;
        //scrollBar.size
    }

    public void Regen(float amt)
    {
        GameObject hPopup = Instantiate(decreaseHealthPopup, scrollBar.transform.position, Quaternion.identity, canvas.transform);
        popuptext = hPopup.GetComponent<TextMeshProUGUI>();
        popuptext.color = Color.green;

        if (popuptext != null)
        {
            popuptext.text = "+" + amt;
        }

        hPopup.GetComponent<RectTransform>().DOAnchorPosY(2, 0.3f)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                Destroy(hPopup);
            });

        scrollBar.value += amt;
        //scrollBar.size += amt;
        //scrollBar.size = Mathf.Clamp(scrollBar.v, 0, maxHealth);
    }

    public void IncreaseMaxHealth(float amt)
    {
        maxHealth += amt;
        scrollBar.maxValue = maxHealth;
        scrollBar.value += amt;
    }
}
