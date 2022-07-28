using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Signature
{
    public interface ISignatureService
    {
        string GenerateSignature(string dataToSign);
        bool VerifySignature(string ceftificatePath, string signature, string dataToVerify);
    }
}
