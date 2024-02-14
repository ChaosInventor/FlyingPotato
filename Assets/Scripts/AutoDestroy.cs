        using System.Collections;
        using System.Collections.Generic;
        using UnityEngine;

        public class AutoDestroy : MonoBehaviour {

            private PlayerControler Player;

	        // Use this for initialization
	        void Awake ()
            {

                Player = GameObject.Find("Player").GetComponent<PlayerControler>();

	        }
	
	        // Update is called once per frame
	        void Update ()
            {
		
                if(Vector2.Distance(transform.position,Player.gameObject.transform.position) > 39.99f)
                {
                    Destroy(gameObject);
                }

	        }



}
