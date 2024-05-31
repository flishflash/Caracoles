using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTileAnimation : MonoBehaviour
{
    private float animTime = 0.25f;
    private float elementCadency = 0.05f;
    private List<GameObject> _childs = new();
    private List<float> _initialHeights = new();
        
    private WindowGenerator _windowGenerator;

    void Start()
    {
        //_windowGenerator = gameObject.GetComponent<WindowGenerator>();
        //if (_windowGenerator != null) _windowGenerator.ended += GetValues;
        //else GetValues(true);

        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            if (!child.name.StartsWith("Collider"))
            {
                _childs.Add(child);
                _initialHeights.Add(child.transform.localPosition.y);
                child.transform.localPosition += new Vector3(0, Random.Range(-1, -1.5f), 0);
            }
        }
        StartCoroutine(StartAnimation());
    }

    private IEnumerator StartAnimation()
    {
        for (int i = 0; i < _childs.Count; i++)
        {
            if (_childs[i] != null)
            {
                StartCoroutine(ChildAnim(_childs[i], _initialHeights[i]));
                yield return new WaitForSeconds(elementCadency);
            }
            else yield return null;
        }
    }

    private IEnumerator ChildAnim(GameObject go, float end)
    {
        float start = go.transform.localPosition.y;
        float time = 0f;
        while (time <= animTime)
        {
            time += Time.deltaTime;
            float currentHeight = Mathf.Lerp(start, end, time * (1 / animTime));

            go.transform.localPosition = new Vector3(
                go.transform.localPosition.x,
                currentHeight,
                go.transform.localPosition.z
                );

            yield return null;
        }
        go.transform.localPosition = new Vector3(
            go.transform.localPosition.x,
            end,
            go.transform.localPosition.z
            );
    }
}
