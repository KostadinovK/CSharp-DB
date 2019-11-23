using System;
using System.Collections.Generic;
using System.Text;

namespace CompositePattern.Contracts
{
    public interface IGiftOperations
    {
        void Add(GiftBase gift);
        void Remove(GiftBase gift);
    }
}
