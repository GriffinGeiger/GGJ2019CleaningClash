using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{

    float speed = 5.0f;
    float angle = 0;
    Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    Vector2 doorLocation = new Vector2(Screen.width/2, 0);

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(doorLocation.x, doorLocation.y, 0);
        StartCoroutine("Search");
        Destroy(this);
    }

    IEnumerator Search()
    {
       //Move dog out in to the world first
        for (int i =0; i < 5; i++)
            StartCoroutine("MoveTo");

        foreach (Bed b in FindObjectsOfType(typeof(Bed)) as Bed[])
        {
            //Go To Bed
            Vector2 distance = transform.position - b.transform.position;
            angle = Mathf.Atan2(distance.y, distance.x);
            while (transform.position != b.transform.position)
                StartCoroutine("MoveTo");
            //Pull all items out
            while (b.Retrieve() != null)
                //Do something with item

            //Wait x amount of time
            //Go To next bed
            yield return new WaitForSeconds(2.0f);
        }

        Vector3 dogBounds = transform.GetComponent<Mesh>().bounds.size;

        //If the dog is to the right of center
        if (transform.position.x > screenBounds.x / 2 - dogBounds.x/2)
        {
            angle = Mathf.PI;
        }
        //If the dog is to the left of center
        else if (transform.position.x < screenBounds.x / 2)
        {
            angle = 0;
        }

        //Adjust Dog to be above door
        while (Mathf.Abs(transform.position.x - doorLocation.x) > 10)
            StartCoroutine("MoveTo");

        angle = 1.5f * Mathf.PI;

        //Walk Dog through door
        while (transform.position.y - doorLocation.y >= -dogBounds.y)
            StartCoroutine("MoveTo");

        
    }

    IEnumerator MoveTo()
    {
        transform.TransformVector(speed * Mathf.Cos(angle), speed * Mathf.Sin(angle), 0);
        yield return null;
    }
}
