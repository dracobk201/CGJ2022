using ScriptableObjectArchitecture;
using UnityEngine;

public class GameSetter : MonoBehaviour
{
    [SerializeField] private BoolReference isGameOver = default(BoolReference);
    [SerializeField] private FloatReference hardnessCurrentAmount = default(FloatReference);

    private void Awake()
    {
        isGameOver.Value = false;
        hardnessCurrentAmount.Value = 0;
    }
}
