using Kogane.Internal;
using System;
using System.Collections.Generic;

namespace Kogane
{
	/// <summary>
	/// Package Manager を操作する機能を管理するクラス
	/// </summary>
	public static class PackageManagerUtils
	{
		//================================================================================
		// 関数(static)
		//================================================================================
		/// <summary>
		/// 複数のパッケージを追加します
		/// </summary>
		public static void Add( IEnumerable<string> identifiers )
		{
			PackageAdder.Add( identifiers, null );
		}
		
		/// <summary>
		/// 複数のパッケージを追加します
		/// </summary>
		public static void Add
		(
			IEnumerable<string> identifiers,
			Action<string[]>    onComplete
		)
		{
			PackageAdder.Add( identifiers, onComplete );
		}
		
		/// <summary>
		/// 複数のパッケージを削除します
		/// </summary>
		public static void Remove( IEnumerable<string> packageNames )
		{
			PackageRemover.Remove( packageNames, null );
		}
		
		/// <summary>
		/// 複数のパッケージを削除します
		/// </summary>
		public static void Remove
		(
			IEnumerable<string> packageNames,
			Action<string[]>    onComplete
		)
		{
			PackageRemover.Remove( packageNames, onComplete );
		}
	}
}