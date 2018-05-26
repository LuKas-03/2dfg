using UnityEngine;

public class ChildQuestPoints : MonoBehaviour, IRequirement
{
    public string Notification { get; private set; }

    [SerializeField] private QuestPoint[] questPoints = null;

    public bool IsComplete()
    {
        foreach (var i in questPoints)
        {
            if (!i.IsComplete)
            {
                Notification = i.TaskNotification;
                if (i.GetComponent<BoxCollider2D>().enabled == false)
                {
                    i.GetComponent<BoxCollider2D>().enabled = true;
                }
                return false;
            }
        }
        Notification = "";
        return true;
    }
}
