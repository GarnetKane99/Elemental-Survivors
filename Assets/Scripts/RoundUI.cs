using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RoundUI : MonoBehaviour
{
    public static RoundUI inst;
    public RectTransform trans;

    private void Awake()
    {
        if (inst != null)
            Destroy(inst);

        inst = this;
    }

    bool rightLeft = false;

    public void DoMove()
    {
        if (rightLeft)
        {
            trans.DOAnchorPosX(0, 2.0f)
                .SetEase(Ease.OutExpo)
                .OnComplete(() =>
                {
                    trans.DOAnchorPosX(-1600, 1.0f)
                    .SetEase(Ease.OutExpo);
                });
            rightLeft = false;
        }
        else 
        {
            trans.DOAnchorPosX(0, 2.0f)
                .SetEase(Ease.OutExpo)
                .OnComplete(() =>
                {
                    trans.DOAnchorPosX(1600, 1.0f)
                    .SetEase(Ease.OutExpo);
                });
            rightLeft = true;
        }
    }
}
