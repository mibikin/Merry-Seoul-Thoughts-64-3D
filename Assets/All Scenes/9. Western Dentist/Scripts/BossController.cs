﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    public GameObject textBox;
    public GameObject merryText;
    public GameObject mikeText;
    public GameObject mikeBoss;
    public GameObject mikeDeathBurst;
    public GameObject mikeProjectile;
    public Text text;
    public static bool isStartup;

    public GameObject hp1;
    public GameObject hp2;
    public GameObject hp3;
    public GameObject hp4;
    public Text warningText;

    Coroutine startUpCR;
    Coroutine mikeHoverCR;

    public AudioSource audioSource;
    public AudioClip bossHitClip;
    public AudioClip projectileSpawnClip;
    public AudioClip phaseEndClip;
    public AudioClip deathClip;

    public AudioClip postFightMusic;

    public GameObject deathBurst;
    public GameObject logicObject;

    public static int phase1Health;
    public static int phase2Health;
    public static int phase3Health;
    public static int phase4Health;

    public GameObject instantiatorsGroup1;
    public GameObject instantiator1;
    public GameObject instantiator2;
    public GameObject instantiator3;
    public GameObject instantiator4;
    public GameObject instantiator5;
    public GameObject instantiator6;
    public GameObject instantiator7;
    public GameObject instantiator8;

    public GameObject instantiatorsGroup2;
    public GameObject instantiator9;
    public GameObject instantiator10;
    public GameObject instantiator11;
    public GameObject instantiator12;
    public GameObject instantiator13;
    public GameObject instantiator14;
    public GameObject instantiator15;
    public GameObject instantiator16;

    public GameObject instantiatorsGroup3;
    public GameObject instantiator17;
    public GameObject instantiator18;
    public GameObject instantiator19;
    public GameObject instantiator20;
    public GameObject instantiator21;
    public GameObject instantiator22;
    public GameObject instantiator23;
    public GameObject instantiator24;

    public GameObject instantiatorsGroup4;
    public GameObject instantiator25;
    public GameObject instantiator26;
    public GameObject instantiator27;
    public GameObject instantiator28;
    public GameObject instantiator29;
    public GameObject instantiator30;
    public GameObject instantiator31;
    public GameObject instantiator32;

    Vector2 newPosition;
    float newSpeed;

    float newRotationSpeed1;
    float newRotationSpeed2;
    float newRotationSpeed3;
    float newRotationSpeed4;

    Coroutine movementCR;
    Coroutine phaseCR;

    Coroutine instantiation1CR;
    Coroutine instantiation2CR;
    Coroutine instantiation3CR;
    Coroutine instantiation4CR;

    Coroutine flashDamageCR;

    int choice;

    public static bool phaseOver;

    float fireRate1;
    float fireRate2;
    float fireRate3;
    float fireRate4;

    float lastHit;

    void Start()
    {
        phase1Health = 500;
        phase2Health = 1000;
        phase3Health = 1500;
        phase4Health = 2000;
        lastHit = 0;
        startUpCR = StartCoroutine(StartUp());
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerProjectile")
        {
            if (Time.time > lastHit + 0.07f)
            {
                lastHit = Time.time;
                audioSource.PlayOneShot(bossHitClip);
            }
            if (flashDamageCR != null)
            {
                StopCoroutine(flashDamageCR);
            }
            flashDamageCR = StartCoroutine(FlashDamage());
            Destroy(collision.gameObject);
            LogicController.playerScore += 100;
            if (phase1Health > 0)
            {
                phase1Health -= 1;
            }
            else if (phase2Health > 0)
            {
                phase2Health -= 1;
            }
            else if (phase3Health > 0)
            {
                phase3Health -= 1;
            }
            else if (phase4Health > 0)
            {
                phase4Health -= 1;
            }
        }
        if (collision.gameObject.tag == "SpecialAttack")
        {
            if (Time.time > lastHit + 0.07f)
            {
                lastHit = Time.time;
                audioSource.PlayOneShot(bossHitClip);
            }
            if (flashDamageCR != null)
            {
                StopCoroutine(flashDamageCR);
            }
            flashDamageCR = StartCoroutine(FlashDamage());
            LogicController.playerScore += 1000;
            if (phase1Health > 0)
            {
                phase1Health -= 1;
            }
            else if (phase2Health > 0)
            {
                phase2Health -= 1;
            }
            else if (phase3Health > 0)
            {
                phase3Health -= 1;
            }
            else if (phase4Health > 0)
            {
                phase4Health -= 1;
            }
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "SpecialAttack")
        {
            if (Time.time > lastHit + 0.07f)
            {
                lastHit = Time.time;
                audioSource.PlayOneShot(bossHitClip);
            }
            if (flashDamageCR != null)
            {
                StopCoroutine(flashDamageCR);
            }
            flashDamageCR = StartCoroutine(FlashDamage());
            LogicController.playerScore += 1000;
            if (phase1Health > 0)
            {
                phase1Health -= 1;
            }
            else if (phase2Health > 0)
            {
                phase2Health -= 1;
            }
            else if (phase3Health > 0)
            {
                phase3Health -= 1;
            }
            else if (phase4Health > 0)
            {
                phase4Health -= 1;
            }
        }
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, newPosition, newSpeed * Time.deltaTime);
        instantiatorsGroup1.transform.Rotate(Vector3.forward * newRotationSpeed1 * Time.deltaTime);
        instantiatorsGroup2.transform.Rotate(Vector3.forward * newRotationSpeed2 * Time.deltaTime);
        instantiatorsGroup3.transform.Rotate(Vector3.forward * newRotationSpeed3 * Time.deltaTime);
        instantiatorsGroup4.transform.Rotate(Vector3.forward * newRotationSpeed4 * Time.deltaTime);

        if (phase1Health == 0)
        {
            audioSource.PlayOneShot(phaseEndClip);
            phase1Health--;
            MerryController.merryHealth += 1;
            if (MerryController.merryHealth > 5)
            {
                MerryController.merryHealth = 5;
            }
            StopCoroutine(movementCR);
            movementCR = StartCoroutine(Movement());
            StopCoroutine(phaseCR);
            phaseCR = StartCoroutine(StartPhase());
        }
        else if (phase2Health == 0)
        {
            audioSource.PlayOneShot(phaseEndClip);
            phase2Health--;
            MerryController.merryHealth += 2;
            if (MerryController.merryHealth > 5)
            {
                MerryController.merryHealth = 5;
            }
            StopCoroutine(movementCR);
            movementCR = StartCoroutine(Movement());
            StopCoroutine(phaseCR);
            phaseCR = StartCoroutine(StartPhase());
        }
        else if (phase3Health == 0)
        {
            audioSource.PlayOneShot(phaseEndClip);
            phase3Health--;
            MerryController.merryHealth += 3;
            if (MerryController.merryHealth > 5)
            {
                MerryController.merryHealth = 5;
            }
            StopCoroutine(movementCR);
            movementCR = StartCoroutine(Movement());
            StopCoroutine(phaseCR);
            phaseCR = StartCoroutine(StartPhase());
        }
        else if (phase4Health == 0)
        {
            phase4Health--;
            StopAllCoroutines();
            StartCoroutine(DeathAnimation());
        }
    }

    IEnumerator Movement()
    {
        newPosition = new Vector2(Random.Range(73f, 560f), Random.Range(372f, 627.7f));
        newSpeed = Random.Range(50f, 125f);
        yield return new WaitForSeconds(Random.Range(5f, 10f));
        movementCR = StartCoroutine(Movement());
    }

    IEnumerator StartPhase()
    {
        if (instantiation1CR != null)
        {
            StopCoroutine(instantiation1CR);
        }
        if (instantiation2CR != null)
        {
            StopCoroutine(instantiation2CR);
        }
        if (instantiation3CR != null)
        {
            StopCoroutine(instantiation3CR);
        }
        if (instantiation4CR != null)
        {
            StopCoroutine(instantiation4CR);
        }
        phaseOver = true;
        yield return new WaitForSeconds(3);
        phaseOver = false;
        if (phase1Health > 0)
        {
            Instantiator1.selectedProjectile = "BallBlue";
            Instantiator2.selectedProjectile = "BallRed";
            Instantiator3.selectedProjectile = "BallBlue";
            Instantiator4.selectedProjectile = "BallRed";
        }
        else if (phase2Health > 0)
        {
            Instantiator1.selectedProjectile = "BallBlue";
            Instantiator2.selectedProjectile = "BallRed";
            Instantiator3.selectedProjectile = "BallGreen";
            Instantiator4.selectedProjectile = "BallRed";
        }
        else if (phase3Health > 0)
        {
            Instantiator1.selectedProjectile = "BallBlue";
            Instantiator2.selectedProjectile = "BallRed";
            Instantiator3.selectedProjectile = "BallGreen";
            Instantiator4.selectedProjectile = "OvalYellow";
        }
        else if (phase4Health > 0)
        {
            Instantiator1.selectedProjectile = "BallRed";
            Instantiator2.selectedProjectile = "BallGreen";
            Instantiator3.selectedProjectile = "OvalYellow";
            Instantiator4.selectedProjectile = "CardPink";
        }

        if (Instantiator1.selectedProjectile == "BallBlue")
        {
            fireRate1 = Random.Range(0.25f, 0.3f);
        }
        else if (Instantiator1.selectedProjectile == "BallRed")
        {
            fireRate1 = Random.Range(0.5f, 0.75f);
        }

        if (Instantiator2.selectedProjectile == "BallBlue")
        {
            fireRate2 = Random.Range(0.25f, 0.3f);
        }
        else if (Instantiator2.selectedProjectile == "BallRed")
        {
            fireRate2 = Random.Range(0.5f, 0.75f);
        }
        else if (Instantiator2.selectedProjectile == "BallGreen")
        {
            fireRate2 = Random.Range(1f, 1.25f);
        }

        if (Instantiator3.selectedProjectile == "BallBlue")
        {
            fireRate3 = Random.Range(0.25f, 0.3f);
        }
        else if (Instantiator3.selectedProjectile == "BallRed")
        {
            fireRate3 = Random.Range(0.5f, 0.75f);
        }
        else if (Instantiator3.selectedProjectile == "BallGreen")
        {
            fireRate3 = Random.Range(1f, 1.25f);
        }
        else if (Instantiator3.selectedProjectile == "OvalYellow")
        {
            fireRate3 = Random.Range(0.25f, 0.5f);
        }

        if (Instantiator4.selectedProjectile == "BallBlue")
        {
            fireRate4 = Random.Range(0.25f, 0.3f);
        }
        else if (Instantiator4.selectedProjectile == "BallRed")
        {
            fireRate4 = Random.Range(0.5f, 0.75f);
        }
        else if (Instantiator4.selectedProjectile == "BallGreen")
        {
            fireRate4 = Random.Range(1f, 1.25f);
        }
        else if (Instantiator4.selectedProjectile == "OvalYellow")
        {
            fireRate4 = Random.Range(0.25f, 0.5f);
        }
        else if (Instantiator4.selectedProjectile == "CardPink")
        {
            fireRate4 = 3f;
        }

        choice = Random.Range(0, 3);
        switch (choice)
        {
            case 0:
                newRotationSpeed1 = Random.Range(-50f, 50f);
                newRotationSpeed2 = Random.Range(-50f, 50f);
                newRotationSpeed3 = Random.Range(-50f, 50f);
                newRotationSpeed4 = Random.Range(-50f, 50f);
                instantiation1CR = StartCoroutine(Instantiation1());
                instantiation2CR = StartCoroutine(Instantiation2());
                instantiation3CR = StartCoroutine(Instantiation3());
                instantiation4CR = StartCoroutine(Instantiation4());
                break;
            case 1:
                newRotationSpeed1 = Random.Range(0f, 50f);
                newRotationSpeed2 = -newRotationSpeed1;
                newRotationSpeed3 = newRotationSpeed1;
                newRotationSpeed4 = -newRotationSpeed1;
                instantiation1CR = StartCoroutine(Instantiation1());
                instantiation2CR = StartCoroutine(Instantiation2());
                instantiation3CR = StartCoroutine(Instantiation3());
                instantiation4CR = StartCoroutine(Instantiation4());
                break;
            case 2:
                newRotationSpeed1 = Random.Range(0f, 25f);
                newRotationSpeed2 = newRotationSpeed1;
                newRotationSpeed3 = newRotationSpeed1;
                newRotationSpeed4 = newRotationSpeed1;
                instantiation1CR = StartCoroutine(Instantiation1());
                instantiation2CR = StartCoroutine(Instantiation2());
                instantiation3CR = StartCoroutine(Instantiation3());
                instantiation4CR = StartCoroutine(Instantiation4());
                break;
        }
    }

    IEnumerator Instantiation1()
    {
        audioSource.PlayOneShot(projectileSpawnClip);
        instantiator1.SetActive(true);
        instantiator2.SetActive(true);
        instantiator3.SetActive(true);
        instantiator4.SetActive(true);
        instantiator5.SetActive(true);
        instantiator6.SetActive(true);
        instantiator7.SetActive(true);
        instantiator8.SetActive(true);
        yield return new WaitForSeconds(fireRate1);
       instantiation1CR = StartCoroutine(Instantiation1());
    }

    IEnumerator Instantiation2()
    {
        audioSource.PlayOneShot(projectileSpawnClip);
        instantiator9.SetActive(true);
        instantiator10.SetActive(true);
        instantiator11.SetActive(true);
        instantiator12.SetActive(true);
        instantiator13.SetActive(true);
        instantiator14.SetActive(true);
        instantiator15.SetActive(true);
        instantiator16.SetActive(true);
        yield return new WaitForSeconds(fireRate2);
        instantiation2CR = StartCoroutine(Instantiation2());
    }

    IEnumerator Instantiation3()
    {
        audioSource.PlayOneShot(projectileSpawnClip);
        instantiator17.SetActive(true);
        instantiator18.SetActive(true);
        instantiator19.SetActive(true);
        instantiator20.SetActive(true);
        instantiator21.SetActive(true);
        instantiator22.SetActive(true);
        instantiator23.SetActive(true);
        instantiator24.SetActive(true);
        yield return new WaitForSeconds(fireRate3);
        instantiation3CR = StartCoroutine(Instantiation3());
    }

    IEnumerator Instantiation4()
    {
        audioSource.PlayOneShot(projectileSpawnClip);
        instantiator25.SetActive(true);
        instantiator26.SetActive(true);
        instantiator27.SetActive(true);
        instantiator28.SetActive(true);
        instantiator29.SetActive(true);
        instantiator30.SetActive(true);
        instantiator31.SetActive(true);
        instantiator32.SetActive(true);
        yield return new WaitForSeconds(fireRate4);
        instantiation4CR = StartCoroutine(Instantiation4());
    }

    public void restartPhase()
    {
        StopCoroutine(movementCR);
        movementCR = StartCoroutine(Movement());
        StopCoroutine(phaseCR);
        phaseCR = StartCoroutine(StartPhase());
    }

    public void stopPhase()
    {
        StopCoroutine(movementCR);
        StopCoroutine(phaseCR);
        StopAllCoroutines();
    }

    IEnumerator FlashDamage()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0.5f, 0.5f, 1);
        yield return null;
        yield return null;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
    }

    IEnumerator DeathAnimation()
    {
        Time.timeScale = 0;
        logicObject.GetComponent<AudioSource>().Stop();
        logicObject.GetComponent<LogicController>().stopCameraShake();
        logicObject.GetComponent<LogicController>().startCameraShake();
        yield return new WaitForSecondsRealtime(2);
        logicObject.GetComponent<LogicController>().stopCameraShake();
        Time.timeScale = 1;
        audioSource.PlayOneShot(deathClip);
        deathBurst.SetActive(true);
        transform.GetComponent<SpriteRenderer>().enabled = false;
        audioSource.PlayOneShot(phaseEndClip);
        for (float t = 0f; t < 1.0f; t += Time.unscaledDeltaTime * 4)
        {
            deathBurst.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1 - t);
            deathBurst.transform.localScale = new Vector3(850 * t, 850 * t, 0);
            yield return null;
        }
        deathBurst.transform.localScale = new Vector3(0, 0, 0);
        deathBurst.SetActive(false);
        yield return new WaitForSeconds(5);
        DontDestroyOnLoad(new GameObject("levelCompleted"));
    }

    IEnumerator MikeHover()
    {
        for(float t = 0f; t < (2.0f * Mathf.PI); t += Time.deltaTime)
        {
            mikeBoss.transform.position = new Vector2(303, 560 + (Mathf.Sin(t) * 10));
            yield return null;
        }
        mikeHoverCR = StartCoroutine(MikeHover());
    }

    IEnumerator StartUp()
    {
        isStartup = true;
        logicObject.GetComponent<AudioSource>().Play();
        mikeHoverCR = StartCoroutine(MikeHover());
        yield return new WaitForSeconds(4);
        mikeText.GetComponent<RawImage>().color = new Color(1, 1, 1, 1);
        text.text = "I'm glad you could finally make it, Merry.";
        yield return new WaitForSeconds(4);
        text.text = "Well, not really, but still.";
        yield return new WaitForSeconds(4);
        mikeText.GetComponent<RawImage>().color = new Color(1, 1, 1, 0.6f);
        merryText.GetComponent<RawImage>().color = new Color(1, 1, 1, 1);
        text.text = ". . .";
        yield return new WaitForSeconds(3);
        mikeText.GetComponent<RawImage>().color = new Color(1, 1, 1, 1);
        merryText.GetComponent<RawImage>().color = new Color(1, 1, 1, 0.6f);
        text.text = "You never really were a talker, huh?";
        yield return new WaitForSeconds(4);
        text.text = "I'm kind of tired, so let's just do this whole boss fight thing.";
        yield return new WaitForSeconds(4);
        text.text = "Now, just wait a minute or two, while I do some stre-";
        yield return new WaitForSeconds(3);
        text.text = "Huh!?";
        yield return new WaitForSeconds(1);
        StopCoroutine(mikeHoverCR);
        for (float t = 760f; t > 556f; t -= Time.deltaTime * 500)
        {
            mikeProjectile.transform.position = new Vector2(303, t);
            yield return null;
        }
        textBox.SetActive(false);
        merryText.SetActive(false);
        mikeText.SetActive(false);
        text.enabled = false;
        Destroy(mikeProjectile);
        logicObject.GetComponent<AudioSource>().Stop();
        audioSource.PlayOneShot(deathClip);
        mikeDeathBurst.SetActive(true);
        mikeBoss.GetComponent<SpriteRenderer>().enabled = false;
        for (float t = 0f; t < 1.0f; t += Time.unscaledDeltaTime * 4)
        {
            mikeDeathBurst.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1 - t);
            mikeDeathBurst.transform.localScale = new Vector3(850 * t, 850 * t, 0);
            yield return null;
        }
        mikeDeathBurst.transform.localScale = new Vector3(0, 0, 0);
        mikeDeathBurst.SetActive(false);
        yield return new WaitForSeconds(1);
        for (float t = 768f; t > 560f; t -= Time.unscaledDeltaTime * 50)
        {
            transform.position = new Vector2(303, t);
            yield return null;
        }
        StartCoroutine(WarningTextSweep());
        transform.GetComponent<BoxCollider2D>().enabled = true;
        logicObject.GetComponent<AudioSource>().clip = postFightMusic;
        logicObject.GetComponent<AudioSource>().Play();
        hp1.GetComponent<Image>().enabled = true;
        hp2.GetComponent<Image>().enabled = true;
        hp3.GetComponent<Image>().enabled = true;
        hp4.GetComponent<Image>().enabled = true;
        isStartup = false;
        movementCR = StartCoroutine(Movement());
        phaseCR = StartCoroutine(StartPhase());
    }

    IEnumerator WarningTextSweep()
    {
        warningText.gameObject.SetActive(true);
        for (float t = 270; t >= -100; t -= Time.deltaTime * 1000)
        {
            warningText.rectTransform.localPosition = new Vector3(t, 0, 0);
            yield return null;
        }
        for (float t = -100; t >= -130; t -= Time.deltaTime * 15)
        {
            warningText.rectTransform.localPosition = new Vector3(t, 0, 0);
            yield return null;
        }
        for (float t = -130; t >= -455; t -= Time.deltaTime * 1000)
        {
            warningText.rectTransform.localPosition = new Vector3(t, 0, 0);
            yield return null;
        }
        warningText.gameObject.SetActive(false);
    }
}
