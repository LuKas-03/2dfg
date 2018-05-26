using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {

    public Text nameText_Box;
    public Text dialogText_Box;

    public Animator animator;

    private Queue<Dialogs> dialogs;

    // инициализация очереди из диалоговых фраз
    void Start () {
        dialogs = new Queue<Dialogs>();
    }

    // начать диалог
    public void StartDialog(Dialogs[] dialog)
    {
        animator.SetBool("IsDialogOpen", true);
        dialogs.Clear();
        foreach (Dialogs dial in dialog)
        {
            dialogs.Enqueue(dial);
        }
        DisplayNextPhrase();
    }

    // функция для кнопки продолжения (стрелочки)  диалогах
    public void DisplayNextPhrase()
    {
        if (dialogs.Count == 0)
        {
            EndDialog();
            return;
        }
        Dialogs dialog = dialogs.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(dialog));
    }

    // иллюзия печатания текста в реалтайме
    IEnumerator TypeSentence(Dialogs dialog)
    {
        nameText_Box.text = dialog.npc_name;
        dialogText_Box.text = dialog.phrases;
        yield return null;
        /*
        nameText_Box.text = "";
        dialogText_Box.text = "";
      /*  if (Input.GetKeyDown(KeyCode.Space))
        {
            nameText_Box.text = dialog.npc_name;
            dialogText_Box.text = dialog.phrases;
            yield return null;
        }*//*
        foreach (char letter in dialog.npc_name.ToCharArray())
        {
            nameText_Box.text += letter;
            yield return null;
        }
        foreach (char letter in dialog.phrases.ToCharArray())
        {
            dialogText_Box.text += letter;
            yield return null;
        }*/
    }

    // завершить диалог
    void EndDialog()
    {
        animator.SetBool("IsDialogOpen", false);
        CharacterAnimationController.anim.SetBool("StopMovement", false);
    }
}