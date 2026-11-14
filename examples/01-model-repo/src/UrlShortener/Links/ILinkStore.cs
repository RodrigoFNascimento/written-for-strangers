namespace UrlShortener.Links;

/// <summary>Stores links by short code.</summary>
public interface ILinkStore
{
    #region try-add
    /// <summary>
    /// Adds the link if its code is free. Returns <c>false</c> when the code is
    /// already taken, so the caller can retry with another code instead of
    /// overwriting someone else's link.
    /// </summary>
    bool TryAdd(Link link);
    #endregion

    /// <summary>Returns the link with this code, or <c>null</c> if there is none.</summary>
    Link? Find(string code);
}
