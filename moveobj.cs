using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class point {
	public float x, y;
}
	
public class TransferData {
	public float x;
	public float y;
	public float z;
}


public class moveobj : MonoBehaviour
{
	static UdpClient udp;
	Thread thread;

	

	static readonly object lockObject = new object();
	string returnData = "";

	void Start ()
	{
		
		thread = new Thread(new ThreadStart(ThreadMethod));
		thread.Start();
	}

	void Update()
	{
		if (returnData!="") {
			var d = JsonUtility.FromJson<TransferData>(returnData);
			//Debug.Log("Received: " + d.left);
			//Debug.Log(returnData);
			Debug.Log (d.x);
			Move (d.x, d.z);
		}
			
		//Move ();	

		
	}
	public void Move (float x,float z) {
		Vector3 moveVector = new Vector3(x, 0, z);
		transform.Translate(moveVector);
	}

	private void ThreadMethod()
	{
		udp = new UdpClient(50000);
		while (true)
		{
			IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);

			byte[] receiveBytes = udp.Receive(ref RemoteIpEndPoint);

			/*lock object to make sure there data is 
        *not being accessed from multiple threads at thesame time*/
			lock (lockObject)
			{
				returnData = Encoding.ASCII.GetString(receiveBytes);

				
			}
		}
	}
}

