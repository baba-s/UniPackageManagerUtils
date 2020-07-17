using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;

namespace Kogane.Internal
{
	internal static class PackageAdder
	{
		private static List<string>     m_addedIdentifiers;
		private static Queue<string>    m_identifierQueue;
		private static Action<string[]> m_onComplete;
		private static AddRequest       m_addRequest;

		public static void Add( IEnumerable<string> identifiers )
		{
			Add( identifiers, null );
		}

		public static void Add
		(
			IEnumerable<string> identifiers,
			Action<string[]>    onComplete
		)
		{
			m_addedIdentifiers = new List<string>();
			m_identifierQueue  = new Queue<string>( identifiers );
			m_onComplete       = onComplete;

			EditorApplication.LockReloadAssemblies();

			var identifier = m_identifierQueue.Dequeue();
			m_addRequest = Client.Add( identifier );

			EditorApplication.update += OnUpdate;
		}

		private static void OnUpdate()
		{
			if ( !m_addRequest.IsCompleted ) return;

			if ( m_addRequest.Status == StatusCode.Success )
			{
				m_addedIdentifiers.Add( m_addRequest.Result.name );
			}

			if ( 0 < m_identifierQueue.Count )
			{
				var identifier = m_identifierQueue.Dequeue();
				m_addRequest = Client.Add( identifier );
			}
			else
			{
				m_identifierQueue = null;
				m_addRequest      = null;

				EditorApplication.update -= OnUpdate;
				EditorApplication.UnlockReloadAssemblies();

				m_onComplete?.Invoke( m_addedIdentifiers.ToArray() );
				m_onComplete = null;

				m_addedIdentifiers.Clear();
				m_addedIdentifiers = null;
			}
		}
	}
}