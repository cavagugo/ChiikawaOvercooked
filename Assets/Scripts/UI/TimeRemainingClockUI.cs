using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeRemainingClockUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timeRemaining;
    [SerializeField] Color regularColor;
    [SerializeField] Color alarmColor;
    [SerializeField] private TMP_Text textComponent;
    [SerializeField] private float shakeIntensity = 3f;
    private bool playSound;

    private void Start()
    {
        timeRemaining.color = regularColor;
        playSound = true;
    }
    private void Update()
    {
        float remainingTime = GameManager.Instance.GetRemainingTime();

        if (remainingTime < 0)
        {
            remainingTime = 0;
        }
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timeRemaining.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (minutes == 0 && seconds <= 10)
        {
            if (playSound)
            {
                SoundManager.Instance.PlayKitchenTimerSound();
                playSound = false;
            }
            timeRemaining.color = alarmColor;
            ShakeText();
        }
    }


    private void ShakeText()
    {
        textComponent.ForceMeshUpdate();
        var textInfo = textComponent.textInfo;

        // Ajusta estos valores para controlar la intensidad
        

        for (int i = 0; i < textInfo.characterCount; i++)
        {
            var charInfo = textInfo.characterInfo[i];
            if (!charInfo.isVisible) continue;

            var verts = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;
            var colors = textInfo.meshInfo[charInfo.materialReferenceIndex].colors32;

            // Generamos un desfase aleatorio por cada CARÁCTER para que vibren diferente
            Vector3 offset = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0) * shakeIntensity;

            for (int j = 0; j < 4; ++j)
            {
                int index = charInfo.vertexIndex + j;
                verts[index] += offset;

                // Aseguramos que el color se mantenga en alarmColor
                colors[index] = alarmColor;
            }
        }

        for (int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            var meshInfo = textInfo.meshInfo[i];
            meshInfo.mesh.vertices = meshInfo.vertices;
            meshInfo.mesh.colors32 = meshInfo.colors32; // Actualizamos colores también
            textComponent.UpdateGeometry(meshInfo.mesh, i);
        }
    }

}
