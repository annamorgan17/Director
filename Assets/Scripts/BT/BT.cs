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

    private Sequencer huntSequence;
    private Sequencer attackSequence;
    private Sequencer closeWanderSequence;

    private Sequencer hideSequence;

    //nodes
    private WanderNode wanderNode;
    private WanderNode wanderCloseNode;
    private PursueNode pursueNode;
    private AttackNode attackNode;
    private IdleNode idleNode;
    private HideNode hideNode;
    private HuntNode huntNode;

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
    private HideCheck hideCheck;
    private FindCave findCave;
    private LastKnownLocation prevLocation;
    private closeWanderPos ranPosPrevLoc;
    private CloseWanderCheck closeWanderCheck;
    private CanSee canSee;

    public BT(EnemyAI owner) : base(owner)
    {
        /* connections to scripts */
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

        huntSequence = new Sequencer(owner);
        attackSequence = new Sequencer(owner);
        closeWanderSequence = new Sequencer(owner);

        hideSequence = new Sequencer(owner);

        //nodes
        wanderNode = new WanderNode(owner, 0);
        wanderCloseNode = new WanderNode(owner, 1);
        pursueNode = new PursueNode(owner);
        attackNode = new AttackNode(owner);
        idleNode = new IdleNode(owner);
        hideNode = new HideNode(owner);
        huntNode = new HuntNode(owner);

        //conditions
        randomPos = new RandomPos(owner, owner.wanderTarget);
        setPlace = new SetPlace(owner);
        inHearRadius = new CanHear(owner);
        jumpCheck = new IsPlayerjumping(owner);
        crouchCheck = new IsPlayerCrouching(owner);
        walkCheck = new IsPlayerWalking(owner);
        innerNoiseCheck = new CrouchNoiseCheck(owner);
        justAttacked = new JustAttacked(owner);
        walkTimer = new WalkTimer(owner);
        findCave = new FindCave(owner);
        hideCheck = new HideCheck(owner);
        prevLocation = new LastKnownLocation(owner);
        ranPosPrevLoc = new closeWanderPos(owner, owner.lastKnownLocation); //accessible by director to move close to player
        closeWanderCheck = new CloseWanderCheck(owner);
        canSee = new CanSee(owner);

        //add root connection
        Root = rootSelector;

        /* Adding nodes to lists */

        rootSelector.AddNode(hideSequence);
        rootSelector.AddNode(attackSequence);
        rootSelector.AddNode(hearSequence);
        rootSelector.AddNode(closeWanderSequence);
        rootSelector.AddNode(wanderSeq); 

        /*======== Wandering ======== */

        wanderSeq.AddNode(randomPos);
        wanderSeq.AddNode(wanderNode);

        /*======== Chasing then instant Attacking ======== */

        attackSelector.AddNode(idleSequencer);
        attackSelector.AddNode(pursueSequence);

        idleSequencer.AddNode(justAttacked);
        idleSequencer.AddNode(idleNode);

        pursueSequence.AddNode(pursueNode);
        pursueSequence.AddNode(attackNode);

        /*======== Chasing then Attack Check ======== */

        huntSequence.AddNode(prevLocation);
        huntSequence.AddNode(huntNode); //hunt causes close wander
        huntSequence.AddNode(attackSequence);

        attackSequence.AddNode(canSee);
        attackSequence.AddNode(attackSelector);

        /*======== Close Wander ======== */

        closeWanderSequence.AddNode(closeWanderCheck);
        closeWanderSequence.AddNode(ranPosPrevLoc);
        closeWanderSequence.AddNode(wanderCloseNode);

        /*======== Hiding ======== */

        hideSequence.AddNode(hideCheck);
        hideSequence.AddNode(findCave);
        hideSequence.AddNode(hideNode);

        /*======== Hearing ======== */

        hearSequence.AddNode(inHearRadius); //is it in range to be heard
        hearSequence.AddNode(hearSelector); //decide whether its walking or crouching or jumping

        hearSelector.AddNode(jumpSequence); //check if jump then pursue
        hearSelector.AddNode(crouchingSequence); //check if crouching then whether in close enough range to pursue
        hearSelector.AddNode(walkingSequence); //check if walking then whether its been long enough to pursue

        jumpSequence.AddNode(jumpCheck);
        jumpSequence.AddNode(attackSequence);

        crouchingSequence.AddNode(crouchCheck);
        crouchingSequence.AddNode(innerNoiseCheck);
        crouchingSequence.AddNode(attackSequence);

        walkingSequence.AddNode(walkCheck);
        walkingSequence.AddNode(walkTimer);
        walkingSequence.AddNode(huntSequence);

    }
}
