using System;
using System.Collections.Generic;
using System.Text;

namespace ItSoftware.Syndication.Atom
{
    public interface IAtomValidate
    {
        void Validate(AtomVersion version, bool validateContent);
    }
}
