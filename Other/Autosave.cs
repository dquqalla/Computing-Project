using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Autosave : MonoBehaviour
{
    public RectTransform rectComponent;
    private float rotateSpeed = 200f;

    private void Update()
    {
        rectComponent.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);
    }

    public void ActivateIcon()
    {
        gameObject.SetActive(true);
        StartCoroutine(RemoveAfterSeconds(3));
    }

    IEnumerator RemoveAfterSeconds(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        this.gameObject.SetActive(false);
    }
}