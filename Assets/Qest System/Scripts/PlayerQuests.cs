using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Список активных и завершённых квестов
/// </summary>
public class PlayerQuests : MonoBehaviour {
    public Dictionary<string, QuestInfo> CurrentQests = new Dictionary<string, QuestInfo>();
    public Dictionary<string, QuestInfo> CompletedQuests = new Dictionary<string, QuestInfo>();
}
