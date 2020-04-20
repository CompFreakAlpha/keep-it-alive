using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Entity linkedObj;
    public Vector2 offset;

    public Color color = Color.red;

    public float sizeMultiplier = 1;

    void LateUpdate()
    {
        if (linkedObj != null)
        {
            transform.position = (Vector2)linkedObj.transform.position + offset;
            if (linkedObj.GetComponent<Entity>() != null)
            {
                GetComponent<RectTransform>().sizeDelta = new Vector2(0.125f * linkedObj.GetComponent<Entity>().maxHealth * sizeMultiplier, 0.2f);
                transform.Find("Fill").GetComponent<RectTransform>().sizeDelta = new Vector2(0.125f * (linkedObj.GetComponent<Entity>().health) * sizeMultiplier, 0.2f);
                GetComponent<Image>().color = new Color(color.r / 2, color.g / 2, color.b / 2, 1);
                transform.Find("Fill").GetComponent<Image>().color = color;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
