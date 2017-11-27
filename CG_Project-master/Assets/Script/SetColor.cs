using System.Collections;
using System.Linq;
using UnityEngine;

namespace Script
{
	public class SetColor : MonoBehaviour
	{

		public Texture2D Blank;
        public Camera cam;
        //public GameObject template;
		private Renderer _childRender;
		private Texture2D _newBlack;
		private float _timeLeft;
		private float _numObjects;
        private MaterialPropertyBlock prop;
        Color32[] pixels;

        // Use this for initialization
        private void Start()
		{
            prop = new MaterialPropertyBlock();
			_timeLeft = 0.0f;
			_numObjects = 0.0f;
			print("percentage done: " + _timeLeft);
			RecursiveCount(transform);
			_timeLeft += 1.0f / _numObjects;
            _newBlack = new Texture2D(Blank.width, Blank.height);
            pixels = new Color32[Blank.width * Blank.height];
            Color32 b = new Color32(0, 0, 0, 255);
            for(int i = 0; i < pixels.Length;i++){
                pixels[i] = b;
            }
            _newBlack.SetPixels32(pixels);
            _newBlack.Apply();
            StartCoroutine(RecursiveColor(transform));
           

        }

        private void RecursiveCount(Transform t)
		{
			if (t.childCount == 0)
			{
				_numObjects += 1.0f;
				
			}
			else
			{
				for (var i = 0; i < t.childCount; i++)
				{
					RecursiveCount(t.GetChild(i));
				}
			}
            
		}

		private IEnumerator RecursiveColor(Transform t)
		{
			//base case
            if(t.gameObject.name == "Stairs")
            {
                cam.GetComponent<Camera>().enabled = true;
            }
			if (t.childCount == 0)
			{
                
                _childRender = t.GetComponent<Renderer>();
               _newBlack = new Texture2D(Blank.width, Blank.height);
                _newBlack.SetPixels32(pixels);
                _newBlack.Apply();
                _childRender.GetPropertyBlock(prop);
                prop.SetTexture("_MainTex", _newBlack);
                _childRender.SetPropertyBlock(prop);
                print("percentage done: " + _timeLeft);
                //foreach (var VARIABLE in _childRender.materials)
                //{
                //_newBlack = new Texture2D(Blank.width, Blank.height);

                /*for (var i = 0; i < Blank.width; i++)
                {
                    for (var j = 0; j < Blank.height; j++)
                    {
                        _newBlack.SetPixel(i, j, Color.black);
                    }
                }*/

                /*_newBlack.Apply();
                VARIABLE.mainTexture = _newBlack;*/
                /*for(int i = 0; i < _childRender.materials.Length; i++)
                {
                    someobject.renderer.material = new Material(Shader.Find("Diffuse"));
                    someobject.renderer.material.color = Color.blue;
                    someobject.renderer.material.mainTexture = sometexture2D
                    Material newMaterial = Instantiate(template.GetComponent<Renderer>().material);
                    _childRender.materials[i] = new Material(Shader.Find("Unlit/Distance"));
                    _childRender.materials[i].mainTexture = _newBlack;
                    
				}*/

                _timeLeft += 1.0f / _numObjects;
				
				yield return 0;
			}
			else
			{
				for (var i = 0; i < t.childCount; i++)
				{
					//yield return StartCoroutine(RecursiveColor(t.GetChild(i)));
					yield return RecursiveColor(t.GetChild(i));
				}
			}
		}
	}
}
