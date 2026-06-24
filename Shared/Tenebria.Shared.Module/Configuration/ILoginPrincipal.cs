namespace Tenebria.Shared.Module.Configuration;

/// <summary>
/// ログインユーザー情報を表す静的クラス
/// </summary>
public interface ILoginPrincipal
{
    /// <summary>
    /// ログインユーザーのIdを取得します。
    /// </summary>
    /// <value>ログインユーザーのId</value>
    Guid Id { get; }

    /// <summary>
    /// Tenant identity code
    /// </summary>
    string TenantCode { get; }

    /// <summary>
    /// As keycloak realm
    /// </summary>
    string RealmName { get; }

    /// <summary>
    /// ログインユーザーのユーザーコードを取得します。
    /// </summary>
    /// <value>ログインユーザーのユーザーコード</value>
    string LoginId { get; }

    /// <summary>
    /// ログインユーザーのユーザー名を取得します。
    /// </summary>
    /// <value>ログインユーザーのユーザー名</value>
    string UserNameDisplay { get; }

    /// <summary>
    /// ログインユーザーのユーザー名かなを取得します。
    /// </summary>
    /// <value>ログインユーザー의ユーザー名かな</value>
    string UserNameKana { get; }

    /// <summary>
    /// ログインユーザーの初期実行画面IDを取得します。
    /// </summary>
    /// <value>ログインユーザーの初期実行画面ID</value>
    string DefaultExecFormId { get; }

    /// <summary>
    /// ログインユーザーの担当者コードを取得します。
    /// </summary>
    /// <value>ログインユーザーの担当者コード</value>
    string CtrlerCd { get; }
}
