using Grapevine.Server;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameState4Net
{
	public class GameStateServer<TGameStateFrame> where TGameStateFrame : GameStateFrame
	{
		private readonly HttpListener listener;

		private List<Action<TGameStateFrame>> callbacks = new List<Action<TGameStateFrame>>();


		public GameStateServer(string host = "localhost", string port = "1234")
		{
			listener = new HttpListener();
			listener.Prefixes.Add("http://" + host + ":" + port + "/");
		}

		public void Start()
		{
			if(listener.IsListening)
			{
				throw new InvalidOperationException("Server is already started");
			}

			// Start the listener 
			listener.Start();

			// Get the synchronization context of the main thread and run the processing loop
			var task = Task.Run(() => ProcessingLoop());
		}

		public void Stop()
		{
			if (!listener.IsListening)
			{
				throw new InvalidOperationException("Server was not yet started");
			}
			
			// Stop the listener
			listener.Stop();
		}

		public bool IsListening
		{
			get { return listener.IsListening; }
		}

		private void ProcessingLoop()
		{
			while (listener.IsListening)
			{
				HttpListenerContext context = listener.GetContext();
				string content = context.Request.GetContent();
				
				// We can catch this case for the server, it doesn't like empty gamestate content
				if (string.IsNullOrEmpty(content))
				{
					BadRequest(context);					
				}
				else
				{
					try
					{
						// Let the server process the current gamestate content
						AddGameState(content);

						context.Response.StatusCode = 200;
						context.Response.StatusDescription = "Ok";
						context.Response.ContentType = "text/plain";
						context.Response.SendContent("Success");
					}
					catch (ArgumentException)
					{
						// If the server can't handle the content...well it MUST be a client error :x
						BadRequest(context);
					}
				}
			}
		}


		private void AddGameState(string json)
		{
			// Try to parse the gamestate frame
			TGameStateFrame frame = null;
			try
			{				
				frame = (TGameStateFrame)Activator.CreateInstance(typeof(TGameStateFrame), new[] { json });
			}
			catch (Exception e)
			{
				throw new ArgumentException("Failed parsing a frame from the provided json input", e);
			}

			// Let the callbacks handle the frame
			lock (callbacks)
			{
				foreach (var callback in callbacks)
				{
					callback(frame);
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <remarks>Callbacks will most certainly be called from a thread different than the main thread</remarks>
		/// <param name="callback"></param>
		public void RegisterGameStateCallback(Action<TGameStateFrame> callback)
		{
			lock (callbacks)
			{
				callbacks.Add(callback);
			}
		}

		private void BadRequest(HttpListenerContext context, string payload = "<h1>Bad Request</h1>", string contentType = "text/html")
		{
			context.Response.StatusCode = 400;
			context.Response.StatusDescription = "Bad Request";
			context.Response.ContentType = contentType;
			context.Response.SendContent(payload);
		}
	}
}

