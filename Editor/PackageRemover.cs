using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;

namespace Kogane.Internal
{
	internal static class PackageRemover
	{
		private static List<string>     m_removedPackageNames;
		private static Queue<string>    m_packageNameQueue;
		private static Action<string[]> m_onComplete;
		private static RemoveRequest    m_removeRequest;

		public static void Remove( IEnumerable<string> packageNames )
		{
			Remove( packageNames, null );
		}

		public static void Remove
		(
			IEnumerable<string> packageNames,
			Action<string[]>    onComplete
		)
		{
			m_removedPackageNames = new List<string>();
			m_packageNameQueue    = new Queue<string>( packageNames );
			m_onComplete          = onComplete;

			EditorApplication.LockReloadAssemblies();

			var packageName = m_packageNameQueue.Dequeue();
			m_removeRequest = Client.Remove( packageName );

			EditorApplication.update += OnUpdate;
		}

		private static void OnUpdate()
		{
			if ( !m_removeRequest.IsCompleted ) return;

			if ( m_removeRequest.Status == StatusCode.Success )
			{
				m_removedPackageNames.Add( m_removeRequest.PackageIdOrName );
			}

			if ( 0 < m_packageNameQueue.Count )
			{
				var packageName = m_packageNameQueue.Dequeue();
				m_removeRequest = Client.Remove( packageName );
			}
			else
			{
				m_packageNameQueue = null;
				m_removeRequest    = null;

				EditorApplication.update -= OnUpdate;
				EditorApplication.UnlockReloadAssemblies();

				m_onComplete?.Invoke( m_removedPackageNames.ToArray() );
				m_onComplete = null;

				m_removedPackageNames.Clear();
				m_removedPackageNames = null;
			}
		}
	}
}