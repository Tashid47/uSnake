using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Snake : MonoBehaviour
{
   private Vector2 _direction = Vector2.right;
   private List<Transform> _segments = new List<Transform>();
   public Transform segmentPrefab;
   public int initSize = 3;

   private void Start()
   {
       ResetState();
   }

   private void Update()
   {
       if (Input.GetKeyDown(KeyCode.W)){
           _direction = Vector2.up;
       } else if (Input.GetKeyDown(KeyCode.S)){
           _direction = Vector2.down;
       } else if (Input.GetKeyDown(KeyCode.A)){
           _direction = Vector2.left;
       } else if (Input.GetKeyDown(KeyCode.D)){
           _direction = Vector2.right;
       } else if (Input.GetKeyDown(KeyCode.UpArrow)){
           _direction = Vector2.up;
       } else if (Input.GetKeyDown(KeyCode.DownArrow)){
           _direction = Vector2.down;
       } else if (Input.GetKeyDown(KeyCode.LeftArrow)){
           _direction = Vector2.left;
       } else if (Input.GetKeyDown(KeyCode.RightArrow)){
           _direction = Vector2.right;
       }
   }
   private void FixedUpdate() 
   {
       for (int i = _segments.Count - 1; i > 0; i--) {
            _segments[i].position = _segments[i - 1].position;
        }
       this.transform.position = new Vector3(
           Mathf.Round(this.transform.position.x) + _direction.x,
           Mathf.Round(this.transform.position.y) + _direction.y,
           0.0f
       );
   }
   private void Grow()
    {
       Transform segment = Instantiate(this.segmentPrefab);
       segment.position = _segments[_segments.Count - 1].position;
       _segments.Add(segment);
    }
    private void ResetState()
    {
        for (int i = 1; i < _segments.Count; i++) {
            Destroy(_segments[i].gameObject);
        }

        _segments.Clear();
        _segments.Add(this.transform);
        for (int i = 1; i < this.initSize; i++){
            _segments.Add(Instantiate(this.segmentPrefab));
        }
        this.transform.position = Vector3.zero;
    } 
    private void loadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Food"){
            Grow();
            Score.scoreValue += 1;
            Sounds.PlaySound("eat");
        } else if (other.tag == "Barrier"){
            Sounds.PlaySound("death");
            Invoke("ResetState", 0.1f);
        }
    }
}
