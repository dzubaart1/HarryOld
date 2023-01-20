using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandMagic : MonoBehaviour
{
    private Material material;
    private List<Color> colors;
    private Color first, second;
    private int i;
    private float sum;

    private void Start()
    {
        i = 1;
        sum = 0;
        material = GetComponent<MeshRenderer>().material;
        colors = new List<Color>();
        colors.Add(Color.yellow);
        colors.Add(Color.green);
        colors.Add(Color.blue);
        colors.Add(Color.magenta);
        colors.Add(Color.red);
        first = colors[0];
        second = colors[1];

    }
    public void activateGradient()
    {
        StartCoroutine(animate());
    }

    IEnumerator animate()
    {
        while (i != 4)
        {
            if (sum > 2.5)
            {
                first = second;
                second = colors[(i + 1) % 5];
                i++;
                sum = 0;
            }
            Color currentColor = Color.Lerp(first, second, (Mathf.Sin(Time.time) + 1) / 2) ;
            material.SetColor("_EmissionColor", currentColor * 1f);
            sum += Time.deltaTime;
            yield return null;
        }
    }
}
