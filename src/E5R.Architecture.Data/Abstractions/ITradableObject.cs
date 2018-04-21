namespace E5R.Architecture.Data.Abstractions
{
    /// <summary>
    /// Objeto negociável.
    /// </summary>
    /// <remarks>
    /// Um objeto negociável é um objeto que interage dentro de uma
    /// transação negocial. Portanto, depende de um objeto de sessão,
    /// assim, deve ser capaz de ser configurado para isso.
    /// </remarks>
    public interface ITradableObject
    {
        void Configure(UnderlyingSession session);
    }
}
