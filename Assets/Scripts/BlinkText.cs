using TMPro;
using UnityEngine;

public class BlinkText : MonoBehaviour
{
    [SerializeField] TMP_Text _text;
    [SerializeField] Color _colorIn;
    [SerializeField] Color _colorOut;
    [SerializeField] float _blinkTime;
    float _blinkTimer;

    void Update()
    {
        _blinkTimer += Time.deltaTime;

        if (_blinkTimer > _blinkTime)
        {
            _text.color = _colorIn;
        }
        else
        {
            _text.color = _colorOut;
        }

        if (_blinkTimer >= _blinkTime * 2)
        {
            _blinkTimer = 0;
        }
    }
}
