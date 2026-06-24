namespace Tenebria.Shared.Module.Configuration;

public interface IApplicationContext
{
    /// <summary>
    /// ログイン時に選択されたの言語コードを取得します。
    /// </summary>
    /// <value>ログイン時に選択されたの言語コード</value>
    string LangCd { get; }

    /// <summary>
    /// プログラムIDを取得します。
    /// </summary>
    /// <value>プログラムID</value>
    string PgId { get; }

    /// <summary>
    /// Tenant identity code
    /// </summary>
    string TenantCode { get; }

    /// <summary>
    /// Keycloakのテナントコードを取得します。
    /// </summary>
    string RealmName { get; }
}
