using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Written by Griffin Geiger

public class CharacterMovement : MonoBehaviour
{
    public Rigidbody2D m_rigidbody;
    public Transform m_aimArrowTransform;
    public SpriteRenderer m_aimArrowSprite;
    public PlayerInput.playerTag m_playerTag; //defines which player controls this character
    public float m_speedFactor;
    public float m_arrowDistance;
    public float m_aimDeadZone = .1f;
    public float m_baseThrowStrength = 1f;
    public Vector2 m_topCornerOfOverlap; //offset from character transform so it follows
    public Vector2 m_bottomCornerOfOverlap; //ditto

    private GameManager gm;
    [SerializeField]
    private Item m_heldItem;
    private float m_stunTime = 0;
    private bool m_stunned = false;
    //public Item m_heldItem;
    private Vector2 m_aimVector;

    private enum interactableType { None, Bed, Battery, SpecialThrowable, CommonThrowable }

    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
        ConnectToPlayerInput();
    }

    private void FixedUpdate()
    {
        if(m_heldItem != null)
        {
            m_heldItem.gameObject.transform.position = transform.position;
        }
    }
    public void ConnectToPlayerInput()
    {
        if (m_playerTag == PlayerInput.playerTag.None)
        {
            Debug.LogWarning("PlayerTag set to no player. Add playerTag in prefab. Ex: Player1, Player2");
        }
        else
        {
            PlayerInput player = gm.getPlayerInput((int)m_playerTag);
            if (player.m_controller == null)
            {
                player.m_controller = this;
            }
            else
            {
                Debug.LogWarning("Replacing PlayerController that is already connected to PlayerInput");
            }
        }
    }
    public void Interact()
    {
        //check different things that can be interacted with and interact accordingly
        Collider2D[] nearbyInteractables =
            Physics2D.OverlapAreaAll((Vector2) transform.position + m_topCornerOfOverlap,(Vector2) transform.position + m_bottomCornerOfOverlap);


       // PrintInteractablesOverlappedWith(nearbyInteractables);

        //Note from last line: topCornerOfOverlap and bottom are offsets from position

        //search for highest priority pickup
        Collider2D foundInteractable = FindHighestPriorityInteractable(nearbyInteractables);

        if (foundInteractable == null) //didn't find any interactables
            return;

        //if hand is empty, fill hand
        if(m_heldItem == null)
        {
            Item foundItem = foundInteractable.GetComponent<Item>();
            if(foundItem != null)
            {
                m_heldItem = foundItem.Pickup();
            }
            else
            {
                m_heldItem = foundInteractable.GetComponent<Bed>().Retrieve();
            }
        }
        else //holding item, unload into bed or do nothing
        {
            Bed bed = foundInteractable.GetComponent<Bed>();
            if (bed != null)//Note: if holding an item and there's another item near the bed, you will not be able to stash
            {
                if (bed.Store(m_heldItem)) //returns true if successfully stored
                {
                    m_heldItem = null; //no longer want to hold this item since it has been stored
                }
            }
                    //Throw button takes care of throwing so no action here
        }
    }

    public void Throw()
    {
        if(m_heldItem is Throwable)
        {
            Vector2 directionVector = (m_aimArrowTransform.position - transform.position).normalized;
            ((Throwable)m_heldItem).Throw(m_baseThrowStrength, directionVector);
            m_heldItem = null;
        }
    }

    public void Aim(Vector2 aimVector) //Move arrow that denotes aiming
    {
        //Rotates around this (the character's) transform
        //About the z axis
        if ((Mathf.Abs(aimVector.x) <= m_aimDeadZone) && (Mathf.Abs(aimVector.y) <= m_aimDeadZone)) //inside dead zone
        {
            //snap arrow to forward position
            float angle = 0f;
            m_aimArrowTransform.rotation = Quaternion.AngleAxis(angle, Vector3.back); //update arrow rotation
            m_aimArrowTransform.position = transform.position +
                Quaternion.AngleAxis(angle, Vector3.back) * new Vector3(m_arrowDistance, 0, 0);
            //disable arrow graphic
            m_aimArrowSprite.enabled = false;
        }
        else  //Receiving active aiming input
        {
            if(!m_aimArrowSprite.enabled) //if the sprite is off because it was neutral last frame
            {
                m_aimArrowSprite.enabled = true;
            }

            //set arrow to angle 
            float angle = Mathf.Atan2(aimVector.y, aimVector.x) * Mathf.Rad2Deg; //find angle of aimVector in degrees
            m_aimArrowTransform.rotation = Quaternion.AngleAxis(angle, Vector3.back); //update arrow rotation
            m_aimArrowTransform.position = transform.position +
                Quaternion.AngleAxis(angle, Vector3.back) * new Vector3(m_arrowDistance, 0, 0);//update arrow position
        }
    }

    public void Move(Vector2 velocity) //should be called in FixedUpdate
    {

        //check status effects to see if possible to move
        if(m_stunned)
        {
            velocity = Vector2.zero;
        }
        //if it is possible to move then move according to the input
        m_rigidbody.AddForce(velocity* m_speedFactor);
    }

    public void Stun(float stunTime) //other things call this to stun the character
    {
        m_stunTime = stunTime;
        m_stunned = true;
        StartCoroutine("StunCoroutine");
    }

    private IEnumerator StunCoroutine()
    {
        yield return new WaitForSeconds(m_stunTime);
        m_stunTime = 0;
        m_stunned = false;
    }

    //if somethings screwed its probably this one

    //Returns highest priority interactable (throwable, bed, powerup). Returns null if no interactable
    //Priorities:  powerups->throwables->Battery->Bed
    private Collider2D FindHighestPriorityInteractable(Collider2D[] nearbyInteractables)
    {
        Collider2D highestPriority = null;
        interactableType highPriorityType = interactableType.None;

        foreach (Collider2D interactable in nearbyInteractables)
        {
            if (interactable.GetComponent<Throwable>() != null)
            {
                if (interactable.GetComponent<Socks>() != null || //check if special throwable
                    interactable.GetComponent<Legos>() != null)
                {
                    highestPriority = interactable;
                    // no need to assign highPriorityType since it all ends here
                    return highestPriority; // there is no higher priority than these objects so go ahead and return
                }
                else //this is a common throwable
                {
                    highestPriority = interactable; //continue to see if there is a higher priority item
                    highPriorityType = interactableType.CommonThrowable;
                }
            }
            else
            if ((highPriorityType < interactableType.Battery) && //would battery be higher priority than current item
                interactable.GetComponent<Battery>())
            {
                highestPriority = interactable;
                highPriorityType = interactableType.Battery;
            }
            else
            if ((highPriorityType < interactableType.Bed) && //would bed be a higher priority than current item
                interactable.GetComponent<Bed>() != null)
            {
                highestPriority = interactable;// continue to see if there is higher priority item
                highPriorityType = interactableType.Bed;
            }
        }
        return highestPriority; //Went through all interactables and found highest priority
    }
    private void PrintInteractablesOverlappedWith(Collider2D[] cols)
    {
        Debug.Log("Printing Colliders");
        foreach(Collider2D col in cols)
        {
            Debug.Log("gameobject" + col.gameObject);
            if (col.gameObject.GetComponent<Throwable>() != null)
            {
                Debug.Log("Overlapped Throwable");
            }
            else Debug.Log("Overlapped something");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(transform.position, m_topCornerOfOverlap + m_bottomCornerOfOverlap);
    }
}
