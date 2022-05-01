using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BT : BTBase
{
    //composities
    private Selector rootSelector; //wander attack hunt hide
    private Sequencer wanderSeq;

    private Sequencer hearSequence;
    private Selector hearSelector;
    private Sequencer walkingSequence;
    private Sequencer crouchingSequence;
    private Sequencer jumpSequence;

    private Sequencer pursueSequence;
    private Selector attackSelector;

    private Sequencer idleSequencer;

    //nodes
    private WanderNode wanderNode;
    private PursueNode pursueNode;
    private AttackNode attackNode;
    private IdleNode idleNode;

    //conditions
    private RandomPos randomPos;
    private SetPlace setPlace;
    private CanHear inHearRadius;
    private IsPlayerjumping jumpCheck;
    private IsPlayerCrouching crouchCheck;
    private IsPlayerWalking walkCheck;
    private CrouchNoiseCheck innerNoiseCheck;
    private JustAttacked justAttacked;
    private WalkTimer walkTimer;

    public BT(EnemyAI owner) : base(owner)
    {
        //connections
        //composites
        rootSelector = new Selector(owner);
        wanderSeq = new Sequencer(owner);

        hearSequence = new Sequencer(owner);
        hearSelector = new Selector(owner);
        walkingSequence = new Sequencer(owner);
        crouchingSequence = new Sequencer(owner);
        jumpSequence = new Sequencer(owner);

        pursueSequence = new Sequencer(owner);
        attackSelector = new Selector(owner);

        idleSequencer = new Sequencer(owner);

        //nodes
        wanderNode = new WanderNode(owner);
        pursueNode = new PursueNode(owner);
        attackNode = new AttackNode(owner);
        idleNode = new IdleNode(owner);

        //conditions
        randomPos = new RandomPos(owner);
        setPlace = new SetPlace(owner);
        inHearRadius = new CanHear(owner);
        jumpCheck = new IsPlayerjumping(owner);
        crouchCheck = new IsPlayerCrouching(owner);
        walkCheck = new IsPlayerWalking(owner);
        innerNoiseCheck = new CrouchNoiseCheck(owner);
        justAttacked = new JustAttacked(owner);
        walkTimer = new WalkTimer(owner);

        //adding root
        Root = rootSelector;

        //adding actions to lists
        rootSelector.AddNode(wanderSeq);

        /*======== Wandering ======== */

        wanderSeq.AddNode(randomPos);
        wanderSeq.AddNode(wanderNode);

        /*======== Chasing & Attacking ======== */

        attackSelector.AddNode(idleSequencer);
        attackSelector.AddNode(pursueSequence);

        idleSequencer.AddNode(justAttacked);
        idleSequencer.AddNode(idleNode);

        pursueSequence.AddNode(pursueNode);
        pursueSequence.AddNode(attackNode);

        /*======== Hearing ======== */

        hearSequence.AddNode(inHearRadius); //is it in range to be heard
        hearSequence.AddNode(hearSelector); //decide whether its walking or crouching or jumping

        hearSelector.AddNode(jumpSequence); //check if jump then pursue
        hearSelector.AddNode(crouchingSequence); //check if crouching then whether in close enough range to pursue
        hearSelector.AddNode(walkingSequence); //check if walking then whether its been long enough to pursue

        jumpSequence.AddNode(jumpCheck);
        jumpSequence.AddNode(attackSelector);

        crouchingSequence.AddNode(crouchCheck);
        crouchingSequence.AddNode(innerNoiseCheck);
        crouchingSequence.AddNode(attackSelector);

        walkingSequence.AddNode(walkCheck);
        walkingSequence.AddNode(walkTimer);
        walkingSequence.AddNode(attackSelector);

        /*======== Seeing ======== */

    }
}
