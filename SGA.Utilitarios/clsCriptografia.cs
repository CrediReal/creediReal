using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SGA.Utilitarios
{
    /// <summary>
    /// Clase que contiene los métodos de Encriptar y Desencriptar, bajo el algoritmo AES.
    /// </summary>
    public class clsCriptografia
    {
        private const string CRYPT_KEY = "xkKqH3aUDcp4UrSlTuwvFiBtJVA+bIc51NhR3tAkNnM=";

        /// <summary>
        /// Encripta una contraseña usando el algoritmo AES
        /// </summary>
        /// <param name="password">Contraseña a encriptar</param>
        /// <param name="Key">Arreglo de bytes que representa la llave usada por el algoritmo</param>
        /// <param name="IV">Arreglo de bytes que representa el vector de inicialización IV del algoritmo</param>
        /// <returns>Arreglo de bytes conteniendo la contraseña encriptada</returns>
        /// <exception cref="Credicorp.Sgnt.ApplicactionDataUtil.ApplicationDataUtilException">
        /// Esta excepción se ha de lanzar si alguno de los valores de los siguientes parámetros: <c>password</c>
        /// <c>Key</c> y <c>IV</c> es nulo o no contienen elementos alguno.
        /// </exception>
        /// <exception cref="System.PlatformNotSupportedException">Excepción establecida en el algoritmo de encriptación.</exception>
        /// <exception cref="System.IO.IOException">Excepción establecida en el algoritmo de encriptación.</exception>
        private static byte[] Encriptar(string password, byte[] Key, byte[] IV)
        {
            if (password == null || password.Length <= 0)
            {
                throw new ApplicationDataUtilException(
                         AplicationDataUtilExceptionIds.ValorDePasswordNulo
                         , "La contraseña a encriptar es nula o es unan cadena vacía."
                         , new object[] { null });
            }
            if (Key == null || Key.Length <= 0)
            {
                throw new ApplicationDataUtilException(
                        AplicationDataUtilExceptionIds.ValorDeLlaveNulo
                        , "La llave es nula o el tamaño del arreglo es menor igual a cero."
                        , new object[] { null });
            }
            if (IV == null || IV.Length <= 0)
            {
                throw new ApplicationDataUtilException(
                        AplicationDataUtilExceptionIds.ValorDeLlaveNulo
                        , "El vector de inicialización es nulo o el tamaño del arreglo es menor igual a cero."
                        , new object[] { null });
            }

            MemoryStream msEncrypt = null;
            CryptoStream csEncrypt = null;
            StreamWriter swEncrypt = null;

            AesCryptoServiceProvider aesManagedAlg = null;

            try
            {
                aesManagedAlg = new AesCryptoServiceProvider();
                aesManagedAlg.Key = Key;
                aesManagedAlg.IV = IV;

                ICryptoTransform encryptor = aesManagedAlg.CreateEncryptor(aesManagedAlg.Key, aesManagedAlg.IV);

                msEncrypt = new MemoryStream();
                csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
                swEncrypt = new StreamWriter(csEncrypt);

                swEncrypt.Write(password);
            }
            finally
            {
                if (swEncrypt != null)
                    swEncrypt.Close();
                if (csEncrypt != null)
                    csEncrypt.Close();
                if (msEncrypt != null)
                    msEncrypt.Close();

                if (aesManagedAlg != null)
                    aesManagedAlg.Clear();
            }

            return msEncrypt.ToArray();

        }

        /// <summary>
        /// Desencripta una contraseña usando el algoritmo AES
        /// </summary>
        /// <param name="password">Contraseña a desencriptar</param>
        /// <param name="Key">Arreglo de bytes que representa la llave usada por el algoritmo</param>
        /// <param name="IV">Arreglo de bytes que representa el vector de inicialización IV del algoritmo</param>
        /// <returns>Cadena conteniendo la contraseña desencriptada</returns>
        /// <exception cref="Credicorp.Sgnt.ApplicactionDataUtil.ApplicationDataUtilException">
        /// Esta excepción se ha de lanzar si alguno de los valores de los siguientes parámetros: <c>password</c>
        /// <c>Key</c> y <c>IV</c> es nulo o no contienen elementos alguno.
        /// </exception>
        /// <exception cref="System.PlatformNotSupportedException">Excepción establecida en el algoritmo de encriptación.</exception>
        /// <exception cref="System.IO.IOException">Excepción establecida en el algoritmo de encriptación.</exception>
        private static string Desencriptar(string password, byte[] Key, byte[] IV)
        {
            byte[] passwordCifrado;
            passwordCifrado = CadenaBase64ABytes(password);

            if (password == null || password.Length <= 0)
            {
                throw new ApplicationDataUtilException(
                         AplicationDataUtilExceptionIds.ValorDePasswordNulo
                         , "La contraseña a desencriptar es nula o es una cadena vacía."
                         , new object[] { null });
            }
            if (Key == null || Key.Length <= 0)
            {
                throw new ApplicationDataUtilException(
                        AplicationDataUtilExceptionIds.ValorDeLlaveNulo
                        , "La llave es nula o el tamaño del arreglo es menor igual a cero."
                        , new object[] { null });
            }
            if (IV == null || IV.Length <= 0)
            {
                throw new ApplicationDataUtilException(
                        AplicationDataUtilExceptionIds.ValorDeLlaveNulo
                        , "El vector de inicialización es nulo o el tamaño del arreglo es menor igual a cero."
                        , new object[] { null });
            }

            MemoryStream msDecrypt = null;
            CryptoStream csDecrypt = null;
            StreamReader srDecrypt = null;

            AesCryptoServiceProvider aesManagedAlg = null;

            string plaintext = null;

            try
            {
                aesManagedAlg = new AesCryptoServiceProvider();
                aesManagedAlg.Key = Key;
                aesManagedAlg.IV = IV;

                ICryptoTransform decryptor = aesManagedAlg.CreateDecryptor(aesManagedAlg.Key, aesManagedAlg.IV);

                msDecrypt = new MemoryStream(passwordCifrado);
                csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
                srDecrypt = new StreamReader(csDecrypt);

                plaintext = srDecrypt.ReadToEnd();
            }
            finally
            {
                if (srDecrypt != null)
                    srDecrypt.Close();
                if (csDecrypt != null)
                    csDecrypt.Close();
                if (msDecrypt != null)
                    msDecrypt.Close();

                if (aesManagedAlg != null)
                    aesManagedAlg.Clear();
            }

            return plaintext;
        }

        /// <summary>
        /// Convierte una cadena Base64 a un arreglo de bytes
        /// </summary>
        /// <param name="valor">Valor a convertir</param>
        /// <returns>Arreglo de bytes</returns>
        /// <exception cref="System.FormatException">Si el valor a convertir tiene un formato Base64 incorrecto</exception>
        private static byte[] CadenaBase64ABytes(string valor)
        {
            return (string.IsNullOrEmpty(valor) == false ? System.Convert.FromBase64String(valor) : null);
        }

        /// <summary>
        /// Convierte un arreglo de bytes a una cadena Base64
        /// </summary>
        /// <param name="valor">Arreglo a convertir</param>
        /// <returns>Cadena convertida en Base64</returns>
        private static string BytesACadenaBase64(byte[] valor)
        {
            return ((valor != null && valor.Length >= 0) ? System.Convert.ToBase64String(valor) : string.Empty);
        }

        /// <summary>
        /// Encripta el password del usuario
        /// </summary>
        /// <param name="strPassword">password en formato cadena</param>
        /// <returns>cadena encriptada</returns>
        public static string EncriptarPassword(string strPassword)
        {
            try
            {
                byte[] valorVector = new byte[16];

                Random random = new Random(20110103);
                random.NextBytes(valorVector);

                string ivstring = BytesACadenaBase64(valorVector);
                byte[] IV = CadenaBase64ABytes(ivstring);

                byte[] Key = CadenaBase64ABytes(CRYPT_KEY);

                return BytesACadenaBase64(Encriptar(strPassword, Key, IV));
            }
            catch (Exception ex)
            {
                clsManejoErrores.Procesar(ex);
                return string.Empty;
            }

        }

        /// <summary>
        /// desemcripta el pasword para la validacion
        /// </summary>
        /// <param name="strPassword">pasw0rd encriptado</param>
        /// <returns>password desencriptado</returns>
        public static string DesencriptarPassword(string strPassword)
        {
            try
            {
                byte[] valorVector = new byte[16];

                Random random = new Random(20110103);
                random.NextBytes(valorVector);
                string ivstring = BytesACadenaBase64(valorVector);

                byte[] Key = CadenaBase64ABytes(CRYPT_KEY);
                byte[] IV = CadenaBase64ABytes(ivstring);

                return Desencriptar(strPassword, Key, IV);
            }
            catch (Exception ex)
            {
                clsManejoErrores.Procesar(ex);
                return string.Empty;
            }

        }
    }
}
