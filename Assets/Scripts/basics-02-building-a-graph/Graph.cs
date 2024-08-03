using System;
using UnityEngine;
using basics_03_mathematical_surfaces;

namespace basics_02_building_a_graph
{
    
public class Graph : MonoBehaviour
{
    [SerializeField] private Transform pointPrefab;
    [SerializeField, Range(10,100)] private int resolution = 10;
    [SerializeField] private FunctionLibrary.FunctionName function;
    [SerializeField, Min(0f)] private float functionDuration = 1f, transitionDuration = 1f;

    private Transform[] points;

    private float duration;
    private bool bTransitioning;
    private FunctionLibrary.FunctionName transitionFunction;

    private void Awake()
    {
        float step = 2f / resolution;
        var scale = Vector3.one * step;

        points = new Transform[resolution * resolution];
        
        for (int i = 0, x = 0, z = 0; i < points.Length; i++, x++)
        {
            Transform point = points[i] = Instantiate(pointPrefab);
            point.localScale = scale;
            point.SetParent(transform, false);
        }
    }

    private void Update()
    {
        duration += Time.deltaTime;

        if (bTransitioning)
        {
            if (duration >= transitionDuration) {
                duration -= transitionDuration;
                bTransitioning = false;
            }
        } else if (duration >= functionDuration)
        {
            duration -= functionDuration;
            bTransitioning = true;
            transitionFunction = function;
            function = FunctionLibrary.GetNextFunctionName(function);
        }

        if (bTransitioning)
        {
            UpdateFunctionTransition();
        }
        else
        {
            UpdateFunction();
        }
    }

    void UpdateFunctionTransition()
    {
        FunctionLibrary.Function
            from = FunctionLibrary.GetFunction(transitionFunction),
            to = FunctionLibrary.GetFunction(function);

        float progress = duration / transitionDuration;
        float time = Time.time;
        float step = 2f / resolution;
        float v = 0.5f * step - 1f;

        for (int i = 0, x = 0, z = 0; i < points.Length; i++, x++)
        {
               if (x == resolution)
                        {
                            x = 0;
                            z += 1;
                            v = (z + 0.5f) * step - 1f;
                        }
            
                        float u = (x + 0.5f) * step - 1f;
                        points[i].localPosition = FunctionLibrary.Morph(
                            u, v, time, from, to, progress);
        }
    }

    void UpdateFunction()
    {
        FunctionLibrary.Function f = FunctionLibrary.GetFunction(function);
        
        float time = Time.time;
        float step = 2f / resolution;
        float v = 0.5f * step - 1f;
        for (int i = 0, x = 0, z = 0; i < points.Length; i++, x++)
        {
            if (x == resolution)
            {
                x = 0;
                z += 1;
                v = (z + 0.5f) * step - 1f;
            }

            float u = (x + 0.5f) * step - 1f;
            points[i].localPosition = f(u, v, time);
        }
    }
    }
}
