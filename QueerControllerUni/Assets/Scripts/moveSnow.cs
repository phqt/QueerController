using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class moveSnow : MonoBehaviour
{
    
    void Start()
    {
        this.transform.DOMoveY(-16, 13f).SetEase(Ease.Linear).OnComplete(()=> Destroy(gameObject));
        
    }


}
