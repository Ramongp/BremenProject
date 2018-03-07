using UnityEngine;
using System.Collections;

public class Torque : MonoBehaviour
{
	public float amount = 600f;
	public float Margin =0.5F;
	float X,Y,Z,Xmov,Ymov;
	void Start()
	{
		Margin =0.5F;
	}

	void Update ()
	{
		if (Input.GetMouseButton (0)) {
			X = this.GetComponent<Transform> ().rotation.x;
			Y = this.GetComponent<Transform> ().rotation.y;
			Z = this.GetComponent<Transform> ().rotation.z;

			Xmov = Input.GetAxis ("Mouse X") * amount * Time.deltaTime;
			Ymov = Input.GetAxis ("Mouse Y") * amount * Time.deltaTime;
			Debug.Log ("Xmov " + Xmov.ToString () + " Ymov " + Ymov.ToString ());
			Debug.Log ("X " + X.ToString () + " Y " + Y.ToString () + " Z " + Z.ToString ());
			TorqueCube (Xmov, Ymov);

		
				
		}
	}
		void TorqueCube(float h,float v){
		Debug.Log (Margin.ToString ());
			if ((h < Margin) && (h > -Margin)) {
				if ((0 < Y) && (Y < 0.5)) {
					this.GetComponent<Rigidbody> ().AddTorque (transform.right * v);
				} else {
					if ((0.5 < Y) && (Y < 0.8)) {
						this.GetComponent<Rigidbody> ().AddTorque (transform.forward * v);
					} else {
						if ((-0.5 < Y) && (Y < 0)) {
							this.GetComponent<Rigidbody> ().AddTorque (transform.right * v);
						} else {
							if ((-0.8 < Y) && (Y < -0.5)) {
								this.GetComponent<Rigidbody> ().AddTorque (-transform.forward * v);
							} else {
								if ((0.8 < Y) || (Y < -0.8)) {
									this.GetComponent<Rigidbody> ().AddTorque (-transform.right * v);
								}
							}
						}
					}
				}
			return;
			} 
			if ((v < Margin) && (v > -Margin)) {
				{
					if ((0 < X) && (X < 0.5)) {
						this.GetComponent<Rigidbody> ().AddTorque (-transform.up * h);
					} else {
						if ((0.5 < X) && (X < 0.8)) {
							this.GetComponent<Rigidbody> ().AddTorque (transform.forward * h);
						} else {
							if ((-0.5 < X) && (X < 0)) {
								this.GetComponent<Rigidbody> ().AddTorque (-transform.up * h);
							} else {
								if ((-0.8 < X) && (X < -0.5)) {
									this.GetComponent<Rigidbody> ().AddTorque (-transform.forward * h);
								} else {
									if ((0.8 < X) || (X < -0.8)) {
										this.GetComponent<Rigidbody> ().AddTorque (transform.up * h);
									}
								}
							}
						}
					}
				}
			return;
			}
			/*if ((h > Margin) || (h < -Margin) && (v > Margin) || (v < -Margin)) {
				Debug.Log ("Rotar en la Z");
				if ((0 < Z) && (Z < 0.5)) {
					this.GetComponent<Rigidbody> ().AddTorque (-transform.right * h);
				} else {
					if ((0.5 < Z) && (Z < 0.8)) {
						this.GetComponent<Rigidbody> ().AddTorque (transform.up * h);
					} else {
						if ((-0.5 < Z) && (Z < 0)) {
							this.GetComponent<Rigidbody> ().AddTorque (-transform.right * h);
						} else {
							if ((-0.8 < Z) && (Z < -0.5)) {
								this.GetComponent<Rigidbody> ().AddTorque (transform.up * h);
							} else {
								if ((0.8 < Z) || (Z < -0.8)) {
									this.GetComponent<Rigidbody> ().AddTorque (transform.right * h);
								}
							}
						}
					}
				}
			}*/
		}
		
}
