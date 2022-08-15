using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class StatusDisplayUI : MonoBehaviour
{
    Player player;
    public TextMeshProUGUI HpValue;
    public TextMeshProUGUI MithrilValue;
    public TextMeshProUGUI TearsValue;
    void Start()
    {
        player = Player.Instance;
    }

    void Update()
    {
        HpValue.text = player.CurrentHP.ToString();
        MithrilValue.text = player.Mithrils.ToString();
        TearsValue.text = player.Tears.ToString();

    }
}
