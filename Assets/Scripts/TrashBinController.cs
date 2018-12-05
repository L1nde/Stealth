using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class TrashBinController : Interactable {

    private bool open;
    public Animator anim;


    public override void interact() {
        open = !open;
        anim.SetBool("open", open);
    }
}
