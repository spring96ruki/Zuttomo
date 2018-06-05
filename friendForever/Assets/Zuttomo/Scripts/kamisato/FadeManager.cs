using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour {

    [SerializeField]
    private GameObject m_image;
    private Image m_fadeimage;
    Color color;
    float m_fadespeed = 0.006f;

    void Start()
    {
        m_fadeimage = GetComponent<Image>();
        color = m_fadeimage.color;
        m_image.SetActive(false);
    }

    void Update()
    {
    }

    public IEnumerator FadeOut()
    {
        m_image.SetActive(true);
        while (color.a < 1)
        {
            m_fadeimage.color = color;
            color.a += m_fadespeed;
            yield return new WaitForSeconds(0.01f);
        }
    }

    public IEnumerator FadeIn()
    {
        while (color.a > 0)
        {
            m_fadeimage.color = color;
            color.a -= m_fadespeed;
            yield return new WaitForSeconds(0.01f);
        }
        m_image.SetActive(false);
    }
}
