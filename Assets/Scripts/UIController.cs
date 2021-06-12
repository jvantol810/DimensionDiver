using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject DimensionTitle;
    public GameObject DimensionTagline;

    private bool titleFadeIn;
    private bool taglineFadeIn;
    private Color titleColor;
    private Color taglineColor;
    // Start is called before the first frame update
    void Start()
    {
        InitializeTitleandTagline();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTitleandTagline();
    }

    private void InitializeTitleandTagline()
    {
        titleColor = new Color(1, 1, 1, 0);
        taglineColor = new Color(1, 1, 1, 0);
        DimensionTitle.SetActive(false);
        DimensionTitle.GetComponent<Text>().color = titleColor;
        DimensionTagline.SetActive(false);
        DimensionTagline.GetComponent<Text>().color = taglineColor;
        titleFadeIn = false;
        taglineFadeIn = false;
        StartCoroutine(TitleIntro());
    }

    private IEnumerator TitleIntro()
    {
        yield return new WaitForSeconds(0.2f);
        DimensionTitle.SetActive(true);
        DimensionTagline.SetActive(true);
        titleFadeIn = true;
        yield return new WaitForSeconds(1.2f);
        taglineFadeIn = true;
        yield return new WaitForSeconds(5f);
        titleFadeIn = false;
        taglineFadeIn = false;
    }

    private void UpdateTitleandTagline()
    {
        if (titleFadeIn && titleColor.a < 1f)
        {
            titleColor = new Color(1, 1, 1, titleColor.a + (.8f * Time.deltaTime));
            DimensionTitle.GetComponent<Text>().color = titleColor;
        }
        else
        {
            titleColor = new Color(1, 1, 1, titleColor.a - (.8f * Time.deltaTime));
            DimensionTitle.GetComponent<Text>().color = titleColor;
        }
        if (taglineFadeIn && taglineColor.a < 1f)
        {
            taglineColor = new Color(1, 1, 1, taglineColor.a + (.8f * Time.deltaTime));
            DimensionTagline.GetComponent<Text>().color = taglineColor;
        }
        else
        {
            taglineColor = new Color(1, 1, 1, taglineColor.a - (.8f * Time.deltaTime));
            DimensionTagline.GetComponent<Text>().color = taglineColor;
        }
    }
}
