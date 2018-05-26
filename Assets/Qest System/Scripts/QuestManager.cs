using UnityEngine;

/// <summary>
/// Основной класс квестовой системы
/// Отвечает за выдачу и завершение квеста
/// </summary>
public class QuestManager : MonoBehaviour {
    public Dialogs[] dialog;
    public QuestInfo Quest;

    [SerializeField]
    [Header("Условие активации")]
    public QuestActivators.Activators QuestActivator;

    private bool isDialogOpen = false;

    void OnTriggerStay2D(Collider2D other)    {
        if ((other.CompareTag("Character")) && QuestActivators.Check(QuestActivator))
        {
            PlayerQuests character = other.gameObject.GetComponent<PlayerQuests>();

            // если точка уже пройдена (квест уже выполнен ранее)
            if (Quest.IsComplete || character.CompletedQuests.ContainsKey(Quest.Name))
            {
                return;
            }

            isDialogOpen = true;
            // если квест ещё не выполнен
            if (character.CurrentQests.ContainsKey(Quest.Name))
            {
                if (!TryCompleteQuest(character))
                {
                    FindObjectOfType<QuestNoticeManager>().ShowNotice(
                        new QuestNotice(Quest.Name, Quest.ShortDescription));
                }
            }
            else
            {
                GiveQuest(other);
            }
        }
    }

    private bool TryCompleteQuest(PlayerQuests character)
    {
        if (Quest.TryСomplete())
        {
            // если квест выполнен, происходит его "сдача" (завершение)
            character.CompletedQuests[Quest.Name] = Quest;
            character.CurrentQests.Remove(Quest.Name);

            FindObjectOfType<QuestNoticeManager>().ShowNotice(new QuestNotice(Quest.Name, "Квест завершён!"));
            return true;
        }
        return false;
    }

    private void GiveQuest(Collider2D other)
    {
        FindObjectOfType<DialogManager>().StartDialog(dialog);
        CharacterAnimationController.anim.SetBool("StopMovement", true);

            other.GetComponent<PlayerQuests>().CurrentQests.Add(Quest.Name, Quest);

            foreach (var trigger in Quest.Points)
            {
                BoxCollider2D collider = trigger.gameObject.GetComponent<BoxCollider2D>();
                if (collider != null)
                {
                    collider.enabled = true;
                }
                trigger.Quest = Quest;
            }
            FindObjectOfType<QuestNoticeManager>().ShowNotice(
                new QuestNotice(Quest.Name, "Получен новый квест: " + Quest.ShortDescription)
                );
        }
    }
