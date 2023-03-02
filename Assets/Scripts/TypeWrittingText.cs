using TMPro;
using UnityEngine;

public class TypeWrittingText : MonoBehaviour
{
    enum Lines
    {
        FirstLine,
        SecondLine,
        ThirdLine,
        NextScene
    }

    [SerializeField] TMP_Text _dialogueText;
    [SerializeField] TMP_Text _skipText;
    [SerializeField] ScenesManager _textSkip;
    [SerializeField] SoundManager _soundManage;
    [SerializeField] AudioClip _letterSound;

    Lines _line;
    float _characterCooldown;
    float _timeCounter;
    int _characterCounter;

    void Start()
    {
        _textSkip._isSkippable = false;

        _timeCounter = Time.time;

        _characterCounter = 0;
        _characterCooldown = 0.06f;

        _line = Lines.FirstLine;
    }

    void Update()
    {
        NextLine();

        if (_textSkip._isSkippable)
        {
            _skipText.gameObject.SetActive(true);
        }
        else
        {
            _skipText.gameObject.SetActive(false);
        }

        switch (_line)
        {
            default:
            case Lines.FirstLine:
                FirstLine();
                break;

            case Lines.SecondLine:
                SecondLine();
                break;

            case Lines.ThirdLine:
                ThirdLine();
                break;

            case Lines.NextScene:
                _textSkip.FadeOutScene();
                break;
        }
    }

    void TypeWrittingEffect()
    {
        _dialogueText.maxVisibleCharacters = _characterCounter;
        TMP_TextInfo textInfo = _dialogueText.textInfo;

        if (_characterCounter < textInfo.characterCount)
        {
            TMP_CharacterInfo charInfo = textInfo.characterInfo[_characterCounter];
            if (charInfo.character == ' ')
            {
                _characterCounter++;
            }
        }

        if (Time.time > _timeCounter + _characterCooldown)
        {
            _timeCounter = Time.time;
            _characterCounter++;
            _soundManage._effectsAudioSource.PlayOneShot(_letterSound);
        }
    }

    void NextLine()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _textSkip._isSkippable == true)
        {
            _characterCounter = 0;
            _textSkip._isSkippable = false;

            switch (_line)
            {
                default:
                case Lines.FirstLine:
                    _line = Lines.SecondLine;
                    break;

                case Lines.SecondLine:
                    _line = Lines.ThirdLine;
                    break;

                case Lines.ThirdLine:
                    _line = Lines.NextScene;
                    break;

                case Lines.NextScene:
                    break;
            }
        }
    }

    void FirstLine()
    {
        _dialogueText.text = "Eu, pessoalmente, entreguei o último Metroid para a Estação Galática de Pesquisa em Ceres, para que os cientistas pudessem estudar a sua qualidade de produção de energia!";

        TypeWrittingEffect();

        if (_characterCounter > _dialogueText.text.Length)
        {
            _textSkip._isSkippable = true;
            _soundManage._effectsAudioSource.Stop();
        }
    }

    void SecondLine()
    {
        _dialogueText.text = "As descobertas dos cientistas foram impressionantes. Eles descobriram que os poderes dos Metroids podem ser extraídos para o bem de toda a civilização!";

        TypeWrittingEffect();

        if (_characterCounter > _dialogueText.text.Length)
        {
            _textSkip._isSkippable = true;
            _soundManage._effectsAudioSource.Stop();
        }
    }

    void ThirdLine()
    {
        _dialogueText.text = "Satisfeita de que tudo ocorreu bem, eu deixei a estação em busca de uma nova recompensa. Mas, eu nem cheguei a estar tão distante do cinturão de asteroides, quando eu captei um sinal de socorro e decidi averiguar!";

        TypeWrittingEffect();

        if (_characterCounter > _dialogueText.text.Length)
        {
            _textSkip._isSkippable = true;
            _soundManage._effectsAudioSource.Stop();
        }
    }
}
