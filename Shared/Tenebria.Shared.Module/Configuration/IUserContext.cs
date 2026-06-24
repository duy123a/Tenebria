namespace Tenebria.Shared.Module.Configuration;

public interface IUserContext : IApplicationContext
{
    /// <summary>
    /// 選択されたのグループコードを取得します。
    /// </summary>
    /// <value>選択されたのグループコード</value>
    string GroupCd { get; }

    /// <summary>
    /// 画面IDを取得します。
    /// </summary>
    /// <value>画面ID</value>
    string FormId { get; }

    /// <summary>
    /// 画面権限を取得します。
    /// </summary>
    /// <value>画面権限</value>
    string FormAuthoritySec { get; }

    /// <summary>
    /// クライアントIPアドレスを取得します。
    /// </summary>
    /// <value>クライアントIPアドレス</value>
    string ClientIpAddress { get; }

    string? RequestPath { get; }

    /// <summary>
    /// ログインユーザー情報を取得します。
    /// </summary>
    /// <value>ログインユーザー情報</value>
    ILoginPrincipal LoginPrincipal { get; }

    /// <summary>
    /// Login identity
    /// </summary>
    string LoginId { get; }

    /// <summary>
    /// Define whether the user is an administrator.
    /// </summary>
    bool IsAdmin();
}
