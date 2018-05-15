// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

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
