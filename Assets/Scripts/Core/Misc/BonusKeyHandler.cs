using System.Text;
using TMPro;
using UnityEngine;

namespace CBH.Core.Core.Misc
{
    public class BonusKeyHandler : MonoBehaviour
    {
        [SerializeField] private TMP_Text textField;

        private const string PrefsKey = "BonusKey";
        private const string RandomSymbolsPattern = "!@#$%^&*()_+-=':;][{}|/";
        private const string KeyPattern = "Ab0B4 5Us";

        private void Start()
        {
            if (PlayerPrefs.HasKey(PrefsKey))
            {
                textField.text = PlayerPrefs.GetString(PrefsKey);
            }
            else
            {
                var key = GenerateKey();
                PlayerPrefs.SetString(PrefsKey, key);
                textField.text = key;
            }
        }

        private string GenerateKey()
        {
            var builder = new StringBuilder();
            builder.Append(Application.version);

            var seed = Random.Range(256, 65535);
            seed -= 137;
            builder.Append($"-{seed}-");
            
            var rnd = new System.Random(seed);
            for (var i = 0; i < KeyPattern.Length; i++)
            {
                builder.Append(KeyPattern[i] + rnd.Next(16384));
                builder.Append(GetRandomString());
            }

            return builder.ToString();
        }

        private string GetRandomString()
        {
            var size = Random.Range(1, 6);
            var builder = new StringBuilder();

            for (var i = 0; i < size; i++)
            {
                builder.Append(RandomSymbolsPattern[Random.Range(0, RandomSymbolsPattern.Length)]);
            }

            return builder.ToString();
        }
    }
}