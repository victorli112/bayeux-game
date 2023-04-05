using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneStart : MonoBehaviour
{
    public SpriteRenderer hero;
    public SpriteRenderer boss;
    public float fadeDuration;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Canvas>().enabled = false;
        StartCoroutine(FadeInCharacters());
    }

    IEnumerator FadeInCharacters() {
        // chars start with alpha value 0
        Color heroColor = hero.color;
        hero.color = new Color(heroColor.r, heroColor.g, heroColor.b, 0f);
        Color bossColor = boss.color;
        boss.color = new Color(bossColor.r, bossColor.g, bossColor.b, 0f);

        float currentTime = 0f;
        while (currentTime < fadeDuration) {
            currentTime += Time.deltaTime;
            Color currentHeroColor = hero.color;
            hero.color = new Color(currentHeroColor.r, currentHeroColor.g, currentHeroColor.b, 
                Mathf.Lerp(currentHeroColor.a, 1, currentTime / fadeDuration));
            Color currentBossColor = boss.color;
            boss.color = new Color(currentBossColor.r, currentBossColor.g, currentBossColor.b, 
                Mathf.Lerp(currentBossColor.a, 1, currentTime / fadeDuration));
            yield return null;
        }

        // after fade in, enable canvas
        GetComponent<Canvas>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
