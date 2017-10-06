using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckScript : MonoBehaviour {

    private Card[] cards;
    public GameObject card;
    private Vector3 originalPosition, originalRotation, originalScale;
    // Use this for initialization
    void Start()
    {
        originalPosition = new Vector3(0.0f, 0.0f, 0.0f);
        originalRotation = new Vector3(0.0f, 0.0f, 0.0f);
        originalScale = new Vector3(40.0f, 50.0f, 0.0f);
        card = (GameObject)Instantiate(Resources.Load("_prefab_/Card"));
        cards = CardFactory.sortToDealingOrder(CardFactory.CreateDeck("3", "Spades"), 4);
        StartCoroutine(Deal(cards, 0.05f));
    }

    // Update is called once per frame
    void Update()
    {
        //card.SetActive(false);
    }

    /**
     * Sequence that deals cards to respective players' hands
     * All wait for second calls are done so animation or statement is successfully executed
     */
    IEnumerator Deal(Card[] cards, float dealTime)
    {
        //Sets canvas value
        Canvas c = gameObject.GetComponentInParent<Canvas>();

        for (int i = 0; i < cards.Length; i++)
        {
            GameObject temp = (GameObject)Instantiate(card, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
            Animation anim = GetComponent<Animation>();
            anim.GetComponent<SpriteRenderer>().sortingOrder = i;
            AnimationCurve curve;

            // create a curve to move the GameObject and assign to the clip
            Keyframe[] keyposx, keyposy, keyrotx, keyroty, keyrotz, keyscalex, keyscaley;

            // create a new AnimationClip
            AnimationClip clip = new AnimationClip();
            clip.legacy = true;

            // initial values of all position and rotation variables other than flipping rotation should be 0, scaling is 40x50 at 0 seconds
            keyposx = new Keyframe[2];
            keyposx[0] = new Keyframe(0.0f, originalPosition.x);
            keyposy = new Keyframe[2];
            keyposy[0] = new Keyframe(0.0f, originalPosition.y);
            keyrotx = new Keyframe[2];
            keyrotx[0] = new Keyframe(0.0f, originalRotation.x);
            keyrotz = new Keyframe[2];
            keyrotz[0] = new Keyframe(0.0f, originalRotation.z);
            keyscalex = new Keyframe[2];
            keyscalex[0] = new Keyframe(0.0f, originalScale.x);
            keyscaley = new Keyframe[2];
            keyscaley[0] = new Keyframe(0.0f, originalScale.y);

            // condition to check if hand belongs to current player
            if (i % 4 == 0)
            {
                // controls the rotation that flips the card
                keyroty = new Keyframe[4];
                keyroty[0] = new Keyframe(0.0f, originalRotation.y);
                keyroty[1] = new Keyframe(dealTime/2, 90f);

                //flip card in the middle of animation event
                //AnimationEvent[] ae = new AnimationEvent[1];
                //ae[0] = new AnimationEvent();
                //ae[0].time = dealTime/2;
                //ae[0].functionName = "Flip";
                //ae[0].intParameter = i;

                keyroty[2] = new Keyframe(dealTime/2, -90f);
                keyroty[3] = new Keyframe(dealTime, originalRotation.y);
                curve = new AnimationCurve(keyroty);
                clip.SetCurve("", typeof(Transform), "localEulerAngles.y", curve);
                //clip.events = ae;

                // controls the scaling of the card
                keyscalex[1] = new Keyframe(dealTime, originalScale.x * 1.5f);
                curve = new AnimationCurve(keyscalex);
                clip.SetCurve("", typeof(Transform), "localScale.x", curve);

                keyscaley[1] = new Keyframe(dealTime, originalScale.y * 1.5f);
                curve = new AnimationCurve(keyscaley);
                clip.SetCurve("", typeof(Transform), "localScale.y", curve);

                // controls the position of the card
                keyposx[1] = new Keyframe(dealTime, c.pixelRect.width * -0.3f + keyscalex[1].value * (i / 4));
                curve = new AnimationCurve(keyposx);
                clip.SetCurve("", typeof(Transform), "localPosition.x", curve);

                keyposy[1] = new Keyframe(dealTime, c.pixelRect.height * -0.5f);
                curve = new AnimationCurve(keyposy);
                clip.SetCurve("", typeof(Transform), "localPosition.y", curve);

                // rotation is zero if it is the player's hand
                keyrotz[1] = new Keyframe(dealTime, originalRotation.z);
                curve = new AnimationCurve(keyrotz);
                clip.SetCurve("", typeof(Transform), "localEulerAngles.z", curve);
            }
            else
            {
                // flipping rotation is zero if not the player's hand
                keyroty = new Keyframe[2];
                keyroty[0] = new Keyframe(0.0f, originalRotation.y);
                keyroty[1] = new Keyframe(dealTime, 180f - 90 * (i % 4));
                curve = new AnimationCurve(keyroty);
                clip.SetCurve("", typeof(Transform), "localEulerAngles.y", curve);

                // controls the scaling of the card
                keyscalex[1] = new Keyframe(dealTime, originalScale.x);
                curve = new AnimationCurve(keyscalex);
                clip.SetCurve("", typeof(Transform), "localScale.x", curve);

                keyscaley[1] = new Keyframe(dealTime, originalScale.y);
                curve = new AnimationCurve(keyscaley);
                clip.SetCurve("", typeof(Transform), "localScale.y", curve);

                // controls the position of the card
                if (i % 2 == 0)
                {
                    keyposx[1] = new Keyframe(dealTime, c.pixelRect.width * 0.25f - keyscalex[1].value * (i / 4));
                }
                else
                {
                    keyposx[1] = new Keyframe(dealTime, c.pixelRect.width * 0.5f * (2 - (i % 4)));
                }
                curve = new AnimationCurve(keyposx);
                clip.SetCurve("", typeof(Transform), "localPosition.x", curve);

                if (i % 2 == 0)
                {
                    keyposy[1] = new Keyframe(dealTime, c.pixelRect.height * 0.5f);
                }
                else
                {
                    keyposy[1] = new Keyframe(dealTime, (c.pixelRect.height * 0.2f - keyscaley[1].value / 2 * (i / 4)) * ((i % 4) - 2));
                }
                curve = new AnimationCurve(keyposy);
                clip.SetCurve("", typeof(Transform), "localPosition.y", curve);

                // controls the rotation that rotates the card to match player's orientation
                keyrotx[1] = new Keyframe(dealTime, -45f + 45f * (i % 2));
                curve = new AnimationCurve(keyrotx);
                clip.SetCurve("", typeof(Transform), "localEulerAngles.x", curve);
                keyrotz[1] = new Keyframe(dealTime, 90 * (i % 4) - 180);
                curve = new AnimationCurve(keyrotz);
                clip.SetCurve("", typeof(Transform), "localEulerAngles.z", curve);
            }

            string animationName = "DealCard" + i;
            // now animate the GameObject
            anim.AddClip(clip, animationName);
            anim.Play(animationName);
            GameObject.Destroy(temp);
            yield return new WaitForSeconds(0.05f);

            Vector3 position = new Vector3(keyposx[1].value, keyposy[1].value, 0);
            Quaternion angles;
            if (i % 4 != 0)
            {
                angles = Quaternion.Euler(keyrotx[1].value, keyroty[1].value, keyrotz[1].value);
            }
            else
            {
                angles = Quaternion.Euler(keyrotx[1].value, keyroty[3].value, keyrotz[1].value);
            }
            Vector3 scale = new Vector3(keyscalex[1].value, keyscaley[1].value, 1);
            GameObject g = Instantiate(card, new Vector3(), angles);

            //have to put local position transform here because when gameobject is instantiated, the parent is not set to canvas yet
            g.transform.SetParent(c.transform);
            g.transform.localPosition = position;
            g.transform.localScale = scale;
            g.GetComponent<SpriteRenderer>().sortingOrder = i / 4;
            
            if (i % 4 == 0)
            {
                g.GetComponent<SpriteRenderer>().sprite = cards[i].Flip();
            }
            else
            {
                g.GetComponent<SpriteRenderer>().sprite = cards[i].GetFace();
            }
            g.SetActive(true); 
        }
    }
}
