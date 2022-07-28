using Services.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Services.Signature
{
    public class SignatureService : ISignatureService 
    {
        /// <summary>
        /// Method used to generate signatures based on input data and selected certificates.
        /// </summary>
        /// <param name="dataToSign">Data for signature generation.</param>
        /// <param name="certificatePath">Physical path to certificate.</param>
        /// <param name="certificatePass">Certificate password.</param>
        /// <returns>Return generated signature.</returns>
       
        private readonly ISettingService _settingService;

        public  SignatureService(ISettingService settingService)
        {
            _settingService = settingService;
        }

        public /*static*/ string GenerateSignature(string dataToSign)
        {
            var certificatePath = _settingService.PrivateKeyPath;
            var certificatePass = _settingService.PrivateKeyPass;

            var certificate = new X509Certificate2(certificatePath, certificatePass, X509KeyStorageFlags.Exportable);
            // Create byte arrays to hold original, encrypted, and decrypted data.
            var originalData = Encoding.UTF8.GetBytes(dataToSign);
            // Create a new instance of the RSACryptoServiceProvider class

            using (var rsa = certificate.GetRSAPrivateKey())
            {
                //dataToSign = "";
                var signeddata = rsa.SignData(originalData, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
                return Convert.ToBase64String(signeddata);
            }
        }

        /// <summary>
        /// <summary>
        /// Method that verify signature for requests received from Aircash service.
        /// </summary>
        /// <param name="ceftificatePath">Physical path to public certificate.</param>
        /// <param name="signature">Signature property received form Aircash in request.</param>
        /// <param name="dataToSign">All parameters expect signature joined in one string as Aircash documentation state.</param>
        /// <returns>Returns true in case signature verification was successful or false otherwise.</returns>
        public static bool VerifySignature(string ceftificatePath, string signature, string dataToVerify)
        {
            // Load the certificate we’ll use to verify the signature from a file.
            var certificate = new X509Certificate2(ceftificatePath);
            // Create byte arrays to hold original, encrypted, and decrypted data.
            var dataToVerifyBytes = Encoding.UTF8.GetBytes(dataToVerify);
            var signatureBytes = Convert.FromBase64String(signature);
            // Create a new instance of the RSACryptoServiceProvider class.
            using (var rsaAlg = (RSACryptoServiceProvider)certificate.PublicKey.Key)
            using (var sha256 = new SHA256Managed())
            {
                return rsaAlg.VerifyData(dataToVerifyBytes, sha256, signatureBytes);
            }
        }
    }
}
