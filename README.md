# UniPackageManagerUtils

Package Manager を操作する機能を管理するクラス

## 使用例

### 複数のパッケージを追加

```cs
var list = new[]
{
    "https://github.com/baba-s/UniCameraShaker.git",
    "https://github.com/baba-s/UniEnumerableExtensionMethods.git",
    "https://github.com/baba-s/UniRendererMaterialDestroyer.git",
};

PackageManagerUtils.Add
(
    list, result =>
    {
        Debug.Log( "完了" );
        foreach ( var s in result )
        {
            Debug.Log( s ); // 追加したパッケージの名前
        }
    }
);
```

### 複数のパッケージを削除

```cs
var list = new[]
{
    "com.baba-s.uni-camera-shaker",
    "com.baba-s.uni-enumerable-extension-methods",
    "com.baba-s.uni-renderer-material-destroyer",
};

PackageManagerUtils.Remove
(
    list, result =>
    {
        Debug.Log( "完了" );
        foreach ( var s in result )
        {
            Debug.Log( s ); // 削除したパッケージの名前
        }
    }
);
```
