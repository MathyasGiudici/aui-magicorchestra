using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CorsiAlertContextManager : MonoBehaviour
{
    /* <summary>
     * Start is called before the first frame update
     * </summary>
     */
    void Start()
    {
        if (!MagicOrchestraParameters.IsContext)
            RemoveContext();
    }

    /* <summary>
     * Update is called once per frame
     * </summary>
     */
    void Update()
    {
        if (!MagicOrchestraParameters.IsContext)
            RemoveContext();
    }

    private void RemoveContext()
    {
        // Changing the bg color
        gameObject.GetComponent<Image>().color = new Color(255, 255, 255);
        // Disabling the director image
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        // Centralizing the text
        gameObject.transform.GetChild(1).gameObject.GetComponent<RectTransform>().localPosition = new Vector3(gameObject.transform.GetChild(1).gameObject.GetComponent<RectTransform>().rect.width / 2,
            gameObject.transform.GetChild(1).gameObject.GetComponent<RectTransform>().localPosition.y, gameObject.transform.GetChild(1).gameObject.GetComponent<RectTransform>().localPosition.z);
        // Changing color of the text
        Color black = new Color();
        ColorUtility.TryParseHtmlString("#000000", out black);
        gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().color = black;
    }
}
