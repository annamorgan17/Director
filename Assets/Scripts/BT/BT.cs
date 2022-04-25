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

    //nodes
    private WanderNode wanderNode;
    private PursueNode pursueNode;
    private AttackNode attackNode;

    //conditions
    private RandomPos randomPos;
    private SetPlace setPlace;
    private CanHear inHearRadius;
    private IsPlayerjumping jumpCheck;
    private IsPlayerCrouching crouchCheck;
    private IsPlayerWalking walkCheck;
    private CrouchNoiseCheck innerNoiseCheck;

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

        //nodes
        wanderNode = new WanderNode(owner);
        pursueNode = new PursueNode(owner);
        attackNode = new AttackNode(owner);

        //conditions
        randomPos = new RandomPos(owner);
        setPlace = new SetPlace(owner);
        inHearRadius = new CanHear(owner);
        jumpCheck = new IsPlayerjumping(owner);
        crouchCheck = new IsPlayerCrouching(owner);
        walkCheck = new IsPlayerWalking(owner);
        innerNoiseCheck = new CrouchNoiseCheck(owner);

        //adding root
        Root = rootSelector;

        //adding actions to lists
        rootSelector.AddNode(wanderSeq);

        wanderSeq.AddNode(randomPos);
        wanderSeq.AddNode(wanderNode);

        hearSequence.AddNode(inHearRadius); //is it in range to be heard
        hearSequence.AddNode(hearSelector); //decide whether its walking or crouching or jumping

        hearSelector.AddNode(jumpSequence); //check if jump then pursue
        hearSelector.AddNode(crouchingSequence); //check if crouching then whether in close enough range to pursue
        hearSelector.AddNode(walkingSequence); //check if walking then whether its been long enough to pursue

        jumpSequence.AddNode(jumpCheck);
        jumpSequence.AddNode(pursueSequence);

        crouchingSequence.AddNode(crouchCheck);
        crouchingSequence.AddNode(innerNoiseCheck);
        crouchingSequence.AddNode(pursueSequence);

        walkingSequence.AddNode(walkCheck);
        
    }
}
